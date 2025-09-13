using Radixor.Components.Base;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Radixor.Components.Base;

/// <summary>
/// Base class for components that support layout properties
/// </summary>
public abstract class LayoutComponentBase : ComponentBase
{
    // Content
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    // Width properties
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public string? MaxWidth { get; set; }
    
    // Height properties
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? MinHeight { get; set; }
    [Parameter] public string? MaxHeight { get; set; }
    
    // Position properties
    [Parameter] public string? Position { get; set; }
    [Parameter] public string? Inset { get; set; }
    [Parameter] public string? Top { get; set; }
    [Parameter] public string? Right { get; set; }
    [Parameter] public string? Bottom { get; set; }
    [Parameter] public string? Left { get; set; }
    
    // Overflow properties
    [Parameter] public string? Overflow { get; set; }
    [Parameter] public string? OverflowX { get; set; }
    [Parameter] public string? OverflowY { get; set; }
    
    // Flex item properties
    [Parameter] public string? FlexBasis { get; set; }
    [Parameter] public string? FlexShrink { get; set; }
    [Parameter] public string? FlexGrow { get; set; }
    
    // Grid item properties
    [Parameter] public string? GridColumn { get; set; }
    [Parameter] public string? GridColumnStart { get; set; }
    [Parameter] public string? GridColumnEnd { get; set; }
    [Parameter] public string? GridRow { get; set; }
    [Parameter] public string? GridRowStart { get; set; }
    [Parameter] public string? GridRowEnd { get; set; }
    
    // CSS customization
    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
    
    /// <summary>
    /// Gets the layout-related CSS classes
    /// </summary>
    protected string GetLayoutClasses()
    {
        var classes = new List<string>();
        
        // Width
        if (!string.IsNullOrEmpty(Width))
            classes.Add(CssHelper.IsSpaceScaleValue(Width) ? $"rt-r-w-{Width}" : "rt-r-w");
        if (!string.IsNullOrEmpty(MinWidth))
            classes.Add(CssHelper.IsSpaceScaleValue(MinWidth) ? $"rt-r-min-w-{MinWidth}" : "rt-r-min-w");
        if (!string.IsNullOrEmpty(MaxWidth))
            classes.Add(CssHelper.IsSpaceScaleValue(MaxWidth) ? $"rt-r-max-w-{MaxWidth}" : "rt-r-max-w");
        
        // Height
        if (!string.IsNullOrEmpty(Height))
            classes.Add(CssHelper.IsSpaceScaleValue(Height) ? $"rt-r-h-{Height}" : "rt-r-h");
        if (!string.IsNullOrEmpty(MinHeight))
            classes.Add(CssHelper.IsSpaceScaleValue(MinHeight) ? $"rt-r-min-h-{MinHeight}" : "rt-r-min-h");
        if (!string.IsNullOrEmpty(MaxHeight))
            classes.Add(CssHelper.IsSpaceScaleValue(MaxHeight) ? $"rt-r-max-h-{MaxHeight}" : "rt-r-max-h");
        
        // Position
        if (!string.IsNullOrEmpty(Position))
            classes.Add($"rt-r-position-{Position}");
        
        // Inset
        if (!string.IsNullOrEmpty(Inset))
            classes.Add(CssHelper.IsSpaceScaleValue(Inset) ? $"rt-r-inset-{Inset}" : "rt-r-inset");
        if (!string.IsNullOrEmpty(Top))
            classes.Add(CssHelper.IsSpaceScaleValue(Top) ? $"rt-r-top-{Top}" : "rt-r-top");
        if (!string.IsNullOrEmpty(Right))
            classes.Add(CssHelper.IsSpaceScaleValue(Right) ? $"rt-r-right-{Right}" : "rt-r-right");
        if (!string.IsNullOrEmpty(Bottom))
            classes.Add(CssHelper.IsSpaceScaleValue(Bottom) ? $"rt-r-bottom-{Bottom}" : "rt-r-bottom");
        if (!string.IsNullOrEmpty(Left))
            classes.Add(CssHelper.IsSpaceScaleValue(Left) ? $"rt-r-left-{Left}" : "rt-r-left");
        
        // Overflow
        if (!string.IsNullOrEmpty(Overflow))
            classes.Add($"rt-r-overflow-{Overflow}");
        if (!string.IsNullOrEmpty(OverflowX))
            classes.Add($"rt-r-ox-{OverflowX}");
        if (!string.IsNullOrEmpty(OverflowY))
            classes.Add($"rt-r-oy-{OverflowY}");
        
        // Flex properties
        if (!string.IsNullOrEmpty(FlexBasis))
            classes.Add(CssHelper.IsSpaceScaleValue(FlexBasis) ? $"rt-r-fb-{FlexBasis}" : "rt-r-fb");
        if (!string.IsNullOrEmpty(FlexShrink))
            classes.Add($"rt-r-fs-{FlexShrink}");
        if (!string.IsNullOrEmpty(FlexGrow))
            classes.Add($"rt-r-fg-{FlexGrow}");
        
        // Grid properties
        if (!string.IsNullOrEmpty(GridColumn))
            classes.Add("rt-r-gc");
        if (!string.IsNullOrEmpty(GridColumnStart))
            classes.Add($"rt-r-gcs-{GridColumnStart}");
        if (!string.IsNullOrEmpty(GridColumnEnd))
            classes.Add($"rt-r-gce-{GridColumnEnd}");
        if (!string.IsNullOrEmpty(GridRow))
            classes.Add("rt-r-gr");
        if (!string.IsNullOrEmpty(GridRowStart))
            classes.Add($"rt-r-grs-{GridRowStart}");
        if (!string.IsNullOrEmpty(GridRowEnd))
            classes.Add($"rt-r-gre-{GridRowEnd}");
        
        return string.Join(" ", classes);
    }
    
