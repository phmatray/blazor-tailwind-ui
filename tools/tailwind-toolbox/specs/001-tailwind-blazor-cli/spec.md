# Feature Specification: Tailwind Blazor CLI Setup Tool

**Feature Branch**: `001-tailwind-blazor-cli`
**Created**: 2026-01-03
**Status**: Draft
**Input**: User description: "Build a console application that can help me setup tailwind in Blazor project. It should provides some command to setup a project, check the configuration, update dependencies, create a target file..."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Initial Tailwind Setup (Priority: P1)

A developer with a new or existing Blazor project wants to add Tailwind CSS support. They run a single command that configures all necessary files, dependencies, and build processes required for Tailwind to work in their Blazor project.

**Why this priority**: This is the core value proposition - getting Tailwind working quickly without manual configuration. Without this, developers face a complex, error-prone manual setup process involving multiple configuration files and build tool configurations.

**Independent Test**: Can be fully tested by running the setup command on a fresh Blazor project and verifying that Tailwind classes render correctly in components.

**Acceptance Scenarios**:

1. **Given** a Blazor project without Tailwind, **When** developer runs the setup command, **Then** all required npm packages are installed, configuration files are created, and build process is configured
2. **Given** a Blazor project with partial Tailwind configuration, **When** developer runs the setup command, **Then** missing configuration is added without overwriting existing customizations
3. **Given** the setup command completes successfully, **When** developer adds Tailwind classes to a component and runs the project, **Then** the Tailwind styles are applied correctly

---

### User Story 2 - Configuration Validation (Priority: P2)

A developer wants to verify their Tailwind setup is correct without running the entire project. They run a check command that validates all configuration files, dependencies, and build targets are properly configured.

**Why this priority**: Troubleshooting Tailwind setup issues is time-consuming. A validation command provides immediate feedback on configuration problems, reducing debugging time from hours to minutes.

**Independent Test**: Can be tested by running the check command on projects with various configuration states (correct, missing files, incorrect versions) and verifying appropriate diagnostic messages.

**Acceptance Scenarios**:

1. **Given** a correctly configured Tailwind Blazor project, **When** developer runs the check command, **Then** all validation checks pass with green checkmarks
2. **Given** a project missing required configuration files, **When** developer runs the check command, **Then** specific missing files are listed with remediation guidance
3. **Given** a project with outdated Tailwind dependencies, **When** developer runs the check command, **Then** version mismatches are reported with recommended versions
4. **Given** a project with incorrect build target configuration, **When** developer runs the check command, **Then** the specific configuration issues are identified

---

### User Story 3 - Dependency Updates (Priority: P3)

A developer wants to update Tailwind CSS and related dependencies to the latest compatible versions. They run an update command that safely upgrades packages while maintaining configuration compatibility.

**Why this priority**: Keeping dependencies current is important for security and features, but manual updates risk breaking changes. This provides a safe upgrade path.

**Independent Test**: Can be tested by running the update command on a project with old Tailwind versions and verifying packages are updated to compatible latest versions.

**Acceptance Scenarios**:

1. **Given** a project with outdated Tailwind packages, **When** developer runs the update command, **Then** packages are updated to latest compatible versions
2. **Given** a breaking change exists in latest Tailwind version, **When** developer runs the update command, **Then** developer is warned about breaking changes with option to proceed or cancel
3. **Given** the update command completes, **When** developer runs the check command, **Then** all validation checks pass with updated versions

---

### User Story 4 - Build Target Management (Priority: P4)

A developer needs to create or modify MSBuild targets for Tailwind CSS processing. They use a command to generate or update the build target file that integrates Tailwind compilation into the Blazor build process.

**Why this priority**: MSBuild integration is complex and error-prone. Automating target file generation ensures consistent, correct build configuration.

**Independent Test**: Can be tested by running the target creation command and verifying the generated MSBuild target file compiles Tailwind CSS correctly during project build.

**Acceptance Scenarios**:

