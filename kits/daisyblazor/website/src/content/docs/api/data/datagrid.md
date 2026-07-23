---
title: "DataGrid<T>"
description: "daisyUI replacement for MudDataGrid<T>."
---

daisyUI replacement for `MudDataGrid<T>`. Renders a `.table` with a sortable
header and a paged body. Supports client-side paging/sort over `DataGrid<T>.Items` or
server-side data via `DataGrid<T>.ServerData` / `DataGrid<T>.ServerDataFunc`.

## Type parameters

- `T` — Row item type.

## Properties

### `Bordered`

Render outer/inner borders.

### `ChildContent`

Alternative slot for column declarations.

### `Class`

CSS class applied to the table wrapper.

### `Columns`

Column declarations.

### `Dense`

Compact row styling.

### `EffectivePageSize`

Effective rows per page.

### `Elevation`

Elevation (accepted for MudBlazor parity; mapped to a shadow class).

### `FirstItemIndex`

Index of the first row shown on the current page (1-based; 0 when empty).

### `Hover`

Highlight rows on hover.

### `IsServerData`

True when bound to a server-data source.

### `Items`

Local item source. Mutually exclusive with the server-data callbacks.

### `LastItemIndex`

Index of the last row shown on the current page.

### `Loading`

Loading state (shows `DataGrid<T>.LoadingContent` when set, else skeleton rows).

### `LoadingContent`

Content shown while `DataGrid<T>.Loading` is true.

### `NoRecordsContent`

Content shown when there are no rows.

### `Page`

Current zero-based page index.

### `PageCount`

Total number of pages.

### `PagerContent`

Pager content (typically a `DataGridPager<T>`).

### `PageSize`

Alias for `DataGrid<T>.RowsPerPage`.

### `PageSizeOptions`

Page-size options offered by the pager.

### `ReadOnly`

Read-only flag (accepted for MudBlazor parity; no effect on rendering).

### `RowClick`

Raised when a row is clicked.

### `RowsPerPage`

Rows per page. `DataGrid<T>.PageSize` is an alias.

### `RowStyle`

Inline style applied to each body row (e.g. "cursor: pointer;").

### `ServerData`

Server-data loader (with cancellation). Mirrors the wrapper's signature.

### `ServerDataFunc`

Server-data loader (no cancellation token).

### `SkeletonRows`

Number of skeleton rows rendered while loading without a `DataGrid<T>.LoadingContent`.

### `SortDefinitions`

Active sort definitions keyed by column identifier (mirrors MudBlazor).

### `Striped`

Zebra striping.

### `Style`

Inline style applied to the table wrapper.

### `TotalItems`

Total number of rows across all pages.

## Methods

### `AddColumn(Column<T>)`

Registers a column (called by child columns on init).

### `ReloadServerData()`

Forces a reload from the server-data source (no-op for client-side data).

### `SetPageAsync(int)`

Sets the current page and refreshes the body.

### `SetPageSizeAsync(int)`

Changes the page size, resets to the first page, and refreshes.

### `UsePageSizeOptions(int[])`

Lets a child pager supply the page-size options without externally setting the parameter.
