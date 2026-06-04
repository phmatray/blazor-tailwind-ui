---
title: "BreadcrumbService<TResource>"
description: "daisyUI-returning breadcrumb service."
---

daisyUI-returning breadcrumb service. Generic over the consumer's resource type so each
application can use its own `IStringLocalizer<TResource>`. The trail itself comes from
an app-supplied `IBreadcrumbSource`, keeping DaisyBlazor decoupled from any feature
or routing framework.

## Type parameters

- `TResource` — The resource type used to resolve localized breadcrumb titles.

## Methods

### `BreadcrumbService(IBreadcrumbSource, IStringLocalizer<T>, BreadcrumbServiceOptions)`

Creates a new `BreadcrumbService<TResource>`.

- `source` — The app-supplied source of the (un-localized) breadcrumb trail for a route.
- `localizer` — The string localizer used to resolve breadcrumb titles.
- `options` — Options controlling the Home breadcrumb's title key and icon.

### `GetBreadcrumbs(string)`
