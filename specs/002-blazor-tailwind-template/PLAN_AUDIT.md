# Implementation Plan Audit: Blazor Tailwind Template

**Date**: 2026-01-04
**Auditor**: Claude
**Purpose**: Evaluate plan completeness, task sequencing clarity, and cross-references to implementation details

---

## Executive Summary

**Status**: ⚠️ **NEEDS ENHANCEMENT**

The current plan.md follows the template structure correctly but **lacks sufficient cross-references** to implementation detail files (research.md, data-model.md, contracts/, quickstart.md). This makes it difficult for an implementer to know where to find specific technical details when executing tasks.

**Key Findings**:
1. ✅ Plan structure follows template correctly
2. ✅ Constitution checks are complete
3. ✅ Project structure is well-defined
4. ⚠️ **Missing cross-references** to research findings
5. ⚠️ **Missing links** to data model entities
6. ⚠️ **Missing pointers** to contract specifications
7. ⚠️ **No implementation sequencing** (expected in tasks.md, not plan.md)

---

## Detailed Audit Findings

### 1. Cross-References to Implementation Details

#### Current State
The plan.md lists documentation files in "Project Structure" but provides **no inline references** to guide implementers to specific sections.

#### Gaps Identified

| Plan Section | Missing Reference | Should Point To |
|--------------|-------------------|-----------------|
| **Technical Context** | .NET 10 version details | research.md "Version Targets" table |
| **Technical Context** | Tailwind CSS v4.x specifics | research.md "Tailwind CSS v4.x Integration Patterns" |
| **Project Structure - .template.config/** | template.json schema details | contracts/template.json + data-model.md "Template Manifest" |
| **Project Structure - package.json** | npm dependencies | contracts/package.json + research.md "package.json (v4.x Dependencies)" |
| **Project Structure - TailwindBuild.targets** | MSBuild integration | research.md "TailwindBuild.targets (MSBuild Integration)" |
| **Project Structure - Components/** | .NET 10 template structure | research.md ".NET 10 Blazor Template Structure (Verified)" |
| **Constitution Check - TDD** | Test strategy details | research.md "Template Testing Strategy (TDD Workflow)" |
| **Constitution Check - Integration Testing** | Contract test specs | data-model.md "Contract Files" section |

### 2. Missing Implementation Guidance

#### What's Missing
- **No guidance** on where to start implementation
- **No references** to Bootstrap → Tailwind conversion mapping
- **No pointers** to specific .NET 10 features that must be preserved
- **No links** to post-action configuration details

#### Recommended Additions

Add these cross-reference sections to plan.md:

```markdown
## Implementation Resources

### Core Technical References

**For .NET 10 Blazor Template Structure:**
- See [research.md: .NET 10 Blazor Template Structure (Verified)](#) for exact file structure from `dotnet new blazor`
- Critical: ReconnectModal.razor and NotFound.razor are NEW in .NET 10 (research.md lines 46-54, 69-74)
- Critical: CSS location changed to `wwwroot/app.css` NOT `wwwroot/css/app.css` (research.md line 56)

**For Tailwind CSS v4.x Integration:**
- See [research.md: Tailwind CSS v4.x Integration Patterns](#) for v4-specific syntax
- Critical: Use `@import "tailwindcss";` NOT `@tailwind` directives (research.md lines 132-143)
- Critical: Requires separate `@tailwindcss/cli` package (research.md lines 186-198)
- See [research.md: TailwindBuild.targets (MSBuild Integration)](#) for build configuration (research.md lines 279-295)

**For Template Configuration:**
- See [contracts/template.json](./contracts/template.json) for complete template manifest schema
- See [data-model.md: Template Manifest (template.json)](#) for schema explanation (data-model.md lines 9-73)
- See [research.md: Post-Actions for npm Installation](#) for platform-specific npm handling (research.md lines 323-369)

**For npm Package Configuration:**
- See [contracts/package.json](./contracts/package.json) for exact dependencies
- See [data-model.md: npm Package Manifest (package.json)](#) for schema details (data-model.md lines 205-246)
- Critical: Must include BOTH `tailwindcss` AND `@tailwindcss/cli` (research.md line 196-198)

**For Component Conversion:**
- See [research.md: Bootstrap to Tailwind CSS Mapping](#) for class conversions (research.md lines 417-442)
- See [research.md: Component Conversion Priority](#) for recommended order (research.md lines 531-559)
- Icon replacement strategy documented in research.md lines 439-442

**For Test-Driven Development:**
- See [research.md: Template Testing Strategy (TDD Workflow)](#) for 4-phase test approach (research.md lines 503-529)
- Phase 1: Template generation tests FIRST
- Phase 2: Build integration tests SECOND
- Phase 3: Component tests THIRD
- Phase 4: Parameter substitution tests FOURTH

### Quick Reference Cards

**Verify .NET 10 Template Compliance:**
1. ReconnectModal.razor (+ .css + .js) present? ✓ (NEW in .NET 10)
2. NotFound.razor present? ✓ (NEW in .NET 10)
3. CSS at wwwroot/app.css (NOT wwwroot/css/app.css)? ✓
4. @Assets[] syntax in App.razor? ✓
5. MapStaticAssets() in Program.cs? ✓
6. Bootstrap files REMOVED? ✓

**Verify Tailwind v4.x Compliance:**
1. @import "tailwindcss"; (NOT @tailwind directives)? ✓
2. Both tailwindcss AND @tailwindcss/cli in package.json? ✓
3. npx @tailwindcss/cli in TailwindBuild.targets? ✓
4. Tailwind classes use v4 syntax (shadow-xs not shadow-sm)? ✓

**Verify npm Post-Action Configuration:**
1. Platform-specific post-actions (OS == "Windows")? ✓
2. npm.cmd on Windows, npm on Unix? ✓
3. continueOnError: true? ✓
4. manualInstructions provided? ✓
```

### 3. Task Sequencing Clarity

#### Current State
Plan.md correctly **does not include detailed task sequencing** - this is reserved for tasks.md (created by `/speckit.tasks`).

#### Recommendation
When tasks.md is created, it should reference back to plan.md sections and forward to detail files:

**Example task structure:**
```markdown
## Task 1: Create Template Manifest (template.json)

**Reference**: plan.md "Project Structure - .template.config/"
**Schema**: contracts/template.json
**Documentation**: data-model.md "Template Manifest (template.json)" lines 9-73
**Implementation Guide**: research.md "Post-Actions for npm Installation" lines 323-369

**Steps**:
1. Copy schema from contracts/template.json
2. Configure symbols per data-model.md line 42-56
3. Add post-actions per research.md lines 330-368 (platform-specific npm)
4. Add constraints per research.md lines 407-415 (require .NET 10 SDK)
```

### 4. Data Model Cross-References

#### Gap Analysis

The plan.md Project Structure section lists files but doesn't link them to their entity definitions in data-model.md.

#### Recommended Enhancement

Add a mapping table in plan.md:

```markdown
## File-to-Entity Mapping

| Template File | Entity Definition | Schema Contract | Notes |
|--------------|-------------------|-----------------|-------|
| `.template.config/template.json` | data-model.md: Template Manifest | contracts/template.json | See lines 9-73 |
| `package.json` | data-model.md: npm Package Manifest | contracts/package.json | See lines 205-246 |
| `tailwind.config.js` | data-model.md: Tailwind Configuration | N/A (JS file) | See lines 77-114 |
| `TailwindBuild.targets` | data-model.md: MSBuild Targets File | N/A (XML file) | See lines 115-157 |
| `BlazorTailwind.csproj` | data-model.md: Project File | N/A (XML file) | See lines 158-198 |
| `Components/**/*.razor` | data-model.md: Blazor Component Files | N/A (Razor files) | See lines 199-215 |
| `Program.cs` | data-model.md: Program.cs | N/A (C# file) | See lines 216-233 |
| `Styles/app.css` | data-model.md: Tailwind Input CSS | N/A (CSS file) | See lines 234-249 |
```

