---
title: "ChartDataPoint"
description: "A single categorical data point: a Label, its numeric Value, and an optional explicit Color that overrides the cycling theme palette."
---

A single categorical data point: a `Label`, its numeric
`Value`, and an optional explicit `Color` that overrides
the cycling theme palette. Used by bar, pie and donut charts.

## Properties

### `Color`

Optional CSS color override (e.g. "var(--color-primary)", "#f00"). Falls back to the palette when null.

### `Label`

Category / slice label shown in axes, legends and tooltips.

### `Value`

Numeric value (must be finite; negatives are treated as their magnitude for circular charts).

## Methods

### `ChartDataPoint(string, double, string)`

A single categorical data point: a `Label`, its numeric
`Value`, and an optional explicit `Color` that overrides
the cycling theme palette. Used by bar, pie and donut charts.

- `Label` — Category / slice label shown in axes, legends and tooltips.
- `Value` — Numeric value (must be finite; negatives are treated as their magnitude for circular charts).
- `Color` — Optional CSS color override (e.g. "var(--color-primary)", "#f00"). Falls back to the palette when null.
