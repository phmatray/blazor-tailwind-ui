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
