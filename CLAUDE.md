# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Common Development Commands

### Build and Run
```bash
# Build the entire solution
dotnet build

# Build specific projects
dotnet build CatalystUI.Components
dotnet build CatalystUI.Demo

# Run the demo application
cd CatalystUI.Demo
dotnet run

# Run with hot reload
cd CatalystUI.Demo
dotnet watch
```

### Testing
```bash
# Run tests (when test projects are added)
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Package Management
```bash
# Create NuGet package for the component library
cd CatalystUI.Components
dotnet pack

# Publish to NuGet (when ready)
dotnet nuget push CatalystUI.Components.*.nupkg
```

## Architecture Overview

### Solution Structure
- **CatalystUI.Components**: Core Blazor component library implementing the Catalyst UI Kit design system
- **CatalystUI.Demo**: Blazor WebAssembly demo application showcasing the components
- **catalyst-ui-kit**: Original React/TypeScript implementation reference (not part of .NET build)

### Component Architecture

All Blazor components inherit from `CatalystComponentBase` which provides:
- Common parameters: `Class`, `Id`, `Disabled`, `AdditionalAttributes`
- CSS class combination utilities via `CombineClasses()`
- Attribute management via `GetAttributes()` and `GetAttributesWithPairs()`
- Conditional CSS class helper `If()`

### Component Organization

Components are organized into logical categories:
- **Data**: Tables, pagination, description lists
- **Display**: Alert, avatar, badge, code, divider, heading, link, text
- **FormStructure**: Field, fieldset, label, legend, description, error messages
- **Forms**: Input controls (button, checkbox, input, radio, select, switch, textarea)
- **Interactive**: Complex interactive components (combobox, dialog, dropdown, listbox)
- **Layout**: Application layout components (navbar, sidebar, stacked layout, auth layout)

### Styling Approach

The library uses Tailwind CSS classes directly in components:
- No separate CSS files (except for specific component overrides)
- Consistent use of Tailwind utility classes
- Dark mode support via Tailwind's dark: prefix
- Custom CSS variables for spacing and transitions

### JavaScript Interop

JavaScript functionality is provided through:
- `CatalystUIJsInterop` service for component-specific JS operations
- JavaScript files in `wwwroot/js/catalyst-ui.js`
- CSS files in `wwwroot/css/catalyst-ui.css`

## Key Development Patterns

### Creating New Components

1. Create a new `.razor` file in the appropriate Components subfolder
2. Inherit from `CatalystComponentBase`
3. Follow the existing component patterns for parameter definitions
4. Use `CombineClasses()` to merge component classes with user-provided classes
5. Use `GetAttributes()` for attribute splatting

### Component Naming

- Use descriptive names matching the Catalyst UI Kit terminology
- Group related components (e.g., Dialog, DialogTitle, DialogBody, DialogActions)
- Keep consistency with the React implementation names

### Testing Components

When adding tests:
- Test component rendering with different parameters
- Test event callbacks
- Test CSS class combinations
- Test accessibility attributes

## Development URLs

- HTTP: http://localhost:5183
- HTTPS: https://localhost:7219

## Dependencies

- .NET 9.0
- Microsoft.AspNetCore.Components.Web (9.0.6)
- Microsoft.AspNetCore.Components.WebAssembly (9.0.6)
- Tailwind CSS (via CDN or local installation)