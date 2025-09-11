using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Flex : GapComponentBase, IAsChildSupport
{
    // Core Flex properties
    [Parameter] public string As { get; set; } = "div"; // "div" or "span"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public FlexDisplay? Display { get; set; }
    [Parameter] public FlexDirection? Direction { get; set; }
    [Parameter] public FlexAlign? Align { get; set; }
    [Parameter] public FlexJustify? Justify { get; set; }
    [Parameter] public FlexWrap? Wrap { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Flex")
                .AddClass(GetDisplayClass())
                .AddClass(GetDirectionClass())
                .AddClass(GetAlignClass())
                .AddClass(GetJustifyClass())
                .AddClass(GetWrapClass())
                .AddClass(GetGapClasses())        // From GapComponentBase
                .AddClass(GetLayoutClasses())     // From LayoutComponentBase
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(GetPaddingClasses())    // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetDisplayClass()
    {
        return Display switch
        {
            FlexDisplay.None => "rt-r-display-none",
            FlexDisplay.InlineFlex => "rt-r-display-inline-flex",
            FlexDisplay.Flex => "rt-r-display-flex",
            _ => ""
        };
    }
    
    private string GetDirectionClass()
    {
        return Direction switch
        {
            FlexDirection.Row => "rt-r-fd-row",
            FlexDirection.Column => "rt-r-fd-column",
            FlexDirection.RowReverse => "rt-r-fd-row-reverse",
            FlexDirection.ColumnReverse => "rt-r-fd-column-reverse",
            _ => ""
        };
    }
    
    private string GetAlignClass()
    {
        return Align switch
        {
            FlexAlign.Start => "rt-r-ai-start",
            FlexAlign.Center => "rt-r-ai-center",
            FlexAlign.End => "rt-r-ai-end",
            FlexAlign.Baseline => "rt-r-ai-baseline",
            FlexAlign.Stretch => "rt-r-ai-stretch",
            _ => ""
        };
    }
    
    private string GetJustifyClass()
    {
        return Justify switch
        {
            FlexJustify.Start => "rt-r-jc-start",
            FlexJustify.Center => "rt-r-jc-center",
            FlexJustify.End => "rt-r-jc-end",
            FlexJustify.Between => "rt-r-jc-space-between",
            _ => ""
        };
    }
    
    private string GetWrapClass()
    {
        return Wrap switch
        {
            FlexWrap.NoWrap => "rt-r-fw-nowrap",
            FlexWrap.Wrap => "rt-r-fw-wrap",
            FlexWrap.WrapReverse => "rt-r-fw-wrap-reverse",
            _ => ""
        };
    }
    
    protected string GetInlineStyles()
    {
        var additionalStyles = GetGapStyles(); // From GapComponentBase
        return BuildInlineStyles(additionalStyles); // From LayoutComponentBase
    }
    
    protected Dictionary<string, object> GetFlexAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        var inlineStyles = GetInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum FlexDisplay
{
    None,
    InlineFlex,
    Flex
}

public enum FlexDirection
{
    Row,
    Column,
    RowReverse,
    ColumnReverse
}

public enum FlexAlign
{
    Start,
    Center,
    End,
    Baseline,
    Stretch
}

public enum FlexJustify
{
    Start,
    Center,
    End,
    Between
}

public enum FlexWrap
{
    NoWrap,
    Wrap,
    WrapReverse
}