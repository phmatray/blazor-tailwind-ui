# Implementation Plan: Tailwind Blazor CLI Setup Tool

**Branch**: `001-tailwind-blazor-cli` | **Date**: 2026-01-03 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-tailwind-blazor-cli/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

A console application that automates Tailwind CSS setup in Blazor projects. Provides commands to initialize Tailwind configuration, validate setup, update dependencies, and manage MSBuild targets. Eliminates the complex manual setup process, reducing configuration time from hours to under 2 minutes while ensuring correct integration between Tailwind CSS build pipeline and Blazor compilation.

## Technical Context

**Language/Version**: C# / .NET 10.0
**Primary Dependencies**: Spectre.Console.Cli (CLI framework), latest Tailwind CSS (target for setup), xunit v3 (testing framework)
**Storage**: File system (reads/writes .csproj, tailwind.config.js, package.json, .targets files)
**Testing**: xunit v3 with integration tests for file operations and contract tests for npm/node interactions
**Target Platform**: macOS and Windows (cross-platform .NET console application)
**Project Type**: Single console application (CLI tool)
**Performance Goals**: Setup command completes in <2 minutes including npm install; validation checks complete in <5 seconds
**Constraints**: Must work offline after initial download (validate without network); must handle existing partial configurations gracefully
**Scale/Scope**: Supports Blazor projects of any size; processes .csproj files up to 10MB; validates up to 100 configuration rules

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

Verify compliance with TailwindToolbox Constitution v1.0.0:

### ✅ Test-Driven Development (Principle I - NON-NEGOTIABLE)
- [x] Tests will be written FIRST before any implementation (xunit v3 test suite designed before code)
- [x] Red-Green-Refactor cycle documented in tasks (Phase 2 planning will enforce test-first workflow)
- [x] Test approval step included before implementation phase (constitution workflow mandates test review)

### ✅ Code Quality First (Principle II)
- [x] C# nullable reference types enabled (.NET 10.0 project with `<Nullable>enable</Nullable>`)
- [x] Warnings treated as errors in project configuration (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`)
- [x] Complexity justifications documented (no complex patterns needed for CLI tool)
- [x] YAGNI principle applied (only 4 commands: setup, check, update, create-target - no speculative features)

### ✅ User Experience Consistency (Principle III)
- [x] N/A - Console application (no Blazor UI components in the CLI tool itself)
- [x] Spectre.Console provides consistent CLI UX patterns (tables, progress bars, prompts)
- [x] Terminal output follows accessibility best practices (clear text, color with fallbacks)
- [x] Help text and error messages follow consistent formatting

### ✅ Integration & Contract Testing (Principle IV)
- [x] Integration tests planned for file system operations (read/write .csproj, config files)
- [x] Contract tests planned for npm/node.js interactions (validate tool presence, version checks)
- [x] Contract tests for MSBuild target file generation (validate XML structure)

### ✅ Simplicity & Clarity (Principle V)
- [x] No speculative abstractions (direct file I/O, process execution, validation logic)
- [x] Direct dependencies via constructor injection (Spectre.Console.Cli handles DI)
- [x] No repository patterns needed (CLI commands directly operate on file system)
- [x] No event buses/mediators (simple command → handler flow)

## Project Structure

### Documentation (this feature)

```text
specs/[###-feature]/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```text
TailwindToolbox/
├── TailwindToolbox.csproj          # Main CLI project
├── Program.cs                       # Entry point, Spectre.Console.Cli setup
├── Commands/
│   ├── SetupCommand.cs             # US1: Initialize Tailwind in Blazor project
│   ├── CheckCommand.cs             # US2: Validate configuration
│   ├── UpdateCommand.cs            # US3: Update dependencies
│   └── CreateTargetCommand.cs      # US4: Generate MSBuild targets
├── Services/
│   ├── ProjectDetector.cs          # Detect Blazor project type
│   ├── FileGenerator.cs            # Create config files (tailwind.config.js, etc.)
│   ├── NpmService.cs               # Execute npm commands, manage packages
│   ├── ValidationService.cs        # Validate setup, check versions
│   └── TargetFileGenerator.cs      # Generate MSBuild .targets files
├── Models/
│   ├── BlazorProject.cs            # Represents detected project metadata
│   ├── TailwindConfig.cs           # Tailwind configuration structure
│   ├── ValidationResult.cs         # Validation rule results
│   └── DependencyVersion.cs        # Package version information
└── Templates/
    ├── tailwind.config.template.js  # Template for tailwind.config.js
    ├── package.template.json        # Template for package.json
    ├── app.template.css             # Template for Tailwind CSS input file
    └── TailwindBuild.template.targets # Template for MSBuild targets

TailwindToolbox.Tests/
├── TailwindToolbox.Tests.csproj
├── Unit/
│   ├── ProjectDetectorTests.cs
│   ├── FileGeneratorTests.cs
│   ├── ValidationServiceTests.cs
│   └── TargetFileGeneratorTests.cs
├── Integration/
│   ├── SetupCommandIntegrationTests.cs
│   ├── CheckCommandIntegrationTests.cs
│   └── FileSystemIntegrationTests.cs
└── Contract/
    ├── NpmServiceContractTests.cs   # Validate npm/node interactions
    └── MSBuildTargetContractTests.cs # Validate .targets XML structure

scripts/
└── install-tool.sh                  # macOS installation script (build + copy to PATH)
```

**Structure Decision**: Single console application with command pattern. Uses Spectre.Console.Cli's built-in command structure. Services encapsulate reusable logic (file generation, npm interaction, validation). Templates stored as embedded resources for easy deployment. Tests organized by type (unit, integration, contract) following xunit v3 best practices.

## Complexity Tracking

**Status**: ✅ **NO VIOLATIONS** - All constitution checks pass without complexity justifications needed.
