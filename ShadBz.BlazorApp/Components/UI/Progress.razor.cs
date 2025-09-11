using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Progress : SpacingComponentBase
{
    [Parameter] public ProgressSize? Size { get; set; } = ProgressSize.Size2;
    [Parameter] public ProgressVariant? Variant { get; set; } = ProgressVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    
    // Progress value (0-100)
    [Parameter] public double? Value { get; set; }
    [Parameter] public double Max { get; set; } = 100;
    
    // Animation duration for indeterminate progress
    [Parameter] public string? Duration { get; set; } = "660ms";
    
    // Accessibility
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public string? AriaValueText { get; set; }
    
    private bool IsIndeterminate => !Value.HasValue;
    
    private double ProgressPercentage
    {
        get
        {
            if (!Value.HasValue) return 0;
            if (Max <= 0) return 0;
            
            var percentage = (Value.Value / Max) * 100;
            return Math.Max(0, Math.Min(100, percentage));
        }
    }
    
    private string ProgressCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-ProgressRoot")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass(GetMarginClasses())
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            ProgressSize.Size1 => "rt-r-size-1",
            ProgressSize.Size2 => "rt-r-size-2",
            ProgressSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            ProgressVariant.Classic => "rt-variant-classic",
            ProgressVariant.Surface => "rt-variant-surface",
            ProgressVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    private Dictionary<string, object> GetProgressAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = ProgressCssClass;
        attributes["role"] = "progressbar";
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        if (Value.HasValue)
        {
            attributes["aria-valuenow"] = Value.Value.ToString();
            attributes["aria-valuemin"] = "0";
            attributes["aria-valuemax"] = Max.ToString();
            attributes["data-value"] = Value.Value.ToString();
            attributes["data-max"] = Max.ToString();
        }
        else
        {
            // Indeterminate state
            attributes["data-state"] = "indeterminate";
        }
        
        if (!string.IsNullOrEmpty(AriaLabel))
            attributes["aria-label"] = AriaLabel;
        
        if (!string.IsNullOrEmpty(AriaValueText))
            attributes["aria-valuetext"] = AriaValueText;
        
        var styles = new List<string>();
        
        if (IsIndeterminate && !string.IsNullOrEmpty(Duration))
        {
            styles.Add($"--progress-duration: {Duration}");
        }
        else if (Value.HasValue)
        {
            styles.Add($"--progress-value: {Value.Value}");
            styles.Add($"--progress-max: {Max}");
        }
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            styles.Add(inlineStyles);
        
        if (styles.Count > 0)
            attributes["style"] = string.Join("; ", styles);
        
        return attributes;
    }
    
    private string GetIndicatorStyle()
    {
        if (IsIndeterminate)
        {
            // For indeterminate progress, the animation is handled by CSS
            return "";
        }
        else
        {
            // For determinate progress, set the width
            return $"transform: translateX(-{100 - ProgressPercentage}%);";
        }
    }
}

public enum ProgressSize
{
    Size1,
    Size2,
    Size3
}

public enum ProgressVariant
{
    Classic,
    Surface,
    Soft
}