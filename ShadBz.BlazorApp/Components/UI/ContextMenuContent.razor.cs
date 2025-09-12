using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using ShadBz.BlazorApp.Components.UI.Base;
using System;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public enum ContextMenuSize
{
    Size1,
    Size2
}

public partial class ContextMenuContent : SpacingComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JSRuntime { get; set; }
    
    [Parameter] public new RenderFragment? ChildContent { get; set; }
    [Parameter] public ContextMenuSize? Size { get; set; } = ContextMenuSize.Size2;
    [Parameter] public new string? MinWidth { get; set; } = "200px";
    [Parameter] public EventCallback<KeyboardEventArgs> OnEscapeKeyDown { get; set; }
    
    [CascadingParameter] private ContextMenuRoot? ContextMenuRoot { get; set; }
    
    private DotNetObjectReference<ContextMenuContent>? _dotNetRef;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && JSRuntime != null)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            
            // Set up click outside handler
            await JSRuntime.InvokeVoidAsync("eval", @"
                document.addEventListener('click', function(e) {
                    const menu = document.querySelector('.rt-ContextMenuContent');
                    if (menu && !menu.contains(e.target)) {
                        menu.dispatchEvent(new KeyboardEvent('keydown', { key: 'Escape' }));
                    }
                });
            ");
        }
    }
    
    private string ContentCssClass => new CssBuilder("rt-ContextMenuContent")
        .AddClass(GetSizeClass())
        .Build();
    
    private string GetSizeClass() => Size switch
    {
        ContextMenuSize.Size1 => "rt-r-size-1",
        ContextMenuSize.Size2 => "rt-r-size-2",
        _ => "rt-r-size-2"
    };
    
    private string GetPositionStyle()
    {
        var position = ContextMenuRoot?.GetMousePosition() ?? (0, 0);
        var minWidth = !string.IsNullOrEmpty(MinWidth) ? $"min-width: {MinWidth};" : "";
        return $"position: fixed; z-index: 50; left: {position.X}px; top: {position.Y}px; {minWidth}";
    }
    
    private async Task HandleOverlayClick()
    {
        if (ContextMenuRoot != null)
        {
            await ContextMenuRoot.SetOpen(false);
        }
    }
    
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        if (e.Key == "Escape")
        {
            if (OnEscapeKeyDown.HasDelegate)
            {
                await OnEscapeKeyDown.InvokeAsync(e);
            }
            
            if (ContextMenuRoot != null)
            {
                await ContextMenuRoot.SetOpen(false);
            }
        }
    }
    
    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}