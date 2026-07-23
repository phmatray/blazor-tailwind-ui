![blazor-tailwind-ui banner](.github/banner.png)

# Blazor Tailwind UI

<!-- portfolio-badges:start -->
<!-- Identity -->
[![phmatray - blazor-tailwind-ui](https://img.shields.io/static/v1?label=phmatray&message=blazor-tailwind-ui&color=blue&logo=github)](https://github.com/phmatray/blazor-tailwind-ui)
![Top language](https://img.shields.io/github/languages/top/phmatray/blazor-tailwind-ui)
[![Stars](https://img.shields.io/github/stars/phmatray/blazor-tailwind-ui?style=social)](https://github.com/phmatray/blazor-tailwind-ui/stargazers)
[![Forks](https://img.shields.io/github/forks/phmatray/blazor-tailwind-ui?style=social)](https://github.com/phmatray/blazor-tailwind-ui/network/members)
[![License](https://img.shields.io/github/license/phmatray/blazor-tailwind-ui)](https://github.com/phmatray/blazor-tailwind-ui/blob/HEAD/LICENSE)

<!-- Activity -->
[![Issues](https://img.shields.io/github/issues/phmatray/blazor-tailwind-ui)](https://github.com/phmatray/blazor-tailwind-ui/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/phmatray/blazor-tailwind-ui)](https://github.com/phmatray/blazor-tailwind-ui/pulls)
[![Last commit](https://img.shields.io/github/last-commit/phmatray/blazor-tailwind-ui)](https://github.com/phmatray/blazor-tailwind-ui/commits)
<!-- portfolio-badges:end -->

<!-- portfolio-toc:start -->


## Features

- **Three component kits** — daisyUI, Catalyst UI and shadcn-style, each an independent NuGet package
- **A starter template** — `dotnet new` dashboard shell with Tailwind, transitions and Heroicons
- **A setup CLI** — automates Tailwind CSS 4 wiring for Blazor / ASP.NET Core
- **Shared tooling** — one build, CI, and versioning across the whole family

## Getting Started

```bash
git clone https://github.com/phmatray/blazor-tailwind-ui.git
cd blazor-tailwind-ui/kits/daisyblazor   # or catalyst-ui / shadbz
dotnet build
```

## Table of Contents

- [Structure](#structure)
- [Which kit should I use?](#which-kit-should-i-use)
- [History](#history)
- [Tech Stack](#tech-stack)
- [Roadmap](#roadmap)
- [License](#license)
- [Contributing](#contributing)

<!-- portfolio-toc:end -->



> Everything for building **Tailwind CSS + Blazor** UIs — three component kits, a
> starter template, and a setup CLI, in one workspace.

This monorepo consolidates a family of previously-separate repositories. Each kit
targets a different design system, so pick the one that fits your project — they
share tooling, CI, and versioning but ship as independent packages.

## Structure

| Path | What it is | From |
|---|---|---|
| `kits/daisyblazor` | Tailwind v4 + **daisyUI v5** kit — 60+ components, MudBlazor-compatible API, theming, charts, `dotnet new` template | `phmatray/daisyblazor` |
| `kits/catalyst-ui` | **Catalyst UI Kit** components for Blazor (.NET 9), accessible | `phmatray/CatalystUI` |
| `kits/shadbz` | **shadcn**-inspired components (internal name Radixor) | `phmatray/ShadBz` |
| `starter/` | A `dotnet new` **starter template** — Tailwind UI dashboard shell, transitions, Heroicons | `phmatray/BlazorTailwind` |
| `tools/tailwind-toolbox` | A **CLI** that automates Tailwind CSS 4 setup for Blazor / ASP.NET Core | `phmatray/TailwindToolbox` |

## Which kit should I use?

- **daisyblazor** — the most complete; great if you like daisyUI's semantic classes and want a MudBlazor-like API.
- **catalyst-ui** — if you own a Catalyst UI Kit license and want its exact look.
- **shadbz** — if you prefer the shadcn/Radix primitives style.

## History

Each subfolder was merged with **full git history preserved** (`git subtree`).
The original repositories are archived and redirect here.

<!-- portfolio-techstack:start -->

## Tech Stack

- **.NET 10 · .NET 9**
- Microsoft.AspNetCore.Components.Web
- Microsoft.AspNetCore.Components.WebAssembly
- Microsoft.AspNetCore.Components.WebAssembly.DevServer

<!-- portfolio-techstack:end -->

<!-- portfolio-roadmap:start -->

## Roadmap

Planned work and known limitations are tracked in the [open issues](https://github.com/phmatray/blazor-tailwind-ui/issues). Contributions toward them are welcome.

<!-- portfolio-roadmap:end -->

## License

MIT — see [`LICENSE`](LICENSE). Individual kits retain any license declared in their subfolder.

---

<!-- portfolio-sections:start -->

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork the repository and create your branch (`git checkout -b feat/my-feature`)
2. Commit your changes (`git commit -m 'feat: ...'`)
3. Push the branch and open a Pull Request

<!-- portfolio-sections:end -->