### 5. Contract Specification References

#### Gap Analysis

The contracts/ folder is mentioned in Project Structure but there are **no references** explaining what these contracts validate or how to use them during implementation.

#### Recommended Enhancement

Add a "Contract Validation" section to plan.md:

```markdown
## Contract Validation

### Template Manifest Contract (contracts/template.json)

**Purpose**: Defines the complete schema for .template.config/template.json
**Usage**: Copy this file as starting point for template manifest
**Validation**: Template instantiation will fail if schema is invalid

**Key Sections**:
- `symbols`: Parameter definitions (lines 42-56)
- `postActions`: npm installation + restore (lines 330-368)
- `constraints`: Require .NET 10 SDK (lines 407-415)
- `sources.modifiers.exclude`: Exclude node_modules, bin, obj (lines 88-94)

**Cross-Reference**: See data-model.md lines 9-73 for entity explanation

### npm Package Contract (contracts/package.json)

**Purpose**: Defines exact npm dependencies for Tailwind CSS v4.x
**Usage**: Copy this file as starting point for package.json in template
**Validation**: npm install will fail if dependencies are incompatible

**Critical Requirements**:
- BOTH `tailwindcss` AND `@tailwindcss/cli` required (v4.x change)
- `autoprefixer` for CSS vendor prefixing
- Node.js >=16.0.0 (engines constraint)

**Cross-Reference**: See data-model.md lines 205-246 for schema details
**Cross-Reference**: See research.md lines 256-277 for v4.x dependencies
```

