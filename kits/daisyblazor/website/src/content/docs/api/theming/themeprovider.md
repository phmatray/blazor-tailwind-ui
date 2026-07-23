---
title: "ThemeProvider"
description: "daisyUI theme provider."
---

daisyUI theme provider. Manages the selected theme (any enabled daisyUI theme, or the
special `"system"` value which follows the OS light/dark setting), applies it to the
document `data-theme` attribute via `IJSRuntime`, and persists the choice
to localStorage + cookie. Cascades itself so descendants can read `ThemeProvider.Preference` /
`ThemeProvider.Theme` / `ThemeProvider.IsDarkMode` and call `SetThemeAsync(System.String)`.

Brand-neutral: the light/dark theme applied for `"system"`, the set of selectable themes,
and the set of themes considered "dark" are all parameter-driven, so any application can plug in
its own daisyUI themes without modifying this component.

## Properties

### `ChildContent`

Content rendered inside the provider; receives `this` as a cascading value.

### `CookieKey`

Cookie name for the preference (so the server can read it during SSR). Defaults to `daisy-theme`.

### `DarkModeCookieKey`

Cookie name for the resolved theme (so SSR can paint the right theme). Defaults to `daisy-darkmode`.

### `DarkThemes`

Themes considered "dark" (used to resolve `ThemeProvider.IsDarkMode`). Defaults to `ThemeProvider.DaisyUiDarkThemes`.

### `InitialIsDarkMode`

Kept for backwards compatibility; no longer used to seed state (the resolved theme cookie is preferred).

### `InitialPreference`

Initial preference read by the consumer (typically from the server-side cookie during SSR) to avoid
a flash of the wrong theme on first paint. May be a theme name or `"system"`. Pass `null` to skip.

### `IsDarkMode`

Whether the applied theme is a dark theme.

### `OnThemeChanged`

Raised after the applied theme changes (e.g. to refresh a theme picker).

### `Preference`

Current selection: a theme name, or `"system"`.

### `StorageKey`

localStorage key for the saved preference. Defaults to `daisy-theme`.

### `SystemDarkTheme`

Theme applied for `ThemeProvider.SystemPreference` when the OS is in dark mode. Defaults to `dark`.

### `SystemLightTheme`

Theme applied for `ThemeProvider.SystemPreference` when the OS is in light mode. Defaults to `light`.

### `Theme`

The daisyUI theme actually applied (resolves `"system"` to a concrete theme).

### `Themes`

All selectable themes, in display order. Must stay in sync with the `themes:` list enabled
in your Tailwind/daisyUI css. Defaults to all 35 built-in daisyUI themes (`ThemeProvider.DaisyUiThemes`).

## Methods

### `OnAfterRenderAsync(bool)`

### `OnInitialized()`

### `ResolveTheme(string, bool)`

Resolves a preference (theme name or "system") to a concrete daisyUI theme.

### `SetThemeAsync(string)`

Selects a theme and persists it. Pass any value from `ThemeProvider.Themes`, or
`ThemeProvider.SystemPreference` to follow the OS setting.

## Fields

### `_preference`

The persisted choice: a theme name, or `ThemeProvider.SystemPreference`.

### `_resolved`

The actual daisyUI theme currently applied to `data-theme`.

### `DaisyUiDarkThemes`

The built-in daisyUI themes that use a dark color scheme. Default for `ThemeProvider.DarkThemes`.

### `DaisyUiThemes`

The 35 built-in daisyUI v5 themes, in display order. Used as the default for `ThemeProvider.Themes`.

### `SystemPreference`

The special preference value that follows the OS light/dark setting.
