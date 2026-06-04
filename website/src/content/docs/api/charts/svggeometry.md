---
title: "SvgGeometry"
description: "Pure-math helpers that turn point lists into SVG path data."
---

Pure-math helpers that turn point lists into SVG path data. No DOM, no JS — every chart
component composes its geometry from these so the math stays in one testable place.

## Methods

### `ArcPath(double, double, double, double, double, double, CultureInfo)`

Builds an SVG arc/wedge path for a slice spanning [`startRad`,
`endRad`]. When `innerRadius` > 0 a donut ring segment
is produced; otherwise a pie wedge from the center.

### `PointOnCircle(double, double, double, double)`

Point on a circle of `radius` at `angleRad` (0 = 12 o'clock, clockwise).

### `PolyPath(IReadOnlyList<ValueTuple<double, double>>, CultureInfo)`

Builds a straight "M …L …" polyline path through the given points.

### `SmoothPath(IReadOnlyList<ValueTuple<double, double>>, CultureInfo)`

Builds a smoothed cubic-Bézier path through the points using Catmull-Rom-to-Bézier
conversion (tension 1). Falls back to a straight path for fewer than three points.