---

## Implementation Sequence (Recommended for tasks.md)

When creating tasks.md with `/speckit.tasks`, follow this sequence:

### Phase 0: Environment Setup
1. Create template folder structure (plan.md lines 79-113)
2. Reference: plan.md "Source Code (repository root)"

### Phase 1: Write Tests FIRST (TDD - Constitution Principle I)
1. Template generation tests (research.md lines 507-512)
2. Build integration tests (research.md lines 514-518)
3. Component tests (research.md lines 520-524)
4. Parameter substitution tests (research.md lines 526-529)

**Test Approval Gate**: User must approve tests before Phase 2

### Phase 2: Core Template Files
1. Create template.json (contracts/template.json + data-model.md lines 9-73)
2. Create dotnetcli.host.json (data-model.md lines 75-88)
3. Create ide.host.json (data-model.md lines 90-112)
4. Reference: research.md "Post-Actions for npm Installation" (lines 323-369)

### Phase 3: Blazor Project Files
1. Create BlazorTailwind.csproj (data-model.md lines 158-198)
2. Create Program.cs (data-model.md lines 216-233)
3. Create _Imports.razor
4. Create Routes.razor
5. Reference: research.md ".NET 10 Blazor Template Structure" (lines 27-94)

### Phase 4: Layout Components
1. Create App.razor (data-model.md lines 297-319 for App.razor example)
2. Create MainLayout.razor (convert Bootstrap to Tailwind per research.md lines 417-442)
3. Create NavMenu.razor (convert Bootstrap navbar per research.md line 428)
4. Create ReconnectModal.razor (NEW in .NET 10, see research.md lines 69-72)
5. Reference: research.md "Component Conversion Priority" (lines 531-559)

### Phase 5: Page Components
1. Create NotFound.razor (NEW in .NET 10, see research.md line 74)
2. Create Home.razor (convert Bootstrap card classes per research.md line 429)
3. Create Counter.razor (convert buttons per research.md line 427)
4. Create Weather.razor (convert tables)
5. Create Error.razor

### Phase 6: Tailwind CSS Configuration
1. Create Styles/app.css (data-model.md lines 234-249)
2. Create tailwind.config.js (data-model.md lines 77-114)
3. Create package.json (contracts/package.json)
4. Create TailwindBuild.targets (data-model.md lines 115-157)
5. Reference: research.md "Recommended File Structure for Blazor + Tailwind v4.x" (lines 220-234)

### Phase 7: Static Assets
1. Create favicon.png
2. Create appsettings.json
3. Create appsettings.Development.json
4. Create .gitignore (data-model.md lines 250-263)

### Phase 8: Test Execution (Verify All Tests Pass)
1. Run template generation tests
2. Run build integration tests
3. Run component tests
4. Run parameter substitution tests
5. All tests must pass before proceeding

### Phase 9: Template Package Project
1. Create templates/blazor-tailwind.csproj (NuGet template packaging project)
2. Reference: research.md "NuGet Packaging" (lines 102-125)

