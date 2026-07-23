# Research: Blazor Tailwind Template

**Branch**: `002-blazor-tailwind-template` | **Date**: 2026-01-04 (Updated with .NET 10 & Tailwind v4.x specifics) |  **Spec**: [spec.md](./spec.md)

## Decision

Use the .NET template engine with a single template package structure that mirrors Microsoft's official `dotnet new blazor` template (.NET 10), replacing Bootstrap with Tailwind CSS v4.x utilities while maintaining exact component structure and functionality.

## Rationale

1. **Familiarity**: Developers already know Microsoft's Blazor template structure - keeping this structure reduces learning curve
2. **Proven Pattern**: Microsoft's template represents battle-tested conventions and best practices for Blazor applications
3. **Minimal Changes**: Only CSS classes and build configuration need to change, reducing maintenance burden
4. **Template Engine Integration**: .NET template engine provides built-in parameter substitution, NuGet packaging, and IDE integration
5. **TailwindToolbox CLI Integration**: Leverage existing `tailwind-blazor setup` command to automate Tailwind configuration during template instantiation

## Version Targets

| Component | Version | Notes |
|-----------|---------|-------|
| **.NET SDK** | 10.0 (LTS) | Released November 2025, supported until November 10, 2028 |
| **Tailwind CSS** | 4.0.8+ | Stable release as of January 22, 2025 |
| **@tailwindcss/cli** | 4.0.8+ | Separate package for CLI in v4.x |
| **Template Engine** | .NET 10 SDK | Includes latest dotnet/templating features |
| **Node.js** | 16.0.0+ | Required for Tailwind CSS compilation |

## .NET 10 Blazor Template Structure (Verified)

### Actual File Structure from `dotnet new blazor`

```
MyBlazorApp/
├── MyBlazorApp.csproj                        # Project file
├── Program.cs                                # App entry point
├── Properties/
│   └── launchSettings.json                   # Debug launch profiles
├── Components/
│   ├── App.razor                             # Root component with <head>
│   ├── Routes.razor                          # Routing configuration
│   ├── _Imports.razor                        # Global using directives
│   ├── Layout/
│   │   ├── MainLayout.razor                  # Main layout component
│   │   ├── MainLayout.razor.css              # Scoped CSS
│   │   ├── NavMenu.razor                     # Navigation menu
│   │   ├── NavMenu.razor.css                 # Scoped CSS
│   │   ├── ReconnectModal.razor              # NEW in .NET 10
│   │   ├── ReconnectModal.razor.css          # NEW in .NET 10
│   │   └── ReconnectModal.razor.js           # NEW in .NET 10
│   └── Pages/
│       ├── Home.razor                        # Home page
│       ├── Counter.razor                     # Counter demo
│       ├── Weather.razor                     # Weather forecast
│       ├── NotFound.razor                    # NEW in .NET 10 (404 page)
│       └── Error.razor                       # Error page
├── wwwroot/
│   ├── app.css                               # Main CSS (NOT in css/ folder!)
│   ├── favicon.png                           # Site icon
│   └── lib/                                  # Client libraries
│       └── bootstrap/                        # Bootstrap 5.x (to be removed)
│           └── dist/
│               ├── css/                      # Bootstrap CSS files
│               └── js/                       # Bootstrap JS files
├── appsettings.json                          # App configuration
└── appsettings.Development.json              # Development configuration
```

### Critical Structure Changes from Previous .NET Versions

**NEW in .NET 10:**
1. **ReconnectModal Component**: Handles WebSocket reconnection UI (replaces inline reconnection logic)
   - Separate .css and .js files for CSP compliance
   - Better control over reconnection UX

2. **NotFound.razor**: Default 404 page (automatically rendered on 404 errors)

3. **CSS Location**: `wwwroot/app.css` (directly in wwwroot, NOT in css/ subfolder)

4. **Bootstrap Location**: `wwwroot/lib/bootstrap/dist/` (organized under lib/)

5. **@Assets[] Syntax**: New asset optimization feature in App.razor:
   ```razor
   <link rel="stylesheet" href="@Assets["lib/bootstrap/dist/css/bootstrap.min.css"]" />
   <link rel="stylesheet" href="@Assets["app.css"]" />
   ```

