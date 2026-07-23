---
title: CSS preset
description: What preset.css ships — the daisyUI plugin, registered themes, the per-extension @source glob rule, and the C# class safelist.
---

DaisyBlazor ships a Tailwind CSS v4 + daisyUI v5 **preset** — `preset.css` — that does three jobs so
your app doesn't have to:

1. **Enables the daisyUI plugin** (pinned to **daisyUI 5.5.20**) with `logs: false`.
2. **Registers themes**: the two neutral DaisyBlazor defaults — `daisyblazor` (light, the default) and
   `daisyblazor-dark` (dark, `prefers-color-scheme: dark`) — plus all built-in daisyUI v5 themes
   (`light`, `dark`, `cupcake`, `synthwave`, `dracula`, `nord`, `business`, `night`, `dim`, `sunset`,
   `abyss`, `silk`, …).
3. **Safelists the C#-computed classes** so they survive Tailwind's purge (see below).

## Wiring it into your app

Your app's source stylesheet (`Styles/main.css`) imports Tailwind, then the preset, then declares the
`@source` globs Tailwind uses to discover the classes you actually use:

```css
@import "tailwindcss";
@import "@daisyblazor/tailwind/preset.css";

@source "./Components/**/*.razor";
@source "./Components/**/*.cs";
```

Compile it with the Tailwind CLI (`npm run build:css` / `watch:css`) and link the output in your host
page. The DaisyBlazor template wires all of this for you.

## The per-extension `@source` rule

Tailwind v4 source globs are **literal globs, not brace expansions**. A pattern like
`*.{razor,cs}` does **not** work — Tailwind treats `{razor,cs}` as a literal directory/file token, so
nothing matches. Always write **one `@source` line per file extension**:

```css
/* ✅ correct */
@source "./Components/**/*.razor";
@source "./Components/**/*.cs";

/* ❌ silently matches nothing */
@source "./Components/**/*.{razor,cs}";
```

## The comment-scanning gotcha

Tailwind v4 scans for `@source` and `@import` **tokens even inside CSS comments**. If you put a literal
`@source "..."` or `@import "..."` at-rule inside a `/* … */` block as documentation, Tailwind will try
to resolve it and can fail the build (or pull in unexpected files).

Never place those at-rule literals inside comments. When you want to *document* an optional source glob
in a comment, write it as plain prose (e.g. `source "<path>"` without the leading `@`) and tell the
reader to turn it into a real at-rule.

## The C# safelist

Many DaisyBlazor components compose their daisyUI classes **dynamically in C#** (in `CssClass.cs`,
`Grid`, `Item`, `Stack`, status/loading helpers, etc.). Tailwind's static analyzer can't see a class
that only exists as a runtime-concatenated string, so the preset force-generates them with
`@source inline(...)` blocks — color/size variants (`btn-primary`, `alert-soft`, `loading-spinner`,
`step-success`, …), the 12-column grid/span/gap utilities, `mask-*`, `tooltip-*`, `chat-bubble-*`, and
more.

Because this safelist ships **inside the preset**, you get those classes for free — you do **not** need
to add a `@source` glob pointing at the DaisyBlazor.Components package just to keep them. The two app
globs above are enough for almost every app. The only time you'd add a glob at the package is if you use
a packaged component that relies on a plain Tailwind utility nothing in *your* markup references; point
it at the restored NuGet package in your local cache (path is machine-specific):

```css
@source "<HOME>/.nuget/packages/daisyblazor.components/0.1.0/**/*.razor";
@source "<HOME>/.nuget/packages/daisyblazor.components/0.1.0/**/*.cs";
```

## The `@daisyblazor/tailwind` npm package

So the preset is resolvable by name (rather than by a fragile relative path into a NuGet cache),
DaisyBlazor publishes the preset as the npm package **`@daisyblazor/tailwind`**. Add it to your
`devDependencies` and import it as `@daisyblazor/tailwind/preset.css`:

```json
{
  "devDependencies": {
    "@daisyblazor/tailwind": "0.1.0",
    "@tailwindcss/cli": "^4.1.0",
    "daisyui": "5.5.20",
    "tailwindcss": "^4.1.0"
  }
}
```

```css
@import "@daisyblazor/tailwind/preset.css";
```

> Alternatively, copy `preset.css` out of the DaisyBlazor.Components NuGet package's
> `staticwebassets/styles` folder and import it by relative path.

## The required Material Symbols webfont

The `Icon` component and the `Icons.Material.*` constants render
[Material Symbols](https://fonts.google.com/icons) **ligatures** — the font turns a name like
`check_circle` into the glyph. The preset can't bundle a webfont, so load it once in your host page
(`App.razor` / `index.html`):

```html
<link rel="stylesheet"
      href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined" />
```

Without it, icons render as their literal ligature text.

## Monorepo / workspace node_modules note

The Tailwind CLI resolves the daisyUI plugin (referenced by the preset) through Node's normal module
resolution. In a single app that's just the app's own `node_modules`. In a **monorepo** where the CSS is
built from a nested project, Node walks up the directory tree — so daisyUI must be resolvable from
either the project's `node_modules` **or** a workspace/root `node_modules`. This repo keeps a
root-level `package.json` with `daisyui`/`tailwindcss` for exactly that reason: it lets the shipped
preset's `@plugin "daisyui"` resolve during local CSS builds of the samples. If a build fails with
"cannot find module 'daisyui'", add daisyUI to the nearest `node_modules` the CLI can see.

## See also

- [Getting started](/daisyblazor/getting-started/) — full install + wiring walkthrough.
- [Theming](/daisyblazor/theming/) — the themes the preset registers and how to switch / extend them.