### Phase 10: Manual Verification
1. Install template locally: `dotnet new install ./templates/`
2. Create test project: `dotnet new blazor-tailwind -n TestApp`
3. Run npm install
4. Build project: `dotnet build`
5. Verify CSS compilation
6. Run project: `dotnet run`
7. Verify all pages render with Tailwind styles

---

## Recommendations for Enhancement

### 1. Add "Implementation Resources" Section to plan.md
Location: After "Project Structure", before "Complexity Tracking"
Content: Cross-references to research.md, data-model.md, contracts/ (as shown in section 2 above)

### 2. Add "File-to-Entity Mapping" Table to plan.md
Location: Within "Project Structure" section
Content: Map each template file to its data-model.md entity definition (as shown in section 4 above)

### 3. Add "Contract Validation" Section to plan.md
Location: After "File-to-Entity Mapping"
Content: Explain purpose and usage of contracts/ files (as shown in section 5 above)

### 4. Update Project Structure Comments
Current structure is accurate but add inline comments:

```text
templates/
└── blazor-tailwind/
    ├── .template.config/
    │   ├── template.json              # See contracts/template.json + data-model.md lines 9-73
    │   ├── dotnetcli.host.json        # See data-model.md lines 75-88
    │   └── ide.host.json              # See data-model.md lines 90-112
    ├── BlazorTailwind.csproj          # See data-model.md lines 158-198 (parameter substitution)
    ├── Program.cs                      # See research.md lines 91-93 for .NET 10 changes
    ├── Components/
    │   ├── App.razor                   # See research.md lines 297-319 for @Assets[] syntax
    │   ├── Layout/
    │   │   ├── ReconnectModal.razor    # NEW in .NET 10! See research.md lines 69-72
    │   │   └── ...
    │   └── Pages/
    │       ├── NotFound.razor          # NEW in .NET 10! See research.md line 74
    │       └── ...
    ├── Styles/
    │   └── app.css                     # See research.md lines 236-254 for v4.x @import syntax
    ├── tailwind.config.js              # See data-model.md lines 77-114
    ├── package.json                    # See contracts/package.json (v4.x requires @tailwindcss/cli!)
    └── TailwindBuild.targets           # See research.md lines 279-295 (npx @tailwindcss/cli)
```

### 5. Create Quick Reference Card in plan.md

Add a "Pre-Implementation Checklist" section:

```markdown
## Pre-Implementation Checklist

Before starting implementation, ensure you understand:

- [ ] .NET 10 template structure differences from .NET 9 (research.md lines 67-94)
- [ ] Tailwind CSS v4.x breaking changes from v3.x (research.md lines 127-218)
- [ ] Template manifest post-action platform differences (research.md lines 323-369)
- [ ] Bootstrap → Tailwind class mapping (research.md lines 417-442)
- [ ] TDD workflow: tests FIRST (research.md lines 503-529)
- [ ] All entity schemas in data-model.md
- [ ] All contracts in contracts/ folder

**Critical .NET 10 Changes:**
1. ReconnectModal.razor component (NEW)
2. NotFound.razor component (NEW)
3. CSS location: wwwroot/app.css (changed)
4. @Assets[] syntax in App.razor (NEW)
5. MapStaticAssets() in Program.cs (NEW)

**Critical Tailwind v4.x Changes:**
1. @import "tailwindcss"; (NOT @tailwind directives)
2. Separate @tailwindcss/cli package (required)
3. npx @tailwindcss/cli command (NOT npx tailwindcss)
4. Class renames: shadow-xs, outline-hidden, etc.
```

---

## Conclusion

The current plan.md is **structurally correct** but **lacks implementation guidance** through cross-references. Adding the recommended enhancements will:

1. ✅ Guide implementers to specific technical details
2. ✅ Reduce time spent searching for information
3. ✅ Ensure critical .NET 10 and Tailwind v4.x changes are not missed
4. ✅ Connect plan structure to detailed entity definitions
5. ✅ Clarify contract validation requirements

**Next Step**: Update plan.md with the recommended enhancements from sections 2, 4, and 5 above.

**Future Step**: When creating tasks.md with `/speckit.tasks`, use the implementation sequence from this audit (section "Implementation Sequence").
