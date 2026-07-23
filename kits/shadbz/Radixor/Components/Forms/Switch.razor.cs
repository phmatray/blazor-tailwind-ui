using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Forms;

public partial class Switch : SpacingComponentBase
{
    [Parameter] public SwitchSize? Size { get; set; } = SwitchSize.Size2;
    [Parameter] public SwitchVariant? Variant { get; set; } = SwitchVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    // State management
    [Parameter] public bool Checked { get; set; }
    [Parameter] public EventCallback<bool> CheckedChanged { get; set; }
    
    // Default state for uncontrolled usage
    [Parameter] public bool? DefaultChecked { get; set; }
    
    private bool _internalChecked;
    private bool _isControlled;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        _isControlled = CheckedChanged.HasDelegate;
        
        if (!_isControlled)
        {
            _internalChecked = DefaultChecked ?? false;
        }
    }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        if (_isControlled)
        {
            _internalChecked = Checked;
        }
        else if (!CheckedChanged.HasDelegate && Checked != _internalChecked)
        {
            // Update internal state if Checked is set but we're in uncontrolled mode
            _internalChecked = Checked;
        }
    }
    
    protected bool IsChecked => _isControlled ? Checked : _internalChecked;
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-SwitchRoot")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            SwitchSize.Size1 => "rt-r-size-1",
            SwitchSize.Size2 => "rt-r-size-2",
            SwitchSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            SwitchVariant.Classic => "rt-variant-classic",
            SwitchVariant.Surface => "rt-variant-surface",
            SwitchVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetSwitchAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        // Add state attributes
        attributes["data-state"] = IsChecked ? "checked" : "unchecked";
        
        if (Disabled)
            attributes["data-disabled"] = "";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected async Task HandleClick(MouseEventArgs e)
    {
        if (Disabled) return;
        
        var newValue = !IsChecked;
        
        if (_isControlled)
        {
            // Controlled mode - notify parent
            if (CheckedChanged.HasDelegate)
            {
                await CheckedChanged.InvokeAsync(newValue);
            }
        }
        else
        {
            // Uncontrolled mode - update internal state
            _internalChecked = newValue;
            StateHasChanged();
        }
    }
}

public enum SwitchSize
{
    Size1,
    Size2,
    Size3
}

public enum SwitchVariant
{
    Classic,
    Surface,
    Soft
}