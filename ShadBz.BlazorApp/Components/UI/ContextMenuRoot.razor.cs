using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class ContextMenuRoot : ComponentBase
{
    private bool _isOpen;
    private double _mouseX;
    private double _mouseY;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    [Parameter] public bool Modal { get; set; } = true;
    
    public bool IsOpen => OpenChanged.HasDelegate ? Open : _isOpen;
    
    internal (double X, double Y) GetMousePosition() => (_mouseX, _mouseY);
    
    internal void SetMousePosition(double x, double y)
    {
        _mouseX = x;
        _mouseY = y;
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
    }
    
    protected override void OnParametersSet()
    {
        if (OpenChanged.HasDelegate)
        {
            _isOpen = Open;
        }
    }
}