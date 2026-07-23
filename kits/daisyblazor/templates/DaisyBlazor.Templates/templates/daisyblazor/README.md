# DaisyBlazor.App

A Blazor Web App (Interactive Server) pre-wired with
[DaisyBlazor](https://github.com/phmatray/daisyblazor) — Tailwind CSS v4 + daisyUI v5 components,
theme switching, and dependency-free SVG charts.

## Run it

```bash
npm install     # restores Tailwind CLI + daisyUI + the DaisyBlazor preset (first run only)
dotnet run      # builds the CSS, then starts the app on http://localhost:5080
```

`dotnet build` / `dotnet run` automatically rebuild `wwwroot/css/app.css` via the
`BuildDaisyBlazorCss` MSBuild target (it runs `npm install` on the first build, then
`npm run build:css`). For a fast feedback loop while editing markup, run the Tailwind watcher in a
second terminal:

```bash
npm run watch:css
```

## Where things live

| Path | What it is |
|------|------------|
| `Styles/main.css` | Tailwind entry point: imports the local preset, declares `@source` globs, and the app brand layer. **Edit themes / source globs here.** |
| `Styles/preset.css` | The DaisyBlazor Tailwind/daisyUI preset (daisyUI plugin + themes + C# safelist), vendored locally so the app needs no npm dependency on DaisyBlazor. Edit freely. |
| `wwwroot/css/app.css` | Generated stylesheet (do not hand-edit). |
| `package.json` | Tailwind CLI + daisyUI (the front-end build tools). |
| `Program.cs` | `AddRazorComponents().AddInteractiveServerComponents()` + `AddDaisyBlazor()`. |
| `Components/App.razor` | Host page — links `app.css`, Material Symbols + Plus Jakarta Sans fonts. |
| `Components/Layout/MainLayout.razor` | `ThemeProvider`, navbar, theme picker, `DialogProvider` + `SnackbarProvider`. |
| `Components/Pages/Home.razor` | The sample page (hero, card, buttons, a chart). |

## Themes

The `daisyblazor` (light) and `daisyblazor-dark` themes ship in the preset, alongside all built-in
daisyUI themes. Switch at runtime via the picker in the navbar, or wire your own brand theme by
adding a `@plugin "daisyui/theme"` block to `Styles/main.css` and pointing `ThemeProvider` at it.

See the DaisyBlazor docs: getting-started, theming, css-preset, components, and charts.
