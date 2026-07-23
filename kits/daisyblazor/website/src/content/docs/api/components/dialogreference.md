---
title: "DialogReference"
description: "Internal record describing a single shown dialog: the component type to render, its parameters, options, title and the reference whose IDialogReference.Result completes when the dialog closes."
---

Internal record describing a single shown dialog: the component type to render, its
parameters, options, title and the reference whose `IDialogReference.Result`
completes when the dialog closes.

## Properties

### `ComponentType`

Component type rendered inside the modal box.

### `Id`

Unique id of this dialog instance.

### `Options`

Display options for the modal.

### `Parameters`

Parameters forwarded to the hosted component.

### `Result`

### `Title`

## Methods

### `Cancel()`

### `Close()`

### `Close(DialogResult)`

## Events

### `OnClose`

Raised when this dialog requests to be removed from the provider.
