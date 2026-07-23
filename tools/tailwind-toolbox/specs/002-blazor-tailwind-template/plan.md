# Implementation Plan: Blazor Tailwind Template

**Branch**: `002-blazor-tailwind-template` | **Date**: 2026-01-04 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/002-blazor-tailwind-template/spec.md`

**Note**: This template is filled in by the `/speckit.plan` command. See `.specify/templates/commands/plan.md` for the execution workflow.

## Summary

A .NET template package that generates Blazor web applications pre-configured with Tailwind CSS instead of Bootstrap. The template maintains the exact same structure, components, and functionality as Microsoft's official `dotnet new blazor` template while using Tailwind CSS utility classes for styling. Uses the TailwindToolbox CLI to automatically configure Tailwind CSS during project creation, eliminating manual setup and reducing time-to-first-run from hours to under 2 minutes.

## Technical Context

**Language/Version**: C# / .NET 10.0 (Blazor Web App), .NET Template Engine
**Primary Dependencies**: Tailwind CSS v4.x, @tailwindcss/cli, TailwindToolbox CLI (for setup)
**Storage**: File system (template files, .template.config/template.json manifest)
**Testing**: xunit v3 with integration tests for template generation and verification
**Target Platform**: Cross-platform (.NET templates work on Windows, macOS, Linux)
**Project Type**: .NET Template Package (generates single Blazor web application)
**Performance Goals**: Template instantiation completes in <30 seconds; generated project builds in <10 seconds
**Constraints**: Must preserve exact Microsoft Blazor template structure; must work with `dotnet new` commands; Tailwind must compile during build without manual intervention
**Scale/Scope**: Generates single-page Blazor apps; template package <5MB; supports all standard .NET template parameters (name, output, etc.)

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

Verify compliance with TailwindToolbox Constitution v1.0.0:

### ✅ Test-Driven Development (Principle I - NON-NEGOTIABLE)
- [x] Tests will be written FIRST before any implementation (template generation tests before template content)
- [x] Red-Green-Refactor cycle documented in tasks (Phase 2 planning will enforce test-first workflow)
- [x] Test approval step included before implementation phase (constitution workflow mandates test review)

### ✅ Code Quality First (Principle II)
- [x] C# nullable reference types enabled (.NET 10.0 template with `<Nullable>enable</Nullable>`)
- [x] Warnings treated as errors in generated project configuration (`<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`)
- [x] Complexity justifications documented (simple template structure, no speculative features)
- [x] YAGNI principle applied (only includes features from Microsoft's template + Tailwind CSS)

### ✅ User Experience Consistency (Principle III)
- [x] WCAG 2.1 Level AA compliance planned for UI components (same as Microsoft template, with Tailwind)
- [x] Tailwind utility classes used exclusively (replacing all Bootstrap classes)
- [x] Responsive design with mobile-first approach (Tailwind responsive utilities)
- [x] Semantic HTML5 elements preserved from Microsoft template
- [x] Component reusability maintained (same component structure as Microsoft template)

### ✅ Integration & Contract Testing (Principle IV)
- [x] Component integration tests planned for generated Blazor components (verify templates render correctly)
- [x] Contract tests planned for template.json schema validation
- [x] Integration tests for TailwindToolbox CLI setup execution during template instantiation
- [x] Contract tests for npm package.json and Tailwind config files

### ✅ Simplicity & Clarity (Principle V)
- [x] No speculative abstractions (direct file-based template, standard .NET template engine)
- [x] No hidden global state (template parameters explicitly defined in template.json)
- [x] No repository/service patterns needed (template is file-based content)
- [x] No event buses/mediators (simple template instantiation → TailwindToolbox CLI setup flow)

## Project Structure

### Documentation (this feature)

```text
specs/002-blazor-tailwind-template/
├── plan.md              # This file (/speckit.plan command output)
├── research.md          # Phase 0 output (/speckit.plan command)
├── data-model.md        # Phase 1 output (/speckit.plan command)
├── quickstart.md        # Phase 1 output (/speckit.plan command)
├── contracts/           # Phase 1 output (/speckit.plan command)
│   ├── template.json    # Template manifest contract
│   └── package.json     # npm package contract for Tailwind
└── tasks.md             # Phase 2 output (/speckit.tasks command - NOT created by /speckit.plan)
```

### Source Code (repository root)

```text
templates/
└── blazor-tailwind/
    ├── .template.config/
    │   ├── template.json              # Template manifest (name, shortName, parameters)
    │   ├── dotnetcli.host.json        # CLI host configuration
    │   └── ide.host.json              # IDE integration configuration
    ├── BlazorTailwind.csproj          # Project file with parameter substitution
    ├── Program.cs                      # Blazor app entry point
    ├── Components/
    │   ├── App.razor                   # Root component with CSS reference
    │   ├── Routes.razor                # Routing configuration
    │   ├── _Imports.razor              # Global using statements
    │   ├── Layout/
    │   │   ├── MainLayout.razor        # Main layout (Tailwind classes)
    │   │   ├── MainLayout.razor.css    # Component-scoped CSS (minimal)
    │   │   ├── NavMenu.razor           # Navigation menu (Tailwind classes)
    │   │   └── NavMenu.razor.css       # Component-scoped CSS (minimal)
    │   └── Pages/
    │       ├── Home.razor              # Home page (Tailwind classes)
    │       ├── Counter.razor           # Counter demo (Tailwind classes)
    │       ├── Weather.razor           # Weather forecast (Tailwind classes)
    │       └── Error.razor             # Error page (Tailwind classes)
    ├── Styles/
    │   └── app.css                     # Tailwind CSS input file (@import directives)
    ├── wwwroot/
    │   ├── css/
    │   │   └── app.css                 # Compiled Tailwind CSS (generated by build)
    │   ├── favicon.png                 # Site icon
    │   └── app.css                     # Legacy CSS (minimal/empty)
    ├── appsettings.json                # App configuration
    ├── appsettings.Development.json    # Development configuration
    ├── .gitignore                      # Git ignore (includes node_modules)
    ├── tailwind.config.js              # Tailwind configuration (content paths)
    ├── package.json                    # npm packages (tailwindcss, @tailwindcss/cli)
    └── TailwindBuild.targets           # MSBuild integration for Tailwind compilation

