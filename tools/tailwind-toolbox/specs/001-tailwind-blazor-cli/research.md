# Research: Tailwind Blazor CLI Setup Tool

**Feature**: 001-tailwind-blazor-cli
**Date**: 2026-01-03
**Status**: Complete

## Overview

This document captures technical research and decisions for implementing a CLI tool that automates Tailwind CSS setup in Blazor projects. Research focused on CLI framework selection, npm integration patterns, MSBuild target generation, and cross-platform file handling.

---

## Decision 1: CLI Framework

**Decision**: Use **Spectre.Console.Cli** for command-line interface

**Rationale**:
- Modern, actively maintained .NET CLI framework with excellent documentation
- Built-in dependency injection support (aligns with Constitution Principle V: Clear Dependencies)
- Rich terminal UI capabilities (progress bars, tables, prompts) for better UX
- Type-safe command definition with attributes
- Automatic help text generation
- Cross-platform (Windows, macOS, Linux) out of the box

**Alternatives Considered**:
- **System.CommandLine**: Microsoft's official library, but more verbose and less mature for complex CLI apps
- **McMaster.Extensions.CommandLineUtils**: Good option, but less active development than Spectre.Console
- **Custom argument parsing**: Would violate YAGNI principle and require extensive testing

**Implementation Impact**:
- Commands inherit from `Command<TSettings>` or `AsyncCommand<TSettings>`
- Settings classes use attributes for argument/option definition
- DI container configured in `Program.cs` via `ITypeRegistrar`

---

## Decision 2: Tailwind CSS Version Strategy

**Decision**: Target **Tailwind CSS v4.x** (latest stable at implementation time)

**Rationale**:
- v4 represents the current stable branch with modern CSS features
- Oxide engine provides better performance for Blazor's large DOM trees
- Simplified configuration compared to v3 (fewer required plugins)
- Official Tailwind CLI (@tailwindcss/cli) preferred over PostCSS setup for simplicity

**Alternatives Considered**:
- **Tailwind CSS v3**: Older, would miss performance improvements
- **PostCSS with Tailwind plugin**: More complex setup, violates simplicity principle
- **CDN-based Tailwind**: Not suitable for production Blazor apps (performance, offline support)

**Implementation Impact**:
- Package.json will specify `"tailwindcss": "^4.0.0"`
- Template files updated for v4 configuration syntax
- Documentation references v4 features and breaking changes from v3

---

## Decision 3: npm Execution Pattern

**Decision**: Execute npm commands via **System.Diagnostics.Process** with proper error handling

**Rationale**:
- Direct process execution provides full control over output, errors, and exit codes
- No third-party dependencies needed (aligns with Simplicity principle)
- Standard pattern for CLI tools interacting with external commands
- Enables progress reporting via stdout/stderr parsing

**Alternatives Considered**:
- **CliWrap library**: Adds unnecessary dependency for simple process execution
- **Embedded node.js runtime**: Complex, violates simplicity, large distribution size
- **Shell script delegation**: Platform-specific, harder to test and debug

**Implementation Impact**:
```csharp
// NpmService will use ProcessStartInfo with:
// - FileName = "npm" (cross-platform via PATH)
// - RedirectStandardOutput/Error = true
// - CreateNoWindow = true
// - Error handling for npm not found, network failures, etc.
```

**Error Scenarios Handled**:
- npm not installed → clear error with installation instructions
- Network failure during package download → retry logic with user notification
- Permission errors → actionable error message with remediation steps

---

## Decision 4: MSBuild Target File Generation

**Decision**: Generate **standalone .targets file** referenced from .csproj

**Rationale**:
- Separation of concerns: Tailwind build logic isolated from main project file
- Easier to update/remove Tailwind integration (just remove import)
- Version control friendly (can be .gitignored if regenerated)
- Standard MSBuild pattern for third-party build integrations

**Alternatives Considered**:
- **Direct .csproj modification**: Harder to remove, merges poorly in Git
- **Directory.Build.targets**: Affects all projects in directory tree (too broad)
- **NuGet package with targets**: Over-engineered for local CLI tool

