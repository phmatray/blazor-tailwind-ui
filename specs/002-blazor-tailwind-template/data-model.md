# Data Model: Blazor Tailwind Template

**Branch**: `002-blazor-tailwind-template` | **Date**: 2026-01-04 | **Spec**: [spec.md](./spec.md)

## Overview

This document defines the key entities and their relationships in the Blazor Tailwind Template feature. Since this is a .NET template (not a runtime application), the "data model" consists of configuration files, template metadata, and generated project structure.

## Entity Definitions

### 1. Template Manifest (template.json)

**Purpose**: Defines template metadata, parameters, and behavior for the .NET template engine.

**Location**: `templates/blazor-tailwind/.template.config/template.json`

**Schema**:
```typescript
interface TemplateManifest {
  $schema: string;                    // JSON schema URL
  author: string;                     // Template author
  classifications: string[];          // Categories (Web, Blazor, Tailwind)
  identity: string;                   // Unique template ID
  name: string;                       // Display name
  shortName: string;                  // Command-line name (blazor-tailwind)
  sourceName: string;                 // Substitution token (BlazorTailwind)
  preferNameDirectory: boolean;       // Use directory name as default project name
  tags: {
    language: string;                 // C#
    type: string;                     // project
  };
  symbols: Record<string, Symbol>;    // Template parameters
  sources: Source[];                  // File inclusion/exclusion rules
  postActions: PostAction[];          // Actions to run after template generation
}

interface Symbol {
  type: "parameter" | "computed" | "generated";
  description: string;
  datatype?: "string" | "bool" | "choice";
  defaultValue?: string | boolean;
  replaces?: string;                  // Token to replace in files
  choices?: Choice[];                 // For choice parameters
}

interface Choice {
  choice: string;
  description: string;
}

interface Source {
  modifiers: Modifier[];
}

interface Modifier {
  exclude: string[];                  // Glob patterns to exclude
}

interface PostAction {
  actionId: string;                   // Standard action ID (restore, etc.)
  description: string;
  manualInstructions?: ManualInstruction[];
  continueOnError: boolean;
}

interface ManualInstruction {
  text: string;
}
```

