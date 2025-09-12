using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using ShadBz.BlazorApp.Components.UI.Base;
using System;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public enum DropdownMenuSize
{
    Size1,
    Size2
}

public enum DropdownMenuSide
{
    Top,
    Right,
    Bottom,
    Left
}

public enum DropdownMenuAlign
{
    Start,
    Center,
    End
}

public partial class DropdownMenuContent : SpacingComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JSRuntime { get; set; }
    
    [Parameter] public new RenderFragment? ChildContent { get; set; }
    [Parameter] public DropdownMenuSize? Size { get; set; } = DropdownMenuSize.Size2;
    [Parameter] public DropdownMenuSide? Side { get; set; } = DropdownMenuSide.Bottom;
    [Parameter] public DropdownMenuAlign? Align { get; set; } = DropdownMenuAlign.Start;
    [Parameter] public int SideOffset { get; set; } = 5;
    [Parameter] public int AlignOffset { get; set; } = 0;
    [Parameter] public new string? MinWidth { get; set; } = "200px";
    [Parameter] public EventCallback<KeyboardEventArgs> OnEscapeKeyDown { get; set; }
    
    [CascadingParameter] private DropdownMenuRoot? DropdownMenuRoot { get; set; }
    
    private DotNetObjectReference<DropdownMenuContent>? _dotNetRef;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && JSRuntime != null)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            
            // Set up click outside handler
            if (DropdownMenuRoot != null && !DropdownMenuRoot.Modal)
            {
                await JSRuntime.InvokeVoidAsync("eval", @"
                    document.addEventListener('click', function(e) {
                        const menu = document.querySelector('.rt-DropdownMenuContent');
                        const trigger = e.target.closest('[data-dropdown-trigger]');
                        if (menu && !menu.contains(e.target) && !trigger) {
                            menu.dispatchEvent(new KeyboardEvent('keydown', { key: 'Escape' }));
                        }
                    });
                ");
            }
        }
    }
    
    private string ContentCssClass => new CssBuilder("rt-DropdownMenuContent")
        .AddClass(GetSizeClass())
        .Build();
    
    private string GetSizeClass() => Size switch
    {
        DropdownMenuSize.Size1 => "rt-r-size-1",
        DropdownMenuSize.Size2 => "rt-r-size-2",
        _ => "rt-r-size-2"
    };
    
    private string GetPositionStyle()
    {
        var minWidth = !string.IsNullOrEmpty(MinWidth) ? $"min-width: {MinWidth};" : "";
        return $"position: absolute; z-index: 50; {minWidth}";
    }
    
    private async Task HandleOverlayClick()
    {
        if (DropdownMenuRoot != null)
        {
            await DropdownMenuRoot.SetOpen(false);
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
            
            if (DropdownMenuRoot != null)
            {
                await DropdownMenuRoot.SetOpen(false);
            }
        }
    }
    
    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}