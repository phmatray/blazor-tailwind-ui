# Feature Specification: Blazor Tailwind Template

**Feature Branch**: `002-blazor-tailwind-template`
**Created**: 2026-01-04
**Status**: Draft
**Input**: User description: "Build a dotnet blazor template using tailwind. You must use the blazor template from Microsoft (the one which use bootstrap) and customize it with our principles. Stay close from the original template but with tailwind. Use our CLI to setup our custom template."

## User Scenarios & Testing

### User Story 1 - Create New Blazor Project with Tailwind CSS (Priority: P1)

A developer wants to create a new Blazor web application that uses Tailwind CSS instead of Bootstrap, without manually setting up the Tailwind configuration and build process.

**Why this priority**: This is the core value proposition - enabling developers to instantly start a Blazor project with Tailwind CSS already configured and working. This represents the MVP that delivers immediate value.

**Independent Test**: Can be fully tested by running `dotnet new [template-name]` and verifying that the generated project builds successfully and includes working Tailwind CSS styling. Delivers a ready-to-use Blazor project with Tailwind CSS.

**Acceptance Scenarios**:

1. **Given** I have .NET SDK installed, **When** I run `dotnet new [template-name] -n MyApp`, **Then** a new Blazor project is created with Tailwind CSS already configured
2. **Given** a newly created project from the template, **When** I run `dotnet build`, **Then** the project compiles successfully and Tailwind CSS is compiled
3. **Given** a newly created project from the template, **When** I run `dotnet run`, **Then** the application starts and displays pages styled with Tailwind CSS utilities
4. **Given** a newly created project, **When** I inspect the project structure, **Then** I see familiar Microsoft Blazor template structure (Components folder, Pages, Layout components)

---

### User Story 2 - Install Template from Package (Priority: P2)

A developer wants to install the Blazor Tailwind template so they can use it to create new projects.

**Why this priority**: Users need to be able to discover and install the template before they can use it. This is essential but secondary to having a working template that can be tested locally.

**Independent Test**: Can be tested by packaging the template as a NuGet package and installing it via `dotnet new install [package]`, then verifying it appears in the template list.

**Acceptance Scenarios**:

1. **Given** the template is packaged as a NuGet package, **When** I run `dotnet new install [package-name]`, **Then** the template is installed and available for use
2. **Given** the template is installed, **When** I run `dotnet new list`, **Then** I see the Blazor Tailwind template listed with a clear description
3. **Given** the template is installed, **When** I want to remove it, **Then** I can run `dotnet new uninstall [package-name]` to remove it

---

### User Story 3 - Customize Template with Project Name and Options (Priority: P3)

A developer wants to customize the generated project with their own project name and namespace, just like the standard Blazor template.

**Why this priority**: This provides convenience and follows standard .NET template conventions, but the template is functional without these options.

**Independent Test**: Can be tested by creating projects with different names and verifying that namespaces, assembly names, and references are correctly updated throughout the project.

**Acceptance Scenarios**:

1. **Given** I want to create a project with a specific name, **When** I run `dotnet new [template-name] -n "MyCompany.MyApp"`, **Then** all namespaces and assembly names use "MyCompany.MyApp"
2. **Given** I create a project, **When** I don't specify a name, **Then** the project uses the current directory name as the default

---

### User Story 4 - Maintain Familiar Blazor Project Structure (Priority: P1)

A developer familiar with Microsoft's Blazor template wants to use Tailwind CSS without learning a completely different project structure or conventions.

**Why this priority**: Keeping the familiar structure reduces the learning curve and makes adoption easier. This is critical for developer experience and adoption.

**Independent Test**: Can be tested by comparing the generated project structure with Microsoft's official Blazor template and verifying that component locations, naming conventions, and folder organization match.

**Acceptance Scenarios**:

