---
title: "Step"
description: "API reference for Step."
---

## Properties

### `Active`

Convenience shortcut: marks the step as the currently active step.
Applies `step-secondary` when no explicit `Step.Color` is set and
`Step.Completed` is false. Ignored when `Step.Color` is not Default.

### `ChildContent`

Label text or markup rendered inside the step item.

### `Color`

Semantic color applied to the step marker (e.g. `Color.Primary` adds `step-primary`).
Defaults to `Color.Default` (no color modifier).

### `Completed`

Convenience shortcut: marks the step as completed by applying `step-primary`
when no explicit `Step.Color` is set. Ignored when `Step.Color` is not Default.

### `DataContent`

Custom marker content placed in the `data-content` attribute.
Use a single character or symbol (e.g. "✓", "?", "!").
When null the step uses its default numeric counter.
