---
title: "Countdown"
description: "API reference for Countdown."
---

## Properties

### `Label`

Optional label rendered below the countdown digit (e.g. "sec", "min", "hours").

### `Value`

The countdown value to display. Clamped to the range 0–99 (daisyUI supports 0–99).

## Methods

### `Digits(int)`

Splits `value` into an array of digit pairs suitable for rendering
multiple `Countdown` components side-by-side (e.g. hours + minutes + seconds).
Each element is clamped to 0–99. Values above 99 in any single cell are clamped.
Example: `Countdown.Digits(3723)` → `[1, 2, 3]` (hours=1, min=2, sec=3).

- `value` — Total seconds (non-negative).

**Returns:** Array of [hours, minutes, seconds], each clamped to 0–99.
