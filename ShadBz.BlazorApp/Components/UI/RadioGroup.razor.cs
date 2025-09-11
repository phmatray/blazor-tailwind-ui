using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class RadioGroup : SpacingComponentBase
{
    private readonly List<Radio> _radios = new();
    private string? _internalValue;
    
    [Parameter] public RadioSize? Size { get; set; } = RadioSize.Size2;
    [Parameter] public RadioVariant? Variant { get; set; } = RadioVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public RadioGroupOrientation Orientation { get; set; } = RadioGroupOrientation.Vertical;
    
    // Value management
    [Parameter] public string? Value { get; set; }
    
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }
    
    // Default value for uncontrolled usage
    [Parameter] public string? DefaultValue { get; set; }
    
    // Name for form submission
    [Parameter] public string? Name { get; set; }
    
    private bool _isControlled;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        
        _isControlled = ValueChanged.HasDelegate;
        
        if (!_isControlled && !string.IsNullOrEmpty(DefaultValue))
        {
            _internalValue = DefaultValue;
        }
        else if (!_isControlled && !string.IsNullOrEmpty(Value))
        {
            _internalValue = Value;
        }
    }
    
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        
        if (_isControlled)
        {
            _internalValue = Value;
        }
    }
    
    private string? CurrentValue => _isControlled ? Value : _internalValue;
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-RadioGroupRoot")
                .AddClass($"rt-r-size-{GetSizeValue()}")
                .AddClass($"rt-variant-{Variant.ToString()?.ToLower()}")
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass($"rt-r-orientation-{Orientation.ToString().ToLower()}")
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeValue()
    {
        return Size switch
        {
            RadioSize.Size1 => "1",
            RadioSize.Size2 => "2",
            RadioSize.Size3 => "3",
            _ => "2"
        };
    }
    
    protected Dictionary<string, object> GetRadioGroupAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (Disabled)
            attributes["data-disabled"] = "";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    public async Task SetValue(string value)
    {
        if (_isControlled)
        {
            // Controlled mode - notify parent
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
        }
        else
        {
            // Uncontrolled mode - update internal state
            _internalValue = value;
            StateHasChanged();
        }
    }
    
    internal void RegisterRadio(Radio radio)
    {
        if (!_radios.Contains(radio))
        {
            _radios.Add(radio);
        }
    }
    
    internal void UnregisterRadio(Radio radio)
    {
        _radios.Remove(radio);
    }
    
    internal string? GetInternalValue() => _internalValue;
}

public enum RadioGroupOrientation
{
    Vertical,
    Horizontal
}

// RadioGroupItem component for convenience
public class RadioGroupItem : ComponentBase
{
    [CascadingParameter] private RadioGroup? RadioGroup { get; set; }
    
    [Parameter, EditorRequired] public string Value { get; set; } = string.Empty;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "label");
        builder.AddAttribute(1, "class", "rt-RadioGroupItem");
        
        builder.OpenComponent<Flex>(2);
        builder.AddAttribute(3, "Gap", "2");
        builder.AddAttribute(4, "Align", FlexAlign.Center);
        builder.AddAttribute(5, "ChildContent", (RenderFragment)(builder2 =>
        {
            builder2.OpenComponent<Radio>(6);
            builder2.AddAttribute(7, "Value", Value);
            builder2.AddAttribute(8, "Disabled", Disabled);
            builder2.CloseComponent();
            
            if (ChildContent != null)
            {
                builder2.OpenComponent<Text>(9);
                builder2.AddAttribute(10, "Size", "2");
                builder2.AddAttribute(11, "ChildContent", ChildContent);
                builder2.CloseComponent();
            }
        }));
        builder.CloseComponent();
        
        builder.CloseElement();
    }
}