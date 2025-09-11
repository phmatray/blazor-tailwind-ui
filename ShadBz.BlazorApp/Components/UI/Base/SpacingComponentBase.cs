using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI.Base;

/// <summary>
/// Base class for components that support margin and padding properties
/// </summary>
public abstract class SpacingComponentBase : LayoutComponentBase
{
    // Margin properties
    [Parameter] public string? M { get; set; }
    [Parameter] public string? Mx { get; set; }
    [Parameter] public string? My { get; set; }
    [Parameter] public string? Mt { get; set; }
    [Parameter] public string? Mr { get; set; }
    [Parameter] public string? Mb { get; set; }
    [Parameter] public string? Ml { get; set; }
    
    // Padding properties
    [Parameter] public string? P { get; set; }
    [Parameter] public string? Px { get; set; }
    [Parameter] public string? Py { get; set; }
    [Parameter] public string? Pt { get; set; }
    [Parameter] public string? Pr { get; set; }
    [Parameter] public string? Pb { get; set; }
    [Parameter] public string? Pl { get; set; }
    
    /// <summary>
    /// Gets the margin-related CSS classes
    /// </summary>
    protected string GetMarginClasses()
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
    
    /// <summary>
    /// Gets the padding-related CSS classes
    /// </summary>
    protected string GetPaddingClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(P))
            classes.Add($"rt-r-p-{P}");
        if (!string.IsNullOrEmpty(Px))
            classes.Add($"rt-r-px-{Px}");
        if (!string.IsNullOrEmpty(Py))
            classes.Add($"rt-r-py-{Py}");
        if (!string.IsNullOrEmpty(Pt))
            classes.Add($"rt-r-pt-{Pt}");
        if (!string.IsNullOrEmpty(Pr))
            classes.Add($"rt-r-pr-{Pr}");
        if (!string.IsNullOrEmpty(Pb))
            classes.Add($"rt-r-pb-{Pb}");
        if (!string.IsNullOrEmpty(Pl))
            classes.Add($"rt-r-pl-{Pl}");
        
        return string.Join(" ", classes);
    }
}