templates/blazor-tailwind.Tests/
├── blazor-tailwind.Tests.csproj       # Test project
├── TemplateGenerationTests.cs         # Tests for template instantiation
├── ComponentRenderingTests.cs          # Tests for generated component rendering
└── TailwindIntegrationTests.cs        # Tests for Tailwind CSS compilation
```

**Structure Decision**: Single template package structure following .NET template conventions. The template content mirrors Microsoft's `dotnet new blazor` template structure exactly, with only CSS classes and build configuration modified to use Tailwind CSS instead of Bootstrap.

## Implementation Resources

### Core Technical References

**For .NET 10 Blazor Template Structure:**
- See [research.md: .NET 10 Blazor Template Structure (Verified)](./research.md#net-10-blazor-template-structure-verified) for exact file structure from `dotnet new blazor`
- **Critical**: ReconnectModal.razor and NotFound.razor are NEW in .NET 10 (research.md lines 46-54, 69-74)
- **Critical**: CSS location changed to `wwwroot/app.css` NOT `wwwroot/css/app.css` (research.md line 56)
- **Critical**: @Assets[] syntax in App.razor (research.md lines 80-84)
- **Critical**: MapStaticAssets() in Program.cs (research.md lines 91-93)

**For Tailwind CSS v4.x Integration:**
- See [research.md: Tailwind CSS v4.x Integration Patterns](./research.md#tailwind-css-v4x-integration-patterns) for v4-specific syntax
- **Critical**: Use `@import "tailwindcss";` NOT `@tailwind` directives (research.md lines 132-143)
- **Critical**: Requires separate `@tailwindcss/cli` package (research.md lines 186-198)
- See [research.md: TailwindBuild.targets](./research.md#tailwindbuildtargets-msbuild-integration) for build configuration (research.md lines 279-295)
- See [research.md: Styles/app.css](./research.md#stylesappcss-tailwind-input) for v4.x @import syntax (research.md lines 236-254)

**For Template Configuration:**
- See [contracts/template.json](./contracts/template.json) for complete template manifest schema
- See [data-model.md: Template Manifest](./data-model.md#1-template-manifest-templatejson) for schema explanation (data-model.md lines 9-73)
- See [research.md: Post-Actions for npm Installation](./research.md#post-actions-for-npm-installation) for platform-specific npm handling (research.md lines 323-369)

**For npm Package Configuration:**
- See [contracts/package.json](./contracts/package.json) for exact dependencies
- See [data-model.md: npm Package Manifest](./data-model.md#5-npm-package-manifest-packagejson) for schema details (data-model.md lines 205-246)
- **Critical**: Must include BOTH `tailwindcss` AND `@tailwindcss/cli` (research.md lines 196-198, 256-277)

**For Component Conversion:**
- See [research.md: Bootstrap to Tailwind CSS Mapping](./research.md#bootstrap-to-tailwind-css-mapping) for class conversions (research.md lines 417-442)
- See [research.md: Component Conversion Priority](./research.md#component-conversion-priority) for recommended order (research.md lines 531-559)
- Icon replacement strategy documented in research.md lines 439-442

**For Test-Driven Development:**
- See [research.md: Template Testing Strategy (TDD Workflow)](./research.md#template-testing-strategy-tdd-workflow) for 4-phase test approach (research.md lines 503-529)
- **Phase 1**: Template generation tests FIRST
- **Phase 2**: Build integration tests SECOND
- **Phase 3**: Component tests THIRD
- **Phase 4**: Parameter substitution tests FOURTH

### File-to-Entity Mapping

| Template File | Entity Definition | Schema Contract | Implementation Guide |
|--------------|-------------------|-----------------|---------------------|
| `.template.config/template.json` | [data-model.md: Template Manifest](./data-model.md#1-template-manifest-templatejson) | [contracts/template.json](./contracts/template.json) | See lines 9-73 |
| `.template.config/dotnetcli.host.json` | [data-model.md: CLI Host Configuration](./data-model.md#2-cli-host-configuration-dotnetclihostjson) | N/A | See lines 75-88 |
| `.template.config/ide.host.json` | [data-model.md: IDE Host Configuration](./data-model.md#3-ide-host-configuration-idehostjson) | N/A | See lines 90-112 |
| `tailwind.config.js` | [data-model.md: Tailwind Configuration](./data-model.md#4-tailwind-configuration-tailwindconfigjs) | N/A (JS file) | See lines 77-114 + research.md lines 169-183 |
| `package.json` | [data-model.md: npm Package Manifest](./data-model.md#5-npm-package-manifest-packagejson) | [contracts/package.json](./contracts/package.json) | See lines 205-246 + research.md lines 256-277 |
| `TailwindBuild.targets` | [data-model.md: MSBuild Targets File](./data-model.md#6-msbuild-targets-file-tailwindbuildtargets) | N/A (XML file) | See lines 115-157 + research.md lines 279-295 |
| `BlazorTailwind.csproj` | [data-model.md: Project File](./data-model.md#7-project-file-blazortailwindcsproj) | N/A (XML file) | See lines 158-198 |
| `Components/**/*.razor` | [data-model.md: Blazor Component Files](./data-model.md#8-blazor-component-files-razor) | N/A (Razor files) | See lines 199-215 |
| `Program.cs` | [data-model.md: Program.cs](./data-model.md#9-programcs-application-entry-point) | N/A (C# file) | See lines 216-233 + research.md lines 95-111 |
| `Styles/app.css` | [data-model.md: Tailwind Input CSS](./data-model.md#10-tailwind-input-css-stylesappcss) | N/A (CSS file) | See lines 234-249 + research.md lines 236-254 |
| `.gitignore` | [data-model.md: .gitignore](./data-model.md#11-gitignore) | N/A | See lines 250-263 |

### Contract Validation

#### Template Manifest Contract (contracts/template.json)

**Purpose**: Defines the complete schema for .template.config/template.json
**Usage**: Copy this file as starting point for template manifest
**Validation**: Template instantiation will fail if schema is invalid

**Key Sections**:
- `symbols`: Parameter definitions (lines 42-56 in contract)
- `postActions`: npm installation + restore (lines 330-368 in research.md)
- `constraints`: Require .NET 10 SDK (lines 407-415 in research.md)
- `sources.modifiers.exclude`: Exclude node_modules, bin, obj (lines 88-94 in research.md)

**Cross-Reference**: See [data-model.md: Template Manifest](./data-model.md#1-template-manifest-templatejson) for entity explanation

#### npm Package Contract (contracts/package.json)

**Purpose**: Defines exact npm dependencies for Tailwind CSS v4.x
**Usage**: Copy this file as starting point for package.json in template
**Validation**: npm install will fail if dependencies are incompatible

**Critical Requirements**:
- BOTH `tailwindcss` AND `@tailwindcss/cli` required (v4.x change)
- `autoprefixer` for CSS vendor prefixing
- Node.js >=16.0.0 (engines constraint)

**Cross-Reference**: See [data-model.md: npm Package Manifest](./data-model.md#5-npm-package-manifest-packagejson) for schema details
**Cross-Reference**: See [research.md: package.json (v4.x Dependencies)](./research.md#packagejson-v4x-dependencies) for v4.x requirements

### Pre-Implementation Checklist

Before starting implementation, ensure you understand:

- [ ] .NET 10 template structure differences from .NET 9 (research.md lines 67-94)
- [ ] Tailwind CSS v4.x breaking changes from v3.x (research.md lines 127-218)
- [ ] Template manifest post-action platform differences (research.md lines 323-369)
- [ ] Bootstrap → Tailwind class mapping (research.md lines 417-442)
- [ ] TDD workflow: tests FIRST (research.md lines 503-529)
- [ ] All entity schemas in data-model.md
- [ ] All contracts in contracts/ folder

**Critical .NET 10 Changes:**
1. ✅ ReconnectModal.razor component (NEW - research.md lines 69-72)
2. ✅ NotFound.razor component (NEW - research.md line 74)
3. ✅ CSS location: wwwroot/app.css (changed - research.md line 56)
4. ✅ @Assets[] syntax in App.razor (NEW - research.md lines 80-84)
5. ✅ MapStaticAssets() in Program.cs (NEW - research.md lines 91-93)
6. ✅ ResourcePreloader, ImportMap, HeadOutlet (NEW - research.md lines 86-89)

**Critical Tailwind v4.x Changes:**
1. ✅ @import "tailwindcss"; (NOT @tailwind directives - research.md lines 132-143)
2. ✅ Separate @tailwindcss/cli package (required - research.md lines 186-198)
3. ✅ npx @tailwindcss/cli command (NOT npx tailwindcss - research.md lines 279-295)
4. ✅ Class renames: shadow-xs, outline-hidden, etc. (research.md lines 200-209)
5. ✅ Browser requirements: Safari 16.4+, Chrome 111+, Firefox 128+ (research.md lines 211-218)

### Quick Reference: Version Targets

| Component | Version | Support Until | Notes |
|-----------|---------|---------------|-------|
| **.NET SDK** | 10.0 (LTS) | Nov 10, 2028 | Long-term support release |
| **Tailwind CSS** | 4.0.8+ | Current stable | Released Jan 22, 2025 |
| **@tailwindcss/cli** | 4.0.8+ | Current stable | Separate package in v4.x |
| **Node.js** | 16.0.0+ | - | Required for Tailwind compilation |
| **npm** | 8.0.0+ | - | Package management |

See [research.md: Version Targets](./research.md#version-targets) for complete version table.

## Complexity Tracking

> **No Constitution violations - this table is not needed**

All complexity is justified by .NET template engine requirements (template.json, parameter substitution) and Tailwind CSS integration (config files, build targets). No speculative patterns or unnecessary abstractions introduced.
