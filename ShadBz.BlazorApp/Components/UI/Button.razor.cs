using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Button : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;
    
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Solid;
    
    [Parameter] public string? Color { get; set; }
    
    [Parameter] public string? Radius { get; set; }
    
    [Parameter] public bool Loading { get; set; }
    
    [Parameter] public bool Disabled { get; set; }
    
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    
    [Parameter] public string? Class { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
    
    private bool IsDisabled => Disabled || Loading;
    
    private string CssClass => new CssBuilder("rt-reset rt-BaseButton rt-Button")
        .AddClass($"rt-r-size-{GetSizeValue()}")
        .AddClass($"rt-variant-{Variant.ToString().ToLower()}")
        .AddClass("rt-loading", Loading)
        .AddClass(Class)
        .Build();
    
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
    
    private string GetSpinnerSize()
    {
        return Size switch
        {
            ButtonSize.Small => "spinner-size-1",
            ButtonSize.Medium => "spinner-size-2",
            ButtonSize.Large => "spinner-size-2",
            ButtonSize.ExtraLarge => "spinner-size-3",
            _ => "spinner-size-2"
        };
    }
}