**Implementation Impact**:
- Generate `TailwindToolbox/TailwindBuild.targets` file
- Add `<Import Project="TailwindBuild.targets" />` to .csproj
- Target runs before `BeforeBuild` to compile Tailwind CSS into wwwroot
- Template uses MSBuild `Exec` task to run `npx tailwindcss`

**Target File Structure**:
```xml
<Project>
  <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
    <Exec Command="npx tailwindcss -i ./Styles/app.css -o ./wwwroot/css/app.css --minify" />
  </Target>
</Project>
```

---

## Decision 5: Blazor Project Detection

**Decision**: Parse **.csproj XML** to detect Blazor SDK and project type

**Rationale**:
- Reliable: SDK reference in .csproj is the source of truth
- Distinguishes between Blazor Server, WebAssembly, and Hybrid
- Can extract .NET version for compatibility checks
- No false positives (unlike searching for .razor files which could exist in non-Blazor projects)

**Alternatives Considered**:
- **File system heuristics** (.razor files, wwwroot folder): Unreliable, false positives
- **MSBuild API**: Over-engineered, requires MSBuild runtime dependencies
- **Regex on .csproj text**: Fragile, breaks on formatting changes

**Implementation Impact**:
```csharp
// ProjectDetector will:
// 1. Find all .csproj files in current directory
// 2. Parse as XML (System.Xml.Linq)
// 3. Check for <Project Sdk="Microsoft.NET.Sdk.Web"> or Sdk.Razor
// 4. Verify <PackageReference Include="Microsoft.AspNetCore.Components*">
// 5. Extract TargetFramework for .NET version validation
```

**Detection Logic**:
- Blazor Server: `Microsoft.AspNetCore.Components` + Sdk.Web
- Blazor WebAssembly: `Microsoft.AspNetCore.Components.WebAssembly` + Sdk.BlazorWebAssembly
- .NET version: Parse `<TargetFramework>net10.0</TargetFramework>`

---

## Decision 6: Configuration File Templates

**Decision**: Embed templates as **EmbeddedResource** in assembly

