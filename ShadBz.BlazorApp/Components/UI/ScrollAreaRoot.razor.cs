using Microsoft.AspNetCore.Components;
using System;

namespace ShadBz.BlazorApp.Components.UI;

public enum ScrollAreaSize
{
    Size1,
    Size2,
    Size3
}

public enum ScrollAreaType
{
    Auto,
    Always,
    Scroll,
    Hover
}

public partial class ScrollAreaRoot : ComponentBase
{
    [Parameter] public ScrollAreaSize Size { get; set; } = ScrollAreaSize.Size1;
    [Parameter] public ScrollAreaType Type { get; set; } = ScrollAreaType.Hover;
    [Parameter] public int? Scrollbars { get; set; }
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public string? MaxWidth { get; set; }
    [Parameter] public string? MinHeight { get; set; }
    [Parameter] public string? MaxHeight { get; set; }
    
    private ScrollAreaViewport? _viewport;
    private ScrollAreaScrollbar? _verticalScrollbar;
    private ScrollAreaScrollbar? _horizontalScrollbar;
    
    internal void RegisterViewport(ScrollAreaViewport viewport)
    {
        _viewport = viewport;
    }
    
    internal void UnregisterViewport(ScrollAreaViewport viewport)
    {
        if (_viewport == viewport)
        {
            _viewport = null;
        }
    }
    
    internal void RegisterScrollbar(ScrollAreaScrollbar scrollbar)
    {
        if (scrollbar.Orientation == ScrollbarOrientation.Vertical)
        {
            _verticalScrollbar = scrollbar;
        }
        else
        {
            _horizontalScrollbar = scrollbar;
        }
    }
    
    internal void UnregisterScrollbar(ScrollAreaScrollbar scrollbar)
    {
        if (scrollbar.Orientation == ScrollbarOrientation.Vertical && _verticalScrollbar == scrollbar)
        {
            _verticalScrollbar = null;
        }
        else if (scrollbar.Orientation == ScrollbarOrientation.Horizontal && _horizontalScrollbar == scrollbar)
        {
            _horizontalScrollbar = null;
        }
    }
    
    internal ScrollAreaType GetScrollbarType() => Type;
    
    private string GetStyle()
    {
        var style = "";
        
        if (!string.IsNullOrEmpty(Width))
            style += $"width: {Width}; ";
        if (!string.IsNullOrEmpty(Height))
            style += $"height: {Height}; ";
        if (!string.IsNullOrEmpty(MinWidth))
            style += $"min-width: {MinWidth}; ";
        if (!string.IsNullOrEmpty(MaxWidth))
            style += $"max-width: {MaxWidth}; ";
        if (!string.IsNullOrEmpty(MinHeight))
            style += $"min-height: {MinHeight}; ";
        if (!string.IsNullOrEmpty(MaxHeight))
            style += $"max-height: {MaxHeight}; ";
            
        return style.Trim();
    }
}