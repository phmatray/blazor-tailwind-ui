# Research: .NET Template Package for Blazor + Tailwind CSS

**Created**: 2026-01-04
**Purpose**: Document best practices for creating a .NET template package that generates Blazor web applications with Tailwind CSS pre-configured

## Executive Summary

This research covers the creation of a .NET template package for the Blazor Tailwind Template feature. The template will work with `dotnet new` commands, maintain the exact structure of Microsoft's `dotnet new blazor` template, and include pre-configured Tailwind CSS with MSBuild integration.

**Key Decisions**:
- Use standard .NET template engine with `.template.config` folder structure
- Package as NuGet template package with `PackageType=Template`
- Exclude `node_modules` from template, generate via post-action or post-setup
- Integrate with existing TailwindToolbox CLI for post-generation setup
- Follow .NET 10 Blazor Web App template structure

---

## 1. .NET Template Engine

### Decision: Standard .NET Template Engine with .template.config

**Rationale**:
- Native integration with `dotnet new` CLI
- Widely adopted, well-documented approach
- No additional tooling required for users
- Direct support from Microsoft and community

**Implementation Approach**:

#### Required .template.config Structure

```
BlazorTailwindTemplate/
├── .template.config/
│   ├── template.json          # Core template configuration
│   └── dotnetcli.host.json    # CLI-specific settings (optional)
├── Company.BlazorApp1/        # Template content (sourceName will replace this)
│   ├── Company.BlazorApp1.csproj
│   ├── Program.cs
│   ├── Components/
│   ├── wwwroot/
│   │   └── css/
│   │       └── app.css        # Pre-compiled Tailwind CSS
│   ├── Styles/
│   │   └── app.css            # Tailwind input CSS
│   ├── tailwind.config.js
│   ├── package.json
│   └── TailwindBuild.targets
└── README.md                   # Template documentation
```

#### Required template.json Properties

Based on official documentation, the minimum required configuration:

```json
{
  "$schema": "http://json.schemastore.org/template",
  "author": "TailwindToolbox",
  "classifications": [ "Web", "Blazor", "Tailwind CSS" ],
  "identity": "TailwindToolbox.BlazorTemplate",
  "name": "Blazor Web App with Tailwind CSS",
  "shortName": "blazor-tw",
  "tags": {
    "language": "C#",
    "type": "project"
  },
  "sourceName": "Company.BlazorApp1",
  "preferNameDirectory": true,
  "defaultName": "BlazorApp1"
}
```

**Property Explanations**:
- `$schema`: References JSON schema for IntelliSense support
- `author`: Template creator name (displayed in `dotnet new list`)
- `classifications`: Searchable tags for categorization
- `identity`: Unique identifier for the template (used for uninstall)
- `name`: Full human-readable template name
- `shortName`: Command to invoke template (`dotnet new blazor-tw`)
- `tags.language`: Must be set for Visual Studio integration
- `tags.type`: Must be "project" for Visual Studio New Project Dialog
- `sourceName`: String to be replaced throughout template (in code and filenames)
- `preferNameDirectory`: Simplifies usage by matching project name to directory name
- `defaultName`: Fallback name when user doesn't specify `-n` parameter

### Parameter Substitution Syntax

**Best Practices for sourceName**:

Choose a distinct value that produces different forms under transformations. Good example: `Company.BlazorApp1` generates:
- **Namespace form**: `Company.BlazorApp1`
- **Class form**: `Company__BlazorApp1` (dots become underscores)
- **Identity form**: `Company.BlazorApp1`

**Bad example**: `blazorapp` (no transformations, less distinct)

**Symbol Definition for Custom Parameters**:

```json
{
  "symbols": {
    "Framework": {
      "type": "parameter",
      "description": "The target framework for the project.",
      "datatype": "choice",
      "choices": [
        {
          "choice": "net10.0",
          "description": "Target net10.0"
        }
      ],
      "replaces": "net10.0",
      "defaultValue": "net10.0"
    },
    "InteractivityPlatform": {
      "type": "parameter",
      "description": "Chooses which interactive render mode to use",
      "datatype": "choice",
      "choices": [
        {
          "choice": "Server",
          "description": "Interactivity using Blazor Server."
        },
        {
          "choice": "WebAssembly",
          "description": "Interactivity using Blazor WebAssembly."
        },
        {
          "choice": "None",
          "description": "No interactivity (static SSR only)."
        }
      ],
      "defaultValue": "Server"
    },
    "HostIdentifier": {
      "type": "bind",
      "binding": "HostIdentifier"
    }
  }
}
```

**Symbol Features**:
- `replaces`: Text in template files to replace with parameter value
- `fileRename`: Portion of template filenames to replace with symbol value
- `type`: "parameter" (user-supplied), "computed" (calculated), "generated" (built-in generator), "bind" (from host)
- `datatype`: "text", "bool", "choice", etc.

### Post-Action Configuration

**Decision**: Use post-actions to run TailwindToolbox CLI after template generation

**Configuration**:

```json
{
  "postActions": [
    {
      "actionId": "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2",
      "description": "Run TailwindToolbox setup to initialize Tailwind CSS",
      "manualInstructions": [
        {
          "text": "Run 'tailwind-blazor setup' to complete Tailwind CSS configuration"
        }
      ],
      "continueOnError": true,
      "args": {
        "executable": "tailwind-blazor",
        "args": "setup --project-dir ."
      }
    }
  ]
}
```

**Important Considerations**:
- Post-action scripts require user confirmation ("Do you want to run this action (Y|N)?")
- In Visual Studio, post-action prompts may not appear and scripts won't run
- Set `continueOnError: true` to prevent template failure if CLI not installed
- Provide `manualInstructions` as fallback for when post-action doesn't execute

**Alternative Approach**: Pre-configured template with compiled CSS

Since post-actions have limitations, consider including pre-compiled Tailwind CSS in the template package and documenting manual setup steps.

### Conditional Processing

Use MSBuild-style conditional processing for project files:

```xml
<!--#if (InteractivityPlatform == "WebAssembly")-->
<ItemGroup>
  <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.Server" Version="10.0.0" />
</ItemGroup>
<!--#endif-->
```

**Syntax**:
- MSBuild files: `<!--#if (condition) -->` and `<!--#endif -->`
- C# files: `//#if (condition)` and `//#endif`
- Razor files: `@*#if (condition)*@` and `@*#endif*@`

---

## 2. Template Packaging

### Decision: NuGet Package with PackageType=Template

**Rationale**:
- Standard distribution mechanism for .NET templates
- Works with `dotnet new install` command
- Supports versioning and updates
- Can be published to public or private NuGet feeds

**Implementation**:

Create a separate packaging project (`.csproj`) that packages the template:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <PackageType>Template</PackageType>
    <PackageVersion>1.0.0</PackageVersion>
    <PackageId>TailwindToolbox.Templates.Blazor</PackageId>

    <Title>Blazor Web App with Tailwind CSS Template</Title>
    <Authors>TailwindToolbox</Authors>
    <Description>Project template for creating a Blazor Web App with Tailwind CSS pre-configured, replacing Bootstrap</Description>
    <PackageTags>dotnet-new;templates;blazor;tailwindcss</PackageTags>
    <PackageProjectUrl>https://github.com/yourusername/TailwindToolbox</PackageProjectUrl>

    <IncludeContentInPack>true</IncludeContentInPack>
    <IncludeBuildOutput>false</IncludeBuildOutput>
    <ContentTargetFolders>content</ContentTargetFolders>
    <NoWarn>$(NoWarn);NU5128</NoWarn>
    <NoDefaultExcludes>true</NoDefaultExcludes>
  </PropertyGroup>

  <ItemGroup>
    <Content Include="templates\**\*" Exclude="templates\**\bin\**;templates\**\obj\**;templates\**\node_modules\**" />
    <Compile Remove="**\*" />
  </ItemGroup>