1. **Given** a Blazor project without Tailwind build targets, **When** developer runs the create-target command, **Then** a properly configured .targets file is created in the project directory
2. **Given** an existing custom build target file, **When** developer runs the create-target command with force flag, **Then** the file is overwritten with standard configuration
3. **Given** a .targets file is created, **When** developer builds the project, **Then** Tailwind CSS is compiled automatically before the Blazor build completes

---

### Edge Cases

- What happens when the CLI is run outside a Blazor project directory?
- How does the system handle network failures during npm package installation?
- What happens if the project uses Yarn or pnpm instead of npm?
- How does the CLI handle projects with existing conflicting Tailwind configuration?
- What happens when node.js or npm is not installed on the system?
- How does the CLI handle permission errors when creating files?
- What happens if the Blazor project file (.csproj) is in a non-standard location?
- How does the system handle multiple Blazor projects in the same solution?

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST detect whether the current directory contains a valid Blazor project by checking for .csproj files with Blazor SDK references
- **FR-002**: System MUST install Tailwind CSS npm packages (tailwindcss, @tailwindcss/cli, autoprefixer) via npm commands
- **FR-003**: System MUST create a tailwind.config.js file with correct content paths for Blazor (.razor, .html, .cshtml files)
- **FR-004**: System MUST create or update package.json with Tailwind build scripts
- **FR-005**: System MUST create an MSBuild .targets file that runs Tailwind compilation before Blazor build
- **FR-006**: System MUST create a CSS input file (e.g., app.css) with Tailwind directives (@tailwind base, @tailwind components, @tailwind utilities)
- **FR-007**: System MUST validate that required tools (node.js, npm) are installed and accessible
- **FR-008**: System MUST check for existing configuration files and prompt before overwriting
- **FR-009**: System MUST verify npm package versions are compatible with the Blazor project version
- **FR-010**: System MUST generate clear, actionable error messages when validation fails
- **FR-011**: System MUST display progress indicators during long-running operations (npm install, file creation)
- **FR-012**: System MUST create a .gitignore entry for node_modules if not already present
- **FR-013**: System MUST validate that generated CSS output is referenced in the Blazor layout files
- **FR-014**: System MUST support both Blazor Server and Blazor WebAssembly project types
- **FR-015**: System MUST provide a dry-run mode that shows what changes would be made without executing them

### Assumptions

- Developers have basic command-line experience
- Projects use standard Blazor project structure (created via dotnet new blazor templates)
- Developers have node.js and npm installed (validated at runtime)
- Projects use Git for version control (for .gitignore handling)
- Standard npm is the default package manager (Yarn/pnpm support is future enhancement)
- Projects use .NET 6.0 or higher (minimum Blazor version supporting this integration pattern)
- Developers have write permissions in their project directory

### Key Entities

- **Blazor Project**: A .csproj file containing Blazor SDK references, represents the target project for Tailwind setup
- **Configuration File**: Files like tailwind.config.js, package.json that define Tailwind behavior and dependencies
- **Build Target**: MSBuild .targets file that integrates Tailwind CSS compilation into the build pipeline
- **Dependency Package**: npm packages (tailwindcss, autoprefixer) with specific version requirements
- **Validation Rule**: A specific check (file exists, version match, configuration valid) with pass/fail status and remediation guidance

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: Developers can complete initial Tailwind setup in under 2 minutes from command execution to styled components
- **SC-002**: Configuration validation identifies 100% of common setup issues (missing files, version mismatches, incorrect configuration)
- **SC-003**: 90% of developers successfully set up Tailwind on first attempt without consulting external documentation
- **SC-004**: Generated configuration works without modification for standard Blazor project structures
- **SC-005**: Validation errors include specific file paths and actionable remediation steps
- **SC-006**: Setup process completes successfully on both Windows and macOS development environments
- **SC-007**: Update command safely upgrades dependencies without breaking existing Tailwind functionality in 95% of cases
