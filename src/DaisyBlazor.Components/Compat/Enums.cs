namespace DaisyBlazor;

// MudBlazor-compatible enums so consuming markup (Color.Primary, Variant.Filled, Typo.h6, ...)
// migrates with only a tag rename. Members mirror the subset used across DaisyBlazor.

/// <summary>Semantic color, mapped to daisyUI color tokens.</summary>
public enum Color
{
    Default,
    Inherit,
    Primary,
    Secondary,
    Tertiary,
    Info,
    Success,
    Warning,
    Error,
    Dark,
    Surface
}

/// <summary>Control size.</summary>
public enum Size
{
    Small,
    Medium,
    Large
}

/// <summary>Visual variant for buttons/chips/alerts/fields.</summary>
public enum Variant
{
    Text,
    Filled,
    Outlined
}

/// <summary>Typography scale, mapped to Tailwind text sizes + weight.</summary>
public enum Typo
{
    h1,
    h2,
    h3,
    h4,
    h5,
    h6,
    subtitle1,
    subtitle2,
    body1,
    body2,
    button,
    caption,
    overline,
    inherit
}

/// <summary>Severity for alerts/snackbars, mapped to daisyUI status colors.</summary>
public enum Severity
{
    Normal,
    Info,
    Success,
    Warning,
    Error
}

/// <summary>Flex main-axis distribution.</summary>
public enum Justify
{
    FlexStart,
    Center,
    FlexEnd,
    SpaceBetween,
    SpaceAround,
    SpaceEvenly
}

/// <summary>Flex cross-axis alignment.</summary>
public enum AlignItems
{
    Start,
    Center,
    End,
    Stretch,
    Baseline
}

/// <summary>Container max width.</summary>
public enum MaxWidth
{
    Small,
    Medium,
    Large,
    ExtraLarge,
    ExtraExtraLarge,
    False
}

/// <summary>Adornment position for input fields.</summary>
public enum Adornment
{
    None,
    Start,
    End
}

/// <summary>Anchor origin for popovers/badges.</summary>
public enum Origin
{
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    CenterCenter,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

/// <summary>Edge placement for icon buttons in bars.</summary>
public enum Edge
{
    False,
    Start,
    End
}

/// <summary>Drawer/anchor side.</summary>
public enum Anchor
{
    Start,
    End,
    Left,
    Right,
    Top,
    Bottom
}

/// <summary>Responsive breakpoints.</summary>
public enum Breakpoint
{
    Xs,
    Sm,
    Md,
    Lg,
    Xl,
    Xxl,
    None,
    Always
}

/// <summary>Drawer behavior.</summary>
public enum DrawerVariant
{
    Temporary,
    Persistent,
    Mini,
    Responsive
}

/// <summary>Drawer clip mode relative to the app bar.</summary>
public enum DrawerClipMode
{
    Never,
    Docked,
    Always
}

/// <summary>HTML button type.</summary>
public enum ButtonType
{
    Button,
    Submit,
    Reset
}

/// <summary>Sort direction for tables/grids.</summary>
public enum SortDirection
{
    None,
    Ascending,
    Descending
}

/// <summary>Skeleton placeholder shape (MudBlazor compatibility).</summary>
public enum SkeletonType
{
    Text,
    Circle,
    Rectangle
}

/// <summary>Animation style for the daisyUI loading indicator.</summary>
public enum LoadingType
{
    Spinner,
    Dots,
    Ring,
    Ball,
    Bars,
    Infinity
}

/// <summary>Flex wrap behavior (MudBlazor compatibility).</summary>
public enum Wrap
{
    NoWrap,
    Wrap,
    WrapReverse
}

/// <summary>Field density/margin.</summary>
public enum Margin
{
    None,
    Dense,
    Normal
}

/// <summary>Overflow behavior for popovers/menus.</summary>
public enum OverflowBehavior
{
    FlipOnOpen,
    FlipNever,
    FlipAlways
}

/// <summary>Link underline behavior (MudLink compatibility).</summary>
public enum Underline
{
    None,
    Hover,
    Always
}

/// <summary>Size axis for the <see cref="Status"/> dot; covers all five daisyUI status-size steps.</summary>
public enum StatusSize
{
    ExtraSmall,
    Small,
    Medium,
    Large,
    ExtraLarge
}

/// <summary>Size modifier for the daisyUI dock component (dock-xs … dock-xl).</summary>
public enum DockSize
{
    Xs,
    Sm,
    Md,
    Lg,
    Xl
}

/// <summary>Input type for text fields.</summary>
public enum InputType
{
    Text,
    Password,
    Email,
    Number,
    Search,
    Telephone,
    Url,
    Date,
    DateTimeLocal,
    Month,
    Time,
    Week,
    Color
}
