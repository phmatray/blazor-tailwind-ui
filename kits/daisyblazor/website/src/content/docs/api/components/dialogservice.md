---
title: "DialogService"
description: "Scoped IDialogService implementation."
---

Scoped `IDialogService` implementation. Builds a `DialogReference` per
request and raises `DialogService.OnDialogShown` for the `DialogProvider` to render.

## Methods

### `ShowAsync<T>(string, DialogParameters, DialogOptions)`

### `ShowMessageBoxAsync(string, string, string, string, string)`

## Events

### `OnDialogShown`
