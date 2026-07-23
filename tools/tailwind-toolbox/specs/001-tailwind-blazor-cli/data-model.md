# Data Model: Tailwind Blazor CLI Setup Tool

**Feature**: 001-tailwind-blazor-cli
**Date**: 2026-01-03
**Status**: Complete

## Overview

This document defines the core data structures and their relationships for the Tailwind Blazor CLI tool. Models represent Blazor projects, configuration files, validation results, and dependency information.

---

## Core Entities

### BlazorProject

Represents a detected Blazor project with metadata about project type, version, and file locations.

**Purpose**: Central model for all operations - setup, validation, updates.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| ProjectFilePath | string | Absolute path to .csproj file | Must exist, must be .csproj extension |
| ProjectDirectory | string | Directory containing .csproj | Derived from ProjectFilePath |
| ProjectName | string | Name of the project | Extracted from .csproj filename |
| DotNetVersion | string | Target framework (e.g., "net10.0") | Must be net6.0 or higher |
| ProjectType | BlazorProjectType | Server, WebAssembly, or Hybrid | Determined from SDK and package references |
| HasTailwindConfig | bool | Whether tailwind.config.js exists | File existence check |
| HasPackageJson | bool | Whether package.json exists | File existence check |
| HasBuildTargets | bool | Whether .targets file exists | File existence check |
| WwwRootPath | string? | Path to wwwroot folder | null if not found |

**Relationships**:
- Has one TailwindConfig (if configured)
- Has one PackageConfig (package.json representation)
- Has zero or more ValidationResult instances

**State Transitions**:
```
Unconfigured → Partially Configured → Fully Configured
     ↓                  ↓                      ↓
  (setup)          (check warns)          (check passes)
```

**Invariants**:
- ProjectFilePath must always be absolute path
- DotNetVersion must match pattern `net\d+\.\d+`
- If HasTailwindConfig is true, TailwindConfig must not be null

---

### BlazorProjectType

Enumeration of supported Blazor project types.

**Values**:
- `Server`: Blazor Server app (renders on server, uses SignalR)
- `WebAssembly`: Blazor WebAssembly app (runs in browser via WASM)
- `Hybrid`: Blazor Hybrid app (.NET MAUI or WPF with Blazor components)
- `Unknown`: Could not determine type (error condition)

**Detection Logic**:
```csharp
// Server: Has Microsoft.AspNetCore.Components + Sdk.Web
// WebAssembly: Has Microsoft.AspNetCore.Components.WebAssembly
// Hybrid: Has Microsoft.AspNetCore.Components.WebView
```

**Impact on Setup**:
- Server/WebAssembly: CSS output to `wwwroot/css/`
- Hybrid: CSS output to `Resources/Styles/` or platform-specific location

---

### TailwindConfig

Represents the tailwind.config.js configuration structure.

**Purpose**: Validate and generate Tailwind configuration files.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| FilePath | string | Path to tailwind.config.js | Must be in project root |
| ContentPaths | List<string> | Glob patterns for content files | Must include .razor, .html, .cshtml |
| Theme | Dictionary<string, object>? | Theme customizations | Optional, valid JavaScript object |
| Plugins | List<string> | Tailwind plugins | Optional, valid npm package names |
| DarkMode | string? | Dark mode strategy ("media" or "class") | Optional, defaults to "media" |

**Default Content Paths**:
```javascript
[
  "./**/*.razor",
  "./**/*.html",
  "./**/*.cshtml"
]
```

**Validation Rules**:
1. ContentPaths must not be empty
2. ContentPaths must include at least one pattern matching Blazor files
3. Plugins must be valid npm package names (if specified)
4. Theme must be valid JSON (if specified)

**Relationships**:
- Belongs to one BlazorProject
- Referenced by ValidationRules

---

### PackageConfig

Represents package.json for npm dependency management.

