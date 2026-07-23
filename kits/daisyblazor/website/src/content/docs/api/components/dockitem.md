---
title: "DockItem"
description: "API reference for DockItem."
---

## Properties

### `Active`

Marks this item as the currently active/selected dock entry.

### `Href`

When set the item renders as an <a> element pointing to this URL; otherwise a <button> is rendered.

### `Icon`

Material Symbol ligature name (e.g. Icons.Material.Filled.Home). When set, renders an `DockItem.Icon` child.

### `Label`

Optional text label rendered below the icon via the daisyUI.dock-label class.

### `OnClick`

Click handler used when the item is rendered as a <button> (ignored when Href is set).
