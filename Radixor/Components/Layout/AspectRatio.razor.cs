using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;
using System.Collections.Generic;
using System.Globalization;

namespace Radixor.Components.Layout;

public class AspectRatioBase : ComponentBase
{
    /// <summary>
    /// The aspect ratio value (width/height). Default is 1 (square).
    /// Common values: 16/9 (widescreen), 4/3 (standard), 1 (square)
    /// </summary>
    [Parameter] public double Ratio { get; set; } = 1;
    
    /// <summary>
    /// The content to be displayed within the aspect ratio container
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Additional CSS classes to apply to the root element
    /// </summary>
    [Parameter] public string? Class { get; set; }
    
    /// <summary>
    /// Additional attributes to apply to the root element
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object> AdditionalAttributes { get; set; } = new();
    
    /// <summary>
    /// Calculate the padding-bottom percentage for the aspect ratio
    /// </summary>
    protected string PaddingBottom
    {
        get
        {
            // Calculate percentage: (1 / ratio) * 100
            var percentage = (1 / Ratio) * 100;
            return $"{percentage.ToString("F2", CultureInfo.InvariantCulture)}%";
        }
    }
    
    /// <summary>
    /// Style for the inner content div
    /// </summary>
    protected string ContentStyle
    {
        get
        {
            return "position: absolute; top: 0; right: 0; bottom: 0; left: 0;";
        }
    }
    
    protected Dictionary<string, object> GetAspectRatioAttributes()
    {
        var attributes = new Dictionary<string, object>(AdditionalAttributes);
        
        // Build CSS classes
        var cssClass = "rt-AspectRatio";
        if (!string.IsNullOrEmpty(Class))
        {
            cssClass = $"{cssClass} {Class}";
        }
        
        attributes["class"] = cssClass;
        attributes["style"] = $"position: relative; width: 100%; padding-bottom: {PaddingBottom};";
        attributes["data-radix-aspect-ratio-wrapper"] = "";
        
        return attributes;
    }
}

public partial class AspectRatio : AspectRatioBase
{
    // Component implementation in .razor file
}