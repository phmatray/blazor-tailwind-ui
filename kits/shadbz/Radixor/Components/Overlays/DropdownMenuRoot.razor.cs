using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class DropdownMenuRoot : ComponentBase
{
    private bool _isOpen;
    private ElementReference? _triggerElement;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    [Parameter] public bool Modal { get; set; } = true;
    
    public bool IsOpen => OpenChanged.HasDelegate ? Open : _isOpen;
    
    internal void SetTriggerElement(ElementReference element)
    {
        _triggerElement = element;
    }
    
    internal ElementReference? GetTriggerElement() => _triggerElement;
    
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
    }
    
    internal async Task ToggleOpen()
    {
        await SetOpen(!IsOpen);
    }
    
    protected override void OnParametersSet()
    {
        if (OpenChanged.HasDelegate)
        {
            _isOpen = Open;
        }
    }
}