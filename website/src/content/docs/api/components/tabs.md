---
title: "Tabs"
description: "API reference for Tabs."
---

## Properties

### `ActiveIndex`

Zero-based index of the currently active tab.

### `ActiveIndexChanged`

Raised when the active index changes.

### `ChildContent`

The tab definitions (one or more `Tab` components).

### `Size`

Size of the tab buttons.

### `Variant`

Visual style of the tab strip (Box, Border, or Lift).

## Methods

### `RegisterTab(Tab)`

Called by each `Tab` during OnInitialized to register itself.

### `SetActiveIndexAsync(int)`

Activates the tab at the given zero-based index programmatically.

### `UnregisterTab(Tab)`

Called by each `Tab` during Dispose to unregister itself.