**Key Properties**:
- `shortName`: "blazor-tailwind" (used in `dotnet new blazor-tailwind`)
- `sourceName`: "BlazorTailwind" (replaced with user's project name)
- `symbols.Framework`: Target framework parameter (.NET 10.0)
- `sources[0].modifiers[0].exclude`: Exclude bin/, obj/, node_modules/
- `postActions[0]`: Manual instruction to run `tailwind-blazor setup`

---

### 2. CLI Host Configuration (dotnetcli.host.json)

**Purpose**: Configures template behavior in dotnet CLI.

**Location**: `templates/blazor-tailwind/.template.config/dotnetcli.host.json`

**Schema**:
```typescript
interface DotNetCliHostConfig {
  $schema: string;
  symbolInfo: Record<string, SymbolInfo>;
}

interface SymbolInfo {
  longName: string;                   // --framework
  shortName?: string;                 // -f
}
```

**Key Properties**:
- `symbolInfo.Framework.longName`: "framework" (CLI parameter name)
- `symbolInfo.Framework.shortName`: "f" (short alias)

---

### 3. IDE Host Configuration (ide.host.json)

**Purpose**: Configures template behavior in Visual Studio and other IDEs.

**Location**: `templates/blazor-tailwind/.template.config/ide.host.json`

**Schema**:
```typescript
interface IdeHostConfig {
  $schema: string;
  icon: string;                       // Icon file name
  symbolInfo: IdeSymbolInfo[];
}

interface IdeSymbolInfo {
  id: string;                         // Symbol name
  name: {
    text: string;                     // Display name
  };
  isVisible: boolean;
}
```

**Key Properties**:
- `icon`: "icon.png" (template icon for IDE)
- `symbolInfo[0].isVisible`: Controls parameter visibility in IDE wizard

---

### 4. Tailwind Configuration (tailwind.config.js)

**Purpose**: Configures Tailwind CSS content scanning, theme, and plugins.

**Location**: `templates/blazor-tailwind/tailwind.config.js`

**Schema** (JavaScript/CommonJS):
```typescript
interface TailwindConfig {
  content: string[];                  // Paths to scan for CSS classes
  theme: {
    extend: Record<string, any>;      // Theme extensions
  };
  plugins: any[];                     // Tailwind plugins
}
```

**Key Properties**:
- `content`: `['./Components/**/*.razor', './Components/**/*.html']`
- `theme.extend`: Empty object (use Tailwind defaults)
- `plugins`: Empty array (no plugins by default)

**Parameter Substitution**: None - this file is static in template.

---

### 5. npm Package Manifest (package.json)

**Purpose**: Defines npm dependencies and scripts for Tailwind CSS.

**Location**: `templates/blazor-tailwind/package.json`

**Schema**:
```typescript
interface PackageJson {
  name: string;                       // Package name (lowercase project name)
  version: string;                    // Always "1.0.0"
  private: boolean;                   // Always true
  scripts: Record<string, string>;    // npm scripts
  devDependencies: Record<string, string>; // npm packages
}
```

**Key Properties**:
- `name`: Project name (lowercase, no spaces) - SUBSTITUTED with `sourceName`
- `scripts.build:css`: "@tailwindcss/cli -i ./Styles/app.css -o ./wwwroot/css/app.css --minify"
- `devDependencies.tailwindcss`: "^4.0.0"
- `devDependencies.@tailwindcss/cli`: "^4.0.0"
- `devDependencies.autoprefixer`: "^10.4.16"

**Parameter Substitution**:
- Replace `BlazorTailwind` with user's project name in `name` field

---

### 6. MSBuild Targets File (TailwindBuild.targets)

**Purpose**: Integrates Tailwind CSS compilation into MSBuild process.

**Location**: `templates/blazor-tailwind/TailwindBuild.targets`

**Schema** (MSBuild XML):
```xml
<Project>
  <Target Name="BuildTailwindCSS" BeforeTargets="Build">
    <Exec Command="..." WorkingDirectory="$(ProjectDir)" />
  </Target>
</Project>
```

**Key Properties**:
- `Target.Name`: "BuildTailwindCSS"
- `Target.BeforeTargets`: "Build" (runs before main build)
- `Exec.Command` (Debug): `npx @tailwindcss/cli -i Styles/app.css -o wwwroot/css/app.css`
- `Exec.Command` (Release): Same with `--minify` flag

**Parameter Substitution**: None - paths are relative and don't need project name.

---

### 7. Project File (BlazorTailwind.csproj)

**Purpose**: Defines .NET project configuration and references MSBuild targets.

**Location**: `templates/blazor-tailwind/BlazorTailwind.csproj`

**Schema** (MSBuild XML):
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <RootNamespace>BlazorTailwind</RootNamespace>
    <AssemblyName>BlazorTailwind</AssemblyName>
  </PropertyGroup>

  <Import Project="TailwindBuild.targets" />
</Project>
```

**Key Properties**:
- `Sdk`: "Microsoft.NET.Sdk.Web" (Blazor web app SDK)
- `TargetFramework`: "net10.0"
- `Nullable`: "enable" (nullable reference types - constitution requirement)
- `RootNamespace`: Project name - SUBSTITUTED
- `AssemblyName`: Project name - SUBSTITUTED
- `Import Project`: "TailwindBuild.targets" (MSBuild integration)

**Parameter Substitution**:
- File name: `BlazorTailwind.csproj` → `{ProjectName}.csproj`
- `RootNamespace`: `BlazorTailwind` → `{ProjectName}`
- `AssemblyName`: `BlazorTailwind` → `{ProjectName}`

---

### 8. Blazor Component Files (*.razor)

**Purpose**: UI components with Tailwind CSS utility classes.

**Locations**:
- `templates/blazor-tailwind/Components/App.razor`
- `templates/blazor-tailwind/Components/Routes.razor`
- `templates/blazor-tailwind/Components/Layout/MainLayout.razor`
- `templates/blazor-tailwind/Components/Layout/NavMenu.razor`
- `templates/blazor-tailwind/Components/Pages/Home.razor`
- `templates/blazor-tailwind/Components/Pages/Counter.razor`
- `templates/blazor-tailwind/Components/Pages/Weather.razor`
- `templates/blazor-tailwind/Components/Pages/Error.razor`

**Schema** (Razor syntax):
```razor
@page "/route"
@* Razor directives *@

<PageTitle>Title</PageTitle>

<div class="tailwind-utility-classes">
    <!-- HTML with Tailwind CSS -->
</div>

@code {
    // C# code
}
```

**Parameter Substitution**:
- Namespace declarations: `@namespace BlazorTailwind.Components` → `@namespace {ProjectName}.Components`

---

### 9. Program.cs (Application Entry Point)

**Purpose**: Configures and starts the Blazor application.

**Location**: `templates/blazor-tailwind/Program.cs`

**Schema** (C# code):
```csharp
using BlazorTailwind.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure pipeline
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
```

**Parameter Substitution**:
- Using directive: `using BlazorTailwind.Components;` → `using {ProjectName}.Components;`

---

### 10. Tailwind Input CSS (Styles/app.css)

**Purpose**: Tailwind CSS input file with @tailwind directives.

**Location**: `templates/blazor-tailwind/Styles/app.css`

**Schema** (CSS):
```css
@tailwind base;
@tailwind components;
@tailwind utilities;

/* Custom styles (if needed) */
```

**Parameter Substitution**: None - file is static.

---

### 11. .gitignore

**Purpose**: Excludes build artifacts and dependencies from Git.

**Location**: `templates/blazor-tailwind/.gitignore`

**Key Entries**:
```
bin/
obj/
node_modules/
wwwroot/css/app.css
.vs/
*.user
```

**Parameter Substitution**: None - file is static.

---

## Entity Relationships

```
Template Manifest (template.json)
├── Defines → Template Parameters (sourceName, Framework)
├── References → Source Exclusions (exclude node_modules/, bin/, obj/)
└── Includes → Post-Actions (manual instruction to run tailwind-blazor setup)

CLI Host Config (dotnetcli.host.json)
└── Maps → Template Parameters to CLI arguments (--framework, -f)

IDE Host Config (ide.host.json)
└── Maps → Template Parameters to IDE wizard controls

Project File (BlazorTailwind.csproj)
├── Imports → MSBuild Targets (TailwindBuild.targets)
├── Defines → Root Namespace (substituted with project name)
└── Defines → Assembly Name (substituted with project name)

MSBuild Targets (TailwindBuild.targets)
├── Reads → Tailwind Input CSS (Styles/app.css)
├── Executes → Tailwind CLI (@tailwindcss/cli)
└── Outputs → Compiled CSS (wwwroot/css/app.css)

Tailwind Config (tailwind.config.js)
└── Scans → Blazor Components (Components/**/*.razor)

npm Package Manifest (package.json)
├── Defines → npm Scripts (build:css)
└── Specifies → Dependencies (tailwindcss, @tailwindcss/cli, autoprefixer)

Blazor Components (*.razor)
├── Use → Tailwind CSS Classes (text-center, bg-blue-500, etc.)
├── Reference → Root Namespace (substituted with project name)
└── Rendered By → Program.cs (app entry point)

Program.cs
├── References → Root Namespace (substituted with project name)
└── Registers → Blazor Components (App.razor)

.gitignore
├── Excludes → node_modules/ (npm packages)
├── Excludes → wwwroot/css/app.css (generated CSS)
└── Excludes → bin/, obj/ (build artifacts)
```

## Parameter Substitution Map

| Source Value | Target Value | Applied To |
|--------------|--------------|------------|
| `BlazorTailwind` | User's project name | File names (*.csproj), namespaces, assembly names |
| `blazor-tailwind` | User's lowercase project name | package.json name field |
| `net10.0` | Selected framework | TargetFramework in .csproj |

## File Generation Flow

1. **User runs**: `dotnet new blazor-tailwind -n MyApp`
2. **Template engine**:
   - Copies all files from `templates/blazor-tailwind/` to output directory
   - Excludes files matching patterns in `sources[0].modifiers[0].exclude`
   - Renames `BlazorTailwind.csproj` to `MyApp.csproj`
   - Replaces all instances of `BlazorTailwind` with `MyApp` in file contents
   - Replaces `net10.0` with selected framework (if --framework parameter provided)
3. **Post-actions**:
   - Displays manual instruction to run `tailwind-blazor setup`
4. **User runs**: `tailwind-blazor setup` (optional but recommended)
   - Validates/updates tailwind.config.js
   - Validates/updates package.json
   - Installs npm packages
   - Validates TailwindBuild.targets
5. **User runs**: `dotnet build`
   - MSBuild imports TailwindBuild.targets
   - TailwindBuild.targets executes `npx @tailwindcss/cli` command
   - Tailwind CLI reads Styles/app.css
   - Tailwind CLI scans Components/**/*.razor for CSS classes
   - Tailwind CLI generates wwwroot/css/app.css

## Contract Files

See [contracts/](./contracts/) folder for detailed JSON schemas:
- `contracts/template.json` - Complete template.json schema with all properties
- `contracts/package.json` - npm package.json schema for Tailwind dependencies

## Notes

- **No Database**: This is a template feature, not a runtime application - no database entities
- **No State Management**: Template files are static - no runtime state
- **Parameter Substitution Only**: The only "transformation" is string replacement by .NET template engine
- **Build-Time Generation**: CSS is generated at build time via MSBuild, not runtime
