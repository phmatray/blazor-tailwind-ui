# Quickstart: Blazor Tailwind Template

**Branch**: `002-blazor-tailwind-template` | **Date**: 2026-01-04 | **Spec**: [spec.md](./spec.md)

This guide shows how developers will use the Blazor Tailwind Template after it's published.

## Prerequisites

- .NET 10.0 SDK or later
- Node.js 16+ and npm (for Tailwind CSS)
- TailwindToolbox CLI (optional but recommended)

## Installation

### Install the Template

```bash
# Install from NuGet (when published)
dotnet new install TailwindToolbox.Blazor.Template

# Or install locally for testing
dotnet new install /path/to/template/package
```

### Verify Installation

```bash
dotnet new list | grep blazor-tailwind
```

Expected output:
```
blazor-tailwind    Blazor Web App with Tailwind CSS    Web/Blazor/Tailwind    C#
```

## Quick Start (2 Minutes)

### 1. Create New Project

```bash
# Create project in new directory
dotnet new blazor-tailwind -n MyBlazorApp

# Or create in current directory
mkdir MyBlazorApp
cd MyBlazorApp
dotnet new blazor-tailwind
```

### 2. Setup Tailwind CSS (Recommended)

```bash
cd MyBlazorApp

# Option 1: Use TailwindToolbox CLI (recommended)
tailwind-blazor setup

# Option 2: Manual npm install
npm install
```

### 3. Build and Run

```bash
# Build (compiles Tailwind CSS automatically)
dotnet build

# Run the application
dotnet run
```

### 4. Open in Browser

Navigate to `http://localhost:5000` to see your Blazor app with Tailwind CSS!

## What You Get

After running `dotnet new blazor-tailwind`, your project contains:

```
MyBlazorApp/
├── MyBlazorApp.csproj           # Project file
├── Program.cs                    # App entry point
├── Components/
│   ├── App.razor                 # Root component
│   ├── Routes.razor              # Routing
│   ├── Layout/
│   │   ├── MainLayout.razor      # Main layout (Tailwind styled)
│   │   └── NavMenu.razor         # Navigation (Tailwind styled)
│   └── Pages/
│       ├── Home.razor            # Home page
│       ├── Counter.razor         # Counter demo
│       └── Weather.razor         # Weather forecast
├── Styles/
│   └── app.css                   # Tailwind input file
├── wwwroot/
│   ├── css/
│   │   └── app.css               # Compiled CSS (generated on build)
│   └── favicon.png
├── tailwind.config.js            # Tailwind configuration
├── package.json                  # npm dependencies
└── TailwindBuild.targets         # MSBuild integration
```

## Template Parameters

### Target Framework

```bash
# Default: .NET 10.0
dotnet new blazor-tailwind -n MyApp

# Specify framework explicitly
dotnet new blazor-tailwind -n MyApp --framework net10.0
```

### Output Directory

```bash
# Create in specific directory
dotnet new blazor-tailwind -n MyApp -o ./projects/MyApp
```

### Skip Restore

```bash
# Skip automatic dotnet restore
dotnet new blazor-tailwind -n MyApp --skipRestore
```

## Customizing Tailwind CSS

### Modify Tailwind Configuration

Edit `tailwind.config.js`:

```javascript
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Components/**/*.razor',
    './Components/**/*.html',
  ],
  theme: {
    extend: {
      colors: {
        primary: '#3b82f6',
        secondary: '#8b5cf6',
      },
    },
  },
  plugins: [],
}
```

### Add Custom Styles

Edit `Styles/app.css`:

```css
@tailwind base;
@tailwind components;
@tailwind utilities;

/* Custom component classes */
@layer components {
  .btn-primary {
    @apply bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded;
  }
}
```

### Rebuild CSS

```bash
# Manual rebuild
npm run build:css

# Watch mode (auto-rebuild on changes)
npm run watch:css
```

## Using Tailwind in Components

### Example: Button Component