1. **Given** I create a project from the template, **When** I inspect the folder structure, **Then** I see Components/Pages/, Components/Layout/, and wwwroot/ folders matching Microsoft's template
2. **Given** I'm familiar with Microsoft's Blazor template, **When** I open the template project, **Then** I find Home.razor, Counter.razor, and Weather.razor in the expected locations
3. **Given** I inspect the routing configuration, **When** I look at Routes.razor, **Then** I see the same routing setup as Microsoft's template
4. **Given** I look at the layout components, **When** I inspect MainLayout.razor and NavMenu.razor, **Then** they have the same structure and responsibilities as the original template

---

### Edge Cases

- What happens when a developer tries to install the template without having .NET SDK installed?
- How does the template handle existing projects with the same name in the directory?
- What happens if Node.js or npm is not installed when creating a project from the template?
- How does the template work on different operating systems (Windows, macOS, Linux)?
- What happens when the TailwindToolbox CLI is not globally installed when using the template?

## Requirements

### Functional Requirements

- **FR-001**: Template MUST generate a Blazor project with the same structure as Microsoft's official `dotnet new blazor` template
- **FR-002**: Template MUST include Tailwind CSS configuration files (tailwind.config.js, package.json, Styles/app.css)
- **FR-003**: Template MUST include TailwindBuild.targets for MSBuild integration to compile Tailwind CSS during build
- **FR-004**: Template MUST replace all Bootstrap CSS classes in components with equivalent Tailwind CSS utility classes
- **FR-005**: Template MUST preserve all functionality from the original Blazor template (navigation, counter, weather pages)
- **FR-006**: Template MUST use the TailwindToolbox CLI setup during template initialization to configure Tailwind CSS
- **FR-007**: Template MUST be installable as a .NET template via `dotnet new install`
- **FR-008**: Template MUST support standard .NET template parameters (name, output directory)
- **FR-009**: Template MUST include the same component files as Microsoft's template: Home.razor, Counter.razor, Weather.razor, MainLayout.razor, NavMenu.razor
- **FR-010**: Template MUST compile and run successfully without additional manual configuration
- **FR-011**: Template MUST maintain responsive design behavior equivalent to the Bootstrap version
- **FR-012**: Template MUST include npm packages for Tailwind CSS (tailwindcss and @tailwindcss/cli)
- **FR-013**: Template MUST preserve the Blazor Interactive Server rendering mode from the original template

### Key Entities

- **Template Project**: The .NET template package containing the Blazor project structure, files, and configuration
- **Generated Project**: The Blazor web application created when a developer uses the template
- **Component Files**: Razor components (Pages, Layout) that make up the Blazor application UI
- **Tailwind Configuration**: Set of files that configure Tailwind CSS (tailwind.config.js, package.json, app.css, build targets)
- **Template Manifest**: The .template.config folder containing template.json that defines template metadata and parameters

## Success Criteria

### Measurable Outcomes

- **SC-001**: Developers can create a new Blazor project with working Tailwind CSS in under 2 minutes using a single command
- **SC-002**: Generated projects build successfully without any manual configuration or additional steps
- **SC-003**: All visual components from Microsoft's Blazor template are present and functionally equivalent in the Tailwind version
- **SC-004**: Template installation completes in under 30 seconds on standard internet connection
- **SC-005**: Generated project has identical page structure and navigation as Microsoft's Blazor template
- **SC-006**: Developers familiar with Microsoft's Blazor template can navigate the generated project structure without learning new conventions

## Assumptions

- Developers using this template have .NET 10.0 SDK or later installed
- Developers have Node.js and npm installed on their system (required for Tailwind CSS)
- The TailwindToolbox CLI tool is available globally or can be installed as part of the template setup process
- The template will target the same .NET version as the current Microsoft Blazor template (.NET 10.0)
- The template will use Tailwind CSS v4.x as configured by the TailwindToolbox CLI
- Developers want a direct replacement for Bootstrap with Tailwind CSS while keeping the same functionality and component structure
