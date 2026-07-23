---
title: "PropertyColumn<T1, T2>"
description: "A column bound to a member of the row type via an expression."
---

A column bound to a member of the row type via an expression. Mirrors MudBlazor's
`PropertyColumn<T, TProperty>`. Renders the (optionally formatted) member value.

## Type parameters

- `T` — Row item type.
- `TProperty` — Member type.

## Properties

### `Format`

Standard/custom format string applied to the value (e.g. "dd/MM/yyyy").

### `Identifier`

Sort key: the member name resolved from `PropertyColumn<T1, T2>.Property`.

### `Property`

Expression selecting the member to display/sort.

### `SortBy`

Optional explicit sort selector (overrides `PropertyColumn<T1, T2>.Property` for sorting).