```razor
@page "/example"

<PageTitle>Example</PageTitle>

<div class="container mx-auto p-8">
    <h1 class="text-4xl font-bold text-blue-600 mb-4">
        Hello Tailwind!
    </h1>

    <button @onclick="HandleClick"
            class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
        Click Me
    </button>

    @if (clicked)
    {
        <div class="mt-4 p-4 bg-green-100 border border-green-400 text-green-700 rounded">
            Button clicked!
        </div>
    }
</div>

@code {
    private bool clicked = false;

    private void HandleClick()
    {
        clicked = true;
    }
}
```

### Responsive Design

```razor
<div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
    <div class="bg-white p-6 rounded-lg shadow-md">
        <h2 class="text-xl font-bold mb-2">Card 1</h2>
        <p class="text-gray-600">Content here</p>
    </div>
    <!-- More cards... -->
</div>
```

## Development Workflow

### 1. Watch Mode (Recommended)

Terminal 1 - Watch CSS:
```bash
npm run watch:css
```

Terminal 2 - Run App:
```bash
dotnet watch run
```

### 2. Build for Production

```bash
# Build with minified CSS
dotnet build -c Release

# Publish
dotnet publish -c Release -o ./publish
```

## Updating Tailwind

### Update to Latest Version

```bash
# Using TailwindToolbox CLI
tailwind-blazor update

# Or manually
npm update tailwindcss @tailwindcss/cli
```

### Check Configuration

```bash
# Validate Tailwind setup
tailwind-blazor check
```

## Troubleshooting

### CSS Not Compiling

**Problem**: `wwwroot/css/app.css` is empty or not generated.

**Solutions**:
1. Ensure npm packages are installed: `npm install`
2. Check Node.js is installed: `node --version`
3. Manually build CSS: `npm run build:css`
4. Check TailwindBuild.targets is imported in .csproj

### Node.js Not Found

**Problem**: Build fails with "node: command not found".

**Solution**:
1. Install Node.js from https://nodejs.org/
2. Or use nvm: `curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash`
3. Verify: `node --version`

### Classes Not Applied

**Problem**: Tailwind classes in components don't work.

**Solutions**:
1. Check `tailwind.config.js` includes correct content paths
2. Rebuild: `npm run build:css`
3. Clear browser cache
4. Verify `wwwroot/css/app.css` contains your classes

### Template Not Found

**Problem**: `dotnet new blazor-tailwind` returns "No templates matched the input".

**Solution**:
1. Verify installation: `dotnet new list | grep blazor-tailwind`
2. Reinstall template: `dotnet new install TailwindToolbox.Blazor.Template`
3. Check template package version

## Next Steps

### Learn More

- **Tailwind CSS Documentation**: https://tailwindcss.com/docs
- **Blazor Documentation**: https://learn.microsoft.com/aspnet/core/blazor
- **TailwindToolbox CLI**: Run `tailwind-blazor --help`

### Customize Your App

1. **Modify Layout**: Edit `Components/Layout/MainLayout.razor`
2. **Add Pages**: Create new `.razor` files in `Components/Pages/`
3. **Update Styles**: Customize `tailwind.config.js` and `Styles/app.css`
4. **Add Components**: Create reusable components in `Components/`

### Deploy

```bash
# Build for production
dotnet publish -c Release -o ./publish

# Deploy to Azure, AWS, or your hosting provider
```

## Example Projects

After creating a project, you can:

1. **E-commerce Site**: Use Tailwind's utility classes for product cards, checkout forms
2. **Dashboard**: Create responsive layouts with Tailwind's grid system
3. **Blog**: Style articles with Tailwind typography utilities
4. **Admin Panel**: Build data tables and forms with Tailwind components

## Uninstallation

```bash
# Remove template
dotnet new uninstall TailwindToolbox.Blazor.Template
```

## Support

- **Issues**: https://github.com/phmatray/TailwindToolbox/issues
- **Discussions**: https://github.com/phmatray/TailwindToolbox/discussions
- **Documentation**: See project README and specification docs

---

**Time to First Run**: Under 2 minutes from `dotnet new` to `dotnet run`! 🚀
