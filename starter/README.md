![BlazorTailwind banner](.github/banner.png)

# BlazorTailwind

> **A Blazor Web App starter with a working Tailwind CSS v4 build pipeline and a full Tailwind UI–style responsive dashboard shell, ready to scaffold as a `dotnet new` template.**

<!-- Row 1 — Identity -->
[![phmatray - BlazorTailwind](https://img.shields.io/static/v1?label=phmatray&message=BlazorTailwind&color=blue&logo=github)](https://github.com/phmatray/BlazorTailwind)
[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?logo=blazor&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![stars](https://img.shields.io/github/stars/phmatray/BlazorTailwind?style=social)](https://github.com/phmatray/BlazorTailwind/stargazers)
[![forks](https://img.shields.io/github/forks/phmatray/BlazorTailwind?style=social)](https://github.com/phmatray/BlazorTailwind/network/members)

<!-- Row 2 — Activity -->
[![issues](https://img.shields.io/github/issues/phmatray/BlazorTailwind)](https://github.com/phmatray/BlazorTailwind/issues)
[![pull requests](https://img.shields.io/github/issues-pr/phmatray/BlazorTailwind)](https://github.com/phmatray/BlazorTailwind/pulls)
[![last commit](https://img.shields.io/github/last-commit/phmatray/BlazorTailwind)](https://github.com/phmatray/BlazorTailwind/commits/dev)

## Table of Contents

- [The Problem](#the-problem)
- [The Solution](#the-solution)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Use as a Project Template](#use-as-a-project-template)
- [Project Structure](#project-structure)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

## The Problem

A default Blazor Web App looks like a default Blazor Web App: Bootstrap, a fixed sidebar, no design system. Getting to a modern, SaaS-style dashboard UI usually means either adopting a heavyweight component library or hand-wiring Tailwind CSS's npm-based build (PostCSS, autoprefixer, content scanning) into a .NET project and re-implementing the interactive bits — dropdowns, mobile drawers, transitions — that Tailwind UI ships as JavaScript/React but Blazor doesn't have natively.

## The Solution

BlazorTailwind is a Blazor Server project with Tailwind CSS v4 already wired into the build: `dotnet build` runs `npm run build:css`, which compiles `Styles/app.css` through PostCSS/autoprefixer/`@tailwindcss/forms` into a minified `wwwroot/tailwind/app.min.css`. On top of that it implements a Tailwind UI–style application shell entirely in Blazor components — a collapsible mobile sidebar, static desktop sidebar, top bar with search/notifications/profile dropdown — including its own `TwTransition`/`TwTransitionChild` components that reproduce Headless UI–style enter/leave CSS transitions without any JS framework. The whole repo also doubles as an installable `dotnet new` template, so the shell can be scaffolded straight into a new project.

## Features

- **Tailwind CSS v4 pipeline** — `postcss.config.js` + `tailwind.config.js` + `@tailwindcss/forms`, built automatically on `dotnet build` via an MSBuild target that shells out to npm.
- **Responsive dashboard shell** — `MainLayout.razor` + `Sidebar.razor` reproduce the Tailwind UI "sidebar" application shell: responsive mobile drawer, desktop sidebar, top bar, profile dropdown.
- **Custom transition components** — `TwTransition` / `TwTransitionChild` implement enter/leave CSS transitions in pure Blazor (no Alpine/Headless UI JS), covered by bUnit tests in `BlazorTailwind.Tests`.
- **Heroicons-style icon components** — Outline/Solid SVG icons (`HomeIcon`, `Bars3Icon`, `Cog6ToothIcon`, …) as individual Razor components, rendered dynamically per nav item via `<DynamicComponent>`.
- **Data-driven navigation** — `NavigationService` supplies the main menu, external links, and settings menu consumed by the layout.
- **Sample pages** — the stock Blazor `Home` / `Counter` / `Weather` pages (with `WeatherForecastService`), restyled with Tailwind.
- **`dotnet new` template** — `.template.config/template.json` (`shortName: blazortailwind`) turns the repo into an installable project template.

## Tech Stack

- **.NET 8.0** — Blazor Server (Interactive Server Components)
- **Tailwind CSS v4** + **PostCSS** + **autoprefixer** + **`@tailwindcss/forms`** — styling, built via npm
- **bUnit** + **xUnit** + **Shouldly** — component tests (`BlazorTailwind.Tests`)
- **Docker** — `Dockerfile` + `.dockerignore` for containerized deployment

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) (`global.json` pins the SDK channel used for local development)
- [Node.js](https://nodejs.org/) — the build fails fast with a clear error if `npm` isn't on the `PATH`

### Installation

```bash
git clone https://github.com/phmatray/BlazorTailwind.git
cd BlazorTailwind
dotnet restore
dotnet build
```

### Running

```bash
dotnet run --project BlazorTailwind
```

Then open the URL printed in the console (see `BlazorTailwind/Properties/launchSettings.json`).

## Use as a Project Template

The repository ships a `.template.config/template.json`, so it can be installed and reused as a `dotnet new` template:

```bash
dotnet new install .
dotnet new blazortailwind -n MyNewApp
```

## Project Structure

```
BlazorTailwind/
├─ BlazorTailwind/              # Blazor Server app
│  ├─ Components/Layout/        # MainLayout, Sidebar, NavigationItem
│  ├─ Components/UI/            # TwTransition, TwTransitionChild, CustomNavLink
│  ├─ Components/Icons/         # Outline24 / Solid20 SVG icon components
│  ├─ Components/Pages/         # Home, Counter, Weather
│  ├─ Services/                 # NavigationService, WeatherForecastService
│  ├─ Styles/app.css            # Tailwind source, compiled to wwwroot/tailwind/
│  └─ Dockerfile
└─ BlazorTailwind.Tests/        # bUnit component tests
```

## Roadmap

- [ ] Expand bUnit coverage beyond the `TwTransition` components
- [ ] Wire the plugin-style icon set to more Tailwind UI patterns (tables, forms)

See the [open issues](https://github.com/phmatray/BlazorTailwind/issues) for the full list.

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork → branch (`git checkout -b feat/my-feature`)
2. Commit (`git commit -m 'feat: ...'`)
3. Push + Pull Request

## License

No license has been declared for this repository yet. Until one is added, default copyright applies — see [choosealicense.com](https://choosealicense.com/) if you intend to open it up.

---

© 2026 Philippe Matray
