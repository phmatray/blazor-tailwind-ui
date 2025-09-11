using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Separator : SpacingComponentBase
{
    [Parameter] public SeparatorOrientation? Orientation { get; set; } = SeparatorOrientation.Horizontal;
    [Parameter] public SeparatorSize? Size { get; set; } = SeparatorSize.Size1;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool Decorative { get; set; } = true;
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Separator")
                .AddClass(GetOrientationClass())
                .AddClass(GetSizeClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetOrientationClass()
    {
        return Orientation switch
        {
            SeparatorOrientation.Horizontal => "rt-r-orientation-horizontal",
            SeparatorOrientation.Vertical => "rt-r-orientation-vertical",
            _ => ""
        };
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            SeparatorSize.Size1 => "rt-r-size-1",
            SeparatorSize.Size2 => "rt-r-size-2",
            SeparatorSize.Size3 => "rt-r-size-3",
            SeparatorSize.Size4 => "rt-r-size-4",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetSeparatorAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        // Add role for semantic separation (when not decorative)
        if (!Decorative)
            attributes["role"] = "separator";
        
        // Add aria-orientation for vertical separators
        if (Orientation == SeparatorOrientation.Vertical && !Decorative)
            attributes["aria-orientation"] = "vertical";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum SeparatorOrientation
{
    Horizontal,
    Vertical
}

public enum SeparatorSize
{
    Size1,
    Size2,
    Size3,
    Size4
}