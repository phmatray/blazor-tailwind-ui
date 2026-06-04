---
title: "FeatureHomePage"
description: "Feature home-page scaffold: title, optional subtitle, optional header actions, nav-cards grid, optional quick-access button strip, and an additional-content slot."
---

Feature home-page scaffold: title, optional subtitle, optional header actions,
nav-cards grid, optional quick-access button strip, and an additional-content slot.
Wraps body content with an `AuthorizeView` when `FeatureHomePage.RequireAccessCheck` is true.

## Properties

### `AccessDeniedContent`

Optional custom access-denied content; defaults to a warning alert.

### `AdditionalContent`

Optional content rendered below the quick-access strip.

### `Error`

Optional error banner rendered above the nav cards.

### `Eyebrow`

Optional uppercase eyebrow (section name) shown above the title in the hero.

### `GradientEnd`

Hero gradient end colour. Only used when `FeatureHomePage.Icon` is set.

### `GradientStart`

Hero gradient start colour. Only used when `FeatureHomePage.Icon` is set.

### `HeaderActions`

Optional right-aligned action area in the header row.

### `HeroStats`

Optional stat strip rendered inside the hero (only when `FeatureHomePage.Icon` is set).

### `Icon`

Feature icon. When set, the page renders a gradient `FeatureHero`
header instead of the plain title row.

### `IsAccessGranted`

When false (with `FeatureHomePage.RequireAccessCheck`), shows the access-denied slot.

### `NavCards`

Grid of feature nav cards (use `FeatureNavCard`).

### `NavCardsTitle`

Optional title rendered above the `NavCards` grid.

### `QuickAccessButtons`

Optional quick-access strip (use `QuickAccessButton`).

### `QuickAccessTitle`

Optional title above the quick-access strip.

### `RequireAccessCheck`

When true, wraps the body in an `AuthorizeView`.

### `Subtitle`

Optional secondary line beneath the title.

### `Title`

Page title.
