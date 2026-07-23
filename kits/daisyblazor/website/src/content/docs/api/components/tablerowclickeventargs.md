---
title: "TableRowClickEventArgs<T>"
description: "Mirrors MudBlazor's TableRowClickEventArgs<T> so existing OnRowClick=\"@(args =>..."
---

Mirrors MudBlazor's TableRowClickEventArgs<T> so existing
`OnRowClick="@(args =>... args.Item...)"` handlers keep compiling.

## Properties

### `Item`

The clicked row's data item (may be null).

### `RowIndex`

Zero-based index of the clicked row in the current page.
