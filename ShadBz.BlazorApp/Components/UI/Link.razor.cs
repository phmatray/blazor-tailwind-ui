using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Link : SpacingComponentBase, IAsChildSupport
{
    // Core Link properties
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public string? Size { get; set; } // "1" through "9"
    [Parameter] public TextWeight? Weight { get; set; }
    [Parameter] public LinkUnderline? Underline { get; set; } = LinkUnderline.Auto;
    [Parameter] public bool Trim { get; set; } // Leading trim for better alignment
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public TextWrap? Wrap { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    
    // Link-specific properties
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? Target { get; set; }
    [Parameter] public string? Rel { get; set; }
    [Parameter] public bool Download { get; set; }
    
    // Button properties (when rendered as button)
    [Parameter] public string? Type { get; set; } = "button";
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-Link")
                .AddClass(GetSizeClass())
                .AddClass(GetWeightClass())
                .AddClass(GetUnderlineClass())
                .AddClass(GetWrapClass())
                .AddClass("rt-truncate", Truncate)
                .AddClass("rt-leading-trim-start", Trim)
                .AddClass("rt-leading-trim-end", Trim)
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return !string.IsNullOrEmpty(Size) ? $"rt-r-size-{Size}" : "";
    }
    
    private string GetWeightClass()
    {
        return Weight switch
        {
            TextWeight.Light => "rt-r-weight-light",
            TextWeight.Regular => "rt-r-weight-regular",
            TextWeight.Medium => "rt-r-weight-medium",
            TextWeight.Bold => "rt-r-weight-bold",
            _ => ""
        };
    }
    
    private string GetUnderlineClass()
    {
        return Underline switch
        {
            LinkUnderline.Auto => "rt-underline-auto",
            LinkUnderline.Always => "rt-underline-always",
            LinkUnderline.Hover => "rt-underline-hover",
            LinkUnderline.None => "rt-underline-none",
            _ => ""
        };
    }
    
    private string GetWrapClass()
    {
        return Wrap switch
        {
            TextWrap.Wrap => "rt-r-tw-wrap",
            TextWrap.NoWrap => "rt-r-tw-nowrap",
            TextWrap.Pretty => "rt-r-tw-pretty",
            TextWrap.Balance => "rt-r-tw-balance",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetLinkAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        var inlineStyles = BuildInlineStyles(); // From LayoutComponentBase
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        // Add link-specific attributes
        if (!string.IsNullOrEmpty(Href))
            attributes["href"] = Href;
        if (!string.IsNullOrEmpty(Target))
            attributes["target"] = Target;
        if (!string.IsNullOrEmpty(Rel))
            attributes["rel"] = Rel;
        if (Download)
            attributes["download"] = true;
        
        // Add button-specific attributes when rendered as button
        if (string.IsNullOrEmpty(Href) && !AsChild)
        {
            attributes["type"] = Type!;
            if (Disabled)
                attributes["disabled"] = true;
            if (OnClick.HasDelegate)
                attributes["onclick"] = OnClick;
        }
        
        return attributes;
    }
    
    protected bool ShouldRenderAsButton()
    {
        // Render as button if no href is provided and not using AsChild
        return string.IsNullOrEmpty(Href) && !AsChild;
    }
}

public enum LinkUnderline
{
    Auto,
    Always,
    Hover,
    None
}