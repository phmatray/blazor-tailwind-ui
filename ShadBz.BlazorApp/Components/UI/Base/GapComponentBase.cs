using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI.Base;

/// <summary>
/// Base class for components that support gap properties (Flex, Grid)
/// </summary>
public abstract class GapComponentBase : SpacingComponentBase
{
    // Gap properties
    [Parameter] public string? Gap { get; set; }
    [Parameter] public string? GapX { get; set; }
    [Parameter] public string? GapY { get; set; }
    
    /// <summary>
    /// Gets the gap-related CSS classes
    /// </summary>
    protected string GetGapClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(Gap))
        {
            if (CssHelper.IsSpaceScaleValue(Gap))
                classes.Add($"rt-r-gap-{Gap}");
            else
                classes.Add("rt-r-gap");
        }
        
        if (!string.IsNullOrEmpty(GapX))
        {
            if (CssHelper.IsSpaceScaleValue(GapX))
                classes.Add($"rt-r-cg-{GapX}");
            else
                classes.Add("rt-r-cg");
        }
        
        if (!string.IsNullOrEmpty(GapY))
        {
            if (CssHelper.IsSpaceScaleValue(GapY))
                classes.Add($"rt-r-rg-{GapY}");
            else
                classes.Add("rt-r-rg");
        }
        
        return string.Join(" ", classes);
    }
    
    /// <summary>
    /// Gets the gap-related inline styles (CSS custom properties)
    /// </summary>
    protected Dictionary<string, string> GetGapStyles()
    {
        var styles = new Dictionary<string, string>();
        
        // Add custom gap values
        if (!string.IsNullOrEmpty(Gap) && !CssHelper.IsSpaceScaleValue(Gap))
            styles["--gap"] = Gap;
        if (!string.IsNullOrEmpty(GapX) && !CssHelper.IsSpaceScaleValue(GapX))
            styles["--column-gap"] = GapX;
        if (!string.IsNullOrEmpty(GapY) && !CssHelper.IsSpaceScaleValue(GapY))
            styles["--row-gap"] = GapY;
        
        return styles;
    }
}