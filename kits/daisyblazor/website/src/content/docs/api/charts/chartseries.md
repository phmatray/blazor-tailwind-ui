---
title: "ChartSeries"
description: "A named numeric series for multi-series line, area and bar charts."
---

A named numeric series for multi-series line, area and bar charts. The ordinal index of each
value lines up with the chart's X labels.

## Properties

### `Color`

Optional CSS color override; falls back to the cycling palette when null.

### `Data`

Ordered values; index `i` aligns with X label `i`.

### `Name`

Series display name (legend / tooltip label).

## Methods

### `ChartSeries()`

Creates an empty series.

### `ChartSeries(string, IReadOnlyList<double>, string)`

Creates a named series from a sequence of values.
