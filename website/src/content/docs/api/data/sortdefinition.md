---
title: "SortDefinition<T>"
description: "Describes a single active sort."
---

Describes a single active sort. Mirrors MudBlazor's `SortDefinition<T>`.

## Type parameters

- `T` — Row item type.

## Properties

### `Descending`

True when sorting descending.

### `Index`

Priority index (0 = primary sort).

### `SortBy`

Identifier of the sorted column (member name / title).

### `SortFunc`

Extracts the sortable value from a row.

## Methods

### `SortDefinition(string, bool, int, Func<T, object>)`

Creates a sort definition.
