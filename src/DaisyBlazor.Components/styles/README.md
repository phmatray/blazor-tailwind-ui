# DaisyBlazor styles

`preset.css` is a Tailwind v4 + daisyUI v5 preset shipped with the package. It enables daisyUI,
registers two neutral default themes (`daisyblazor` / `daisyblazor-dark`) plus all built-in daisyUI
themes, and force-generates the classes DaisyBlazor composes dynamically in C# (so they are never
purged).

## Consumer wiring

In your app's source stylesheet (e.g. `Styles/main.css`):

```css
@import "tailwindcss";
@import "daisyblazor/preset.css";
/* Let Tailwind see the literal classes used INSIDE packaged .razor components: */
@source "../../**/DaisyBlazor.Components/**/*.{razor,cs}";
```

The `@source` glob must resolve to the DaisyBlazor component sources. Pick whichever matches your
setup:

- **Project reference (monorepo / sample):** point at the relative project path, e.g.
  `@source "../../src/DaisyBlazor.Components/**/*.{razor,cs}";`
- **NuGet package:** point at the restored package's `staticwebassets`/`contentFiles` under your
  NuGet cache, or copy the preset locally. The `@source inline(...)` safelist in `preset.css` already
  covers the dynamically-computed classes; the glob covers literal classes in component markup.

## Custom themes

Add your own daisyUI theme blocks after the import and reference them in `ThemeProvider`:

```css
@plugin "daisyui/theme" {
    name: "mybrand";
    default: true;
    /* --color-primary: ...; etc. */
}
```

```razor
<ThemeProvider SystemLightTheme="mybrand" SystemDarkTheme="daisyblazor-dark"
               Themes="@(new[] { "mybrand", "daisyblazor-dark", "dark", "light" })">
    ...
</ThemeProvider>
```

## Required webfont

The `Icon` component and the `Icons.Material.*` constants render
[Material Symbols](https://fonts.google.com/icons) ligatures. Load the font once in your host page:

```html
<link rel="stylesheet"
      href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined" />
```

### Using a different icon font or icon set

The Material Symbols font is the default, not a hard requirement — there are two opt-outs.

**1. Swap the icon font** by cascading an `IconOptions` near the app root. Every component that
renders icons (`Pagination`, `Alert`, `Menu`, …) inherits the new `FontClass`:

```razor
<CascadingValue Value="_icons" IsFixed="true">
    <Router ... />
</CascadingValue>

@code { readonly IconOptions _icons = new() { FontClass = "material-symbols-rounded" }; }
```

Because Material Symbols renders through **ligatures** (the span text `chevron_left` becomes a
glyph), this swap is transparent only for a *ligature-compatible* font that shares Material's
ligature names — e.g. another Material Symbols build or variant (`-outlined` / `-rounded` /
`-sharp`), or a self-hosted Material Symbols. The built-in composites (`Pagination`, `Alert`, …)
pass `Icons.Material.*` ligature names, so an **arbitrary** icon set (Lucide, Heroicons, …) is
*not* yet wired through them — overriding those built-in icons is a separate, not-yet-implemented
step. For your own icons, use the SVG escape below.

**2. Render inline SVG** by passing child content to `Icon` instead of a ligature `Name`
(the icon-font class is then omitted entirely). This covers any `<Icon>` you author directly:

```razor
<Icon><svg viewBox="0 0 24 24" class="w-6 h-6">...</svg></Icon>
```

> Notes: `Size` is ignored when child content is supplied — size the SVG yourself (e.g. `w-6 h-6`).
> The hero-badge and nav-card icon-sizing CSS still targets `.material-symbols-outlined`, so a
> fully custom icon set may need to restyle those two spots.
