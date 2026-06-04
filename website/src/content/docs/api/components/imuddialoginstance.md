---
title: "IMudDialogInstance"
description: "Cascaded handle a hosted dialog component uses to close or cancel itself."
---

Cascaded handle a hosted dialog component uses to close or cancel itself.
Mirrors MudBlazor's `IMudDialogInstance` so existing dialog components migrate unchanged.

## Properties

### `Title`

Title shown in the dialog header.

## Methods

### `Cancel()`

Close the dialog as canceled.

### `Close()`

Close the dialog with a successful, payload-less result.

### `Close(DialogResult)`

Close the dialog with the supplied result.
