---
title: "ChartPalette"
description: "Resolves chart colors as daisyUI v5 theme tokens."
---

Resolves chart colors as daisyUI v5 theme tokens. daisyUI exposes each theme color as a CSS
custom property (e.g. `--color-primary`), so returning `var(--color-primary)` makes
every chart follow the active `data-theme` automatically — no recompilation, no JS.

## Methods

### `At(int, string)`

Returns `overrideColor` when provided; otherwise the palette entry at
`index` (wrapping with modulo). Safe for any non-negative index.

### `FromColor(Color)`

CSS variable for a semantic `Color`, or null for Default/Inherit.

## Fields

### `Default`

The default cycling order: primary, secondary, accent, info, success, warning, error.
Charts index into this (modulo length) so series/slices get distinct, theme-driven colors.

### `Neutral`

The neutral token, handy for axes/gridlines fallbacks.
