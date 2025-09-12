using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class DialogRoot : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool DefaultOpen { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    [Parameter] public bool Modal { get; set; } = true;
    
    private bool _isOpen;
    private DialogTrigger? _trigger;
    private DialogContent? _content;
    
    internal bool IsOpen => OpenChanged.HasDelegate ? Open : _isOpen;
    internal bool IsModal => Modal;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (!OpenChanged.HasDelegate)
        {
            _isOpen = DefaultOpen;
        }
    }
    
    internal async Task SetOpen(bool value)
    {
        if (OpenChanged.HasDelegate)
        {
            await OpenChanged.InvokeAsync(value);
        }
        else
        {
            _isOpen = value;
            StateHasChanged();
        }
        
        if (!value)
        {
            _trigger?.ReturnFocus();
        }
    }
    
    internal void RegisterTrigger(DialogTrigger trigger)
    {
        _trigger = trigger;
    }
    
    internal void UnregisterTrigger(DialogTrigger trigger)
    {
        if (_trigger == trigger)
        {
            _trigger = null;
        }
    }
    
    internal void RegisterContent(DialogContent content)
    {
        _content = content;
    }
    
    internal void UnregisterContent(DialogContent content)
    {
        if (_content == content)
        {
            _content = null;
        }
    }
    
    internal async Task HandleEscapeKey()
    {
        await SetOpen(false);
    }
    
    internal async Task HandleOverlayClick()
    {
        if (Modal)
        {
            await SetOpen(false);
        }
    }
}