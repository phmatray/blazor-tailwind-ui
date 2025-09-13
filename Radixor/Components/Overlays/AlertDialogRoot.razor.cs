using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class AlertDialogRoot : ComponentBase
{
    private bool _isOpen;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    
    public bool IsOpen => OpenChanged.HasDelegate ? Open : _isOpen;
    
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
    
    protected override void OnParametersSet()
    {
        if (OpenChanged.HasDelegate)
        {
            _isOpen = Open;
        }
    }
}