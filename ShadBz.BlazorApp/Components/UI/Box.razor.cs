using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Box : SpacingComponentBase, IAsChildSupport
{
    // Core Box properties
    [Parameter] public string As { get; set; } = "div"; // "div" or "span"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public string? Display { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Box")
                .AddClass(GetDisplayClass())
                .AddClass(GetLayoutClasses())     // From LayoutComponentBase
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(GetPaddingClasses())    // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetDisplayClass()
    {
        return !string.IsNullOrEmpty(Display) ? $"rt-r-display-{Display}" : "";
    }
    
    protected Dictionary<string, object> GetBoxAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        var inlineStyles = BuildInlineStyles(); // From LayoutComponentBase
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}