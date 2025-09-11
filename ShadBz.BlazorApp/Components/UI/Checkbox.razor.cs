using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Checkbox : SpacingComponentBase
{
    [Parameter] public CheckboxSize? Size { get; set; } = CheckboxSize.Size2;
    [Parameter] public CheckboxVariant? Variant { get; set; } = CheckboxVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    // State management
    [Parameter] public CheckboxState CheckedState { get; set; } = CheckboxState.Unchecked;
    [Parameter] public EventCallback<CheckboxState> CheckedStateChanged { get; set; }
    
    // Two-way binding support for bool
    [Parameter] public bool Checked { get; set; }
    [Parameter] public EventCallback<bool> CheckedChanged { get; set; }
    
    // Default state for uncontrolled usage
    [Parameter] public CheckboxState? DefaultChecked { get; set; }
    
    private CheckboxState _internalState;
    private bool _isControlled;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        // Determine if component is controlled
        _isControlled = CheckedStateChanged.HasDelegate || CheckedChanged.HasDelegate;
        
        if (!_isControlled)
        {
            // Uncontrolled mode - use default or current state
            _internalState = DefaultChecked ?? CheckboxState.Unchecked;
            
            // Sync with bool Checked parameter if provided
            if (Checked)
            {
                _internalState = CheckboxState.Checked;
            }
        }
    }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        // Sync CheckedState with Checked bool parameter
        if (_isControlled && CheckedChanged.HasDelegate && !CheckedStateChanged.HasDelegate)
        {
            CheckedState = Checked ? CheckboxState.Checked : CheckboxState.Unchecked;
        }
        else if (!_isControlled)
        {
            _internalState = CheckedState != CheckboxState.Unchecked ? CheckedState : _internalState;
        }
    }
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset")
                .AddClass("rt-BaseCheckboxRoot")
                .AddClass("rt-CheckboxRoot")
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
            CheckboxSize.Size1 => "rt-r-size-1",
            CheckboxSize.Size2 => "rt-r-size-2",
            CheckboxSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            CheckboxVariant.Classic => "rt-variant-classic",
            CheckboxVariant.Surface => "rt-variant-surface",
            CheckboxVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetCheckboxAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        // Add state attributes
        var currentState = _isControlled ? CheckedState : _internalState;
        attributes["data-state"] = currentState switch
        {
            CheckboxState.Checked => "checked",
            CheckboxState.Indeterminate => "indeterminate",
            _ => "unchecked"
        };
        
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
        
        var currentState = _isControlled ? CheckedState : _internalState;
        var newState = currentState switch
        {
            CheckboxState.Unchecked => CheckboxState.Checked,
            CheckboxState.Checked => CheckboxState.Unchecked,
            CheckboxState.Indeterminate => CheckboxState.Checked,
            _ => CheckboxState.Checked
        };
        
        if (_isControlled)
        {
            // Controlled mode - notify parent
            if (CheckedStateChanged.HasDelegate)
            {
                await CheckedStateChanged.InvokeAsync(newState);
            }
            
            if (CheckedChanged.HasDelegate)
            {
                await CheckedChanged.InvokeAsync(newState == CheckboxState.Checked);
            }
        }
        else
        {
            // Uncontrolled mode - update internal state
            _internalState = newState;
            StateHasChanged();
        }
    }
    
    protected string GetAriaChecked()
    {
        var currentState = _isControlled ? CheckedState : _internalState;
        return currentState switch
        {
            CheckboxState.Checked => "true",
            CheckboxState.Indeterminate => "mixed",
            _ => "false"
        };
    }
}

public enum CheckboxSize
{
    Size1,
    Size2,
    Size3
}

public enum CheckboxVariant
{
    Classic,
    Surface,
    Soft
}

public enum CheckboxState
{
    Unchecked,
    Checked,
    Indeterminate
}