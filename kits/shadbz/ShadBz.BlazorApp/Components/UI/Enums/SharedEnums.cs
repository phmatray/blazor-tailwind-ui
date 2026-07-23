namespace ShadBz.BlazorApp.Components.UI;

/// <summary>
/// Text weight options shared across Text, Heading, and other typography components
/// </summary>
public enum TextWeight
{
    Light,
    Regular,
    Medium,
    Bold
}

/// <summary>
/// Text alignment options shared across typography components
/// </summary>
public enum TextAlign
{
    Left,
    Center,
    Right
}

/// <summary>
/// Text wrap options shared across typography components
/// </summary>
public enum TextWrap
{
    Wrap,
    NoWrap,
    Pretty,
    Balance
}

/// <summary>
/// Display options for layout components
/// </summary>
public enum DisplayType
{
    None,
    Block,
    InlineBlock,
    Flex,
    InlineFlex,
    Grid,
    InlineGrid,
    Initial
}