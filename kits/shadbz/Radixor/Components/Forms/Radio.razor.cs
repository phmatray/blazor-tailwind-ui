using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Forms;

public partial class Radio : SpacingComponentBase
{
    [CascadingParameter] private RadioGroup? RadioGroup { get; set; }
    
    [Parameter, EditorRequired] public string Value { get; set; } = string.Empty;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public EventCallback<string> OnValueChange { get; set; }
    
    // These are typically set by RadioGroup via cascading parameter
    [Parameter] public RadioSize? Size { get; set; }
    [Parameter] public RadioVariant? Variant { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    
    protected bool IsChecked => RadioGroup != null && 
        ((RadioGroup.ValueChanged.HasDelegate ? RadioGroup.Value : RadioGroup.GetInternalValue()) == Value);
    
    internal string? GetGroupValue() => RadioGroup?.GetInternalValue();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        RadioGroup?.RegisterRadio(this);
    }
    
    protected string RootCssClass
    {
        get
        {
            var actualSize = Size ?? RadioGroup?.Size ?? RadioSize.Size2;
            var actualVariant = Variant ?? RadioGroup?.Variant ?? RadioVariant.Surface;
            var actualHighContrast = HighContrast || (RadioGroup?.HighContrast ?? false);
            
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-BaseRadioRoot")
                .AddClass("rt-RadioRoot")
                .AddClass(GetSizeClass(actualSize))
                .AddClass(GetVariantClass(actualVariant))
                .AddClass("rt-high-contrast", actualHighContrast)
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass(RadioSize size)
    {
        return size switch
        {
            RadioSize.Size1 => "rt-r-size-1",
            RadioSize.Size2 => "rt-r-size-2",
            RadioSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass(RadioVariant variant)
    {
        return variant switch
        {
            RadioVariant.Classic => "rt-variant-classic",
            RadioVariant.Surface => "rt-variant-surface",
            RadioVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetRadioAttributes()
    {
        var actualColor = Color ?? RadioGroup?.Color;
        
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(actualColor))
            attributes["data-accent-color"] = actualColor;
        
        attributes["data-state"] = IsChecked ? "checked" : "unchecked";
        
        if (Disabled || (RadioGroup?.Disabled ?? false))
            attributes["data-disabled"] = "";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected async Task HandleClick(MouseEventArgs e)
    {
        if (Disabled || (RadioGroup?.Disabled ?? false)) return;
        
        if (RadioGroup != null)
        {
            await RadioGroup.SetValue(Value);
        }
        
        if (OnValueChange.HasDelegate)
        {
            await OnValueChange.InvokeAsync(Value);
        }
    }
    
    public void Dispose()
    {
        RadioGroup?.UnregisterRadio(this);
    }
}

public enum RadioSize
{
    Size1,
    Size2,
    Size3
}

public enum RadioVariant
{
    Classic,
    Surface,
    Soft
}