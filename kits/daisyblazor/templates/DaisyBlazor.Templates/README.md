# DaisyBlazor.Templates

`dotnet new` templates for [DaisyBlazor](https://github.com/phmatray/daisyblazor) — a Tailwind CSS
v4 + daisyUI v5 component kit for Blazor.

## Install

```bash
dotnet new install DaisyBlazor.Templates
```

## Templates

| Short name | Description |
|------------|-------------|
| `daisyblazor` | A Blazor Web App (Interactive Server) pre-wired with DaisyBlazor components, the DaisyBlazor.Charts SVG charts, theme switching, and the Tailwind build pipeline. |

## Use

```bash
dotnet new daisyblazor -o MyApp
cd MyApp
npm install
dotnet run
```

The generated app builds its Tailwind/daisyUI CSS automatically on `dotnet build`. See the project
`README.md` for details.
