# Radix UI to Blazor Conversion Guide

## Overview
This guide provides a systematic approach for converting Radix UI React components to Blazor components while maintaining feature parity and following best practices.

## Table of Contents
1. [Conversion Principles](#conversion-principles)
2. [File Structure](#file-structure)
3. [Component Conversion Steps](#component-conversion-steps)
4. [Pattern Mappings](#pattern-mappings)
5. [CSS Integration](#css-integration)
6. [Component Task List](#component-task-list)

---

## Conversion Principles

### Core Principles
1. **Maintain Feature Parity**: Every Radix UI feature should be available in the Blazor component
2. **Follow Blazor Conventions**: Use code-behind files, proper parameter naming, and Blazor patterns
3. **Preserve CSS Architecture**: Keep the same CSS classes and structure for seamless theming
4. **Type Safety**: Use enums and strongly-typed parameters where appropriate

### Key Differences

| React/TypeScript | Blazor/C# |
|-----------------|-----------|
| Props | Parameters |
| forwardRef | ElementReference |
| children | ChildContent (RenderFragment) |
| className | Class or CssClass |
| useState | Private fields with StateHasChanged |
| useEffect | OnInitialized, OnParametersSet, OnAfterRender |
| onClick | OnClick (EventCallback) |
| asChild pattern | AsChild parameter with CascadingValue |
| spread props | AdditionalAttributes with CaptureUnmatchedValues |

---

## File Structure

### For each component, create:

```
Components/
  UI/
    ComponentName.razor           # Component markup
    ComponentName.razor.cs        # Code-behind with logic
    ComponentName.razor.css       # Scoped styles (if needed)
    ComponentNameSize.cs          # Enums for sizes
    ComponentNameVariant.cs       # Enums for variants
```

### CSS Structure (maintained from Radix):

```
Styles/
  components/
    component-name.css           # Component styles
    _internal/
      base-component.css        # Base component styles
  tokens/                       # Design tokens
  utilities/                    # Utility classes
```

---

## Component Conversion Steps

### Step 1: Analyze the React Component

1. **Read TypeScript files**:
   - Main component file (`.tsx`)
   - Props definition (`.props.ts`)
   - Any helper files

2. **Identify features**:
   - Props and their types
   - Event handlers
   - Composition patterns (asChild)
   - State management
   - Refs usage

3. **Review CSS**:
   - Component CSS files
   - CSS class patterns
   - Data attributes usage

### Step 2: Create Blazor Component Structure

#### Component.razor
```razor
@namespace ShadBz.BlazorApp.Components.UI

@if (AsChild)
{
    <CascadingValue Value="@GetComponentAttributes()" Name="ComponentAttributes">
        @ChildContent
    </CascadingValue>
}
else
{
    <element @attributes="GetAllAttributes()" class="@CssClass">
        @ChildContent
    </element>
}
```

#### Component.razor.cs
```csharp
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Component : ComponentBase
{
    // Parameters (Props)
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public string? Class { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
    
    // Computed properties
    protected string CssClass => new CssBuilder("rt-Component")
        .AddClass(GetVariantClass())
        .AddClass(GetSizeClass())
        .AddClass(Class)
        .Build();
    
    // Helper methods
    protected Dictionary<string, object> GetAllAttributes()
    {
        var attributes = new Dictionary<string, object>();
        // Add data attributes, styles, etc.
        return attributes;
    }
}
```

### Step 3: Map Props to Parameters

#### Primitive Props
```typescript
// React
interface Props {
  size?: '1' | '2' | '3' | '4';
  variant?: 'solid' | 'soft' | 'outline';
  loading?: boolean;
}
```

```csharp
// Blazor
[Parameter] public ComponentSize Size { get; set; } = ComponentSize.Medium;
[Parameter] public ComponentVariant Variant { get; set; } = ComponentVariant.Solid;
[Parameter] public bool Loading { get; set; }

// Enums
public enum ComponentSize { Small, Medium, Large, ExtraLarge }
public enum ComponentVariant { Solid, Soft, Outline }
```

#### Layout Props (Margin, Padding, etc.)
```csharp
// Margin properties
[Parameter] public string? M { get; set; }
[Parameter] public string? Mx { get; set; }
[Parameter] public string? My { get; set; }
[Parameter] public string? Mt { get; set; }
[Parameter] public string? Mr { get; set; }
[Parameter] public string? Mb { get; set; }
[Parameter] public string? Ml { get; set; }

// Padding properties
[Parameter] public string? P { get; set; }
[Parameter] public string? Px { get; set; }
[Parameter] public string? Py { get; set; }
[Parameter] public string? Pt { get; set; }
[Parameter] public string? Pr { get; set; }
[Parameter] public string? Pb { get; set; }
[Parameter] public string? Pl { get; set; }
```

### Step 4: Handle CSS Classes

```csharp
// Use CssBuilder for clean class composition
private string GetSizeClass() => Size switch
{
    ComponentSize.Small => "rt-r-size-1",
    ComponentSize.Medium => "rt-r-size-2",
    ComponentSize.Large => "rt-r-size-3",
    ComponentSize.ExtraLarge => "rt-r-size-4",
    _ => "rt-r-size-2"
};

// Handle space scale vs custom values
private bool IsSpaceScaleValue(string value)
{
    if (string.IsNullOrEmpty(value)) return false;
    return value == "0" || 
           (value.Length == 1 && char.IsDigit(value[0])) ||
           (value.Length == 2 && value[0] == '-' && char.IsDigit(value[1]));
}

// Apply appropriate class or custom property
if (!string.IsNullOrEmpty(Width))
    classes.Add(IsSpaceScaleValue(Width) ? $"rt-r-w-{Width}" : "rt-r-w");
```

### Step 5: Data Attributes

```csharp
// Add data attributes for theming and state
protected Dictionary<string, object> GetDataAttributes()
{
    var attributes = new Dictionary<string, object>();
    
    if (IsDisabled)
        attributes["data-disabled"] = "";
    
    if (!string.IsNullOrEmpty(Color))
        attributes["data-accent-color"] = Color;
    
    if (!string.IsNullOrEmpty(Radius))
        attributes["data-radius"] = Radius;
    
    if (IsOpen)
        attributes["data-state"] = "open";
    
    return attributes;
}
```

---

## Pattern Mappings

### AsChild Pattern
```razor
@if (AsChild)
{
    <CascadingValue Value="@GetComponentAttributes()" Name="ComponentAttributes">
        @ChildContent
    </CascadingValue>
}
else
{
    <button @attributes="AllAttributes">@ChildContent</button>
}
```

### Loading State with Spinner
```razor
@if (Loading)
{
    <span style="display: contents; visibility: hidden;" aria-hidden="true">
        @ChildContent
    </span>
    <span class="visually-hidden">@ChildContent</span>
    <span class="rt-Flex spinner-container">
        <Spinner Size="@GetSpinnerSize()" />
    </span>
}
else
{
    @ChildContent
}
```

### Responsive Props (Future Enhancement)
```csharp
// For future responsive support
public class ResponsiveValue<T>
{
    public T? Initial { get; set; }
    public T? Xs { get; set; }
    public T? Sm { get; set; }
    public T? Md { get; set; }
    public T? Lg { get; set; }
    public T? Xl { get; set; }
}
```

---

## CSS Integration

### Build Process
1. **Source CSS**: Keep original Radix CSS in `Styles/` folder
2. **PostCSS Build**: Process with PostCSS using same plugins as Radix
3. **Output**: Compile to `wwwroot/radix/index.css`
4. **Reference**: Include in App.razor

### PostCSS Configuration
```javascript
// postcss.config.js
module.exports = {
  plugins: [
    require('postcss-import'),
    require('postcss-nesting'),
    require('./postcss-breakpoints.js'),
    require('postcss-custom-media'),
    require('postcss-combine-duplicated-selectors'),
    require('postcss-discard-empty'),
    require('./postcss-whitespace.js'),
    require('autoprefixer'),
  ],
};
```

### MSBuild Integration
```xml
<!-- .csproj -->
<Target Name="BuildCSS" BeforeTargets="Build">
    <Message Text="Building Radix UI CSS..." Importance="high" />
    <Exec Command="npm run build:css" WorkingDirectory="$(MSBuildProjectDirectory)" />
</Target>
```

---

## Component Task List

### ✅ Completed Components

#### 1. Button ✅
- [x] Create Button.razor and Button.razor.cs
- [x] Create ButtonSize and ButtonVariant enums
- [x] Implement all 6 variants (classic, solid, soft, surface, outline, ghost)
- [x] Add loading state with spinner
- [x] Support all margin props
- [x] Add high contrast support
- [x] Implement AsChild pattern
- [x] Create comprehensive demo page

#### 2. Box ✅
- [x] Create Box.razor and Box.razor.cs
- [x] Implement div/span rendering
- [x] Add all layout props (width, height, position, etc.)
- [x] Support all margin and padding props
- [x] Handle flexbox and grid properties
- [x] Support custom CSS values with CSS variables
- [x] Create comprehensive demo page

### 📋 Components To Convert

#### 3. Text ✅
- [x] Create Text.razor and Text.razor.cs
- [x] Implement size variants (1-9)
- [x] Add weight variants
- [x] Support as prop (span, div, label, p)
- [x] Add color prop
- [x] Implement align prop
- [x] Add trim prop for line height adjustment
- [x] Support truncate prop
- [x] Add wrap prop
- [x] Create demo page

#### 4. Heading ✅
- [x] Create Heading.razor and Heading.razor.cs
- [x] Implement size variants (1-9)
- [x] Support as prop (h1-h6)
- [x] Add weight variants
- [x] Support color prop
- [x] Implement align prop
- [x] Add trim prop
- [x] Support truncate prop
- [x] Add wrap prop
- [x] Create demo page

#### 5. Flex ✅
- [x] Create Flex.razor and Flex.razor.cs
- [x] Implement direction prop
- [x] Add align prop
- [x] Support justify prop
- [x] Add wrap prop
- [x] Implement gap prop (responsive)
- [x] Support all layout props from Box
- [x] Create demo page

#### 6. Grid ✅
- [x] Create Grid.razor and Grid.razor.cs
- [x] Implement columns prop
- [x] Add rows prop
- [x] Support flow prop
- [x] Add align prop
- [x] Implement justify prop
- [x] Support gap prop (responsive)
- [x] Add areas prop
- [x] Create demo page

#### 7. Container ✅
- [x] Create Container.razor and Container.razor.cs
- [x] Implement size variants (1-4)
- [x] Add responsive breakpoints
- [x] Support all layout props
- [x] Create demo page

#### 8. Section ✅
- [x] Create Section.razor and Section.razor.cs
- [x] Implement size variants (1-4)
- [x] Support all layout props
- [x] Create demo page

#### 9. Card ✅
- [x] Create Card.razor and Card.razor.cs
- [x] Implement variant prop (classic, surface, ghost)
- [x] Add size prop (1-5)
- [x] Support AsChild pattern
- [x] Support interactive elements (a, button, label)
- [x] Create demo page

#### 10. Link ✅
- [x] Create Link.razor and Link.razor.cs
- [x] Implement size variants (1-9)
- [x] Add weight variants
- [x] Support underline prop (auto, always, hover, none)
- [x] Add color support
- [x] Implement high contrast mode
- [x] Support button rendering when no href
- [x] Add text properties (truncate, wrap, trim)
- [x] Support AsChild pattern
- [x] Create demo page

#### 11. Badge ✅
- [x] Create Badge.razor and Badge.razor.cs
- [x] Implement variant prop (solid, soft, surface, outline)
- [x] Add size prop (1-3)
- [x] Support color prop (all color palette)
- [x] Add radius prop (none, small, medium, large, full)
- [x] Implement high contrast mode
- [x] Support AsChild pattern
- [x] Create comprehensive demo page

#### 12. Avatar ✅
- [x] Create Avatar.razor and Avatar.razor.cs
- [x] Implement size variants (1-9)
- [x] Add fallback support with initials
- [x] Support variant prop (solid, soft)
- [x] Add radius prop
- [x] Support color prop
- [x] Implement image loading states
- [x] Add high contrast mode
- [x] Create demo page

#### 13. Separator ✅
- [x] Create Separator.razor and Separator.razor.cs
- [x] Implement orientation prop (horizontal, vertical)
- [x] Add size prop (1-4)
- [x] Support color prop (all color palette)
- [x] Add decorative prop (semantic vs visual)
- [x] Support margin props from base class
- [x] Create comprehensive demo page

#### 14. IconButton ✅
- [x] Create IconButton.razor and IconButton.razor.cs
- [x] Inherit from SpacingComponentBase (shares Button logic)
- [x] Implement all 6 button variants (classic, solid, soft, surface, outline, ghost)
- [x] Support all 4 sizes with proper icon scaling
- [x] Add loading state with spinner
- [x] Support high contrast mode
- [x] Implement AsChild pattern
- [x] Create comprehensive demo page

#### 15. Checkbox ✅
- [x] Create Checkbox.razor and Checkbox.razor.cs
- [x] Implement checked/indeterminate/unchecked states
- [x] Add size variants (1-3)
- [x] Support variant prop (surface, classic, soft)
- [x] Support color prop (all color palette)
- [x] Add disabled state
- [x] Implement controlled and uncontrolled modes
- [x] Support two-way binding with bool and CheckboxState
- [x] Add high contrast mode
- [x] Create comprehensive demo page

#### 16. Radio ✅
- [x] Create Radio.razor and Radio.razor.cs
- [x] Create RadioGroup.razor and RadioGroup.razor.cs
- [x] Implement size variants (1-3)
- [x] Support variant prop (surface, classic, soft)
- [x] Support color prop (all color palette)
- [x] Add disabled state (individual and group level)
- [x] Handle group value management (controlled/uncontrolled)
- [x] Support horizontal and vertical orientation
- [x] Add high contrast mode
- [x] Create comprehensive demo page with RadioGroupItem helper

#### 17. Switch ✅
- [x] Create Switch.razor and Switch.razor.cs
- [x] Implement size variants (1-3)
- [x] Support variant prop (surface, classic, soft)
- [x] Add color prop (all color palette)
- [x] Support disabled state
- [x] Add radius prop (none, small, medium, large, full)
- [x] Implement controlled and uncontrolled modes
- [x] Support two-way binding
- [x] Add high contrast mode
- [x] Create comprehensive demo page

#### 18. TextField ✅
- [x] Create TextField.razor and TextField.razor.cs with Root/Slot composition
- [x] Implement size variants (1-3)
- [x] Add variant prop (classic, surface, soft)
- [x] Support placeholder and all input types
- [x] Add disabled/readonly states
- [x] Support color prop (all color palette)
- [x] Add radius prop (none, small, medium, large, full)
- [x] Implement TextField.Slot for icons and buttons
- [x] Support validation attributes (required, min, max, pattern, etc)
- [x] Two-way binding support
- [x] Create comprehensive demo page

#### 19. TextArea ✅
- [x] Create TextArea.razor and TextArea.razor.cs
- [x] Implement size variants (1-3)
- [x] Add variant prop (classic, surface, soft)
- [x] Support resize prop (none, vertical, horizontal, both)
- [x] Add disabled/readonly states
- [x] Support color prop (all color palette)
- [x] Add radius prop (none, small, medium, large, full)
- [x] Support validation attributes (required, min/max length)
- [x] Two-way binding support
- [x] Create comprehensive demo page

#### 20. Select ✅
- [x] Create SelectRoot.razor and SelectRoot.razor.cs with generic type support
- [x] Create SelectTrigger, SelectContent, SelectItem sub-components
- [x] Create SelectGroup, SelectLabel, SelectSeparator sub-components
- [x] Implement size variants (1-3)
- [x] Add trigger variant prop (classic, surface, soft, ghost)
- [x] Add content variant prop (solid, soft)
- [x] Support placeholder and disabled states
- [x] Support color prop (all color palette)
- [x] Add radius prop (none, small, medium, large, full)
- [x] Two-way binding support with generic types
- [x] Create comprehensive demo page

#### 21. Slider ✅
- [x] Create Slider.razor and Slider.razor.cs
- [x] Implement size variants (1-3)
- [x] Add variant prop (classic, surface, soft)
- [x] Support min/max/step configuration
- [x] Add disabled state
- [x] Support color prop (all color palette)
- [x] Add radius prop (none, small, medium, large, full)
- [x] Support high contrast mode
- [x] Implement single and range sliders
- [x] Add drag interaction with mouse and touch support
- [x] Implement onChange and onCommit callbacks
- [x] Two-way binding support
- [x] Create comprehensive demo page

#### 22. Progress ✅
- [x] Create Progress.razor and Progress.razor.cs
- [x] Implement size variants (1-3)
- [x] Add variant prop (classic, surface, soft)
- [x] Support value/max props for determinate progress
- [x] Support indeterminate progress (no value)
- [x] Add color prop (all color palette)
- [x] Support radius prop (none, small, medium, large, full)
- [x] Support high contrast mode
- [x] Add duration prop for animation speed
- [x] Implement accessibility attributes (role, aria-valuenow, etc.)
- [x] Create comprehensive demo page with interactive examples

#### 23. Spinner
- [ ] Update existing Spinner.razor ✅ (basic version exists)
- [ ] Add size variants
- [ ] Support loading prop
- [ ] Create demo page

#### 24. Dialog
- [ ] Create Dialog.razor and Dialog.razor.cs
- [ ] Create DialogTrigger, DialogContent, DialogTitle, DialogDescription
- [ ] Implement size variants
- [ ] Add modal/non-modal support
- [ ] Support close button
- [ ] Add overlay
- [ ] Create demo page

#### 25. AlertDialog
- [ ] Create AlertDialog.razor and AlertDialog.razor.cs
- [ ] Create AlertDialogTrigger, AlertDialogContent, AlertDialogTitle, AlertDialogDescription, AlertDialogAction, AlertDialogCancel
- [ ] Implement proper focus management
- [ ] Add size variants
- [ ] Create demo page

#### 26. Popover
- [ ] Create Popover.razor and Popover.razor.cs
- [ ] Create PopoverTrigger, PopoverContent
- [ ] Implement positioning logic
- [ ] Add size variants
- [ ] Support close on outside click
- [ ] Create demo page

#### 27. DropdownMenu
- [ ] Create DropdownMenu.razor and DropdownMenu.razor.cs
- [ ] Create DropdownMenuTrigger, DropdownMenuContent, DropdownMenuItem, DropdownMenuSeparator
- [ ] Implement sub-menus
- [ ] Add keyboard navigation
- [ ] Support icons and shortcuts
- [ ] Create demo page

#### 28. ContextMenu
- [ ] Create ContextMenu.razor and ContextMenu.razor.cs
- [ ] Create ContextMenuTrigger, ContextMenuContent, ContextMenuItem
- [ ] Implement right-click handling
- [ ] Add keyboard navigation
- [ ] Support sub-menus
- [ ] Create demo page

#### 29. Tooltip
- [ ] Create Tooltip.razor and Tooltip.razor.cs
- [ ] Create TooltipTrigger, TooltipContent
- [ ] Implement positioning logic
- [ ] Add delay props
- [ ] Support multiline content
- [ ] Create demo page

#### 30. HoverCard
- [ ] Create HoverCard.razor and HoverCard.razor.cs
- [ ] Create HoverCardTrigger, HoverCardContent
- [ ] Implement hover delay
- [ ] Add size variants
- [ ] Support positioning
- [ ] Create demo page

#### 31. Tabs
- [ ] Create Tabs.razor and Tabs.razor.cs
- [ ] Create TabsList, TabsTrigger, TabsContent
- [ ] Implement size variants
- [ ] Add orientation support
- [ ] Support keyboard navigation
- [ ] Create demo page

#### 32. ScrollArea
- [ ] Create ScrollArea.razor and ScrollArea.razor.cs
- [ ] Implement custom scrollbar styling
- [ ] Add size prop
- [ ] Support type prop (auto, always, scroll, hover)
- [ ] Create demo page

#### 33. Table
- [ ] Create Table.razor and Table.razor.cs
- [ ] Create TableHeader, TableBody, TableRow, TableCell
- [ ] Implement size variants
- [ ] Add variant prop
- [ ] Support layout prop
- [ ] Create demo page

#### 34. Callout
- [ ] Create Callout.razor and Callout.razor.cs
- [ ] Implement variant prop
- [ ] Add size prop
- [ ] Support color prop
- [ ] Add icon support
- [ ] Create demo page

#### 35. Code
- [ ] Create Code.razor and Code.razor.cs
- [ ] Implement variant prop (solid, soft, outline, ghost)
- [ ] Add size prop
- [ ] Support color prop
- [ ] Add weight prop
- [ ] Create demo page

#### 36. Em
- [ ] Create Em.razor and Em.razor.cs
- [ ] Support all text props
- [ ] Create demo page

#### 37. Kbd
- [ ] Create Kbd.razor and Kbd.razor.cs
- [ ] Implement size prop
- [ ] Support key combinations
- [ ] Create demo page

#### 38. Quote
- [ ] Create Quote.razor and Quote.razor.cs
- [ ] Support all text props
- [ ] Create demo page

#### 39. Strong
- [ ] Create Strong.razor and Strong.razor.cs
- [ ] Support all text props
- [ ] Create demo page

#### 40. Blockquote
- [ ] Create Blockquote.razor and Blockquote.razor.cs
- [ ] Implement size prop
- [ ] Add weight prop
- [ ] Support color prop
- [ ] Create demo page

---

## Testing Checklist

For each component:
- [ ] Component renders correctly
- [ ] All props/parameters work as expected
- [ ] CSS classes are applied correctly
- [ ] Data attributes are set properly
- [ ] Event handlers fire correctly
- [ ] AsChild pattern works (if applicable)
- [ ] Loading states display properly (if applicable)
- [ ] Disabled states work correctly (if applicable)
- [ ] Component is accessible (ARIA attributes)
- [ ] Demo page shows all features

---

## Demo Page Template

```razor
@page "/component-demo"
@using ShadBz.BlazorApp.Components.UI

<PageTitle>Component Demo</PageTitle>

<div class="demo-container">
    <h1>Component Demo</h1>

    <div class="demo-section">
        <h2>Feature Name</h2>
        <div class="demo-content">
            <!-- Component examples -->
        </div>
    </div>
</div>

<style>
    .demo-container {
        padding: var(--space-4);
        max-width: 1200px;
        margin: 0 auto;
    }

    .demo-section {
        margin-bottom: var(--space-6);
    }

    .demo-section h2 {
        margin-bottom: var(--space-3);
        font-size: var(--font-size-4);
        font-weight: var(--font-weight-bold);
        color: var(--gray-12);
    }
</style>
```

---

## Resources

- [Radix UI Themes Documentation](https://www.radix-ui.com/themes/docs)
- [Radix UI GitHub Repository](https://github.com/radix-ui/themes)
- [Blazor Component Guidelines](https://docs.microsoft.com/en-us/aspnet/core/blazor/components/)
- [PostCSS Documentation](https://postcss.org/)

---

## Notes

1. **Responsive Props**: Current implementation doesn't support responsive props. This could be added in the future using media query detection in Blazor.

2. **Animation**: Some Radix components use Framer Motion for animations. These would need to be reimplemented using CSS animations or Blazor animation libraries.

3. **Portal/Teleport**: Components that render outside their parent (Dialog, Popover, etc.) need special handling in Blazor.

4. **Focus Management**: Components with complex focus management (Dialog, Menu) need careful implementation to maintain accessibility.

5. **Keyboard Navigation**: Components with keyboard navigation need proper key event handling in Blazor.

---

## Contributing

When adding new components:
1. Follow the conversion steps outlined above
2. Maintain consistency with existing components
3. Ensure all features from Radix UI are preserved
4. Add comprehensive demo pages
5. Update this guide with any new patterns discovered