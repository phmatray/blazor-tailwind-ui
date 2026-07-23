---
title: "FilterPanel"
description: "Standard filter container: title row, child filter inputs, active-filter chips, and apply/clear buttons."
---

Standard filter container: title row, child filter inputs, active-filter chips, and apply/clear buttons.

## Properties

### `ActiveFilters`

Optional list of currently-active filters rendered as removable chips.

### `ActiveFiltersLabel`

Prefix shown before the active-filter chips. Defaults to `"Active filters:"`.

### `ApplyLabel`

Label of the apply button in the footer. Defaults to `"Filter"`.

### `ChildContent`

Filter inputs (typically `Item` + form controls).

### `Class`

Additional CSS classes applied to the panel.

### `ClearLabel`

Label of the clear button in the footer. Defaults to `"Clear"`.

### `OnApply`

Invoked when the user clicks the apply button.

### `OnClear`

Invoked when the user clicks the clear/reset button.

### `OnRemoveFilter`

Invoked when the user closes a single active-filter chip.

### `ResetLabel`

Label of the inline reset button shown when any filters are active. Defaults to `"Reset"`.

### `Style`

Inline style applied to the panel.

### `Title`

Panel title. Defaults to `"Filters"`; pass a localized string to override.

### `UserAttributes`

Arbitrary attributes splatted onto the panel.
