# Contributing to Tailwind Blazor CLI

Thank you for your interest in contributing to Tailwind Blazor CLI! This document provides guidelines and instructions for setting up your development environment and submitting contributions.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Setup](#development-setup)
- [Project Structure](#project-structure)
- [Constitution Compliance](#constitution-compliance)
- [Testing Guide](#testing-guide)
- [Coding Standards](#coding-standards)
- [Pull Request Process](#pull-request-process)
- [Release Process](#release-process)

## Code of Conduct

This project follows a professional, respectful, and inclusive code of conduct. Be kind, constructive, and focused on technical merit.

## Getting Started

### Prerequisites

- **.NET 10.0 SDK** or higher
- **Node.js 16+** and **npm** (for testing npm integration)
- **Git** for version control
- **IDE:** Visual Studio, VS Code, or JetBrains Rider

### Fork and Clone

```bash
# Fork the repository on GitHub, then clone your fork
git clone https://github.com/YOUR-USERNAME/TailwindToolbox.git
cd TailwindToolbox

# Add upstream remote
git remote add upstream https://github.com/original-repo/TailwindToolbox.git
```

## Development Setup

### 1. Restore Dependencies

```bash
# Restore all NuGet packages
dotnet restore
```

### 2. Build the Project

```bash
# Build in Debug configuration
dotnet build TailwindToolbox/TailwindToolbox.csproj

# Build in Release configuration
dotnet build -c Release TailwindToolbox/TailwindToolbox.csproj
```

### 3. Run Tests

```bash
# Run all tests
dotnet test TailwindToolbox.Tests/TailwindToolbox.Tests.csproj

# Run with detailed output
dotnet test --verbosity detailed

# Run specific test categories
dotnet test --filter Category=Unit
dotnet test --filter Category=Integration
dotnet test --filter Category=Contract
```

### 4. Run the CLI Tool Locally

```bash
# Run without installing
dotnet run --project TailwindToolbox/TailwindToolbox.csproj -- setup --help

# Or build and run directly
dotnet build TailwindToolbox/TailwindToolbox.csproj
./TailwindToolbox/bin/Debug/net10.0/tailwind-blazor setup --dry-run
```

## Project Structure

```
TailwindToolbox/
├── TailwindToolbox/                  # Main CLI project
│   ├── Commands/                     # Spectre.Console.Cli commands
│   │   ├── SetupCommand.cs           # Setup command (US1)
│   │   ├── CheckCommand.cs           # Validation command (US2)
│   │   ├── UpdateCommand.cs          # Update command (US3)
│   │   └── CreateTargetCommand.cs    # Target generation command (US4)
│   ├── Services/                     # Business logic
│   │   ├── ProjectDetector.cs        # Blazor project detection
│   │   ├── FileGenerator.cs          # Template-based file generation
│   │   ├── NpmService.cs             # npm command execution
│   │   ├── ValidationService.cs      # Configuration validation
│   │   └── TargetFileGenerator.cs    # MSBuild target generation
│   ├── Models/                       # Data models
│   │   ├── BlazorProject.cs          # Project metadata
│   │   ├── ValidationResult.cs       # Validation outcomes
│   │   ├── TailwindConfig.cs         # Tailwind configuration
│   │   └── ...                       # Other models
│   ├── Templates/                    # Embedded resource templates
│   │   ├── tailwind.config.template.js
│   │   ├── package.template.json
│   │   ├── app.template.css
│   │   └── TailwindBuild.template.targets
│   ├── Program.cs                    # Application entry point
│   └── TailwindToolbox.csproj        # Project file
├── TailwindToolbox.Tests/            # Test project
│   ├── Unit/                         # Unit tests (isolated logic)
│   ├── Integration/                  # Integration tests (file I/O)
│   └── Contract/                     # Contract tests (npm, MSBuild)
├── scripts/                          # Installation and automation
│   └── install-tool.sh               # macOS/Linux installation
├── specs/                            # Feature specifications
│   └── 001-tailwind-blazor-cli/      # Current feature documentation
└── README.md                         # User-facing documentation
```

## Constitution Compliance

This project follows the **TailwindToolbox Constitution v1.0.0**, which defines non-negotiable principles:

### ✅ Principle I: Test-Driven Development (NON-NEGOTIABLE)

**All code must be test-driven.** This means:

1. **Write tests FIRST** before any implementation code
2. **Verify tests FAIL** (Red phase) before writing implementation
3. **Write minimum code** to make tests pass (Green phase)
4. **Refactor** while keeping tests green (Refactor phase)

**Example TDD Workflow:**

```bash
# 1. Write a failing test
# Edit: TailwindToolbox.Tests/Unit/ProjectDetectorTests.cs
# Add new test method that calls non-existent functionality

# 2. Run tests - should FAIL
dotnet test --filter "FullyQualifiedName~ProjectDetectorTests"
# ❌ Expected: Test fails because functionality doesn't exist

# 3. Implement minimum code to pass
# Edit: TailwindToolbox/Services/ProjectDetector.cs
# Add the minimal implementation

# 4. Run tests - should PASS
dotnet test --filter "FullyQualifiedName~ProjectDetectorTests"
# ✅ Expected: Test passes

# 5. Refactor if needed while keeping tests green
```

**For new features:**
- Create test file in appropriate category (Unit/Integration/Contract)
- Write all test cases first
- Get tests reviewed before implementing
- Implement feature to satisfy tests
- Never skip tests or write tests after implementation

### ✅ Principle II: Code Quality First

**Requirements:**
- **Nullable reference types enabled** (`<Nullable>enable</Nullable>`)
- **Warnings as errors** (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`)
- **No complex patterns without justification**
- **YAGNI principle** - No speculative features

**Code Review Checklist:**
- [ ] All nullable warnings resolved
- [ ] No `#pragma warning disable` without documented reason
- [ ] No abstract classes or interfaces without multiple implementations
- [ ] No repository patterns for simple file I/O
- [ ] No event buses or mediators for simple command flow

### ✅ Principle III: User Experience Consistency

**CLI UX Requirements:**
- Use Spectre.Console for consistent terminal output
- Provide clear, actionable error messages with remediation steps
- Use progress indicators for long-running operations
- Support `--help` for every command with examples
- Follow consistent option naming across commands

### ✅ Principle IV: Integration & Contract Testing

**Test Categories:**

1. **Unit Tests** - Isolated logic, mocked dependencies
2. **Integration Tests** - File I/O, temp directories, end-to-end command execution
3. **Contract Tests** - Validate assumptions about npm, node, MSBuild

**When to write each:**
- **Unit:** Pure logic (validation rules, version comparison, file path logic)
- **Integration:** Commands, file generation, project detection
- **Contract:** npm command execution, MSBuild XML structure

### ✅ Principle V: Simplicity & Clarity

**Avoid:**
- Speculative abstractions (interfaces with only one implementation)
- Repository patterns for file operations (use `System.IO` directly)
- Event buses or mediators (commands call services directly)
- Complex dependency graphs (prefer constructor injection)

**Prefer:**
- Direct file I/O via `System.IO`
- Process execution via `System.Diagnostics.Process`
- Simple dependency injection via Spectre.Console.Cli's built-in DI

## Testing Guide

### Test Organization

Tests are organized into three categories:

#### 1. Unit Tests (`TailwindToolbox.Tests/Unit/`)

Test individual classes in isolation with mocked dependencies.

**Example:**
```csharp
public class ProjectDetectorTests
{
    [Fact]
    public void DetectProject_WithValidCsproj_ReturnsBlazorProject()
    {
        // Arrange
        var detector = new ProjectDetector();
        var projectPath = "/path/to/valid/BlazorApp.csproj";

        // Act
        var result = detector.DetectProject(projectPath);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(BlazorProjectType.Server, result.ProjectType);
    }
}
```

#### 2. Integration Tests (`TailwindToolbox.Tests/Integration/`)

Test full command execution with real file I/O in isolated temp directories.

**Example:**
```csharp
public class SetupCommandIntegrationTests : IDisposable
{
    private readonly string _tempDir;

    public SetupCommandIntegrationTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(_tempDir);
    }

    [Fact]
    public async Task Setup_CreatesAllConfigurationFiles()
    {
        // Arrange
        CreateSampleBlazorProject(_tempDir);
        var command = new SetupCommand(/* dependencies */);

        // Act
        var exitCode = await command.ExecuteAsync(/* context */);

        // Assert
        Assert.Equal(0, exitCode);
        Assert.True(File.Exists(Path.Combine(_tempDir, "tailwind.config.js")));
        Assert.True(File.Exists(Path.Combine(_tempDir, "package.json")));
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, recursive: true);
    }
}
```

#### 3. Contract Tests (`TailwindToolbox.Tests/Contract/`)

Validate assumptions about external dependencies (npm, MSBuild).

**Example:**
```csharp
[Trait("Category", "Contract")]
public class NpmServiceContractTests
{
    [Fact]
    [SkipOnCI] // Skip on CI if npm not available
    public async Task CheckForUpdates_WithRealNpm_ReturnsValidVersions()
    {
        // Arrange
        var service = new NpmService();

        // Act
        var result = await service.CheckForUpdates("tailwindcss");

        // Assert
        Assert.NotNull(result);
        Assert.Matches(@"^\d+\.\d+\.\d+$", result.LatestVersion);
    }
}
```

### Running Tests

```bash
# Run all tests
dotnet test

# Run only unit tests
dotnet test --filter Category=Unit

# Run only integration tests
dotnet test --filter Category=Integration

# Run only contract tests (requires npm)
dotnet test --filter Category=Contract

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Test Coverage Goals

- **Unit tests:** 80%+ code coverage for services and models
- **Integration tests:** All commands tested end-to-end
- **Contract tests:** All external dependencies validated

## Coding Standards

### C# Style Guidelines

```csharp
// ✅ Good: Nullable reference types enabled, all warnings resolved
public class ProjectDetector : IProjectDetector
{
    public BlazorProject? DetectProject(string projectPath)
    {
        if (!File.Exists(projectPath))
        {
            return null; // Explicit null return
        }

        // Implementation...
    }
}

// ❌ Bad: Nullable warnings suppressed
#pragma warning disable CS8602
public BlazorProject? DetectProject(string projectPath)
{
    var project = FindProject(projectPath);
    return project.Name; // Possible null reference
}
#pragma warning restore CS8602
```

### Naming Conventions

- **Commands:** `{Verb}Command.cs` (e.g., `SetupCommand`, `CheckCommand`)
- **Services:** `I{Name}Service.cs` interface, `{Name}Service.cs` implementation
- **Models:** Descriptive nouns (e.g., `BlazorProject`, `ValidationResult`)
- **Tests:** `{ClassUnderTest}Tests.cs`

### Error Handling

Always provide actionable error messages:

```csharp
// ✅ Good: Actionable error message
throw new InvalidOperationException(
    "Node.js is not installed or not in PATH. " +
    "Install from https://nodejs.org/ or use nvm."
);

// ❌ Bad: Vague error
throw new Exception("Node not found");
```

### Documentation

- **XML comments** for public APIs
- **Inline comments** only for non-obvious logic
- **README updates** for new commands or major features

## Pull Request Process

### 1. Create a Feature Branch

```bash
# Sync with upstream
git fetch upstream
git checkout main
git merge upstream/main

# Create feature branch
git checkout -b feature/add-new-validation-rule
```

### 2. Make Changes Following TDD

1. Write failing tests first
2. Implement minimum code to pass tests
3. Refactor while keeping tests green
4. Ensure all tests pass: `dotnet test`

### 3. Commit with Clear Messages

```bash
# Stage changes
git add .

# Commit with descriptive message
git commit -m "feat: add validation rule for CSS file existence

- Add ValidationRule for app.css existence
- Include remediation guidance
- Add unit tests for new validation rule
- Update CheckCommand to include new rule

Resolves #123"
```

**Commit Message Format:**
```
<type>: <subject>

<body>

<footer>
```

**Types:**
- `feat:` New feature
- `fix:` Bug fix
- `test:` Adding or updating tests
- `docs:` Documentation changes
- `refactor:` Code refactoring
- `chore:` Maintenance tasks

### 4. Push and Create Pull Request

```bash
# Push feature branch
git push origin feature/add-new-validation-rule

# Create pull request on GitHub with:
# - Clear title and description
# - Reference to related issue
# - Screenshots/examples if applicable
```

### 5. Pull Request Checklist

Before submitting, ensure:

- [ ] All tests pass (`dotnet test`)
- [ ] Code follows nullable reference type guidelines
- [ ] No compiler warnings (`dotnet build` with zero warnings)
- [ ] New features have tests written FIRST (TDD)
- [ ] Documentation updated (README, XML comments)
- [ ] Commit messages are clear and descriptive
- [ ] Constitution principles followed (TDD, Code Quality, Simplicity)

### 6. Code Review Process

1. **Automated checks** run (build, tests, linting)
2. **Maintainer review** for:
   - Constitution compliance (TDD, nullable types, simplicity)
   - Test coverage and quality
   - Code clarity and documentation
   - User-facing error messages
3. **Address feedback** by pushing additional commits
4. **Merge** once approved and checks pass

## Release Process

### Version Numbering

Follow [Semantic Versioning](https://semver.org/):
- **Major (1.x.x):** Breaking changes
- **Minor (x.1.x):** New features, backward compatible
- **Patch (x.x.1):** Bug fixes, backward compatible

### Release Steps

1. **Update version** in `TailwindToolbox.csproj`:
   ```xml
   <Version>1.1.0</Version>
   <InformationalVersion>1.1.0</InformationalVersion>
   ```

2. **Update CHANGELOG.md** with release notes

3. **Create release commit:**
   ```bash
   git commit -m "chore: release v1.1.0"
   git tag v1.1.0
   git push origin main --tags
   ```

4. **Build release binaries:**
   ```bash
   dotnet build -c Release TailwindToolbox/TailwindToolbox.csproj
   ```

5. **Create GitHub release** with binaries and changelog

## Questions or Issues?

- **Questions:** Open a [GitHub Discussion](https://github.com/yourusername/TailwindToolbox/discussions)
- **Bugs:** Open a [GitHub Issue](https://github.com/yourusername/TailwindToolbox/issues)
- **Feature Requests:** Open a [GitHub Issue](https://github.com/yourusername/TailwindToolbox/issues) with `[Feature]` prefix

Thank you for contributing to Tailwind Blazor CLI! 🎉
