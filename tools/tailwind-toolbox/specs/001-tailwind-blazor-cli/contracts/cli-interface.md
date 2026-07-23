# CLI Interface Contract

**Feature**: 001-tailwind-blazor-cli
**Date**: 2026-01-03
**Status**: Complete

## Overview

This document defines the command-line interface contracts for the Tailwind Blazor CLI tool. Includes command signatures, options, arguments, exit codes, and output formats.

---

## Global Options

Available for all commands:

| Option | Short | Type | Default | Description |
|--------|-------|------|---------|-------------|
| `--help` | `-h` | flag | - | Display command help |
| `--version` | `-v` | flag | - | Display tool version |
| `--verbose` | - | flag | false | Enable detailed logging |
| `--no-color` | - | flag | false | Disable colored output |

**Examples**:
```bash
tailwind-blazor --help
tailwind-blazor --version
tailwind-blazor setup --verbose
```

---

## Command: setup

**Purpose**: Initialize Tailwind CSS configuration in a Blazor project (User Story 1)

**Syntax**:
```bash
tailwind-blazor setup [OPTIONS]
```

**Options**:

| Option | Short | Type | Default | Description |
|--------|-------|------|---------|-------------|
| `--project-dir` | `-p` | path | `.` | Path to Blazor project directory |
| `--tailwind-version` | `-t` | version | `latest` | Tailwind CSS version to install |
| `--force` | `-f` | flag | false | Overwrite existing configuration |
| `--dry-run` | `-d` | flag | false | Show what would be done without executing |
| `--skip-npm-install` | - | flag | false | Skip npm package installation |
| `--css-output` | - | path | `wwwroot/css/app.css` | Output path for compiled CSS |

**Arguments**: None

**Examples**:
```bash
# Setup in current directory
tailwind-blazor setup

# Setup in specific project
tailwind-blazor setup --project-dir ./MyBlazorApp

# Setup with specific Tailwind version
tailwind-blazor setup --tailwind-version 4.0.5

# Dry run to preview changes
tailwind-blazor setup --dry-run

# Force overwrite existing config
tailwind-blazor setup --force
```

**Exit Codes**:
- `0`: Success - Tailwind configured successfully
- `1`: Error - Project not found, not a Blazor project, or npm install failed
- `2`: User cancelled - Prompted to overwrite but user declined
- `3`: Prerequisites missing - Node.js or npm not installed

**Output (Success)**:
```
Tailwind Blazor Setup
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

✓ Blazor project detected: MyBlazorApp (Blazor WebAssembly, net10.0)
✓ Node.js v20.10.0 detected
✓ npm v10.2.0 detected

Installing Tailwind CSS packages...
✓ tailwindcss@4.0.0 installed
✓ autoprefixer@10.4.16 installed

Creating configuration files...
✓ tailwind.config.js created
✓ package.json created
✓ Styles/app.css created
✓ .gitignore updated

Setting up build integration...
✓ TailwindBuild.targets created
✓ MyBlazorApp.csproj updated

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Setup complete! Run 'dotnet build' to compile Tailwind CSS.
```

**Output (Dry Run)**:
```
Dry Run Mode - No changes will be made
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

The following changes would be made:

Files to create:
  • tailwind.config.js
  • package.json
  • Styles/app.css
  • TailwindBuild.targets

Files to modify:
  • MyBlazorApp.csproj (add MSBuild import)
  • .gitignore (add node_modules)

npm packages to install:
  • tailwindcss@4.0.0
  • autoprefixer@10.4.16

Run without --dry-run to apply changes.
```

**Output (Error - Node.js not found)**:
```
Error: Node.js not found

Tailwind CSS requires Node.js to be installed.

Install Node.js:
  • Download from: https://nodejs.org/
  • Or use nvm: curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash

After installation, run 'tailwind-blazor setup' again.

Exit code: 3
```

---

## Command: check

**Purpose**: Validate Tailwind CSS configuration (User Story 2)

**Syntax**:
```bash
tailwind-blazor check [OPTIONS]
```

**Options**:

| Option | Short | Type | Default | Description |
|--------|-------|------|---------|-------------|
| `--project-dir` | `-p` | path | `.` | Path to Blazor project directory |
| `--category` | `-c` | enum | `all` | Validation category (environment, files, config, dependencies, integration) |
| `--format` | `-f` | enum | `table` | Output format (table, json, text) |
| `--fail-on-warning` | - | flag | false | Treat warnings as errors |

