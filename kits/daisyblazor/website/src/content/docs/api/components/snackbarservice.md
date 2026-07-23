---
title: "SnackbarService"
description: "Scoped ISnackbar implementation."
---

Scoped `ISnackbar` implementation. Holds the visible toast list and notifies
`SnackbarProvider` via `SnackbarService.OnChange`. Auto-dismiss timing is owned by the provider.

## Properties

### `Shown`

## Methods

### `Add(string, Severity, Action<SnackbarOptions>)`

### `Clear()`

### `Remove(SnackbarMessage)`

## Events

### `OnChange`
