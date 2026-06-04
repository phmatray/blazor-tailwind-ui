---
title: "IDialogService"
description: "MudBlazor-compatible dialog service."
---

MudBlazor-compatible dialog service. A `DialogProvider` mounted once in the app
listens to `IDialogService.OnDialogShown` and renders the requested component.

## Methods

### `ShowAsync<T>(string, DialogParameters, DialogOptions)`

Show dialog component `T` with the given title, parameters and options.

### `ShowMessageBoxAsync(string, string, string, string, string)`

Show a built-in confirm dialog. Returns `true` for yes, `false` for no,
or `null` when canceled / dismissed.

## Events

### `OnDialogShown`

Raised when a new dialog is shown so the provider can render it.
