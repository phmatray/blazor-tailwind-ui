using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Layout;

public partial class Container : SpacingComponentBase, IAsChildSupport
{
    // Core Container properties
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public ContainerSize? Size { get; set; } = ContainerSize.Size4; // Default size="4"
    [Parameter] public ContainerDisplay? Display { get; set; }
    [Parameter] public ContainerAlign? Align { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Container")
                .AddClass(GetSizeClass())
                .AddClass(GetDisplayClass())
                .AddClass(GetAlignClass())
                .AddClass(GetLayoutClasses())     // From LayoutComponentBase
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(GetPaddingClasses())    // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    protected string InnerCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-ContainerInner")
                .AddClass(GetInnerWidthClasses())
                .AddClass(GetInnerHeightClasses());
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            ContainerSize.Size1 => "rt-r-size-1",
            ContainerSize.Size2 => "rt-r-size-2",
            ContainerSize.Size3 => "rt-r-size-3",
            ContainerSize.Size4 => "rt-r-size-4",
            _ => ""
        };
    }
    
    private string GetDisplayClass()
    {
        return Display switch
        {
            ContainerDisplay.None => "rt-r-display-none",
            ContainerDisplay.Block => "rt-r-display-block",
            _ => ""
        };
    }
    
    private string GetAlignClass()
    {
        return Align switch
        {
            ContainerAlign.Left => "rt-r-mx-0",
            ContainerAlign.Center => "rt-r-mx-auto",
            ContainerAlign.Right => "rt-r-mx-0 rt-r-ml-auto",
            _ => ""
        };
    }
    
    private string GetInnerWidthClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(Width))
            classes.Add(CssHelper.IsSpaceScaleValue(Width) ? $"rt-r-w-{Width}" : "rt-r-w");
        if (!string.IsNullOrEmpty(MinWidth))
            classes.Add(CssHelper.IsSpaceScaleValue(MinWidth) ? $"rt-r-min-w-{MinWidth}" : "rt-r-min-w");
        if (!string.IsNullOrEmpty(MaxWidth))
            classes.Add(CssHelper.IsSpaceScaleValue(MaxWidth) ? $"rt-r-max-w-{MaxWidth}" : "rt-r-max-w");
        
        return string.Join(" ", classes);
    }
    
    private string GetInnerHeightClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(Height))
            classes.Add(CssHelper.IsSpaceScaleValue(Height) ? $"rt-r-h-{Height}" : "rt-r-h");
        if (!string.IsNullOrEmpty(MinHeight))
            classes.Add(CssHelper.IsSpaceScaleValue(MinHeight) ? $"rt-r-min-h-{MinHeight}" : "rt-r-min-h");
        if (!string.IsNullOrEmpty(MaxHeight))
            classes.Add(CssHelper.IsSpaceScaleValue(MaxHeight) ? $"rt-r-max-h-{MaxHeight}" : "rt-r-max-h");
        
        return string.Join(" ", classes);
    }
    
    protected string GetInlineStyles()
    {
        var additionalStyles = new Dictionary<string, string>();
        
        // Add custom inner container dimension values
        if (!string.IsNullOrEmpty(Width) && !CssHelper.IsSpaceScaleValue(Width))
            additionalStyles["--width"] = Width;
        if (!string.IsNullOrEmpty(MinWidth) && !CssHelper.IsSpaceScaleValue(MinWidth))
            additionalStyles["--min-width"] = MinWidth;
        if (!string.IsNullOrEmpty(MaxWidth) && !CssHelper.IsSpaceScaleValue(MaxWidth))
            additionalStyles["--max-width"] = MaxWidth;
        if (!string.IsNullOrEmpty(Height) && !CssHelper.IsSpaceScaleValue(Height))
            additionalStyles["--height"] = Height;
        if (!string.IsNullOrEmpty(MinHeight) && !CssHelper.IsSpaceScaleValue(MinHeight))
            additionalStyles["--min-height"] = MinHeight;
        if (!string.IsNullOrEmpty(MaxHeight) && !CssHelper.IsSpaceScaleValue(MaxHeight))
            additionalStyles["--max-height"] = MaxHeight;
        
        return BuildInlineStyles(additionalStyles); // From LayoutComponentBase
    }
    
    protected Dictionary<string, object> GetContainerAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        var inlineStyles = GetInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum ContainerSize
{
    Size1,
    Size2,
    Size3,
    Size4
}

public enum ContainerDisplay
{
    None,
    Block
}

public enum ContainerAlign
{
    Left,
    Center,
    Right
}