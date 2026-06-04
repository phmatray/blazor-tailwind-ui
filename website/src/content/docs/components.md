---
title: Component reference
description: The full DaisyBlazor component catalogue — daisyUI v5 wrapped as Blazor components, grouped by category, with daisyUI root classes and MudBlazor compatibility flags.
---

DaisyBlazor wraps [daisyUI v5](https://daisyui.com/components/) as Blazor components in the
`DaisyBlazor` namespace (with `.Data`, `.Feedback`, `.Layout`, `.Theming` sub-namespaces for the
higher-level helpers). The tables below are grouped by daisyUI's own component categories.

- **DaisyBlazor type** — the Blazor component name (use it as a tag, e.g. `<Button>`).
- **daisyUI root class** — the underlying daisyUI class the component renders.
- **Compat** — ● components with a MudBlazor-compatible API (rename `MudX` → `X`; see the
  [MudBlazor migration guide](/daisyblazor/migration/)). The rest are daisyUI-native.

> Most components accept `Class` / `Style` plus an unmatched-attribute splat, and the styled ones take
> `Color` / `Size` / `Variant` where it makes sense (mapped to daisyUI tokens). The neutral `daisyblazor`
> / `daisyblazor-dark` themes apply via [`ThemeProvider`](/daisyblazor/theming/).

## Actions

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `Button` | `btn` | ● |
| `IconButton` | `btn btn-square`/`btn-circle` | ● |
| `ButtonGroup` | `join` | ● |
| `Fab` / `FabAction` | `btn btn-circle` (floating) | ● |
| `Dropdown` | `dropdown` | ● |
| `Swap` | `swap` | |
| `ThemeController` | `theme-controller` | |
| `ToggleGroup` / `ToggleItem` | `join` (toggle buttons) | |

## Data display

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `Avatar` | `avatar` | ● |
| `Badge` | `badge` | ● |
| `Card` / `CardHeader` / `CardContent` / `CardActions` | `card` | ● |
| `Carousel` / `CarouselItem` | `carousel` | |
| `ChatBubble` | `chat` | |
| `Chip` | `badge` (closable) | ● |
| `Countdown` | `countdown` | |
| `Diff` | `diff` | |
| `Kbd` | `kbd` | |
| `List` / `ListItem` / `ListRow` | `list` | ● |
| `Stat` / `Stats` | `stats` / `stat` | |
| `Status` | `status` | |
| `Table` | `table` | ● |
| `SimpleTable` | `table` | ● |
| `Timeline` / `TimelineItem` | `timeline` | |
| `Typography` | text utilities (`Typo`) | ● |
| `Image` | `<img>` helper | |
| `Avatar` group / `Indicator` | `indicator` | |

## Navigation

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `Breadcrumbs` | `breadcrumbs` | ● |
| `Dock` / `DockItem` | `dock` | |
| `Menu` / `MenuItem` | `menu` | ● |
| `NavMenuLink` | `menu` link | ● |
| `Navbar` | `navbar` | ● |
| `Pagination` | `join` (pagination) | ● |
| `Steps` / `Step` | `steps` / `step` | |
| `Tabs` / `Tab` | `tabs` / `tab` | ● |

## Feedback

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `Alert` | `alert` | ● |
| `Loading` | `loading` | ● |
| `ProgressLinear` | `progress` | ● |
| `ProgressCircular` | `radial-progress` | ● |
| `RadialProgress` | `radial-progress` | |
| `Skeleton` | `skeleton` | ● |
| `Tooltip` | `tooltip` | ● |
| `Dialog` / `DialogProvider` / `MessageBoxDialog` | `modal` (via `IDialogService`) | ● |
| `SnackbarProvider` | `toast` + `alert` (via `ISnackbar`) | ● |
| `ConfirmDialog` (`.Feedback`) | `modal` | |
| `EmptyState` (`.Feedback`) | composed | |
| `ErrorAlert` (`.Feedback`) | `alert alert-error` | |

## Data input

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `TextField` | `input` | ● |
| `NumericField` | `input` | ● |
| `Field` | `fieldset` / `form-control` | ● |
| `FloatingLabel` | `floating-label` | |
| `Select` / `SelectItem` | `select` | ● |
| `Autocomplete` | `input` + `dropdown` | ● |
| `Checkbox` | `checkbox` | ● |
| `Switch` | `toggle` | ● |
| `Radio` / `RadioGroup` | `radio` | ● |
| `Range` | `range` | ● |
| `Rating` | `rating` | ● |
| `FileInput` | `file-input` | ● |
| `DatePicker` | `input` (date) | ● |
| `ColorPicker` | `input` (color) | ● |
| `Form` / `Validator` / `ValidatorHint` | form wrapper | ● |

## Layout

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `Container` | max-width wrapper | ● |
| `Grid` / `Item` | CSS grid (12-col) | ● |
| `Stack` | flex stack | ● |
| `Spacer` | flex spacer | ● |
| `Divider` | `divider` | ● |
| `Drawer` | `drawer` | ● |
| `Footer` / `FooterTitle` | `footer` | |
| `Hero` | `hero` | |
| `Indicator` | `indicator` | |
| `Paper` | surface wrapper | ● |
| `Mask` | `mask` | |
| `ExpansionPanels` / `ExpansionPanel` | `collapse` / `accordion` | ● |
| `PageHeader` (`.Layout`) | composed | |
| `DetailLayout` / `DetailCard` / `DetailRow` (`.Layout`) | composed | |

## Mockup

| DaisyBlazor type | daisyUI root class | Compat |
|------------------|--------------------|:------:|
| `MockupBrowser` | `mockup-browser` | |
| `MockupCode` | `mockup-code` | |
| `MockupPhone` | `mockup-phone` | |
| `MockupWindow` | `mockup-window` | |

## Theming

| DaisyBlazor type | Purpose | Compat |
|------------------|---------|:------:|
| `ThemeProvider` (`.Theming`) | Applies `data-theme`, dark-mode + persistence; cascades state. | ● (`MudThemeProvider`) |
| `ThemeController` | Pure-CSS daisyUI theme-controller toggle. | |

See [Theming](/daisyblazor/theming/) for the full `ThemeProvider` parameter list and how to build a picker.

## Higher-level data components (`.Data`)

Beyond the daisyUI primitives, the `.Data` namespace adds composed building blocks: `DataGrid`
(+ `PropertyColumn` / `TemplateColumn` / `Column`), `DataTable`, `FilterPanel`, `KpiCard`, and
`StatusChip`. These are DaisyBlazor-native (no MudBlazor equivalent, though `DataGrid` mirrors
`MudDataGrid`'s column-based shape).

## See also

- [Charts](/daisyblazor/charts/) — the `DaisyBlazor.Charts` SVG charts.
- [MudBlazor migration](/daisyblazor/migration/) — the compatible enums, icons, dialogs and snackbar.
- [Getting started](/daisyblazor/getting-started/) · [Theming](/daisyblazor/theming/) · [CSS preset](/daisyblazor/css-preset/)
