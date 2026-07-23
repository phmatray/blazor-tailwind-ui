using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;
using Radixor.Components.Common;
using System.Collections.Generic;

namespace Radixor.Components.Feedback;

public class SkeletonBase : SpacingComponentBase, IAsChildSupport
{
    /// <summary>
    /// Whether to show the skeleton loading state. Default is true.
    /// </summary>
    [Parameter] public bool Loading { get; set; } = true;
    
    /// <summary>
    /// When true, renders the skeleton as the child element instead of a span
    /// </summary>
    [Parameter] public bool AsChild { get; set; }
    
    
    protected Dictionary<string, object> GetSkeletonAttributes()
    {
        var attributes = GetAllAttributes();
        
        var builder = new CssBuilder("rt-Skeleton")
            .AddClass(GetMarginClasses())
            .AddClass(Class);
        
        attributes["class"] = builder.Build();
        attributes["aria-hidden"] = "true";
        attributes["tabindex"] = "-1";
        
        // Add data attribute for inline skeletons (when wrapping text)
        if (ChildContent != null && !AsChild)
        {
            attributes["data-inline-skeleton"] = "true";
        }
        
        // Build inline styles for dimensions
        var styles = new List<string>();
        
        if (!string.IsNullOrEmpty(Width))
            styles.Add($"width: {Width}");
        if (!string.IsNullOrEmpty(Height))
            styles.Add($"height: {Height}");
        if (!string.IsNullOrEmpty(MinWidth))
            styles.Add($"min-width: {MinWidth}");
        if (!string.IsNullOrEmpty(MaxWidth))
            styles.Add($"max-width: {MaxWidth}");
        if (!string.IsNullOrEmpty(MinHeight))
            styles.Add($"min-height: {MinHeight}");
        if (!string.IsNullOrEmpty(MaxHeight))
            styles.Add($"max-height: {MaxHeight}");
        
        // Add margin styles
        var marginStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(marginStyles))
            styles.Add(marginStyles);
        
        if (styles.Count > 0)
        {
            attributes["style"] = string.Join("; ", styles);
        }
        
        return attributes;
    }
}

public partial class Skeleton : SkeletonBase
{
    // Component implementation in .razor file
}