**Arguments**: None

**Examples**:
```bash
# Check all validations
tailwind-blazor check

# Check only environment
tailwind-blazor check --category environment

# Output as JSON
tailwind-blazor check --format json

# Fail build on warnings
tailwind-blazor check --fail-on-warning
```

**Exit Codes**:
- `0`: Success - All checks passed or only warnings (unless --fail-on-warning)
- `1`: Error - One or more validation errors found
- `2`: Project not found or not a Blazor project

**Output (Success - Table Format)**:
```
Tailwind Configuration Validation
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Environment
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ Node.js installed          │ ✓ Pass │ v20.10.0            │
│ npm installed              │ ✓ Pass │ v10.2.0             │
│ .NET SDK version           │ ✓ Pass │ .NET 10.0           │
└────────────────────────────┴────────┴─────────────────────┘

Files
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ tailwind.config.js exists  │ ✓ Pass │ Found               │
│ package.json exists        │ ✓ Pass │ Found               │
│ CSS input file exists      │ ✓ Pass │ Styles/app.css      │
│ Build targets exist        │ ✓ Pass │ TailwindBuild.targets│
│ .gitignore configured      │ ✓ Pass │ node_modules ignored│
└────────────────────────────┴────────┴─────────────────────┘

Configuration
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ Tailwind config valid      │ ✓ Pass │ Valid JavaScript    │
│ Content paths correct      │ ✓ Pass │ Includes .razor     │
│ package.json valid         │ ✓ Pass │ Valid JSON          │
│ Build scripts present      │ ✓ Pass │ build:css defined   │
└────────────────────────────┴────────┴─────────────────────┘

Dependencies
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ tailwindcss version        │ ⚠ Warn │ 4.0.0 (4.0.5 avail.)│
│ autoprefixer version       │ ✓ Pass │ 10.4.16             │
│ No deprecated packages     │ ✓ Pass │ All packages current│
└────────────────────────────┴────────┴─────────────────────┘

Integration
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ MSBuild import exists      │ ✓ Pass │ .csproj references  │
│ Target XML valid           │ ✓ Pass │ Well-formed XML     │
└────────────────────────────┴────────┴─────────────────────┘

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Summary: 16 passed, 1 warning, 0 errors

Warnings:
  • tailwindcss version 4.0.5 available (currently 4.0.0)
    Run 'tailwind-blazor update' to upgrade
```

**Output (Errors Found)**:
```
Tailwind Configuration Validation
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Environment
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ Node.js installed          │ ✗ Fail │ Not found in PATH   │
│ npm installed              │ ✗ Fail │ Requires Node.js    │
│ .NET SDK version           │ ✓ Pass │ .NET 10.0           │
└────────────────────────────┴────────┴─────────────────────┘

Files
┌────────────────────────────┬────────┬─────────────────────┐
│ Check                      │ Status │ Details             │
├────────────────────────────┼────────┼─────────────────────┤
│ tailwind.config.js exists  │ ✗ Fail │ Not found           │
│ package.json exists        │ ✗ Fail │ Not found           │
└────────────────────────────┴────────┴─────────────────────┘

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Summary: 1 passed, 0 warnings, 4 errors

Errors:
  1. Node.js not found
     → Install Node.js from https://nodejs.org/

  2. tailwind.config.js not found
     → Run 'tailwind-blazor setup' to create configuration

  3. package.json not found
     → Run 'tailwind-blazor setup' to create configuration

Exit code: 1
```

**Output (JSON Format)**:
```json
{
  "timestamp": "2026-01-03T14:30:00Z",
  "projectName": "MyBlazorApp",
  "projectPath": "/path/to/MyBlazorApp",
  "summary": {
    "total": 17,
    "passed": 13,
    "warnings": 1,
    "errors": 3
  },
  "results": [
    {
      "category": "Environment",
      "ruleId": "NODE_INSTALLED",
      "name": "Node.js installed",
      "status": "Pass",
      "severity": "Error",
      "message": "v20.10.0",
      "filePath": null
    },
    {
      "category": "Files",
      "ruleId": "TAILWIND_CONFIG_EXISTS",
      "name": "tailwind.config.js exists",
      "status": "Fail",
      "severity": "Error",
      "message": "File not found",
      "filePath": "/path/to/MyBlazorApp/tailwind.config.js",
      "remediation": "Run 'tailwind-blazor setup' to create configuration"
    }
  ]
}
```