</Project>
```

**Key Properties**:
- `PackageType=Template`: **REQUIRED** - Identifies package as template for `dotnet new`
- `IncludeContentInPack=true`: Include template files in NuGet package
- `IncludeBuildOutput=false`: Exclude compiler binaries
- `ContentTargetFolders=content`: Store files in content folder (required by templating engine)
- `NoDefaultExcludes=true`: Override default exclusions to have fine control

**File Organization**:

```
TailwindToolbox.Templates/
├── TailwindToolbox.Templates.csproj    # Packaging project
└── templates/
    └── blazor-tailwind/                # Individual template
        ├── .template.config/
        │   └── template.json
        └── Company.BlazorApp1/         # Template content
```

**Excluding Files**:

Use `Exclude` attribute in `Content` ItemGroup to prevent including:
- `bin/`, `obj/` (build artifacts)
- `node_modules/` (npm packages)
- `.vs/`, `.vscode/` (IDE folders)
- User-specific files

**Building the Package**:

```bash
dotnet pack TailwindToolbox.Templates.csproj -c Release -o ./artifacts
```

**Installing the Template**:

```bash
# From local package
dotnet new install ./artifacts/TailwindToolbox.Templates.Blazor.1.0.0.nupkg

# From NuGet feed
dotnet new install TailwindToolbox.Templates.Blazor

# Specific version
dotnet new install TailwindToolbox.Templates.Blazor@1.0.0
```

**Note**: The `@` separator replaced `::` in .NET 9.0.200 SDK and later.

### Versioning Strategy

**Decision**: Follow Semantic Versioning 2.0.0

**Format**: `{major}.{minor}.{patch}-{prerelease}+{buildmetadata}`

**Version Increments**:
- **Major** (e.g., 2.0.0): Breaking changes, incompatible with previous versions
- **Minor** (e.g., 1.1.0): New features, backward-compatible
- **Patch** (e.g., 1.0.1): Bug fixes, fully backward-compatible

**Pre-release Versions**:
- Development: `1.0.0-dev`
- Continuous Integration: `1.0.0-ci.123`
- Beta: `1.0.0-beta.1`
- Release Candidate: `1.0.0-rc.1`

**Example Versioning Timeline**:
1. `1.0.0-beta.1` - Initial beta release
2. `1.0.0-rc.1` - Release candidate
3. `1.0.0` - First stable release
4. `1.0.1` - Bug fix (Tailwind config correction)
5. `1.1.0` - New feature (add Dark mode support)
6. `2.0.0` - Breaking change (upgrade to Tailwind CSS v5)

**Version Management**:
- Templates installed in .NET SDK 6.0.100+ are available in later SDK versions
- Not available in earlier SDK versions
- Users can install specific versions: `dotnet new install Package@1.0.0`

---

## 3. Blazor Template Structure

### Decision: Mirror .NET 10 Blazor Web App Template

**Rationale**:
- Ensures familiarity for developers already using `dotnet new blazor`
- Maintains Microsoft's recommended project structure
- Simplifies documentation ("same as blazor template, but with Tailwind")
- Reduces learning curve

**Current .NET 10 Blazor Web App Structure**:

```
BlazorApp1/
├── BlazorApp1.csproj
├── Program.cs
├── Components/
│   ├── App.razor
│   ├── Routes.razor
│   ├── Layout/
│   │   ├── MainLayout.razor
│   │   ├── NavMenu.razor
│   │   └── NotFound.razor               # New in .NET 10
│   └── Pages/
│       ├── Home.razor
│       ├── Weather.razor
│       └── Counter.razor
├── appsettings.json
├── appsettings.Development.json
└── wwwroot/
    ├── css/
    │   └── app.css                       # Will be replaced with Tailwind CSS
    ├── favicon.png
    └── app.css                           # Root CSS file
```

**Key Features in .NET 10**:
- **ReconnectModal component**: Improved reconnection UI with collocated stylesheet and JavaScript
- **NotFound.razor**: Automatically renders when `NotFound` is called
- **Passkey management**: Out-of-the-box authentication features
- **Optimized blazor.web.js**: 76% size reduction (183 KB → 43 KB) with compression

**Project File Structure**:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components" Version="10.0.0" />
  </ItemGroup>

  <!--#if (InteractivityPlatform == "Server")-->
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Components.Web" Version="10.0.0" />
  </ItemGroup>
  <!--#endif-->
</Project>
```

