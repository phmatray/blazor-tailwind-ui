---
title: "IDialogReference"
description: "Handle to a shown dialog."
---

Handle to a shown dialog. Await `IDialogReference.Result` to get the outcome once the dialog closes.

## Properties

### `Result`

Completes when the dialog closes, carrying its `DialogResult`.

## Methods

### `Close()`

Programmatically close the dialog with a default successful result.

### `Close(DialogResult)`

Programmatically close the dialog with the given result.
