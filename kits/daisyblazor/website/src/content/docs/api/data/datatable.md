---
title: "DataTable<T>"
description: "Reusable DataGrid<T> wrapper with sensible defaults for server-side paging, sort state, skeleton loading, and an empty-state slot."
---

Reusable `DataGrid<T>` wrapper with sensible defaults for server-side
paging, sort state, skeleton loading, and an empty-state slot.

## Type parameters

- `T` — Row item type.

## Properties

### `Actions`

Optional per-row action template rendered as the last column.

### `Class`

Additional CSS classes applied to the grid.

### `Columns`

Column declarations (use `PropertyColumn` / `TemplateColumn`).

### `Dense`

When true, applies dense styling.

### `Elevation`

Elevation for the grid surface.

### `EmptyIcon`

Icon shown in the empty state. Defaults to `Filled.SearchOff`.

### `EmptyMessage`

Empty-state message. Defaults to `"No records found"`; pass a localized string to override.

### `Items`

Local items source. Mutually exclusive with `DataTable<T>.ServerData`.

### `Loading`

When true, replaces row content with skeleton placeholders.

### `PageSizeOptions`

Page-size options shown in the pager.

### `ServerData`

Server-data loader. Mutually exclusive with `DataTable<T>.Items`.

### `SkeletonRows`

Number of skeleton rows rendered while `DataTable<T>.Loading`.

### `SortDefinitions`

Current sort definitions (proxied from the underlying grid).

### `Style`

Inline style applied to the grid.

### `UserAttributes`

Arbitrary attributes splatted onto the grid.

## Methods

### `ReloadServerDataAsync()`

Forces the underlying grid to re-call `DataTable<T>.ServerData`.
