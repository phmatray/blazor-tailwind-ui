---
title: "BreadcrumbDefinition"
description: "A single un-localized breadcrumb entry returned by an IBreadcrumbSource."
---

A single un-localized breadcrumb entry returned by an `IBreadcrumbSource`.

## Properties

### `Href`

Navigation href. `null` means the current page (non-clickable).

### `Icon`

Optional Material Symbols ligature rendered before the text.

### `TitleKey`

Resource key resolved via `IStringLocalizer<TResource>` to the display text.

## Methods

### `BreadcrumbDefinition(string, string, string)`

A single un-localized breadcrumb entry returned by an `IBreadcrumbSource`.

- `TitleKey` — Resource key resolved via `IStringLocalizer<TResource>` to the display text.
- `Href` — Navigation href. `null` means the current page (non-clickable).
- `Icon` — Optional Material Symbols ligature rendered before the text.
