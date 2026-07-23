---
title: "IBreadcrumbSource"
description: "Supplies the raw (un-localized) breadcrumb trail for a route."
---

Supplies the raw (un-localized) breadcrumb trail for a route. Implemented by the consuming
application so that `BreadcrumbService<TResource>` stays free of any feature/routing
framework: plug in feature modules, a route table, attributes, or a hard-coded map — DaisyBlazor
only needs the ordered list of `BreadcrumbDefinition` for the current route.

## Methods

### `GetBreadcrumbs(string)`

Returns the breadcrumb trail for `currentRoute`, excluding the Home crumb
(which `BreadcrumbService<TResource>` prepends). Return an empty list when the
route has no trail.

- `currentRoute` — The current page route (e.g. `"/vouchers/lounge/42"`).