---

## Command: update

**Purpose**: Update Tailwind CSS and related dependencies (User Story 3)

**Syntax**:
```bash
tailwind-blazor update [OPTIONS]
```

**Options**:

| Option | Short | Type | Default | Description |
|--------|-------|------|---------|-------------|
| `--project-dir` | `-p` | path | `.` | Path to Blazor project directory |
| `--package` | - | string | `all` | Specific package to update (tailwindcss, autoprefixer, all) |
| `--target-version` | `-t` | version | `latest` | Target version to update to |
| `--dry-run` | `-d` | flag | false | Show what would be updated without executing |
| `--skip-breaking` | - | flag | false | Skip updates with breaking changes |

**Arguments**: None

**Examples**:
```bash
# Update all packages to latest
tailwind-blazor update

# Update only tailwindcss
tailwind-blazor update --package tailwindcss

# Update to specific version
tailwind-blazor update --package tailwindcss --target-version 4.0.5

# Preview updates
tailwind-blazor update --dry-run

# Skip breaking changes
tailwind-blazor update --skip-breaking
```

**Exit Codes**:
- `0`: Success - Packages updated successfully
- `1`: Error - npm update failed or network error
- `2`: User cancelled - Breaking changes detected and user declined
- `3`: No updates available

**Output (Success)**:
```
Tailwind Package Updates
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Checking for updates...

Available updates:
┌─────────────────┬─────────┬─────────┬──────────────┐
│ Package         │ Current │ Latest  │ Change Type  │
├─────────────────┼─────────┼─────────┼──────────────┤
│ tailwindcss     │ 4.0.0   │ 4.0.5   │ Patch        │
│ autoprefixer    │ 10.4.16 │ 10.4.17 │ Patch        │
└─────────────────┴─────────┴─────────┴──────────────┘

Updating packages...
✓ tailwindcss@4.0.5 installed
✓ autoprefixer@10.4.17 installed
✓ package.json updated

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Update complete! Run 'dotnet build' to recompile CSS.
```

**Output (Breaking Changes Warning)**:
```
Tailwind Package Updates
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Checking for updates...

Available updates:
┌─────────────────┬─────────┬─────────┬──────────────┐
│ Package         │ Current │ Latest  │ Change Type  │
├─────────────────┼─────────┼─────────┼──────────────┤
│ tailwindcss     │ 4.0.0   │ 5.0.0   │ Major ⚠      │
└─────────────────┴─────────┴─────────┴──────────────┘

⚠ Warning: Breaking changes detected

tailwindcss v4 → v5 includes breaking changes:
  • Renamed utility classes (bg-opacity-* → bg-*/50)
  • Removed deprecated plugins
  • New configuration format

Review migration guide: https://tailwindcss.com/docs/upgrade-guide

Continue with update? [y/N]:
```

**Output (No Updates)**:
```
Tailwind Package Updates
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Checking for updates...

All packages are up to date:
┌─────────────────┬─────────┐
│ Package         │ Version │
├─────────────────┼─────────┤
│ tailwindcss     │ 4.0.5   │
│ autoprefixer    │ 10.4.17 │
└─────────────────┴─────────┘

No updates available.

Exit code: 3
```

---

## Command: create-target

**Purpose**: Generate or update MSBuild targets file (User Story 4)

**Syntax**:
```bash
tailwind-blazor create-target [OPTIONS]
```

**Options**:

| Option | Short | Type | Default | Description |
|--------|-------|------|---------|-------------|
| `--project-dir` | `-p` | path | `.` | Path to Blazor project directory |
| `--target-name` | `-n` | string | `BuildTailwindCSS` | MSBuild target name |
| `--input-css` | `-i` | path | `Styles/app.css` | Input CSS file path (relative) |
| `--output-css` | `-o` | path | `wwwroot/css/app.css` | Output CSS file path (relative) |
| `--minify` | `-m` | enum | `release-only` | Minification mode (always, never, release-only) |
| `--force` | `-f` | flag | false | Overwrite existing targets file |
| `--dry-run` | `-d` | flag | false | Show generated content without writing |

**Arguments**: None

**Examples**:
```bash
# Create targets file with defaults
tailwind-blazor create-target

# Custom input/output paths
tailwind-blazor create-target --input-css Styles/main.css --output-css wwwroot/css/main.css

# Always minify
tailwind-blazor create-target --minify always

# Preview generated file
tailwind-blazor create-target --dry-run

# Force overwrite
tailwind-blazor create-target --force
```

