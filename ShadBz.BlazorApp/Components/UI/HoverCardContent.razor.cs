using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ShadBz.BlazorApp.Components.UI.Base;
using System;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public enum HoverCardSide
{
    Top,
    Right,
    Bottom,
    Left
}

public enum HoverCardAlign
{
    Start,
    Center,
    End
}

public partial class HoverCardContent : SpacingComponentBase, IDisposable
{
    [CascadingParameter] private HoverCardRoot? HoverCardRoot { get; set; }
    
    [Parameter] public HoverCardSide Side { get; set; } = HoverCardSide.Bottom;
    [Parameter] public HoverCardAlign Align { get; set; } = HoverCardAlign.Center;
    [Parameter] public int SideOffset { get; set; } = 5;
    [Parameter] public int AlignOffset { get; set; } = 0;
    [Parameter] public new string? Width { get; set; } = "360px";
    [Parameter] public bool AvoidCollisions { get; set; } = true;
    
    private bool _isHovered;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        HoverCardRoot?.RegisterContent(this);
    }
    
    private string ContentCssClass => new CssBuilder("rt-HoverCardContent")
        .AddClass("rt-r-size-2")
        .AddClass("rt-BaseCard")
        .AddClass(GetMarginClasses())
        .AddClass(Class)
        .Build();
    
    private string GetPositionStyle()
    {
        var width = !string.IsNullOrEmpty(Width) ? $"width: {Width};" : "";
        var sideOffset = SideOffset != 0 ? $"margin-top: {SideOffset}px;" : "";
        var alignOffset = AlignOffset != 0 ? $"margin-left: {AlignOffset}px;" : "";
        
        return $"position: absolute; z-index: 50; {width} {sideOffset} {alignOffset}";
    }
    
    private async Task HandleMouseEnter(MouseEventArgs args)
    {
        _isHovered = true;
        if (HoverCardRoot != null)
        {
            await HoverCardRoot.SetOpen(true, immediate: true);
        }
    }
    
    private async Task HandleMouseLeave(MouseEventArgs args)
    {
        _isHovered = false;
        if (HoverCardRoot != null)
        {
            await HoverCardRoot.SetOpen(false);
        }
    }
    
    internal void NotifyStateChanged()
    {
        StateHasChanged();
    }
    
    public void Dispose()
    {
        HoverCardRoot?.UnregisterContent(this);
    }
}