    /// <summary>
    /// Gets the layout-related inline styles (CSS custom properties)
    /// </summary>
    protected Dictionary<string, string> GetLayoutStyles()
    {
        var styles = new Dictionary<string, string>();
        
        // Add custom width values
        if (!string.IsNullOrEmpty(Width) && !CssHelper.IsSpaceScaleValue(Width))
            styles["--width"] = Width;
        if (!string.IsNullOrEmpty(MinWidth) && !CssHelper.IsSpaceScaleValue(MinWidth))
            styles["--min-width"] = MinWidth;
        if (!string.IsNullOrEmpty(MaxWidth) && !CssHelper.IsSpaceScaleValue(MaxWidth))
            styles["--max-width"] = MaxWidth;
        
        // Add custom height values
        if (!string.IsNullOrEmpty(Height) && !CssHelper.IsSpaceScaleValue(Height))
            styles["--height"] = Height;
        if (!string.IsNullOrEmpty(MinHeight) && !CssHelper.IsSpaceScaleValue(MinHeight))
            styles["--min-height"] = MinHeight;
        if (!string.IsNullOrEmpty(MaxHeight) && !CssHelper.IsSpaceScaleValue(MaxHeight))
            styles["--max-height"] = MaxHeight;
        
        // Position values
        if (!string.IsNullOrEmpty(Inset) && !CssHelper.IsSpaceScaleValue(Inset))
            styles["--inset"] = Inset;
        if (!string.IsNullOrEmpty(Top) && !CssHelper.IsSpaceScaleValue(Top))
            styles["--top"] = Top;
        if (!string.IsNullOrEmpty(Right) && !CssHelper.IsSpaceScaleValue(Right))
            styles["--right"] = Right;
        if (!string.IsNullOrEmpty(Bottom) && !CssHelper.IsSpaceScaleValue(Bottom))
            styles["--bottom"] = Bottom;
        if (!string.IsNullOrEmpty(Left) && !CssHelper.IsSpaceScaleValue(Left))
            styles["--left"] = Left;
        
        // Flex values
        if (!string.IsNullOrEmpty(FlexBasis) && !CssHelper.IsSpaceScaleValue(FlexBasis))
            styles["--flex-basis"] = FlexBasis;
        
        // Grid values
        if (!string.IsNullOrEmpty(GridColumn))
            styles["--grid-column"] = GridColumn;
        if (!string.IsNullOrEmpty(GridRow))
            styles["--grid-row"] = GridRow;
        
        return styles;
    }
    
    /// <summary>
    /// Combines inline styles into a single string
    /// </summary>
    protected string BuildInlineStyles(Dictionary<string, string>? additionalStyles = null)
    {
        var allStyles = new List<string>();
        
        // Get layout styles
        var layoutStyles = GetLayoutStyles();
        foreach (var style in layoutStyles)
        {
            allStyles.Add($"{style.Key}: {style.Value}");
        }
        
        // Add additional styles
        if (additionalStyles != null)
        {
            foreach (var style in additionalStyles)
            {
                allStyles.Add($"{style.Key}: {style.Value}");
            }
        }
        
        // Add user-provided styles
        if (!string.IsNullOrEmpty(Style))
            allStyles.Add(Style);
        
        return string.Join("; ", allStyles);
    }
    
    /// <summary>
    /// Gets all attributes including additional attributes
    /// </summary>
    protected Dictionary<string, object> GetAllAttributes()
    {
        var attributes = new Dictionary<string, object>();
        
        // Merge with additional attributes
        if (AdditionalAttributes != null)
        {
            foreach (var attr in AdditionalAttributes)
            {
                attributes[attr.Key] = attr.Value;
            }
        }
        
        return attributes;
    }
}