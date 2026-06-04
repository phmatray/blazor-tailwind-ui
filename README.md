<h1 align="center">🌼 DaisyBlazor</h1>

<p align="center">
  A Tailwind CSS v4 + daisyUI v5 component library for Blazor.<br/>
  60+ daisyUI-native components, a MudBlazor-compatible API, theming, dialogs, snackbars,
  a shippable CSS preset, a sample gallery, and a <code>dotnet new</code> template.
</p>

---

## Packages

| Package | Description |
|---|---|
| **DaisyBlazor.Components** | The component kit: daisyUI-native components + MudBlazor-compatible surface, `ThemeProvider`, dialog/snackbar services, and the CSS preset. |
| **DaisyBlazor.Charts** | Dependency-free SVG charts (line, bar, pie/donut, area, sparkline) themed by daisyUI — no third-party charting runtime. |
| **DaisyBlazor.Tailwind** | npm package shipping the Tailwind/daisyUI preset for non-.NET build pipelines. |
| **DaisyBlazor.Templates** | `dotnet new daisyblazor` starter template. |

## Quick start

```bash
dotnet new install DaisyBlazor.Templates
dotnet new daisyblazor -o MyApp
cd MyApp && npm install && dotnet run
```

…or add it to an existing app:

```bash
dotnet add package DaisyBlazor.Components
npm install -D tailwindcss @tailwindcss/cli daisyui
```

```csharp
// Program.cs
builder.Services.AddDaisyBlazor();
```

```css
/* Styles/main.css */
@import "tailwindcss";
@import "daisyblazor/preset.css";
@source "../../src/DaisyBlazor.Components/**/*.{razor,cs}";
```

## Repo layout

```
src/        DaisyBlazor.Components, DaisyBlazor.Charts, DaisyBlazor.Tailwind
samples/    DaisyBlazor.Gallery — a runnable showcase of every component
templates/  DaisyBlazor.Templates — dotnet new template
tests/      bUnit component tests
docs/       getting started, theming, CSS preset, MudBlazor migration, component reference
scripts/    update-deps, build-css, pack
```

## Documentation

See [`docs/`](docs/) for getting started, theming, the CSS preset, the MudBlazor migration guide,
and the per-component reference.

## License

[MIT](LICENSE)
