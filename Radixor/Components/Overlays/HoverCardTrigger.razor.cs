using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class HoverCardTrigger : ComponentBase, IDisposable
{
    [CascadingParameter] private HoverCardRoot? HoverCardRoot { get; set; }
    
    private bool _isHovered;
    private bool _isFocused;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        HoverCardRoot?.RegisterTrigger(this);
    }
    
    private async Task HandleMouseEnter(MouseEventArgs args)
    {
        _isHovered = true;
        if (HoverCardRoot != null)
        {
            await HoverCardRoot.SetOpen(true);
        }
    }
    
    private async Task HandleMouseLeave(MouseEventArgs args)
    {
        _isHovered = false;
        if (HoverCardRoot != null && !_isFocused)
        {
            await HoverCardRoot.SetOpen(false);
        }
    }
    
    private async Task HandleFocus(FocusEventArgs args)
    {
        _isFocused = true;
        if (HoverCardRoot != null)
        {
            await HoverCardRoot.SetOpen(true);
        }
    }
    
    private async Task HandleBlur(FocusEventArgs args)
    {
        _isFocused = false;
        if (HoverCardRoot != null && !_isHovered)
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
        HoverCardRoot?.UnregisterTrigger(this);
    }
}