**Purpose**: Manage Tailwind CSS and related npm packages.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| FilePath | string | Path to package.json | Must be in project root |
| Name | string | Package name | Defaults to project name (lowercase, hyphens) |
| Version | string | Package version | Semantic version (e.g., "1.0.0") |
| Dependencies | Dictionary<string, string> | Production dependencies | Must include "tailwindcss" |
| DevDependencies | Dictionary<string, string> | Development dependencies | Optional (autoprefixer, etc.) |
| Scripts | Dictionary<string, string> | npm scripts | Must include Tailwind build script |

**Required Dependencies**:
```json
{
  "dependencies": {
    "tailwindcss": "^4.0.0"
  },
  "devDependencies": {
    "autoprefixer": "^10.4.16"
  }
}
```

**Required Scripts**:
```json
{
  "scripts": {
    "build:css": "tailwindcss -i ./Styles/app.css -o ./wwwroot/css/app.css --minify"
  }
}
```

**Relationships**:
- Belongs to one BlazorProject
- Contains multiple DependencyVersion instances

---

### DependencyVersion

Represents an npm package with version information.

**Purpose**: Track and validate npm package versions.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| PackageName | string | npm package name | Must be valid npm package identifier |
| InstalledVersion | string? | Currently installed version | null if not installed |
| RequiredVersion | string | Minimum required version | Semantic version or range |
| LatestVersion | string? | Latest available version | null if not fetched |
| IsCompatible | bool | Whether installed version meets requirement | Computed property |

**Computed Properties**:
```csharp
public bool IsCompatible =>
    InstalledVersion != null &&
    SemVersion.Parse(InstalledVersion) >= SemVersion.Parse(RequiredVersion);

public bool HasUpdate =>
    LatestVersion != null &&
    InstalledVersion != null &&
    SemVersion.Parse(LatestVersion) > SemVersion.Parse(InstalledVersion);
```

**Relationships**:
- Belongs to PackageConfig
- Referenced by ValidationRules for version checks

---

### ValidationRule

Represents a single validation check with pass/fail result and remediation.

**Purpose**: Extensible validation framework for configuration checks.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| RuleId | string | Unique identifier (e.g., "NODE_INSTALLED") | Must be unique, uppercase with underscores |
| Name | string | Human-readable name | Short description (max 50 chars) |
| Description | string | Detailed explanation | Full description of what is checked |
| Category | ValidationCategory | Grouping (Environment, Files, Config, Dependencies) | Must be valid enum value |
| Severity | ValidationSeverity | Error, Warning, or Info | Determines failure impact |
| CheckFunction | Func<BlazorProject, ValidationResult> | Logic to perform check | Must not throw exceptions |
| Remediation | string | How to fix if failed | Actionable steps with file paths |

**Example Rules**:
```csharp
new ValidationRule(
    RuleId: "NODE_INSTALLED",
    Name: "Node.js Installation",
    Description: "Verifies Node.js is installed and accessible",
    Category: ValidationCategory.Environment,
    Severity: ValidationSeverity.Error,
    CheckFunction: project => CheckNodeInstalled(),
    Remediation: "Install Node.js from https://nodejs.org/ or use nvm"
);

new ValidationRule(
    RuleId: "TAILWIND_CONFIG_EXISTS",
    Name: "Tailwind Configuration File",
    Description: "Checks for tailwind.config.js in project root",
    Category: ValidationCategory.Files,
    Severity: ValidationSeverity.Error,
    CheckFunction: project => File.Exists(Path.Combine(project.ProjectDirectory, "tailwind.config.js")),
    Remediation: "Run 'tailwind-blazor setup' to create configuration"
);
```

**Relationships**:
- Many ValidationRules per BlazorProject
- Produces one ValidationResult per execution

---

### ValidationCategory

Enumeration for grouping validation rules.