**Template Modifications for Tailwind**:

Replace Bootstrap references with Tailwind CSS:

1. **Remove** `wwwroot/css/bootstrap/` folder
2. **Replace** `wwwroot/css/app.css` with compiled Tailwind CSS
3. **Add** `Styles/app.css` with Tailwind directives:
   ```css
   @tailwind base;
   @tailwind components;
   @tailwind utilities;
   ```
4. **Add** `tailwind.config.js` with Blazor content paths
5. **Add** `package.json` with Tailwind build scripts
6. **Add** `TailwindBuild.targets` for MSBuild integration
7. **Update** `MainLayout.razor` to use Tailwind classes instead of Bootstrap

### wwwroot Folder Structure

**Conventions**:
- Location: `{CONTENT_ROOT}/wwwroot` (project root)
- **Security**: Only files in wwwroot are web-addressable
- **Configuration**: Files like `appsettings.json` are visible to clients - never store secrets
- **Static Files**: Images, CSS, JavaScript, fonts

**Tailwind Template wwwroot**:

```
wwwroot/
├── css/
│   └── app.css              # Generated Tailwind CSS (not in source control if regenerated)
├── js/
│   └── app.js               # Custom JavaScript (optional)
├── images/
│   └── logo.svg
├── favicon.png
└── app.css                  # Additional global styles (optional)
```

**Best Practice**: Include pre-compiled `wwwroot/css/app.css` in template so project works immediately after `dotnet new`. Document how to regenerate using build process.

---

## 4. Tailwind CSS Integration

### Decision: Pre-configured with MSBuild Targets + Optional Post-Generation Setup

**Rationale**:
- Template should work immediately after `dotnet new` (pre-compiled CSS included)
- MSBuild integration ensures CSS rebuilds during development
- Optional TailwindToolbox CLI for advanced management
- Balances "works out of the box" with "easy to customize"

**Integration Approach**:

#### 1. Package.json Configuration

```json
{
  "name": "{{ProjectName}}",
  "version": "1.0.0",
  "private": true,
  "scripts": {
    "build:css": "tailwindcss -i ./Styles/app.css -o ./wwwroot/css/app.css",
    "watch:css": "tailwindcss -i ./Styles/app.css -o ./wwwroot/css/app.css --watch"
  },
  "devDependencies": {
    "tailwindcss": "^4.0.0",
    "@tailwindcss/cli": "^4.0.0"
  }
}
```

**Note**: `{{ProjectName}}` will be replaced by template parameter substitution.

#### 2. Tailwind.config.js Configuration

```javascript
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Components/**/*.{razor,html}',
    './Pages/**/*.{razor,html}',
    './Shared/**/*.{razor,html}',
    './Layout/**/*.{razor,html}',
    './**/*.razor'
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
```

**Key Configuration**:
- `content`: Paths to all files containing Tailwind classes
- Include all `.razor` and `.html` files
- Use glob patterns for comprehensive coverage

#### 3. MSBuild Integration

**TailwindBuild.targets** file:

```xml
<Project>
  <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
    <PropertyGroup>
      <TailwindInputCss>Styles/app.css</TailwindInputCss>
      <TailwindOutputCss>wwwroot/css/app.css</TailwindOutputCss>
      <TailwindMinify Condition="'$(Configuration)' == 'Release'">--minify</TailwindMinify>
    </PropertyGroup>

    <Exec Command="npm run build:css $(TailwindMinify)"
          WorkingDirectory="$(MSBuildProjectDirectory)"
          StandardOutputImportance="low" />
  </Target>
</Project>
```

**Features**:
- Runs before each build (`BeforeTargets="BeforeBuild"`)
- Conditional minification for Release builds
- Configurable input/output paths
- Standard MSBuild properties

**Project File Import**:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <!-- ... existing properties ... -->

  <Import Project="TailwindBuild.targets" />
