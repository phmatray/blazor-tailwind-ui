---
title: "FeatureNavCard"
description: "Feature-navigation card with avatar, title, subtitle, description and an optional \"go to\" action button."
---

Feature-navigation card with avatar, title, subtitle, description and an optional
"go to" action button.

## Properties

### `ActionColor`

Color of the action button. Defaults to `FeatureNavCard.AvatarColor`.

### `ActionLabel`

Optional action-button label (only rendered when `FeatureNavCard.Href` is set).

### `AvatarColor`

Color of the leading avatar.

### `ChildContent`

Optional extra content rendered below the description.

### `Description`

Body description of the feature.

### `GradientEnd`

Optional avatar gradient end. See `FeatureNavCard.GradientStart`.

### `GradientStart`

Optional avatar gradient start. When both gradient colours are set, the avatar
renders as a gradient tile (echoing the feature hero) instead of a flat
`FeatureNavCard.AvatarColor` avatar.

### `Href`

Navigation target. When set, the whole card becomes clickable.

### `Icon`

Icon inside the avatar.

### `Subtitle`

Subtitle shown beneath the title.

### `Title`

Card title.

## Methods

### `FeatureNavCard(NavigationManager)`

Feature-navigation card with avatar, title, subtitle, description and an optional
"go to" action button.