6. **New Components in App.razor**:
   - `<ResourcePreloader />` - Preloads resources
   - `<ImportMap />` - JavaScript import maps
   - `<HeadOutlet />` - Dynamic head content

7. **Program.cs Changes**:
   - `app.MapStaticAssets()` (NEW - replaces `app.UseStaticFiles()`)
   - `app.UseStatusCodePagesWithReExecute("/not-found", ...)` - Maps to NotFound.razor

### .NET 10 Default Rendering Mode

**IMPORTANT**: The default rendering mode is **Static Server-Side Rendering (Static SSR)**, NOT Interactive Server!

```csharp
// Default Program.cs configuration
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
```

- Root `App` component uses **Static SSR** by default
- Individual components opt-in to interactivity using `@rendermode` directive
- This is different from .NET 6/7 "Blazor Server" which was always interactive

### Available Rendering Modes in .NET 10

| Mode | Declaration | Execution | Network Required | Best For |
|------|------------|-----------|-----------------|----------|
| **Static** | No @rendermode | Server SSR | No (after load) | Content pages |
| **InteractiveServer** | `@rendermode InteractiveServer` | Server via SignalR | Always | Real-time apps |
| **InteractiveWebAssembly** | `@rendermode InteractiveWebAssembly` | Client WebAssembly | Initial load only | Offline-first |
| **InteractiveAuto** | `@rendermode InteractiveAuto` | Server → Client | Initial only | Most use cases |

## Tailwind CSS v4.x Integration Patterns

### Current Version: Tailwind CSS 4.0.8

Released January 22, 2025 as the first stable v4 release.

### Major Breaking Changes from v3.x to v4.x

#### 1. Import System (Most Critical Change)

**v3.x (OLD)**:
```css
@tailwind base;
@tailwind components;
@tailwind utilities;
```

**v4.x (NEW)**:
```css
@import "tailwindcss";
```

Single import statement replaces the three `@tailwind` directives.

#### 2. Configuration Format

**v3.x**: JavaScript configuration (`tailwind.config.js`)

**v4.x**: CSS-based configuration using `@theme` directive

**v4 CSS Configuration** (Recommended):
```css
@import "tailwindcss";

@theme {
  --color-primary: hsl(49, 100%, 7%);
  --color-secondary: rgb(59, 130, 246);
  --font-family-sans: ui-sans-serif, system-ui, sans-serif;
  --spacing-xs: 0.25rem;
  --radius-md: 0.375rem;
}

@source '../Components/**/*.razor';
@source '../Components/**/*.html';
@source '../**/*.cs';
```

**v4 JavaScript Configuration** (Still Supported):
```javascript
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Components/**/*.razor',
    './Components/**/*.html',
    './Components/**/*.cshtml'
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

**Decision for Template**: Use JavaScript configuration for familiarity, but document CSS configuration in README.

#### 3. CLI Package Change

**v3.x**: CLI bundled with `tailwindcss` package

**v4.x**: Separate `@tailwindcss/cli` package required

```bash
# v3.x
npm install -D tailwindcss

# v4.x (BOTH packages required)
npm install -D tailwindcss @tailwindcss/cli
```

#### 4. Class Renames

| v3.x Class | v4.x Class | Reason |
|-----------|-----------|---------|
| `outline-none` | `outline-hidden` | Accessibility clarity |
| `shadow-sm` | `shadow-xs` | Consistent sizing |
| `shadow` | `shadow-sm` | Scale adjustment |
| `ring` | `ring-3` | Explicit sizing |

Use automated upgrade tool: `npx @tailwindcss/upgrade`

#### 5. Browser Support

Tailwind CSS v4.x requires:
- **Safari 16.4+**
- **Chrome 111+**
- **Firefox 128+**

Requires support for `@property` CSS custom properties and `color-mix()` function.

### Recommended File Structure for Blazor + Tailwind v4.x

```
BlazorTailwind/
├── Styles/
│   └── app.css                       # Tailwind input file
├── wwwroot/
│   └── css/
│       └── app.css                   # Compiled output (generated during build)
├── tailwind.config.js                # Tailwind configuration (JS format)
├── package.json                      # npm dependencies
├── TailwindBuild.targets             # MSBuild integration
└── Components/
    └── App.razor                     # References @Assets["css/app.css"]
