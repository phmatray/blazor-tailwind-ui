---
title: "SvgText"
description: "Renders an SVG <text> element from code."
---

Renders an SVG `<text>` element from code. Razor reserves the literal
`<text>` markup tag as its template-transition directive, so chart components emit
axis/legend labels through this helper to sidestep the parser while keeping pure inline SVG.

## Properties

### `Anchor`

Text anchor (start | middle | end).

### `Fill`

Fill color (CSS color or theme token).

### `FillOpacity`

Fill opacity (0..1).

### `FontSize`

Font size in SVG user units.

### `FontWeight`

Optional font weight (e.g. "600").

### `Text`

The text content to render.

### `X`

X coordinate in SVG user units.

### `Y`

Y coordinate in SVG user units.
