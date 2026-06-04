---
title: "DaisyBlazorServiceCollectionExtensions"
description: "Extension methods that wire the DaisyBlazor component services into DI."
---

Extension methods that wire the DaisyBlazor component services into DI.

## Methods

### `AddDaisyBlazor(IServiceCollection)`

Registers the core DaisyBlazor services: `ISnackbar` and
`IDialogService` (both scoped). Call this from `Program.cs`.

- `services` — The service collection to add to.

**Returns:** The service collection for chaining.

### `AddDaisyBlazorBreadcrumbs<T>(IServiceCollection, Action<BreadcrumbServiceOptions>)`

Registers the daisyUI-returning breadcrumb service, localised via
`IStringLocalizer<TResource>`. The app must also register an
`IBreadcrumbSource` (see the overload that registers one for you).

- `services` — The service collection to add to.
- `configure` — Optional callback to customise the options (home icon, home title key).

**Returns:** The service collection for chaining.

### `AddDaisyBlazorBreadcrumbs<T1, T2>(IServiceCollection, Action<BreadcrumbServiceOptions>)`

Registers the breadcrumb service together with the app's `IBreadcrumbSource`
implementation (scoped).

- `services` — The service collection to add to.
- `configure` — Optional callback to customise the options (home icon, home title key).

**Returns:** The service collection for chaining.
