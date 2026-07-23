# Blazor Tailwind UI

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

## License

MIT — see [`LICENSE`](LICENSE). Individual kits retain any license declared in their subfolder.
