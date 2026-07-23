---
title: "Column<T>"
description: "Abstract base for a DataGrid<T> column."
---

Abstract base for a `DataGrid<T>` column. Concrete columns
(`PropertyColumn<T1, T2>`, `TemplateColumn<T>`)
register themselves with the owning grid on first render.

## Type parameters

- `T` — Row item type.

## Properties

### `CanSort`

Whether this column can actually be sorted (sortable AND a sort func exists).

### `CellClass`

Optional CSS class applied to every body cell in this column.

### `HeaderClass`

Optional CSS class applied to the header cell.

### `Identifier`

Stable identifier used as the sort key. Defaults to `Column<T>.Title`.

### `Owner`

Owning grid, supplied via cascading value.

### `Sortable`

Whether the column is sortable.

### `Title`

Column header text.

## Methods

### `GetSortFunc()`

Returns the value used to sort rows by this column, or null when not sortable.

### `RenderCell(T)`

Renders the cell content for the given row.
