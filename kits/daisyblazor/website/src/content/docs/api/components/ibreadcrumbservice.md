---
title: "IBreadcrumbService"
description: "Service for generating breadcrumbs based on the current route."
---

Service for generating breadcrumbs based on the current route.

## Methods

### `GetBreadcrumbs(string)`

Gets breadcrumb items for the given route, suitable for a daisyUI breadcrumbs trail.

- `currentRoute` — The current page route (e.g. `"/vouchers/lounge/42"`).

**Returns:** A localized list of breadcrumb items starting with Home, or an empty list if no feature handles the route.
