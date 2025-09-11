using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ShadBz.BlazorApp.Components.UI.Base;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI;

public partial class IconButton : SpacingComponentBase, IAsChildSupport
{
    // Core button properties
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Solid;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string ButtonType { get; set; } = "button";
    
    // Event handlers
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    
    protected bool IsDisabled => Disabled || Loading;
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-BaseButton")
                .AddClass("rt-IconButton")
                .AddClass($"rt-r-size-{GetSizeValue()}")
                .AddClass($"rt-variant-{Variant.ToString().ToLower()}")
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass("rt-loading", Loading)
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    protected new Dictionary<string, object> GetAllAttributes()
    {
        var attributes = base.GetAllAttributes();
        
        attributes["class"] = CssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        if (IsDisabled)
            attributes["data-disabled"] = "";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected async Task HandleClick(MouseEventArgs e)
    {
        if (!IsDisabled && OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }
    }
    
    protected string GetSpinnerSize()
    {
        return Size switch
        {
            ButtonSize.Small => "1",
            ButtonSize.Medium => "2",
            ButtonSize.Large => "2",
            ButtonSize.ExtraLarge => "3",
            _ => "2"
        };
    }
    
    private string GetSizeValue()
    {
        return Size switch
        {
            ButtonSize.Small => "1",
            ButtonSize.Medium => "2",
            ButtonSize.Large => "3",
            ButtonSize.ExtraLarge => "4",
            _ => "2"
        };
    }
}