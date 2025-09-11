using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Slider : SpacingComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JSRuntime { get; set; }
    
    [Parameter] public SliderSize? Size { get; set; } = SliderSize.Size2;
    [Parameter] public SliderVariant? Variant { get; set; } = SliderVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    // Value parameters
    [Parameter] public double[]? Value { get; set; }
    [Parameter] public EventCallback<double[]> ValueChanged { get; set; }
    [Parameter] public double[]? DefaultValue { get; set; }
    
    // Range parameters
    [Parameter] public double Min { get; set; } = 0;
    [Parameter] public double Max { get; set; } = 100;
    [Parameter] public double Step { get; set; } = 1;
    
    // Accessibility
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public string? Name { get; set; }
    
    // Events
    [Parameter] public EventCallback<double[]> OnValueChange { get; set; }
    [Parameter] public EventCallback<double[]> OnValueCommit { get; set; }
    
    private double[] _internalValues = new double[] { 0 };
    private bool _isDragging;
    private int _dragIndex;
    protected ElementReference _sliderElement;
    private DotNetObjectReference<Slider>? _dotNetRef;
    
    protected double[] Values
    {
        get
        {
            if (ValueChanged.HasDelegate && Value != null)
            {
                return Value;
            }
            return _internalValues;
        }
    }
    
    protected bool IsRange => Values.Length > 1;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _dotNetRef = DotNetObjectReference.Create(this);
        
        if (DefaultValue != null && !ValueChanged.HasDelegate)
        {
            _internalValues = DefaultValue.ToArray();
        }
        else if (Value != null)
        {
            _internalValues = Value.ToArray();
        }
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && JSRuntime != null)
        {
            await JSRuntime.InvokeVoidAsync("eval", @"
                window.sliderHelpers = {
                    addMouseMoveListener: function(dotNetRef) {
                        document.addEventListener('mousemove', function(e) {
                            dotNetRef.invokeMethodAsync('HandleMouseMove', e.clientX, e.clientY);
                        });
                        document.addEventListener('mouseup', function(e) {
                            dotNetRef.invokeMethodAsync('HandleMouseUp');
                        });
                    },
                    getElementBounds: function(element) {
                        const rect = element.getBoundingClientRect();
                        return { left: rect.left, width: rect.width };
                    }
                };
            ");
            
            if (_dotNetRef != null)
            {
                await JSRuntime.InvokeVoidAsync("sliderHelpers.addMouseMoveListener", _dotNetRef);
            }
        }
    }
    
    private string SliderCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-SliderRoot")
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
            SliderSize.Size1 => "rt-r-size-1",
            SliderSize.Size2 => "rt-r-size-2",
            SliderSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            SliderVariant.Classic => "rt-variant-classic",
            SliderVariant.Surface => "rt-variant-surface",
            SliderVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    private Dictionary<string, object> GetSliderAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = SliderCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        if (Disabled)
        {
            attributes["data-disabled"] = "";
            attributes["aria-disabled"] = "true";
        }
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    private string GetRangeStyle()
    {
        if (IsRange)
        {
            var leftPercent = ((Values[0] - Min) / (Max - Min)) * 100;
            var rightPercent = ((Values[1] - Min) / (Max - Min)) * 100;
            return $"left: {leftPercent}%; right: {100 - rightPercent}%;";
        }
        else
        {
            var percent = ((Values[0] - Min) / (Max - Min)) * 100;
            return $"left: 0%; right: {100 - percent}%;";
        }
    }
    
    private string GetThumbStyle(int index)
    {
        var percent = ((Values[index] - Min) / (Max - Min)) * 100;
        return $"left: {percent}%;";
    }
    
    private async Task StartDrag(MouseEventArgs e, int index)
    {
        if (Disabled) return;
        
        _isDragging = true;
        _dragIndex = index;
        
        await UpdateValueFromPosition(e.ClientX);
    }
    
    private async Task StartTouchDrag(TouchEventArgs e, int index)
    {
        if (Disabled) return;
        
        _isDragging = true;
        _dragIndex = index;
        
        if (e.Touches.Length > 0)
        {
            await UpdateValueFromPosition(e.Touches[0].ClientX);
        }
    }
    
    [JSInvokable]
    public async Task HandleMouseMove(double clientX, double clientY)
    {
        if (_isDragging && !Disabled)
        {
            await UpdateValueFromPosition(clientX);
        }
    }
    
    [JSInvokable]
    public async Task HandleMouseUp()
    {
        if (_isDragging)
        {
            _isDragging = false;
            
            if (OnValueCommit.HasDelegate)
            {
                await OnValueCommit.InvokeAsync(Values);
            }
        }
    }
    
    private async Task UpdateValueFromPosition(double clientX)
    {
        if (JSRuntime == null) return;
        
        var bounds = await JSRuntime.InvokeAsync<SliderBounds>("sliderHelpers.getElementBounds", _sliderElement);
        
        var percent = Math.Max(0, Math.Min(1, (clientX - bounds.Left) / bounds.Width));
        var rawValue = Min + (percent * (Max - Min));
        var steppedValue = Math.Round(rawValue / Step) * Step;
        var clampedValue = Math.Max(Min, Math.Min(Max, steppedValue));
        
        var newValues = Values.ToArray();
        
        if (IsRange)
        {
            // For range sliders, ensure values don't cross
            if (_dragIndex == 0)
            {
                newValues[0] = Math.Min(clampedValue, newValues[1]);
            }
            else
            {
                newValues[1] = Math.Max(clampedValue, newValues[0]);
            }
        }
        else
        {
            newValues[0] = clampedValue;
        }
        
        await UpdateValues(newValues);
    }
    
    private async Task UpdateValues(double[] newValues)
    {
        if (!newValues.SequenceEqual(Values))
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(newValues);
            }
            else
            {
                _internalValues = newValues;
            }
            
            if (OnValueChange.HasDelegate)
            {
                await OnValueChange.InvokeAsync(newValues);
            }
            
            StateHasChanged();
        }
    }
    
    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
    
    private class SliderBounds
    {
        public double Left { get; set; }
        public double Width { get; set; }
    }
}

public enum SliderSize
{
    Size1,
    Size2,
    Size3
}

public enum SliderVariant
{
    Classic,
    Surface,
    Soft
}