using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radixor.Components.Base;
using System;
using System.Threading.Tasks;

using Radixor.Components.Common;
namespace Radixor.Components.Overlays;

public enum AlertDialogSize
{
    Size1,
    Size2,
    Size3,
    Size4
}

public partial class AlertDialogContent : SpacingComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JSRuntime { get; set; }
    
    [Parameter] public new RenderFragment? ChildContent { get; set; }
    [Parameter] public AlertDialogSize? Size { get; set; } = AlertDialogSize.Size2;
    [Parameter] public new string? MaxWidth { get; set; }
    [Parameter] public new string? MinWidth { get; set; }
    
    [CascadingParameter] private AlertDialogRoot? AlertDialogRoot { get; set; }
    
    private DotNetObjectReference<AlertDialogContent>? _dotNetRef;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && JSRuntime != null)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("eval", @"
                document.addEventListener('keydown', function(e) {
                    if (e.key === 'Escape') {
                        const overlay = document.querySelector('.rt-AlertDialogOverlay');
                        if (overlay) {
                            overlay.click();
                        }
                    }
                });
            ");
        }
    }
    
    private string ContentCssClass => new CssBuilder("rt-BaseDialogContent rt-AlertDialogContent")
        .AddClass(GetSizeClass())
        .Build();
    
    private string GetSizeClass() => Size switch
    {
        AlertDialogSize.Size1 => "rt-r-size-1",
        AlertDialogSize.Size2 => "rt-r-size-2",
        AlertDialogSize.Size3 => "rt-r-size-3",
        AlertDialogSize.Size4 => "rt-r-size-4",
        _ => "rt-r-size-2"
    };
    
    private void HandleOverlayClick()
    {
        // AlertDialog doesn't close on overlay click - it requires explicit action
    }
    
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        // AlertDialog doesn't close on Escape - it requires explicit action
        await Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}