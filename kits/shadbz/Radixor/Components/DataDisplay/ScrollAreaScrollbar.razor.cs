using Microsoft.AspNetCore.Components;
using System;

namespace Radixor.Components.DataDisplay;

public enum ScrollbarOrientation
{
    Horizontal,
    Vertical
}

public partial class ScrollAreaScrollbar : ComponentBase, IDisposable
{
    [CascadingParameter] private ScrollAreaRoot? ScrollAreaRoot { get; set; }
    
    [Parameter] public ScrollbarOrientation Orientation { get; set; } = ScrollbarOrientation.Vertical;
    [Parameter] public bool ForceMount { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ScrollAreaRoot?.RegisterScrollbar(this);
    }
    
    private string GetStyle()
    {
        var visibility = ScrollAreaRoot?.GetScrollbarType() switch
        {
            ScrollAreaType.Always => "display: flex;",
            ScrollAreaType.Auto => "",
            ScrollAreaType.Hover => "",
            _ => ""
        };
        
        return visibility;
    }
    
    public void Dispose()
    {
        ScrollAreaRoot?.UnregisterScrollbar(this);
    }
}