**Values**:
- `Environment`: System requirements (Node.js, npm, dotnet)
- `Files`: File existence and structure checks
- `Configuration`: Configuration file content validation
- `Dependencies`: npm package version checks
- `Integration`: MSBuild target and .csproj integration

**Purpose**: Organize check command output by category for clarity.

---

### ValidationSeverity

Enumeration for validation failure impact.

**Values**:
- `Error`: Must be fixed for Tailwind to work (blocks setup)
- `Warning`: Should be fixed but not blocking (outdated versions)
- `Info`: Informational only (optimization suggestions)

**Impact on Exit Codes**:
- Any Error → exit code 1
- Only Warnings → exit code 0 (success with warnings)
- All pass → exit code 0

---

### ValidationResult

Represents the outcome of a single validation rule execution.

**Purpose**: Capture pass/fail status with context for user feedback.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| RuleId | string | Associated validation rule ID | Must match ValidationRule.RuleId |
| Passed | bool | Whether check succeeded | Required |
| Message | string? | Additional context | Optional, shown to user |
| FilePath | string? | Relevant file path (if applicable) | Optional, absolute path |
| ActualValue | string? | What was found | Optional, for version mismatches |
| ExpectedValue | string? | What was expected | Optional, for version mismatches |
| TimestampUtc | DateTime | When check was performed | UTC timestamp |

**Display Format**:
```
✓ NODE_INSTALLED: Node.js Installation
  Node.js v20.10.0 detected

✗ TAILWIND_CONFIG_EXISTS: Tailwind Configuration File
  File not found: /path/to/project/tailwind.config.js
  → Run 'tailwind-blazor setup' to create configuration
```

**Relationships**:
- Belongs to one ValidationRule
- Many ValidationResults collected by CheckCommand

---

### BuildTarget

Represents an MSBuild .targets file for Tailwind compilation.

**Purpose**: Generate and validate MSBuild integration.

**Properties**:
| Property | Type | Description | Validation Rules |
|----------|------|-------------|------------------|
| FilePath | string | Path to .targets file | Must end with .targets |
| TargetName | string | MSBuild target name | Defaults to "BuildTailwindCSS" |
| InputCssPath | string | Source CSS file path | Relative to project root |
| OutputCssPath | string | Compiled CSS destination | Typically wwwroot/css/app.css |
| RunBeforeTargets | string | MSBuild target to run before | Defaults to "BeforeBuild" |
| IsMinified | bool | Whether to minify output | true for Release, false for Debug |

**XML Template Structure**:
```xml
<Project>
  <Target Name="{{TargetName}}" BeforeTargets="{{RunBeforeTargets}}">
    <Exec
      Command="npx tailwindcss -i {{InputCssPath}} -o {{OutputCssPath}} {{MinifyFlag}}"
      WorkingDirectory="$(ProjectDir)" />
  </Target>
</Project>
```

**Validation**:
- XML must be well-formed
- Target name must not conflict with existing MSBuild targets
- Input/output paths must be valid relative paths
- Command must reference valid npx and tailwindcss

**Relationships**:
- Belongs to one BlazorProject
- Referenced in .csproj via `<Import Project="..." />`

---

## Relationships Diagram

```
BlazorProject (1) ──┬── (0..1) TailwindConfig
                    ├── (0..1) PackageConfig ──── (0..*) DependencyVersion
                    ├── (0..1) BuildTarget
                    └── (0..*) ValidationResult ── (1) ValidationRule
```

**Cardinality**:
- One BlazorProject may have zero or one TailwindConfig (none before setup)
- One BlazorProject may have zero or one PackageConfig (none before setup)
- One PackageConfig has zero or more DependencyVersions
- One BlazorProject may have zero or one BuildTarget
- One BlazorProject generates many ValidationResults (one per rule)
- Each ValidationResult references exactly one ValidationRule

---

## State Management

### Project Configuration States