</Project>
```

**Alternative Approaches Considered**:

1. **Tailwind CLI Standalone**: Download standalone binary, no npm required
   - **Pros**: No Node.js dependency
   - **Cons**: Platform-specific binaries, harder to update

2. **PostCSS with Autoprefixer**: Traditional Tailwind v3 approach
   - **Pros**: More configuration options
   - **Cons**: More complex setup, Tailwind v4 simplifies this

3. **LibMan for Tailwind CDN**: Use CDN version
   - **Pros**: No build process
   - **Cons**: Larger file size, all utilities included (not tree-shaken)

**Decision Rationale**: MSBuild + npm approach is most flexible, aligns with modern frontend tooling, and provides optimal file sizes through tree-shaking.

### Handling node_modules

**Decision**: Exclude from Template Package, Generate Post-Installation

**Approach**:

1. **Template Package**: Exclude `node_modules/` completely
   ```json
   // In .template.config/template.json
   {
     "sources": [
       {
         "exclude": [
           "**/bin/**",
           "**/obj/**",
           ".template.config/**/*",
           "**/node_modules/**",
           "**/*.user",
           "**/.vs/**"
         ]
       }
     ]
   }
   ```

2. **User Workflow**:
   ```bash
   # Create project from template
   dotnet new blazor-tw -n MyBlazorApp

   # Install npm dependencies
   cd MyBlazorApp
   npm install

   # Build and run
   dotnet run
   ```

3. **Optional Post-Action** (if TailwindToolbox CLI is installed):
   ```json
   {
     "postActions": [
       {
         "actionId": "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2",
         "description": "Install npm dependencies",
         "continueOnError": true,
         "args": {
           "executable": "npm",
           "args": "install"
         }
       }
     ]
   }
   ```

4. **Include .gitignore**:
   ```gitignore
   node_modules/
   wwwroot/css/app.css
   ```

**Why This Approach**:
- Template package stays small (KB vs GB)
- Users get latest npm package versions
- Faster template installation
- Consistent with Node.js ecosystem conventions

---

## 5. Integration with TailwindToolbox CLI

### Decision: Template Works Standalone + Optional CLI Enhancement

**Rationale**:
- Template should not require CLI installation (standalone value)
- CLI provides additional automation and validation
- Users can choose their workflow preference

**Integration Points**:

### 1. Post-Action (Optional)

If TailwindToolbox CLI is installed, automatically run setup:

```json
{
  "postActions": [
    {
      "actionId": "3A7C4B45-1F5D-4A30-959A-51B88E82B5D2",
      "description": "Initialize Tailwind CSS with TailwindToolbox CLI",
      "manualInstructions": [
        {
          "text": "If you have TailwindToolbox CLI installed, run: tailwind-blazor check"
        }
      ],
      "continueOnError": true,
      "args": {
        "executable": "tailwind-blazor",
        "args": "check --project-dir ."
      }
    }
  ]
}
```

### 2. README Documentation

Include instructions for both workflows:

**Option A: Standalone (No CLI)**
```bash
dotnet new blazor-tw -n MyBlazorApp
cd MyBlazorApp
npm install
dotnet run
```

**Option B: With TailwindToolbox CLI**
```bash
dotnet new blazor-tw -n MyBlazorApp
cd MyBlazorApp
tailwind-blazor setup  # Validates and completes setup
dotnet run
```

### 3. CLI Enhancement Features

When users have TailwindToolbox CLI installed, they get:
- **Validation**: `tailwind-blazor check` verifies all configuration
- **Updates**: `tailwind-blazor update` safely upgrades Tailwind
- **Customization**: `tailwind-blazor create-target` regenerates MSBuild targets
- **Diagnostics**: Better error messages and troubleshooting

### 4. Template Parameter for CLI Integration

Optional: Add template parameter to control CLI integration:

```json
{
  "symbols": {
    "UseCLI": {
      "type": "parameter",
      "datatype": "bool",
      "defaultValue": "false",
      "description": "Include TailwindToolbox CLI integration"
    }
  }
}
```

Users can then create with: `dotnet new blazor-tw --UseCLI true`

---

## 6. Alternative Approaches Considered

### A. Embedded Tailwind CSS (No Build Process)

**Description**: Include full compiled Tailwind CSS CDN file

**Pros**:
- Zero build configuration
- Works immediately
- No Node.js dependency

**Cons**:
- Large file size (3MB+ uncompressed)
- No tree-shaking (includes all utilities)
- No customization of theme
- Not production-ready

**Verdict**: ❌ Rejected - Not suitable for production applications

### B. LibMan for Tailwind

**Description**: Use LibMan to download Tailwind CSS from CDN

**Pros**:
- Built into Visual Studio
- No npm required
- Familiar to .NET developers

**Cons**:
- Still includes full CSS (no tree-shaking)
- Limited to CDN-available versions
- No build-time optimization

**Verdict**: ❌ Rejected - Same file size issues as CDN approach

### C. Tailwind CLI Standalone Binary

**Description**: Download standalone Tailwind CLI binary (no Node.js)

**Pros**:
- No Node.js dependency
- Single executable
- Faster than npm

**Cons**:
- Platform-specific binaries (Windows/Mac/Linux)
- Complex template configuration for multi-platform
- Harder to update than npm package
- Less familiar to frontend developers

**Verdict**: ⚠️ Possible Alternative - Could support as option

### D. Pre-commit Hook for CSS Generation

**Description**: Use Git hooks to regenerate CSS before commits

**Pros**:
- Automatic regeneration
- Always up-to-date in version control

**Cons**:
- Requires Git
- Adds complexity to workflow
- Slower commit process

**Verdict**: ❌ Rejected - MSBuild integration is more reliable

### E. dotnet watch Integration Only

**Description**: Use `dotnet watch` to rebuild CSS during development

**Pros**:
- Integrated with .NET tooling
- Familiar to .NET developers

**Cons**:
- Only works during development
- Still needs build-time solution for production
- Requires MSBuild targets anyway

**Verdict**: ✅ Complementary - Works alongside MSBuild approach

---

## 7. Implementation Recommendations

### Phase 1: Create Template Content (Week 1)

**Tasks**:
1. Create new Blazor project: `dotnet new blazor -n Company.BlazorApp1`
2. Remove all Bootstrap references from `MainLayout.razor` and `NavMenu.razor`
3. Add Tailwind CSS configuration files (`tailwind.config.js`, `package.json`)
4. Create `Styles/app.css` with Tailwind directives
5. Create `TailwindBuild.targets` file
6. Update `.csproj` with `<Import>` for targets file
7. Convert all layout components to use Tailwind classes
8. Test that project builds and runs correctly
9. Add `.gitignore` with appropriate exclusions

**Validation**: Run project, verify Tailwind classes work, check build process

### Phase 2: Create Template Configuration (Week 1-2)

**Tasks**:
1. Create `.template.config/` folder
2. Create `template.json` with all required properties
3. Define symbols for template parameters (InteractivityPlatform, etc.)
4. Configure file exclusions (node_modules, bin, obj)
5. Add post-actions for npm install (optional)
6. Set `sourceName`, `preferNameDirectory`, `defaultName`
7. Test parameter substitution works correctly

**Validation**: Test template locally with `dotnet new install ./`

### Phase 3: Create Packaging Project (Week 2)

**Tasks**:
1. Create `TailwindToolbox.Templates.csproj` with `PackageType=Template`
2. Configure NuGet package metadata (title, description, tags)
3. Set up content inclusion with proper exclusions
4. Create README.md with usage instructions
5. Build NuGet package: `dotnet pack -c Release`
6. Test package installation: `dotnet new install ./artifacts/...nupkg`

**Validation**: Install template package, create project, verify functionality

### Phase 4: Documentation & Testing (Week 2-3)

**Tasks**:
1. Create comprehensive README for template package
2. Document both standalone and CLI-integrated workflows
3. Test on Windows, macOS, Linux
4. Test with different InteractivityPlatform options
5. Create quickstart guide
6. Create troubleshooting guide
7. Test in Visual Studio 2026 and VS Code

**Validation**: External user testing with fresh machine

### Phase 5: Integration with TailwindToolbox CLI (Week 3)

**Tasks**:
1. Add template package reference to TailwindToolbox documentation
2. Update `setup` command to detect template-generated projects
3. Add `dotnet new blazor-tw` to quickstart guides
4. Create integration tests for CLI + template workflow
5. Document post-actions behavior

**Validation**: End-to-end test of template + CLI workflow

### Phase 6: Publishing (Week 4)

**Tasks**:
1. Create NuGet.org account / organization
2. Generate NuGet API key
3. Publish template package: `dotnet nuget push`
4. Create GitHub release with package
5. Update all documentation with installation instructions
6. Announce release (blog post, social media, etc.)

**Validation**: Install from NuGet.org, verify discovery with `dotnet new search`

---

## 8. Testing Strategy

### Unit Testing Template Configuration

**What to Test**:
- template.json is valid JSON
- All required properties present
- Symbol definitions are complete
- Source exclusions are correct

**How to Test**:
```bash
# Validate template.json against schema
dotnet new --list  # Should show template if installed
```

### Integration Testing Template Generation

**Test Scenarios**:

1. **Default Project Creation**
   ```bash
   dotnet new blazor-tw -n TestApp1
   cd TestApp1
   npm install
   dotnet build
   dotnet run
   ```
   **Expected**: Project builds, runs, Tailwind classes work

2. **Custom Name Parameter**
   ```bash
   dotnet new blazor-tw -n MyCustomApp
   ```
   **Expected**: All `Company.BlazorApp1` replaced with `MyCustomApp`

3. **WebAssembly Interactivity**
   ```bash
   dotnet new blazor-tw -n WasmApp --InteractivityPlatform WebAssembly
   ```
   **Expected**: WebAssembly packages referenced, client project created

4. **CLI Integration**
   ```bash
   dotnet new blazor-tw -n CliApp
   cd CliApp
   tailwind-blazor check
   ```
   **Expected**: All validation checks pass

### Contract Testing

**Tests**:
- Template appears in `dotnet new list`
- Template installs without errors
- Template uninstalls cleanly
- NuGet package metadata is correct
- Template works in Visual Studio New Project Dialog

### Performance Testing

**Metrics**:
- Template installation time: < 10 seconds
- Project generation time: < 5 seconds
- First build time: < 30 seconds
- npm install time: < 60 seconds

---

## 9. Key Decisions Summary

| Decision Point | Chosen Approach | Rationale |
|----------------|----------------|-----------|
| Template Engine | .NET template engine with .template.config | Native integration, widely adopted |
| Package Format | NuGet with PackageType=Template | Standard .NET distribution mechanism |
| Blazor Structure | Mirror .NET 10 Blazor Web App template | Familiarity, best practices |
| Tailwind Integration | MSBuild targets + npm scripts | Automatic builds, tree-shaking, production-ready |
| node_modules | Exclude from package, install post-generation | Small package size, latest versions |
| CLI Integration | Optional enhancement via post-actions | Works standalone, enhanced with CLI |
| Versioning | Semantic Versioning 2.0.0 | Industry standard, clear upgrade path |
| File Exclusions | bin/, obj/, node_modules/, .vs/ | Clean package, no build artifacts |

---

## 10. Resources & References

### Official Documentation
- [Custom templates for dotnet new](https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates) - Microsoft Learn
- [Create a template package for dotnet new](https://learn.microsoft.com/en-us/dotnet/core/tutorials/cli-templates-create-template-package) - Microsoft Learn
- [Reference for template.json](https://github.com/dotnet/templating/wiki/Reference-for-template.json) - dotnet/templating Wiki
- [Post Action Registry](https://github.com/dotnet/templating/wiki/Post-Action-Registry) - dotnet/templating Wiki
- [ASP.NET Core Blazor project structure](https://learn.microsoft.com/en-us/aspnet/core/blazor/project-structure?view=aspnetcore-10.0) - Microsoft Learn
- [NuGet Package Versioning](https://learn.microsoft.com/en-us/nuget/concepts/package-versioning) - Microsoft Learn

### Community Resources
- [How to create your own templates for dotnet new](https://devblogs.microsoft.com/dotnet/how-to-create-your-own-templates-for-dotnet-new/) - .NET Blog
- [Creating Useful .NET Templates](https://knowyourtoolset.com/2021/08/creating-useful-net-templates/) - Know Your Toolset
- [Integrating Tailwind CSS in Blazor](https://timdeschryver.dev/blog/integrating-tailwind-css-in-blazor) - Tim Deschryver
- [GitHub: TailwindBlazor](https://github.com/claudiobernasconi/TailwindBlazor) - Example implementation
- [GitHub: BlazorTailwindTemplate](https://github.com/Practical-ASP-NET/BlazorTailwindTemplate) - Example template package

### Technical References
- [Conditional processing and comment syntax](https://github.com/dotnet/templating/wiki/Conditional-processing-and-comment-syntax) - dotnet/templating Wiki
- [Available Symbols Generators](https://github.com/dotnet/templating/wiki/Available-Symbols-Generators) - dotnet/templating Wiki
- [Naming and default value forms](https://github.com/dotnet/templating/wiki/Naming-and-default-value-forms) - dotnet/templating Wiki
- [Using Primary Outputs for Post Actions](https://github.com/dotnet/templating/wiki/Using-Primary-Outputs-for-Post-Actions) - dotnet/templating Wiki

### Tailwind + Blazor Integration
- [Tailwind CSS Blazor (.NET) - Flowbite](https://flowbite.com/docs/getting-started/blazor/) - Integration guide
- [Blazor with TailwindCSS](https://steven-giesel.com/blogPost/351838ba-e308-4a09-b9f6-75bb95c39610) - Steven Giesel
- [GitHub: blazor-tailwind](https://github.com/Physer/blazor-tailwind) - MSBuild example
- [Adding Tailwind CSS v3 to a Blazor app](https://chrissainty.com/adding-tailwind-css-v3-to-a-blazor-app/) - Chris Sainty

### GitHub Issues & Discussions
- [Add a Tailwind CSS Template for Blazor in .NET CLI](https://github.com/dotnet/aspnetcore/issues/61002) - Feature request
- [Replacements in file name with sourceName and fileRename parameter](https://github.com/dotnet/templating/issues/1270) - Template engine issue

---

## 11. Next Steps

### Immediate Actions
1. ✅ Complete research documentation (this document)
2. Create detailed specification for template package feature
3. Design template structure matching .NET 10 Blazor Web App
4. Prototype template.json configuration
5. Test parameter substitution with sample template

### Short-term Goals (Next Sprint)
1. Implement Phase 1: Create template content
2. Implement Phase 2: Create template configuration
3. Implement Phase 3: Create packaging project
4. Test locally with `dotnet new install`

### Long-term Goals (Future Releases)
1. Publish template package to NuGet.org
2. Integrate with TailwindToolbox CLI
3. Create Visual Studio VSIX extension
4. Support multiple Tailwind versions (v3, v4)
5. Add Flowbite component library integration option
6. Create template variations (minimal, full, with auth, etc.)

---

## Conclusion

Creating a .NET template package for Blazor + Tailwind CSS is well-supported by the .NET tooling ecosystem. The recommended approach is to:

1. Use standard .NET template engine with `.template.config` folder
2. Package as NuGet template with `PackageType=Template`
3. Mirror .NET 10 Blazor Web App template structure
4. Integrate Tailwind CSS via MSBuild targets + npm scripts
5. Exclude node_modules from package, install post-generation
6. Optionally integrate with TailwindToolbox CLI for enhanced workflow

This approach balances ease of use (works out of the box), flexibility (supports customization), and maintainability (follows .NET conventions). The template will be familiar to .NET developers while providing a modern Tailwind CSS development experience.

**Estimated Implementation Time**: 3-4 weeks
**Complexity**: Medium (template creation) + Low (packaging)
**Risk**: Low (well-documented process, many examples available)

---

**Document Version**: 1.0
**Last Updated**: 2026-01-04
**Author**: TailwindToolbox Research
**Status**: Complete - Ready for Specification Phase
