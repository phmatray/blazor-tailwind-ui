using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.DataDisplay;

public partial class Badge : SpacingComponentBase, IAsChildSupport
{
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public BadgeSize? Size { get; set; } = BadgeSize.Size1;
    [Parameter] public BadgeVariant? Variant { get; set; } = BadgeVariant.Soft;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public string? Radius { get; set; }
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-Badge")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            BadgeSize.Size1 => "rt-r-size-1",
            BadgeSize.Size2 => "rt-r-size-2",
            BadgeSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            BadgeVariant.Solid => "rt-variant-solid",
            BadgeVariant.Soft => "rt-variant-soft",
            BadgeVariant.Surface => "rt-variant-surface",
            BadgeVariant.Outline => "rt-variant-outline",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetBadgeAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum BadgeSize
{
    Size1,
    Size2,
    Size3
}

public enum BadgeVariant
{
    Solid,
    Soft,
    Surface,
    Outline
}