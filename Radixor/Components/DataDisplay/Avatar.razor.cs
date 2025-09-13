using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.DataDisplay;

public partial class Avatar : SpacingComponentBase, IAsChildSupport
{
    // Core Avatar properties
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public AvatarSize? Size { get; set; } = AvatarSize.Size3; // Default size="3"
    [Parameter] public AvatarVariant? Variant { get; set; } = AvatarVariant.Soft; // Default variant="soft"
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public string? Radius { get; set; } // "none", "small", "medium", "large", "full"
    
    // Image properties
    [Parameter] public string? Src { get; set; }
    [Parameter] public string? Alt { get; set; }
    
    // Fallback content (required)
    [Parameter, EditorRequired] public RenderFragment Fallback { get; set; } = null!;
    
    // State management
    private ImageLoadStatus imageStatus = ImageLoadStatus.Idle;
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Avatar")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    protected string FallbackCssClass
    {
        get
        {
            var fallbackText = GetFallbackText();
            var builder = new CssBuilder("rt-AvatarFallback");
            
            if (!string.IsNullOrEmpty(fallbackText))
            {
                if (fallbackText.Length == 1)
                    builder.AddClass("rt-one-letter");
                else if (fallbackText.Length == 2)
                    builder.AddClass("rt-two-letters");
            }
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            AvatarSize.Size1 => "rt-r-size-1",
            AvatarSize.Size2 => "rt-r-size-2",
            AvatarSize.Size3 => "rt-r-size-3",
            AvatarSize.Size4 => "rt-r-size-4",
            AvatarSize.Size5 => "rt-r-size-5",
            AvatarSize.Size6 => "rt-r-size-6",
            AvatarSize.Size7 => "rt-r-size-7",
            AvatarSize.Size8 => "rt-r-size-8",
            AvatarSize.Size9 => "rt-r-size-9",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            AvatarVariant.Solid => "rt-variant-solid",
            AvatarVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetAvatarAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        var inlineStyles = BuildInlineStyles(); // From LayoutComponentBase
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected override async Task OnParametersSetAsync()
    {
        // Reset image status when src changes
        if (!string.IsNullOrEmpty(Src))
        {
            imageStatus = ImageLoadStatus.Loading;
        }
        await base.OnParametersSetAsync();
    }
    
    protected void HandleImageLoad()
    {
        imageStatus = ImageLoadStatus.Loaded;
        StateHasChanged();
    }
    
    protected void HandleImageError()
    {
        imageStatus = ImageLoadStatus.Error;
        StateHasChanged();
    }
    
    protected bool ShouldShowFallback()
    {
        return imageStatus == ImageLoadStatus.Idle || 
               imageStatus == ImageLoadStatus.Loading || 
               imageStatus == ImageLoadStatus.Error ||
               string.IsNullOrEmpty(Src);
    }
    
    private string? GetFallbackText()
    {
        // Try to extract text content from the Fallback RenderFragment
        // This is a simplified approach - in a real implementation, 
        // you might need a more sophisticated way to extract text
        // For now, we'll handle this in the Razor file
        return null;
    }
    
    private enum ImageLoadStatus
    {
        Idle,
        Loading,
        Loaded,
        Error
    }
}

public enum AvatarSize
{
    Size1,
    Size2,
    Size3,
    Size4,
    Size5,
    Size6,
    Size7,
    Size8,
    Size9
}

public enum AvatarVariant
{
    Solid,
    Soft
}