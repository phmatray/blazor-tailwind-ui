![ShadBz banner](.github/banner.png)

# ShadBz

<!-- portfolio-badges:start -->
<!-- Identity -->
[![phmatray - ShadBz](https://img.shields.io/static/v1?label=phmatray&message=ShadBz&color=blue&logo=github)](https://github.com/phmatray/ShadBz)
![Top language](https://img.shields.io/github/languages/top/phmatray/ShadBz)
[![Stars](https://img.shields.io/github/stars/phmatray/ShadBz?style=social)](https://github.com/phmatray/ShadBz/stargazers)
[![Forks](https://img.shields.io/github/forks/phmatray/ShadBz?style=social)](https://github.com/phmatray/ShadBz/network/members)

<!-- Activity -->
[![Issues](https://img.shields.io/github/issues/phmatray/ShadBz)](https://github.com/phmatray/ShadBz/issues)
[![Pull requests](https://img.shields.io/github/issues-pr/phmatray/ShadBz)](https://github.com/phmatray/ShadBz/pulls)
[![Last commit](https://img.shields.io/github/last-commit/phmatray/ShadBz)](https://github.com/phmatray/ShadBz/commits)
<!-- portfolio-badges:end -->

<!-- portfolio-toc:start -->

## Table of Contents

- [Description](#description)
- [Stack / Tech](#stack--tech)
- [Getting Started](#getting-started)
- [License](#license)
- [Contributing](#contributing)

<!-- portfolio-toc:end -->



> ShadCN-inspired UI component library for Blazor applications.

## Description

ShadBz brings the elegant, accessible ShadCN/UI component design system to Blazor. Built on Radix primitives adapted for Blazor, it provides a set of reusable, customizable UI components that follow modern design principles. Includes a conversion guide for migrating from existing component libraries.

## Features

- **40+ Radix-inspired components** — layout (`Box`, `Flex`, `Grid`, `Container`, `Section`), forms (`Button`, `Checkbox`, `Radio`, `Switch`, `Slider`, `Select`, `TextField`, `TextArea`), overlays (`Dialog`, `AlertDialog`, `Popover`, `DropdownMenu`, `ContextMenu`, `Tooltip`, `HoverCard`), data display (`Card`, `Avatar`, `Badge`, `Table`, `ScrollArea`) and typography primitives (`Heading`, `Text`, `Code`, `Blockquote`, `Kbd`, `Quote`, `Strong`, `Em`).
- **`AsChild` composition pattern** — components implementing `IAsChildSupport` can render their behavior onto a child element instead of their own tag, mirroring Radix's `asChild` prop for flexible markup.
- **Typed variant and size enums** — components like `Button` expose strongly-typed options (`ButtonVariant.Classic/Solid/Soft/Surface/Outline/Ghost`, `ButtonSize.Small/Medium/Large/ExtraLarge`) instead of raw strings, catching mistakes at compile time.
- **Full Radix color system** — ships all 30 Radix color scales (e.g. `blue`, `gray`, `crimson`, `jade`, `mauve`) as CSS tokens under `Styles/tokens/colors`, with light and dark theme variants driven by `.dark`/`.dark-theme` classes.
- **Design-token driven styling** — spacing, radius, shadow, typography, and scaling tokens live under `Styles/tokens` and compile through an npm `build:css` step wired into the `ShadBz.BlazorApp` MSBuild pipeline.
- **Live component demo app** — `ShadBz.BlazorApp` renders a demo page per component (e.g. `ButtonDemo`, `DialogDemo`, `TabsDemo`) plus a home page cataloguing every available and "coming soon" component.
- **bUnit test coverage** — the `Radixor.Tests` project contains dedicated test suites across Forms, Layout, Feedback, DataDisplay, and Typography components, plus integration tests.
- **Conversion guide included** — `CONVERSION_GUIDE.md` documents the React-to-Blazor mapping conventions (props → parameters, `asChild` → `AsChild`, `className` → `Class`, etc.) used to port each Radix primitive.

## Stack / Tech
- .NET / Blazor
- C#
- CSS / Tailwind CSS
- Radix UI primitives

## Getting Started

```bash
dotnet add package ShadBz
```

See `CONVERSION_GUIDE.md` for migration details.

<!-- portfolio-roadmap:start -->

## Roadmap

Planned work and known limitations are tracked in the [open issues](https://github.com/phmatray/ShadBz/issues). Contributions toward them are welcome.

<!-- portfolio-roadmap:end -->

## License
MIT

---

<!-- portfolio-sections:start -->

## Contributing

Contributions are welcome. Open an issue first to discuss any significant change.

1. Fork the repository and create your branch (`git checkout -b feat/my-feature`)
2. Commit your changes (`git commit -m 'feat: ...'`)
3. Push the branch and open a Pull Request

<!-- portfolio-sections:end -->
