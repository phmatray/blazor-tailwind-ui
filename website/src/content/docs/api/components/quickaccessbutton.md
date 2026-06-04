---
title: "QuickAccessButton"
description: "Outlined full-width button with optional badge, tooltip and loading state."
---

Outlined full-width button with optional badge, tooltip and loading state.
Used as a quick-access shortcut on feature home pages.

## Properties

### `BadgeText`

Optional badge text. When set, the button is wrapped in a `Badge` and disabled.

### `Color`

Color applied to the button.

### `Disabled`

When true, disables the button.

### `Href`

Optional navigation target. When set, the button behaves as a link.

### `Icon`

Leading icon (use values from `Icons`).

### `Label`

Button label.

### `Loading`

When true, shows a progress spinner and disables the button.

### `OnClick`

Click handler. Combined with `QuickAccessButton.Href`, `QuickAccessButton.Href` wins for navigation.

### `TooltipText`

Tooltip shown over the badge when `QuickAccessButton.BadgeText` is set.
