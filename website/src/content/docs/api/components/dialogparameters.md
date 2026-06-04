---
title: "DialogParameters<T>"
description: "Strongly-typed DialogParameters for a specific dialog component T."
---

Strongly-typed `DialogParameters` for a specific dialog component
`T`. Enables the `{ x => x.Prop, value }` initializer used by MudBlazor.

## Methods

### `Add<T>(Expression<Func<T, T>>, T)`

Add or replace a parameter value using a property selector expression.
