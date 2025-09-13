using Microsoft.AspNetCore.Components;
using System;

namespace Radixor.Components.DataDisplay;

public partial class ScrollAreaViewport : ComponentBase, IDisposable
{
    [CascadingParameter] private ScrollAreaRoot? ScrollAreaRoot { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        ScrollAreaRoot?.RegisterViewport(this);
    }
    
    private string GetStyle()
    {
        var scrollbarType = ScrollAreaRoot?.GetScrollbarType() ?? ScrollAreaType.Hover;
        
        return scrollbarType switch
        {
            ScrollAreaType.Auto => "overflow: auto;",
            ScrollAreaType.Always => "overflow: scroll;",
            ScrollAreaType.Scroll => "overflow: scroll;",
            ScrollAreaType.Hover => "overflow: auto;",
            _ => "overflow: auto;"
        };
    }
    
    public void Dispose()
    {
        ScrollAreaRoot?.UnregisterViewport(this);
    }
}