---
title: "CssClass"
description: "Maps the MudBlazor-compatible enums to daisyUI / Tailwind utility classes."
---

Maps the MudBlazor-compatible enums to daisyUI / Tailwind utility classes.
Returned strings are complete literal class names so the Tailwind build picks them up
(see the `@source inline(...)` safelist in Styles/main.css for dynamic combinations).

## Methods

### `AlertSeverity(Severity)`

Alert color class for a `Severity`, e.g. "alert-error".

### `AlignItemsClass(AlignItems)`

align-items utility for `AlignItems`.

### `BadgeColor(Color)`

Badge color class, e.g. "badge-success".

### `BadgeSize(Size)`

Badge size class.

### `ButtonColor(Color)`

Button color class, e.g. "btn-primary".

### `ButtonSize(Size)`

Button size class.

### `ButtonVariant(Variant)`

Button variant modifier ("", "btn-outline", "btn-ghost").

### `ColorToken(Color)`

daisyUI color suffix (e.g. "primary") for a semantic `Color`.

### `JustifyContent(Justify)`

justify-content utility for `Justify`.

### `LoadingSize(Size)`

Spinner/progress size class.

### `MaxWidthClass(MaxWidth)`

Container max-width utility.

### `Merge(string[])`

Joins non-empty class fragments with a single space.

### `TextColor(Color)`

Text color utility for a semantic color, e.g. "text-primary".

### `Typography(Typo)`

Tailwind text classes for a `Typo` scale step.

### `TypographyElement(Typo)`

HTML element for a `Typo` value.
