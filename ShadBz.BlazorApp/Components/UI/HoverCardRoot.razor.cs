using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class HoverCardRoot : ComponentBase, IDisposable
{
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    [Parameter] public bool DefaultOpen { get; set; }
    [Parameter] public int? OpenDelay { get; set; } = 700;
    [Parameter] public int? CloseDelay { get; set; } = 300;
    
    private bool _isOpen;
    private HoverCardTrigger? _trigger;
    private HoverCardContent? _content;
    private CancellationTokenSource? _delayTokenSource;
    
    internal bool IsOpen => OpenChanged.HasDelegate ? Open : _isOpen;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (!OpenChanged.HasDelegate)
        {
            _isOpen = DefaultOpen;
        }
    }
    
    internal async Task SetOpen(bool value, bool immediate = false)
    {
        _delayTokenSource?.Cancel();
        _delayTokenSource = new CancellationTokenSource();
        
        var delay = value ? OpenDelay ?? 700 : CloseDelay ?? 300;
        
        if (!immediate && delay > 0)
        {
            try
            {
                await Task.Delay(delay, _delayTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
        
        if (OpenChanged.HasDelegate)
        {
            await OpenChanged.InvokeAsync(value);
        }
        else
        {
            _isOpen = value;
            StateHasChanged();
        }
        
        _trigger?.NotifyStateChanged();
        _content?.NotifyStateChanged();
    }
    
    internal void RegisterTrigger(HoverCardTrigger trigger)
    {
        _trigger = trigger;
    }
    
    internal void UnregisterTrigger(HoverCardTrigger trigger)
    {
        if (_trigger == trigger)
        {
            _trigger = null;
        }
    }
    
    internal void RegisterContent(HoverCardContent content)
    {
        _content = content;
    }
    
    internal void UnregisterContent(HoverCardContent content)
    {
        if (_content == content)
        {
            _content = null;
        }
    }
    
    public void Dispose()
    {
        _delayTokenSource?.Cancel();
        _delayTokenSource?.Dispose();
    }
}