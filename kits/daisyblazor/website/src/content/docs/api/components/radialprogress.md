---
title: "RadialProgress"
description: "API reference for RadialProgress."
---

## Properties

### `ChildContent`

Custom inner content; defaults to "@Value%" when not provided.

### `Color`

Color token for the ring and label text (maps to text-primary, text-success, etc.).

### `SizeRem`

CSS size of the radial element (e.g. "5rem", "80px"). Maps to the CSS --size custom property.
Defaults to "5rem".

### `Thickness`

CSS thickness of the progress ring (e.g. "4px", "0.5rem"). Maps to the CSS --thickness custom property.
When null the daisyUI default thickness is used.

### `Value`

Progress value between 0 and 100. Maps to the CSS --value custom property.