**Exit Codes**:
- `0`: Success - Targets file created/updated
- `1`: Error - File write failed or .csproj update failed
- `2`: User cancelled - File exists but user declined overwrite

**Output (Success)**:
```
MSBuild Targets Generation
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Generating MSBuild targets file...

Configuration:
  Target Name:    BuildTailwindCSS
  Input CSS:      Styles/app.css
  Output CSS:     wwwroot/css/app.css
  Minify:         Release builds only

✓ TailwindBuild.targets created
✓ MyBlazorApp.csproj updated (Import added)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Targets file created! Tailwind CSS will compile on build.
```

**Output (Dry Run)**:
```
MSBuild Targets Generation (Dry Run)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

Generated content for TailwindBuild.targets:

<Project>
  <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
    <Exec
      Command="npx tailwindcss -i Styles/app.css -o wwwroot/css/app.css --minify"
      Condition="'$(Configuration)' == 'Release'"
      WorkingDirectory="$(ProjectDir)" />
    <Exec
      Command="npx tailwindcss -i Styles/app.css -o wwwroot/css/app.css"
      Condition="'$(Configuration)' == 'Debug'"
      WorkingDirectory="$(ProjectDir)" />
  </Target>
</Project>

Run without --dry-run to create file.
```

**Output (File Exists)**:
```
MSBuild Targets Generation
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

⚠ Warning: TailwindBuild.targets already exists

Overwriting will replace any custom modifications.

Overwrite existing file? [y/N]:
```

---

## Exit Code Reference

| Code | Meaning | User Action |
|------|---------|-------------|
| `0` | Success | Continue workflow |
| `1` | General error | Check error message, fix issue |
| `2` | User cancelled operation | Re-run command with different options |
| `3` | Prerequisites missing / No action needed | Install prerequisites or no updates available |
| `4` | Invalid arguments | Check command syntax with --help |

**CI/CD Integration**:
```bash
# Validate configuration in CI pipeline
tailwind-blazor check --fail-on-warning --format json
EXIT_CODE=$?

if [ $EXIT_CODE -ne 0 ]; then
  echo "Tailwind validation failed"
  exit 1
fi
```

---

## Output Formatting

### Color Scheme

- ✓ Green: Success, passed checks
- ✗ Red: Errors, failed checks
- ⚠ Yellow: Warnings
- ℹ Blue: Informational messages
- Gray: Secondary information

### Progress Indicators

**Spinner** (long operations):
```
⠋ Installing Tailwind CSS packages...
```

**Progress Bar** (deterministic operations):
```
Validating configuration [████████████████████] 100%
```

**Status Updates** (step-by-step):
```
✓ Node.js detected
✓ Blazor project found
⠋ Installing packages...
```

### Tables

All tables use Unicode box drawing:
- `┌─┬─┐` for headers
- `├─┼─┤` for separators
- `└─┴─┘` for footers

### Text Wrapping

- Console width detected automatically
- Long paths truncated with ellipsis: `/very/long/path/.../file.js`
- Table columns sized proportionally

---

## Localization (Future)

**Current**: English only
**Future**: Support for i18n via resource files

Error codes remain constant across languages for CI/CD compatibility.

---

## Contract Testing

All CLI contracts validated by integration tests:

```csharp
[Fact]
public async Task SetupCommand_WithValidProject_ReturnsExitCode0()
{
    var result = await RunCliAsync("setup", "--project-dir", testProjectPath);
    Assert.Equal(0, result.ExitCode);
}

[Fact]
public async Task CheckCommand_OutputsJsonFormat()
{
    var result = await RunCliAsync("check", "--format", "json");
    var json = JsonDocument.Parse(result.Output);
    Assert.NotNull(json.RootElement.GetProperty("summary"));
}
```

---

## Versioning

CLI tool uses semantic versioning: `MAJOR.MINOR.PATCH`

**Version Display**:
```bash
tailwind-blazor --version
# Output:
Tailwind Blazor CLI v1.0.0
.NET Runtime: 10.0
Spectre.Console.Cli: 0.49.0
```

**Breaking Changes**:
- Command renamed → Major bump
- Required option added → Major bump
- Exit code meaning changed → Major bump
- New optional option → Minor bump
- Bug fix in command logic → Patch bump
