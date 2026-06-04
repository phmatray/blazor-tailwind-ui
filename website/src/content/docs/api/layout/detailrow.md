---
title: "DetailRow"
description: "Single label/value table row meant to be used inside a DetailCard."
---

Single label/value table row meant to be used inside a `DetailCard`.
Provide either `DetailRow.Value` for plain text or `DetailRow.ValueContent` for richer rendering.

## Properties

### `Class`

Optional CSS class forwarded to the row.

### `Label`

Label shown in the left column (secondary color).

### `Style`

Optional inline style forwarded to the row.

### `UserAttributes`

Unmatched attributes splatted onto the row.

### `Value`

Plain-text value shown in the right column when `DetailRow.ValueContent` is null.

### `ValueContent`

Richer right-column content (e.g. status chip, multi-element value). Overrides `DetailRow.Value`.
