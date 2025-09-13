using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class TooltipRoot : ComponentBase, IDisposable
{
    private bool _isOpen;
    private ElementReference? _triggerElement;
    private CancellationTokenSource? _delayTokenSource;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    [Parameter] public int? DelayDuration { get; set; }
    
    [CascadingParameter] private TooltipProvider? TooltipProvider { get; set; }
    
    public bool IsOpen => OpenChanged.HasDelegate ? Open : _isOpen;
    
    internal void SetTriggerElement(ElementReference element)
    {
        _triggerElement = element;
    }
    
    internal ElementReference? GetTriggerElement() => _triggerElement;
    
    internal async Task SetOpen(bool value, bool immediate = false)
    {
        _delayTokenSource?.Cancel();
        _delayTokenSource = new CancellationTokenSource();
        
        if (value && !immediate)
        {
            var delay = DelayDuration ?? TooltipProvider?.GetDelayDuration() ?? 700;
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
    }
    
    protected override void OnParametersSet()
    {
        if (OpenChanged.HasDelegate)
        {
            _isOpen = Open;
        }
    }
    
    public void Dispose()
    {
        _delayTokenSource?.Cancel();
        _delayTokenSource?.Dispose();
    }
}