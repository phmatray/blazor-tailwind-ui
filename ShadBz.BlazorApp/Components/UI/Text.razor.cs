using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Text : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    // Core Text properties
    [Parameter] public string As { get; set; } = "span"; // "span", "div", "label", "p"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public string? Size { get; set; } // "1" through "9"
    [Parameter] public TextWeight? Weight { get; set; }
    [Parameter] public TextAlign? Align { get; set; }
    [Parameter] public bool Trim { get; set; } // Leading trim for better alignment
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public TextWrap? Wrap { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    
    // Margin properties
    [Parameter] public string? M { get; set; }
    [Parameter] public string? Mx { get; set; }
    [Parameter] public string? My { get; set; }
    [Parameter] public string? Mt { get; set; }
    [Parameter] public string? Mr { get; set; }
    [Parameter] public string? Mb { get; set; }
    [Parameter] public string? Ml { get; set; }
    
    // CSS customization
    [Parameter] public string? Class { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Text")
                .AddClass(GetSizeClass())
                .AddClass(GetWeightClass())
                .AddClass(GetAlignClass())
                .AddClass(GetWrapClass())
                .AddClass("rt-truncate", Truncate)
                .AddClass("rt-leading-trim-start", Trim)
                .AddClass("rt-leading-trim-end", Trim)
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass(GetMarginClasses())
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
    
    private string GetAlignClass()
    {
        return Align switch
        {
            TextAlign.Left => "rt-r-ta-left",
            TextAlign.Center => "rt-r-ta-center",
            TextAlign.Right => "rt-r-ta-right",
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
    
    private string GetMarginClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(M))
            classes.Add($"rt-r-m-{M}");
        if (!string.IsNullOrEmpty(Mx))
            classes.Add($"rt-r-mx-{Mx}");
        if (!string.IsNullOrEmpty(My))
            classes.Add($"rt-r-my-{My}");
        if (!string.IsNullOrEmpty(Mt))
            classes.Add($"rt-r-mt-{Mt}");
        if (!string.IsNullOrEmpty(Mr))
            classes.Add($"rt-r-mr-{Mr}");
        if (!string.IsNullOrEmpty(Mb))
            classes.Add($"rt-r-mb-{Mb}");
        if (!string.IsNullOrEmpty(Ml))
            classes.Add($"rt-r-ml-{Ml}");
        
        return string.Join(" ", classes);
    }
    
    protected Dictionary<string, object> GetTextAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = CssClass;
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        return attributes;
    }
    
    protected Dictionary<string, object> GetAllAttributes()
    {
        var attributes = new Dictionary<string, object>();
        
        // Merge with additional attributes
        if (AdditionalAttributes != null)
        {
            foreach (var attr in AdditionalAttributes)
            {
                attributes[attr.Key] = attr.Value;
            }
        }
        
        return attributes;
    }
}

public enum TextWeight
{
    Light,
    Regular,
    Medium,
    Bold
}

public enum TextAlign
{
    Left,
    Center,
    Right
}

public enum TextWrap
{
    Wrap,
    NoWrap,
    Pretty,
    Balance
}