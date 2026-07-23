# @daisyblazor/tailwind

The Tailwind CSS v4 + daisyUI v5 **preset** used by the
[DaisyBlazor](https://github.com/phmatray/daisyblazor) component library.

Use this package when your CSS is built outside the .NET tooling (a plain
`tailwindcss` CLI / Vite / webpack pipeline) and you want the same daisyUI
enablement, neutral default themes, and dynamic-class safelist that DaisyBlazor
components rely on.

## Install

```bash
npm install -D tailwindcss @tailwindcss/cli daisyui @daisyblazor/tailwind
```

## Use

In your main stylesheet:

```css
@import "tailwindcss";
@import "@daisyblazor/tailwind/preset.css";

/* Let Tailwind see the classes used inside DaisyBlazor's .razor components: */
@source "../**/DaisyBlazor.Components/**/*.{razor,cs}";
```

`preset.css` enables the daisyUI plugin, registers `daisyblazor` /
`daisyblazor-dark` (plus all built-in daisyUI themes), and force-generates the
classes DaisyBlazor composes dynamically in C# so they are never purged.

### Bring your own themes (safelist only)

If your app declares its **own** daisyUI themes, importing the full `preset.css`
would also pull in the daisyUI plugin and the neutral DaisyBlazor themes. Import
just the safelist instead and keep your own `@plugin "daisyui"` block:

```css
@import "tailwindcss";
@import "@daisyblazor/tailwind/safelist.css";

@plugin "daisyui" {
    themes: mybrand --default, mybrand-dark --prefersdark;
}
@plugin "daisyui/theme" {
    name: "mybrand";
    /* --color-primary: …; etc. */
}
```

`safelist.css` contains only the `@source inline(...)` rules (no plugin, no
themes), so the DaisyBlazor components stay fully styled while your themes own
the palette. `preset.css` `@import`s it, so full-preset consumers need no change.

> Keep the `daisyui` version here in sync with the version the
> `.razor` components were tested against (see the repo's `scripts/update-deps.sh`).
