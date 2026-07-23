---
title: "AppCard"
description: "Compact gradient-icon card used as an app/shortcut tile on the home page."
---

Compact gradient-icon card used as an app/shortcut tile on the home page.

## Properties

### `Badge`

Optional badge content rendered in the corner.

### `GradientEnd`

Ending color of the icon-tile gradient.

### `GradientStart`

Starting color of the icon-tile gradient.

### `Icon`

Icon shown inside the gradient tile.

### `IsExternal`

When true, navigation opens `AppCard.Route` in a new browser tab.

### `Name`

Displayed name beneath the icon.

### `OnCardClick`

Optional explicit click handler. Overrides `AppCard.Route` navigation.

### `Route`

Navigation target on click. Ignored when `AppCard.OnCardClick` is set.

## Methods

### `AppCard(NavigationManager, IJSRuntime)`

Compact gradient-icon card used as an app/shortcut tile on the home page.