**Rationale**:
- Single-file distribution (no separate template files to manage)
- Tamper-proof (users can't accidentally modify templates)
- Versioned with the tool (template updates via tool updates)
- Standard .NET pattern for embedded content

**Alternatives Considered**:
- **External template files**: Fragile, users could delete or modify
- **Hardcoded strings in C#**: Unmaintainable, hard to read/edit
- **Download from GitHub**: Network dependency, offline issues

**Implementation Impact**:
```xml
<!-- TailwindToolbox.csproj -->
<ItemGroup>
  <EmbeddedResource Include="Templates\*.js" />
  <EmbeddedResource Include="Templates\*.json" />
  <EmbeddedResource Include="Templates\*.css" />
  <EmbeddedResource Include="Templates\*.targets" />
</ItemGroup>
```

**Template Variables**:
- Use `{{VARIABLE}}` syntax for substitution (e.g., `{{PROJECT_NAME}}`, `{{TAILWIND_VERSION}}`)
- FileGenerator service performs string replacement before writing

---

## Decision 7: Validation Rule Architecture

**Decision**: Define validation rules as **declarative objects** with rule name, check logic, and remediation

**Rationale**:
- Extensible: Easy to add new rules without modifying command logic
- Testable: Each rule can be unit tested independently
- User-friendly: Remediation steps bundled with failures
- Aligns with Constitution SC-005: actionable error messages

**Alternatives Considered**:
- **Procedural validation in command**: Harder to test, poor separation of concerns
- **Fluent validation library**: Over-engineered for simple file/version checks
- **Script-based rules**: Platform-specific, harder to debug

**Implementation Impact**:
```csharp
public record ValidationRule(
    string Name,
    string Description,
    Func<BlazorProject, ValidationResult> Check,
    string RemediationWhenFailed
);

public record ValidationResult(
    bool Passed,
    string? Message = null,
    string? FilePath = null
);
```

**Example Rules**:
- "Node.js installed": Check `node --version` succeeds
- "package.json exists": Check file exists at project root
- "tailwind.config.js valid": Parse as JavaScript, validate structure
- "MSBuild target imported": Check .csproj contains Import element

---

## Decision 8: Cross-Platform File Path Handling

**Decision**: Use **System.IO.Path** and **System.IO.FileSystem** APIs exclusively

**Rationale**:
- .NET handles platform differences (/ vs \ separators)
- No manual path manipulation needed
- Cross-platform by default (Windows, macOS, Linux)
- Secure (no path traversal vulnerabilities)

**Alternatives Considered**:
- **Manual string concatenation**: Error-prone, platform-specific bugs
- **Third-party path libraries**: Unnecessary dependency

**Implementation Impact**:
```csharp
// Always use:
Path.Combine(projectRoot, "wwwroot", "css", "app.css")
// Never use:
projectRoot + "/wwwroot/css/app.css"  // ❌ Wrong!
```

---

## Decision 9: Testing Strategy

**Decision**: Three-tier testing with xunit v3

**Rationale**:
- Aligns with Constitution Principle I (TDD) and IV (Integration & Contract Testing)
- xunit v3 provides modern test framework with async support
- Clear test organization aids maintainability

**Test Tiers**:

### 1. Unit Tests
- Test individual services in isolation
- Mock file system (System.IO.Abstractions)
- Mock process execution (IProcessExecutor interface)
- Fast, no external dependencies

### 2. Integration Tests
- Test full command execution in isolated temp directories
- Real file I/O but controlled environment
- Verify file creation, modification, and cleanup
- Slower but validates end-to-end file operations

### 3. Contract Tests
- Validate assumptions about external tools (npm, node, MSBuild)
- Test actual npm commands in controlled environment
- Verify MSBuild target XML schema
- Catch breaking changes in external dependencies

**Test Data Management**:
- Fixture pattern for sample Blazor projects
- Temp directory per test (IDisposable cleanup)
- Snapshot testing for generated config files

---

## Decision 10: macOS Installation Script

**Decision**: Provide **bash script** that builds and installs to `/usr/local/bin`

**Rationale**:
- Standard macOS pattern for CLI tools
- `/usr/local/bin` is in default PATH
- Users familiar with this installation method
- Single command installation experience

**Alternatives Considered**:
- **Homebrew formula**: Over-engineered for project-specific tool
- **Global .NET tool**: Requires users to manage .NET tool PATH
- **Manual installation docs**: Error-prone, poor UX

**Script Implementation** (`scripts/install-tool.sh`):
```bash
#!/bin/bash
set -e

echo "Building TailwindToolbox..."
dotnet build -c Release TailwindToolbox/TailwindToolbox.csproj

echo "Installing to /usr/local/bin..."
sudo cp TailwindToolbox/bin/Release/net10.0/TailwindToolbox /usr/local/bin/tailwind-blazor

echo "✓ Installation complete! Run 'tailwind-blazor --help' to get started."
```

**Usage**:
```bash
./scripts/install-tool.sh
tailwind-blazor setup
```

---

## Best Practices Research

### Spectre.Console.Cli Best Practices

**Command Naming**:
- Use verb-based names: `setup`, `check`, `update`, `create-target`
- Avoid generic names: `run`, `do`, `execute`
- Single word preferred, hyphenated for clarity

**Settings Validation**:
- Use `ValidationAttribute` on settings properties
- Validate early in command execution
- Return non-zero exit codes on validation failure

**Progress Reporting**:
- Use `AnsiConsole.Status()` for long operations
- Show spinner during npm install
- Use `Progress` for deterministic operations

**Error Handling**:
- Catch exceptions at command level
- Use `AnsiConsole.MarkupLine("[red]Error:[/]")` for errors
- Return exit code 1 on failure, 0 on success

### npm Integration Best Practices

**Package Version Pinning**:
- Use caret ranges: `"tailwindcss": "^4.0.0"` (allow patches)
- Pin exact versions for build tools: `"autoprefixer": "10.4.16"`
- Document version compatibility in README

**Network Resilience**:
- Set npm timeout: `npm install --timeout=60000`
- Retry on network failure (max 3 attempts)
- Provide offline fallback message

**Security**:
- Validate package integrity via `package-lock.json`
- Warn on deprecated packages
- Document security update process

### MSBuild Target Best Practices

**Conditional Execution**:
- Only run target in Debug/Release configurations
- Skip if Tailwind config missing (graceful degradation)
- Use `Condition` attributes to prevent errors

**Incremental Build Support**:
- Use MSBuild's `Inputs` and `Outputs` for caching
- Only rebuild CSS if source files changed
- Improves build performance

**Error Propagation**:
- Set `IgnoreStandardErrorWarningFormat="false"`
- Propagate npm/npx exit codes to MSBuild
- Build fails if Tailwind compilation fails

---

## Risk Mitigation

### Risk 1: npm Version Incompatibility

**Mitigation**:
- Detect npm version via `npm --version`
- Require npm 8.x or higher
- Document version requirements in error messages

### Risk 2: Node.js Not Installed

**Mitigation**:
- Check for node.js before any npm operations
- Provide download link in error message
- Suggest using nvm (Node Version Manager)

### Risk 3: Partial Configuration State

**Mitigation**:
- Detect existing partial configurations
- Prompt user: "Existing config detected. Merge or overwrite?"
- Backup existing files before modification
- Rollback on failure

### Risk 4: .csproj Parsing Failures

**Mitigation**:
- Use robust XML parsing (System.Xml.Linq)
- Handle malformed XML gracefully
- Provide specific error if .csproj is invalid

### Risk 5: File System Permissions

**Mitigation**:
- Check write permissions before file operations
- Use `UnauthorizedAccessException` handling
- Provide remediation: "Run with elevated permissions or check folder permissions"

---

## Performance Considerations

### Setup Command Performance

**Target**: Complete in <2 minutes (per SC-001)

**Breakdown**:
- Project detection: <1 second
- npm package install: 30-90 seconds (network dependent)
- File generation: <5 seconds
- MSBuild target creation: <1 second

**Optimization**:
- Parallel npm operations where possible
- Cache package manager results
- Skip npm install if packages already present (idempotent)

### Check Command Performance

**Target**: Complete in <5 seconds

**Breakdown**:
- File existence checks: <100ms
- Version parsing: <500ms
- Configuration validation: <1 second
- MSBuild target validation: <500ms
- Display results: <100ms

**Optimization**:
- Run validation rules in parallel (PLINQ)
- Early exit on critical failures
- Cache version check results

---

## Documentation Requirements

### User Documentation

**README.md** must include:
- Installation instructions (macOS script, manual build)
- Quick start guide (single command to setup)
- Command reference (setup, check, update, create-target)
- Troubleshooting guide (common errors and solutions)
- Supported Blazor versions and platforms

**Help Text** (via Spectre.Console):
- Each command must have description and examples
- Options documented with default values
- Exit codes documented

### Developer Documentation

**CONTRIBUTING.md** must include:
- How to run tests (unit, integration, contract)
- How to add new commands
- How to update templates
- Release process (versioning, changelog)

---

## Technology Stack Summary

| Component | Technology | Version | Justification |
|-----------|-----------|---------|---------------|
| Language | C# | 12 (.NET 10) | Latest stable, nullable types, records |
| CLI Framework | Spectre.Console.Cli | Latest | Rich terminal UI, DI support, cross-platform |
| Testing | xunit | v3 | Modern, async support, community standard |
| Target CSS Framework | Tailwind CSS | v4.x | Latest stable with Oxide engine |
| Build Integration | MSBuild Targets | .NET 10 SDK | Native .NET integration |
| Process Execution | System.Diagnostics.Process | .NET 10 BCL | No dependencies needed |
| File I/O | System.IO | .NET 10 BCL | Cross-platform, secure |
| XML Parsing | System.Xml.Linq | .NET 10 BCL | Robust, LINQ-friendly |

---

## Next Steps

With research complete, proceed to Phase 1:
1. Generate `data-model.md` (BlazorProject, ValidationRule, etc.)
2. Define command contracts in `contracts/` (CLI signatures, exit codes)
3. Create `quickstart.md` (user-facing setup guide)
4. Update agent context with selected technologies

All research decisions are **final** and ready for implementation planning.
