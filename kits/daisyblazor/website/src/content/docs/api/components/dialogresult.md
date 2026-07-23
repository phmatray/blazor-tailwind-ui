---
title: "DialogResult"
description: "MudBlazor-compatible result returned when a dialog closes."
---

MudBlazor-compatible result returned when a dialog closes.

## Properties

### `Canceled`

`true` when the dialog was dismissed without confirming.

### `Data`

Payload returned by the dialog (null when canceled or no data).

### `DataType`

CLR type of `DialogResult.Data` when known.

## Methods

### `Cancel()`

Create a canceled result.

### `Ok()`

Create a successful result with no payload.

### `Ok<T>(T)`

Create a successful result carrying `data`.
