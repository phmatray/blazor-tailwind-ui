---
title: "ISnackbar"
description: "MudBlazor-compatible snackbar contract."
---

MudBlazor-compatible snackbar contract. `Snackbar.Add("msg", Severity.Success)` stays working.

## Properties

### `Shown`

Currently visible toasts.

## Methods

### `Add(string, Severity, Action<SnackbarOptions>)`

Queue a new toast.

### `Clear()`

Remove all toasts.

### `Remove(SnackbarMessage)`

Remove a specific toast.

## Events

### `OnChange`

Raised whenever the visible set changes so a provider can re-render.
