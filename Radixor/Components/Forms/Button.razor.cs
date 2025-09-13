using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Radixor.Components.Common;
namespace Radixor.Components.Forms;

public partial class Button : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    // Core button properties
    [Parameter] public ButtonSize Size { get; set; } = ButtonSize.Medium;
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Solid;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Loading { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    // AsChild support for composition
    [Parameter] public bool AsChild { get; set; }
    
    // Event handlers
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    
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
    
    private bool IsDisabled => Disabled || Loading;
    
    private string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-reset rt-BaseButton rt-Button")
                .AddClass($"rt-r-size-{GetSizeValue()}")
                .AddClass($"rt-variant-{Variant.ToString().ToLower()}")
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass("rt-loading", Loading)
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetDataAttributes()
    {
        var attributes = new List<string>();
        
        if (IsDisabled)
            attributes.Add("data-disabled");
        
        if (!string.IsNullOrEmpty(Color))
            attributes.Add($"data-accent-color=\"{Color}\"");
        
        if (!string.IsNullOrEmpty(Radius))
            attributes.Add($"data-radius=\"{Radius}\"");
        
        return string.Join(" ", attributes);
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
    
    private string GetSpinnerSizeClass()
    {
        // Map button size to spinner size according to Radix UI logic
        return Size switch
        {
            ButtonSize.Small => "rt-r-size-1",
            ButtonSize.Medium => "rt-r-size-2",
            ButtonSize.Large => "rt-r-size-2",
            ButtonSize.ExtraLarge => "rt-r-size-3",
            _ => "rt-r-size-2"
        };
    }
}