---
title: "ThemeController"
description: "API reference for ThemeController."
---

## Properties

### `Checked`

Whether this controller is currently in its active (checked) state.
For checkboxes: true = `ThemeController.Value` theme is active.
For radios: true = this option is currently selected.

### `CheckedChanged`

Raised when the checked state changes.

### `ChildContent`

Content rendered inside the wrapping label — typically a `Swap` component
showing themed icons, or a plain text label for radio variants.

### `Name`

The radio group name. Only used when `ThemeController.Type` is
`ThemeControllerType.Radio`. All radio theme-controllers that share
the same Name are mutually exclusive.

### `Type`

Whether this controller renders as a checkbox toggle or a radio button.
Defaults to `ThemeControllerType.Checkbox`.

### `Value`

The daisyUI theme name this controller activates (e.g. "dark", "cupcake", "horizon").
For the checkbox variant this is the theme applied when checked; unchecked reverts to
the page default. For the radio variant this is the theme applied when this option
is selected.
