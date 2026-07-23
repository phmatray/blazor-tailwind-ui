---
title: "GridState<T>"
description: "Snapshot of the grid's paging + sort state passed to a server-data loader."
---

Snapshot of the grid's paging + sort state passed to a server-data loader.
Member names mirror MudBlazor's `GridState<T>`.

## Type parameters

- `T` — Row item type.

## Properties

### `Page`

Zero-based page index.

### `PageSize`

Number of rows per page.

### `SortDefinitions`

Active sort definitions (in priority order).
