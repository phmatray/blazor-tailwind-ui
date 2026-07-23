---
title: "TemplateColumn<T>"
description: "A column whose cell content is supplied by an arbitrary template."
---

A column whose cell content is supplied by an arbitrary template. Mirrors MudBlazor's
`TemplateColumn<T>`. Not sortable by default; supply `TemplateColumn<T>.SortBy` to enable sorting.

## Type parameters

- `T` — Row item type.

## Properties

### `CellTemplate`

Cell template receiving a `CellContext<T>` (access the row via `context.Item`).

### `ChildContent`

Alias for `TemplateColumn<T>.CellTemplate` when content is provided as the column's body.

### `SortBy`

Optional sort selector. When set (and `Column<T>.Sortable`), the column becomes sortable.
