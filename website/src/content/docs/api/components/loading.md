---
title: "Loading"
description: "API reference for Loading."
---

## Properties

### `Color`

Semantic color applied as a Tailwind text-* utility (e.g. Color.Primary → "text-primary").

### `Size`

Size of the indicator using the standard Size enum (Small → loading-sm, Medium → loading-md, Large → loading-lg).
For xs or xl pass `Loading.SizeOverride` instead.

### `SizeOverride`

Raw daisyUI size token when xs or xl is needed ("loading-xs" or "loading-xl").
When set, takes precedence over `Loading.Size`.

### `Type`

Animation style of the loading indicator.
