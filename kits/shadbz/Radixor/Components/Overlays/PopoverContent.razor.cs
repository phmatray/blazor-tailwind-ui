using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radixor.Components.Base;
using System;
using System.Threading.Tasks;

using Radixor.Components.Common;
namespace Radixor.Components.Overlays;

public enum PopoverSide
{
    Top,
    Right,
    Bottom,
    Left
}

public enum PopoverAlign
{
    Start,
    Center,
    End
}

public enum PopoverSize
{
    Size1,
    Size2,
    Size3,
    Size4
}

public partial class PopoverContent : SpacingComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JSRuntime { get; set; }
    
    [Parameter] public new RenderFragment? ChildContent { get; set; }
    [Parameter] public PopoverSize? Size { get; set; } = PopoverSize.Size2;
    [Parameter] public PopoverSide? Side { get; set; } = PopoverSide.Bottom;
    [Parameter] public PopoverAlign? Align { get; set; } = PopoverAlign.Center;
    [Parameter] public int SideOffset { get; set; } = 0;
    [Parameter] public int AlignOffset { get; set; } = 0;
    [Parameter] public new string? MaxWidth { get; set; }
    [Parameter] public new string? MinWidth { get; set; }
    [Parameter] public EventCallback<KeyboardEventArgs> OnEscapeKeyDown { get; set; }
    
    [CascadingParameter] private PopoverRoot? PopoverRoot { get; set; }
    
    private DotNetObjectReference<PopoverContent>? _dotNetRef;
    private string _positionStyle = "";
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && JSRuntime != null)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            
            // Set up click outside handler for non-modal popovers
            if (PopoverRoot != null && !PopoverRoot.Modal)
            {
                await JSRuntime.InvokeVoidAsync("eval", @"
                    document.addEventListener('click', function(e) {
                        const popover = document.querySelector('.rt-PopoverContent');
                        const trigger = e.target.closest('[data-popover-trigger]');
                        if (popover && !popover.contains(e.target) && !trigger) {
                            popover.querySelector('.rt-PopoverClose')?.click();
                        }
                    });
                ");
            }
        }
        
        if (PopoverRoot?.IsOpen == true)
        {
            await UpdatePosition();
        }
    }
    
    private async Task UpdatePosition()
    {
        // In a real implementation, you'd calculate position based on trigger element
        // For now, we'll use CSS positioning
        await Task.CompletedTask;
    }
    
    private string ContentCssClass => new CssBuilder("rt-PopoverContent")
        .AddClass(GetSizeClass())
        .AddClass(GetSideClass())
        .AddClass(GetAlignClass())
        .Build();
    
    private string GetSizeClass() => Size switch
    {
        PopoverSize.Size1 => "rt-r-size-1",
        PopoverSize.Size2 => "rt-r-size-2",
        PopoverSize.Size3 => "rt-r-size-3",
        PopoverSize.Size4 => "rt-r-size-4",
        _ => "rt-r-size-2"
    };
    
    private string GetSideClass() => Side switch
    {
        PopoverSide.Top => "rt-variant-top",
        PopoverSide.Right => "rt-variant-right",
        PopoverSide.Bottom => "rt-variant-bottom",
        PopoverSide.Left => "rt-variant-left",
        _ => "rt-variant-bottom"
    };
    
    private string GetAlignClass() => Align switch
    {
        PopoverAlign.Start => "rt-align-start",
        PopoverAlign.Center => "rt-align-center",
        PopoverAlign.End => "rt-align-end",
        _ => "rt-align-center"
    };
    
    private string GetPositionStyle()
    {
        // Simplified positioning - in production, you'd calculate based on trigger element
        var sideOffset = SideOffset != 0 ? $"margin-top: {SideOffset}px;" : "";
        var alignOffset = AlignOffset != 0 ? $"margin-left: {AlignOffset}px;" : "";
        return $"position: absolute; z-index: 50; {sideOffset} {alignOffset}";
    }
    
    private async Task HandleOverlayClick()
    {
        if (PopoverRoot != null && PopoverRoot.Modal)
        {
            await PopoverRoot.SetOpen(false);
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
            
            if (PopoverRoot != null)
            {
                await PopoverRoot.SetOpen(false);
            }
        }
    }
    
    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}