```

### Styles/app.css (Tailwind Input)

**v4.x Format**:
```css
@import "tailwindcss";

/* Custom styles using @layer */
@layer components {
  .btn-primary {
    @apply bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded;
  }
}

@layer utilities {
  .text-shadow {
    text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.1);
  }
}
```

### package.json (v4.x Dependencies)

```json
{
  "name": "blazortailwind",
  "version": "1.0.0",
  "private": true,
  "scripts": {
    "build:css": "@tailwindcss/cli -i ./Styles/app.css -o ./wwwroot/css/app.css --minify",
    "watch:css": "@tailwindcss/cli -i ./Styles/app.css -o ./wwwroot/css/app.css --watch"
  },
  "devDependencies": {
    "tailwindcss": "^4.0.8",
    "@tailwindcss/cli": "^4.0.8",
    "autoprefixer": "^10.4.16"
  },
  "engines": {
    "node": ">=16.0.0",
    "npm": ">=8.0.0"
  }
}
```

### TailwindBuild.targets (MSBuild Integration)

```xml
<Project>
  <Target Name="BuildTailwindCSS" BeforeTargets="Build">
    <!-- Development build (unminified for debugging) -->
    <Exec Command="npx @tailwindcss/cli -i Styles/app.css -o wwwroot/css/app.css"
          Condition="'$(Configuration)' == 'Debug'"
          WorkingDirectory="$(ProjectDir)" />

    <!-- Production build (minified) -->
    <Exec Command="npx @tailwindcss/cli -i Styles/app.css -o wwwroot/css/app.css --minify"
          Condition="'$(Configuration)' == 'Release'"
          WorkingDirectory="$(ProjectDir)" />
  </Target>
</Project>
```

### Components/App.razor (CSS Reference)

```razor
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <ResourcePreloader />
    <link rel="stylesheet" href="@Assets["css/app.css"]" />
    <link rel="stylesheet" href="@Assets["BlazorTailwind.styles.css"]" />
    <ImportMap />
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet />
</head>
<body>
    <Routes />
    <ReconnectModal />
    <script src="@Assets["_framework/blazor.web.js"]"></script>
