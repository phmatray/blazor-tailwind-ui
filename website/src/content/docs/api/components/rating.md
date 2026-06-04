---
title: "Rating"
description: "API reference for Rating."
---

## Properties

### `Color`

Fill color for the selected stars via bg-{token} (e.g. Color.Warning → bg-warning).

### `Mask`

daisyUI mask class applied to each radio input (e.g. "mask-star-2", "mask-heart").
Defaults to "mask-star-2".

### `Max`

Number of stars to render. Defaults to 5.

### `ReadOnly`

When true the user cannot interact with the widget.

### `Size`

Size of the rating widget: maps to rating-xs / (default) / rating-sm / rating-md / rating-lg / rating-xl.

### `Value`

Current rating value (1–Max). 0 means no selection.

### `ValueChanged`

Raised when the user selects a star.
