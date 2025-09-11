using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Section : SpacingComponentBase, IAsChildSupport
{
    // Core Section properties
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public SectionSize? Size { get; set; } = SectionSize.Size3; // Default size="3"
    [Parameter] public SectionDisplay? Display { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Section")
                .AddClass(GetSizeClass())
                .AddClass(GetDisplayClass())
                .AddClass(GetLayoutClasses())     // From LayoutComponentBase
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(GetPaddingClasses())    // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            SectionSize.Size1 => "rt-r-size-1",
            SectionSize.Size2 => "rt-r-size-2",
            SectionSize.Size3 => "rt-r-size-3",
            SectionSize.Size4 => "rt-r-size-4",
            _ => ""
        };
    }
    
    private string GetDisplayClass()
    {
        return Display switch
        {
            SectionDisplay.None => "rt-r-display-none",
            SectionDisplay.Initial => "rt-r-display-block",
            _ => ""
        };
    }
    
    protected string GetInlineStyles()
    {
        return BuildInlineStyles(); // From LayoutComponentBase
    }
    
    protected Dictionary<string, object> GetSectionAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        var inlineStyles = GetInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum SectionSize
{
    Size1,
    Size2,
    Size3,
    Size4
}

public enum SectionDisplay
{
    None,
    Initial
}