</body>
</html>
```

## .NET Template Best Practices (.NET 10)

### Post-Actions for npm Installation

**Critical Issue**: npm is installed as `npm.cmd` on Windows, not `npm.exe`. Direct script execution fails.

**Solution: Platform-Specific Post-Actions**

```json
"postActions": [
  {
    "actionId": "210D431B-A78B-4D2F-B762-4ED3E3EA9025",
    "description": "Restore NuGet packages",
    "continueOnError": true,
    "condition": "(!skipRestore)",
    "manualInstructions": [
      { "text": "Run 'dotnet restore'" }
    ]
  },
  {
    "actionId": "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2",
    "description": "Install npm packages (Windows)",
    "condition": "(OS == \"Windows\")",
    "args": {
      "executable": "npm.cmd",
      "args": "install"
    },
    "redirectStandardOutput": false,
    "continueOnError": true,
    "manualInstructions": [
      { "text": "Run 'npm install' to install Tailwind CSS packages" }
    ]
  },
  {
    "actionId": "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2",
    "description": "Install npm packages (Unix)",
    "condition": "(OS != \"Windows\")",
    "args": {
      "executable": "npm",
      "args": "install"
    },
    "redirectStandardOutput": false,
    "continueOnError": true,
    "manualInstructions": [
      { "text": "Run 'npm install' to install Tailwind CSS packages" }
    ]
  }
]
```

### Parameter Substitution (.NET 10 Features)

**Conditional Requirements** (Available since .NET SDK 7.0.100):

```json
"symbols": {
  "EnableTailwind": {
    "type": "parameter",
    "datatype": "bool",
    "defaultValue": "true",
    "description": "Include Tailwind CSS configuration",
    "isRequired": "(TargetFramework == \"net10.0\")"
  }
}
```

**Derived Symbols for Case Transformations**:

```json
"symbols": {
  "ProjectNameLowerCase": {
    "type": "derived",
    "valueSource": "name",
    "valueTransform": "lower",
    "replaces": "blazortailwind"
  },
  "SafeProjectName": {
    "type": "derived",
    "valueSource": "name",
    "valueTransform": "identity",
    "replaces": "BlazorTailwind_Safe"
  }
}
```

### Constraints (Require .NET 10 SDK)

```json
"constraints": {
  "requires_dotnet10": {
    "type": "host-version",
    "args": "10.0.0"
  }
}
```

## Bootstrap to Tailwind CSS Mapping

Common Bootstrap 5 classes to Tailwind v4.x equivalents:

| Bootstrap 5 Class | Tailwind v4.x | Notes |
|------------------|--------------|-------|
| `container` | `container mx-auto` | Add mx-auto for centering |
| `container-fluid` | `w-full px-4` | Full width with padding |
| `row` | `flex flex-wrap` | Use flexbox utilities |
| `col-md-6` | `w-full md:w-1/2` | Responsive width |
| `btn btn-primary` | `bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded` | Combine utilities |
| `navbar` | `flex items-center justify-between p-4` | Custom combination |
| `card` | `bg-white rounded-lg shadow-md p-6` | Custom combination |
| `nav-link` | `text-blue-600 hover:text-blue-800` | Link styling |
| `bi bi-house-door-fill` | Use SVG or Heroicons | Bootstrap Icons not included |
| `text-center` | `text-center` | Same name |
| `mt-3` | `mt-3` | Similar spacing scale |
| `d-flex` | `flex` | Simpler syntax |
| `justify-content-between` | `justify-between` | Shorter syntax |
| `flex-column` | `flex-col` | Column direction |
| `gap-3` | `gap-3` | Same spacing scale |

**Icon Replacement**: Bootstrap Icons (`bi bi-*`) should be replaced with:
- Heroicons (recommended for Tailwind projects)
- SVG icons directly in markup
- Or continue using Bootstrap Icons via CDN if desired

## Alternatives Considered

### Alternative 1: Use Tailwind CSS v3.x Instead of v4.x

Keep using v3.x for compatibility with more browsers.

**Rejected because:**
- v4.x is stable as of January 2025
- v4.x offers significant performance improvements (smaller bundle size)
- v4.x provides better developer experience with CSS-based configuration
- Browser requirements (Safari 16.4+) are reasonable for 2025/2026
- Future-proofing: v4.x is the current direction of Tailwind

### Alternative 2: CSS-Based Configuration Only (No tailwind.config.js)

Use only v4.x `@theme` directive in CSS, no JavaScript config.

**Rejected because:**
- JavaScript config is more familiar to existing Tailwind users
- Blazor developers already work with .csproj and MSBuild (XML/JS is familiar)
- More tooling support for `tailwind.config.js`
- Can document CSS alternative in README without forcing it

**Decision**: Ship with `tailwind.config.js`, document `@theme` alternative.

### Alternative 3: Include Bootstrap Icons for Icon Compatibility

Keep Bootstrap Icons library to make conversion easier.

**Rejected because:**
- Increases bundle size
- Mixed icon systems is confusing
- Heroicons are designed for Tailwind projects
- Clean break is better than gradual migration
- Users can add Bootstrap Icons if needed

### Alternative 4: Use dotnet-generated Blazor Icons Instead of Heroicons

Use Blazor-specific icon solutions.

**Rejected because:**
- Tailwind community standardizes on Heroicons
- SVG-in-markup is simple and flexible
- No additional dependencies needed
- Users can choose their own icon library

## Implementation Notes

### Critical Dependencies (Updated for .NET 10 & Tailwind v4.x)

| Dependency | Version | Required At | Purpose |
|-----------|---------|-------------|---------|
| **.NET 10 SDK** | 10.0+ | Build time | Template target framework |
| **Node.js** | 16.0.0+ | Build time | Tailwind CLI execution |
| **npm** | 8.0.0+ | Build time | Package management |
| **tailwindcss** | 4.0.8+ | Build time | Core Tailwind CSS framework |
| **@tailwindcss/cli** | 4.0.8+ | Build time | v4.x CLI tool |
| **autoprefixer** | 10.4.16+ | Build time | CSS vendor prefixing |

### Template Testing Strategy (TDD Workflow)

Following TDD principle from constitution:

#### Phase 1: Template Generation Tests (Write FIRST)
1. Test that `dotnet new blazor-tailwind` creates expected files (including .NET 10-specific files)
2. Test that ReconnectModal.razor is present
3. Test that NotFound.razor is present
4. Test that Bootstrap files are NOT present
5. Test that Tailwind config files ARE present

#### Phase 2: Build Integration Tests (Write FIRST)
1. Test that `dotnet build` succeeds
2. Test that `wwwroot/css/app.css` is generated
3. Test that generated CSS contains Tailwind utilities
4. Test that MSBuild integration triggers Tailwind compilation

#### Phase 3: Component Tests (Write FIRST)
1. Test that all components render without errors
2. Test that Tailwind classes are applied correctly
3. Test that scoped CSS files work with Tailwind
4. Test that @Assets[] syntax works correctly

#### Phase 4: Parameter Substitution Tests (Write FIRST)
1. Test project name substitution in .csproj
2. Test namespace substitution in .razor files
3. Test package.json name substitution

### Component Conversion Priority

1. **App.razor** (Root component)
   - Remove Bootstrap CDN references
   - Add Tailwind CSS reference using @Assets[]
   - Add ReconnectModal component reference

2. **Layout Components** (MainLayout, NavMenu, ReconnectModal)
   - Convert Bootstrap grid to Tailwind flexbox
   - Replace Bootstrap navbar classes
   - Update Bootstrap Icons to Heroicons or SVG

3. **NotFound.razor** (NEW in .NET 10)
   - Create 404 page with Tailwind styling

4. **Home.razor** - Most visible page
   - Replace Bootstrap card classes
   - Use Tailwind typography utilities

5. **Counter.razor** - Simple interactive component
   - Replace button classes
   - Minimal Tailwind utilities

6. **Weather.razor** - Table/list styling
   - Replace table classes with Tailwind
   - Responsive table design

7. **Error.razor** - Edge case styling
   - Error message styling with Tailwind

### Scoped CSS Strategy

- **Minimize scoped CSS**: Prefer Tailwind utilities in .razor files
- **Keep existing scoped CSS structure**: Maintain `.razor.css` files for component-specific styles that can't be achieved with utilities
- **Document when to use scoped CSS**: Only for complex animations, pseudo-elements, or browser-specific hacks

### Version Strategy

- **Template version**: 1.0.0 (first release)
- **Tailwind CSS version**: ^4.0.8 (allow patch updates)
- **.NET version**: net10.0 (LTS release)
- **Semantic versioning**: Follow semver for template updates
  - Major: Breaking changes to generated project structure
  - Minor: New features, additional components
  - Patch: Bug fixes, documentation updates

### Documentation Requirements

1. **README.md in Generated Project**:
   - Explain Tailwind CSS v4.x usage
   - Document npm scripts (build:css, watch:css)
   - Explain @import vs @tailwind directives
   - List .NET 10 specific features

2. **Comments in tailwind.config.js**:
   - Explain content paths for Blazor
   - Document theme customization
   - Note v4.x compatibility

3. **Comments in TailwindBuild.targets**:
   - Explain MSBuild integration
   - Document Debug vs Release builds
   - Note npx @tailwindcss/cli usage

4. **Post-Installation Message**:
   - Next steps after template creation
   - How to run development server with watch mode
   - Link to Tailwind CSS and Blazor documentation

## Research Sources

1. **.NET 10 Official Documentation**: [What's new in .NET 10](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview)
2. **Blazor .NET 10 Documentation**: [ASP.NET Core Blazor render modes](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-modes?view=aspnetcore-10.0)
3. **Tailwind CSS v4.0 Release**: [Tailwind CSS v4.0 Blog](https://tailwindcss.com/blog/tailwindcss-v4)
4. **Tailwind CSS v4 Upgrade Guide**: [Upgrade Guide](https://tailwindcss.com/docs/upgrade-guide)
5. **.NET Template Engine**: [dotnet/templating Wiki](https://github.com/dotnet/templating/wiki)
6. **Context7 Documentation Queries**: ASP.NET Core Blazor documentation for .NET 10
7. **Actual .NET 10 Blazor Template**: Created test project with `dotnet new blazor` to verify structure

## Open Questions

*None - specification is complete with verified .NET 10 and Tailwind CSS v4.x details.*
