using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Card : SpacingComponentBase, IAsChildSupport
{
    // Core Card properties
    [Parameter] public string As { get; set; } = "div"; // "div", "button", "a", or "label"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public CardSize? Size { get; set; } = CardSize.Size1; // Default size="1"
    [Parameter] public CardVariant? Variant { get; set; } = CardVariant.Surface; // Default variant="surface"
    
    // Link properties (when As="a")
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? Target { get; set; }
    
    // Button properties (when As="button")
    [Parameter] public string? Type { get; set; } = "button";
    [Parameter] public bool Disabled { get; set; }
    
    // Interactive state
    [Parameter] public string? DataState { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-BaseCard")
                .AddClass("rt-Card")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(GetPaddingClasses())    // From SpacingComponentBase (for ghost variant override)
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            CardSize.Size1 => "rt-r-size-1",
            CardSize.Size2 => "rt-r-size-2",
            CardSize.Size3 => "rt-r-size-3",
            CardSize.Size4 => "rt-r-size-4",
            CardSize.Size5 => "rt-r-size-5",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            CardVariant.Surface => "rt-variant-surface",
            CardVariant.Classic => "rt-variant-classic",
            CardVariant.Ghost => "rt-variant-ghost",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetCardAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        var inlineStyles = BuildInlineStyles(); // From LayoutComponentBase
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        // Add specific attributes based on the element type
        if (As == "a" && !string.IsNullOrEmpty(Href))
        {
            attributes["href"] = Href;
            if (!string.IsNullOrEmpty(Target))
                attributes["target"] = Target;
        }
        else if (As == "button")
        {
            attributes["type"] = Type!;
            if (Disabled)
                attributes["disabled"] = true;
        }
        
        // Add data-state if provided
        if (!string.IsNullOrEmpty(DataState))
            attributes["data-state"] = DataState;
        
        return attributes;
    }
}

public enum CardSize
{
    Size1,
    Size2,
    Size3,
    Size4,
    Size5
}

public enum CardVariant
{
    Surface,
    Classic,
    Ghost
}