```
State 1: Unconfigured
- HasTailwindConfig = false
- HasPackageJson = false
- HasBuildTargets = false
→ Actions: setup (creates all), check (fails with errors)

State 2: Partially Configured
- Some but not all configuration present
- Example: Has package.json but no tailwind.config.js
→ Actions: setup (merges/completes), check (warns about missing)

State 3: Fully Configured
- HasTailwindConfig = true
- HasPackageJson = true
- HasBuildTargets = true
- All validations pass
→ Actions: update (version bumps), create-target (overwrite)

State 4: Misconfigured
- Files present but invalid (malformed JSON, wrong versions)
→ Actions: setup (fix), check (lists specific issues)
```

---

## Validation Rule Registry

All validation rules are registered in `ValidationService` and executed by `CheckCommand`.

**Rule Categories and Counts**:
- Environment (3 rules): Node.js, npm, dotnet version
- Files (5 rules): tailwind.config.js, package.json, app.css, .targets, .gitignore
- Configuration (4 rules): Tailwind config valid, package.json valid, content paths correct, scripts present
- Dependencies (3 rules): tailwindcss version, autoprefixer version, no deprecated packages
- Integration (2 rules): MSBuild import exists, target XML valid

**Total: 17 validation rules**

---

## Data Access Patterns

### File System Operations

All file I/O goes through abstraction for testability:

```csharp
public interface IFileSystem
{
    bool FileExists(string path);
    string ReadAllText(string path);
    void WriteAllText(string path, string content);
    void DeleteFile(string path);
}

// Production: Uses System.IO
// Tests: Uses in-memory mock
```

### Process Execution

External command execution abstracted:

```csharp
public interface IProcessExecutor
{
    Task<ProcessResult> ExecuteAsync(string command, string args);
}

public record ProcessResult(
    int ExitCode,
    string StandardOutput,
    string StandardError
);
```

---

## Serialization

### TailwindConfig → tailwind.config.js

**Format**: JavaScript module (not JSON)

**Generation**:
```csharp
var configJs = $$"""
module.exports = {
  content: [{{string.Join(", ", config.ContentPaths.Select(p => $"'{p}'"))}}],
  theme: {{JsonSerializer.Serialize(config.Theme ?? new {})}},
  plugins: [{{string.Join(", ", config.Plugins.Select(p => $"require('{p}')")}}],
}
""";
```

### PackageConfig ↔ package.json

**Format**: JSON

**Serialization**:
```csharp
var json = JsonSerializer.Serialize(packageConfig, new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});
```

**Deserialization**:
```csharp
var packageConfig = JsonSerializer.Deserialize<PackageConfig>(json);
```

---

## Performance Considerations

### BlazorProject Detection

**Optimization**: Cache detected projects in memory for command duration
- Avoids re-parsing .csproj on each validation rule
- Invalidate cache if .csproj modified during execution

### Validation Rule Execution

**Optimization**: Execute rules in parallel using PLINQ
```csharp
var results = validationRules
    .AsParallel()
    .Select(rule => rule.CheckFunction(project))
    .ToList();
```

**Constraint**: Environment rules run first (serial) to fail fast if Node.js missing

### Version Checks

**Optimization**: Batch npm package version checks
```csharp
// One npm command instead of multiple:
npm list --json --depth=0
// Parse JSON to extract all package versions at once
```

---

## Error Handling

### Invalid Data

- **Malformed JSON**: Catch `JsonException`, show file path and line number
- **Invalid .csproj XML**: Catch `XmlException`, show parse error details
- **Missing required fields**: Throw `InvalidOperationException` with field name

### File System Errors

- **Permission denied**: Catch `UnauthorizedAccessException`, suggest sudo/admin
- **File not found**: Check existence before operations, provide remediation
- **Disk full**: Catch `IOException`, inform user of space issue

---

## Next Steps

With data model complete, proceed to:
1. Define command contracts (CLI signatures, options, exit codes)
2. Create quickstart.md for user-facing documentation
3. Update agent context with data model details
