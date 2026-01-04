# Blazor Web App with Tailwind CSS

A modern Blazor Web Application built with .NET 10 and Tailwind CSS v4.x.

## Features

- **.NET 10 LTS** - Built on the latest long-term support release
- **Tailwind CSS v4.x** - Utility-first CSS framework with the latest features
- **MSBuild Integration** - Automatic Tailwind compilation during build
- **Interactive Server Components** - Real-time interactivity with SignalR
- **Static SSR** - Optimized performance with static server-side rendering
- **Modern Component Architecture** - Follows Microsoft's official Blazor template structure

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- Node.js 16.0.0 or later
- npm 8.0.0 or later

### Development

1. **Install npm packages** (required for Tailwind CSS):
   ```bash
   npm install
   ```

2. **Run the development server**:
   ```bash
   dotnet run
   ```

3. **Watch Tailwind CSS changes** (optional, in a separate terminal):
   ```bash
   npm run watch:css
   ```

### Build for Production

```bash
dotnet build -c Release
```

The release build automatically minifies the Tailwind CSS output for optimal performance.

## Project Structure

```
├── Components/
│   ├── Layout/          # Layout components (MainLayout, NavMenu, ReconnectModal)
│   ├── Pages/           # Page components (Home, Counter, Weather, etc.)
│   ├── App.razor        # Root component
│   ├── Routes.razor     # Routing configuration
│   └── _Imports.razor   # Global using directives
├── Properties/
│   └── launchSettings.json
├── Styles/
│   └── app.css          # Tailwind CSS input file
├── wwwroot/
│   ├── css/
│   │   └── app.css      # Generated Tailwind CSS (do not edit)
│   └── favicon.svg
├── appsettings.json
├── package.json         # npm dependencies and scripts
├── Program.cs           # Application entry point
├── tailwind.config.js   # Tailwind configuration
└── TailwindBuild.targets # MSBuild integration
```

## Tailwind CSS v4.x

This project uses Tailwind CSS v4.x, which has some differences from v3.x:

### Input CSS Syntax

The `Styles/app.css` file uses the new v4.x import syntax:

```css
@import "tailwindcss";
```

This replaces the v3.x directives (`@tailwind base`, `@tailwind components`, `@tailwind utilities`).

### CLI Package

Tailwind v4.x requires both packages:
- `tailwindcss` - Core framework
- `@tailwindcss/cli` - Command-line interface

Both are included in `package.json`.

### Configuration

The `tailwind.config.js` file configures which files Tailwind should scan for CSS classes:

```javascript
content: [
  './Components/**/*.razor',
  './Components/**/*.html',
  './Components/**/*.cshtml'
]
```

Adjust these paths if you add new directories containing Tailwind classes.

## MSBuild Integration

Tailwind CSS compiles automatically during `dotnet build` thanks to `TailwindBuild.targets`:

- **Debug builds**: Unminified CSS for easier debugging
- **Release builds**: Minified CSS for optimal performance

The compiled CSS is output to `wwwroot/css/app.css` and should not be edited manually.

## npm Scripts

- `npm run build:css` - Build Tailwind CSS (minified)
- `npm run watch:css` - Watch for changes and rebuild automatically

## Customization

### Tailwind Theme

Extend the default Tailwind theme in `tailwind.config.js`:

```javascript
theme: {
  extend: {
    colors: {
      primary: '#3b82f6',
      secondary: '#64748b',
    },
  },
}
```

### Custom CSS

Add custom styles in `Styles/app.css` using `@layer`:

```css
@layer components {
  .btn-custom {
    @apply bg-blue-600 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded;
  }
}
```

## .NET 10 Features

This template includes new .NET 10 features:

- **@Assets[]** syntax for optimized asset loading
- **MapStaticAssets()** for improved static file serving
- **ReconnectModal** component for enhanced WebSocket reconnection UX
- **NotFound** component for custom 404 pages
- **Streaming rendering** with `[StreamRendering]` attribute

## Learn More

- [Blazor Documentation](https://learn.microsoft.com/en-us/aspnet/core/blazor)
- [Tailwind CSS Documentation](https://tailwindcss.com/docs)
- [.NET 10 What's New](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10)

## License

This template is provided as-is under the MIT license.
