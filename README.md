![CatalystUI banner](.github/banner.png)

# CatalystUI — Blazor component library based on Catalyst UI Kit

![Build](https://img.shields.io/github/actions/workflow/status/phmatray/CatalystUI/dotnet.yml?branch=main)
![NuGet](https://img.shields.io/nuget/v/CatalystUI.Components)
![License](https://img.shields.io/github/license/phmatray/CatalystUI)

A production-ready **Blazor component library** implementing the [Catalyst UI Kit](https://catalyst.tailwindui.com/) design system. Built on Tailwind CSS with dark mode support, full accessibility, and a clean C# API — drop-in components for modern Blazor applications on .NET 9.

## ✨ Features

- **30+ components** across 6 categories: Data, Display, Forms, Layout, Interactive, FormStructure
- **Tailwind CSS**: Utility-first styling with no custom CSS overhead
- **Dark mode**: Native dark mode support via Tailwind's `dark:` prefix
- **Accessible**: WCAG-compliant markup out of the box
- **Attribute splatting**: Pass arbitrary HTML attributes to any component
- **Base class**: All components inherit `CatalystComponentBase` for consistent customization

### Component Categories

| Category | Components |
|---|---|
| **Data** | Table, Pagination, DescriptionList |
| **Display** | Alert, Avatar, Badge, Code, Divider, Heading, Link, Text |
| **Forms** | Button, Checkbox, Input, Radio, Select, Switch, Textarea |
| **FormStructure** | Field, Fieldset, Label, Legend, Description, Error |
| **Interactive** | Combobox, Dialog, Dropdown, Listbox |
| **Layout** | Navbar, Sidebar, StackedLayout, AuthLayout |

## 📦 Installation

```bash
dotnet add package CatalystUI.Components
```

## 🚀 Quick Start

Add to `_Imports.razor`:
```razor
@using CatalystUI.Components
```

Register services in `Program.cs`:
```csharp
builder.Services.AddCatalystUI();
```

Use components:
```razor
<Heading Level="1">Hello from CatalystUI</Heading>

<Alert Color="AlertColor.Warning">
    This is a warning alert.
</Alert>

<Button Color="ButtonColor.Primary" OnClick="HandleClick">
    Click me
</Button>

<Input Placeholder="Enter your email" Type="email" />

<Badge Color="BadgeColor.Green">Active</Badge>
```

## 🏗 Project Structure

```
CatalystUI.Components/    # Core component library (NuGet package)
  Components/
    Data/                 # Table, Pagination, DescriptionList
    Display/              # Alert, Avatar, Badge, Code, Divider, Heading...
    Forms/                # Button, Checkbox, Input, Radio, Select...
    Interactive/          # Combobox, Dialog, Dropdown, Listbox
    Layout/               # Navbar, Sidebar, StackedLayout
    FormStructure/        # Field, Fieldset, Label...
  Base/
    CatalystComponentBase.cs  # Shared base class
CatalystUI.Demo/          # Blazor WASM demo app
```

## 🛠 Development

```bash
# Run the demo app
cd CatalystUI.Demo
dotnet watch

# Build NuGet package
cd CatalystUI.Components
dotnet pack
```

Demo available at `https://localhost:7219`.

<!-- portfolio-techstack:start -->

## Tech Stack

- **.NET 9**
- Microsoft.AspNetCore.Components.Web
- Microsoft.AspNetCore.Components.WebAssembly
- Microsoft.AspNetCore.Components.WebAssembly.DevServer

<!-- portfolio-techstack:end -->

## 📄 License

MIT — see [LICENSE](LICENSE)

---

<!-- portfolio-sections:start -->

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork the repository and create your branch (`git checkout -b feat/my-feature`)
2. Commit your changes (`git commit -m 'feat: ...'`)
3. Push the branch and open a Pull Request

<!-- portfolio-sections:end -->
