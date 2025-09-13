using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class DialogTrigger : ComponentBase, IDisposable
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [CascadingParameter] private DialogRoot? DialogRoot { get; set; }
    
    private ElementReference _elementRef;
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogRoot?.RegisterTrigger(this);
    }
    
    private async Task HandleClick()
    {
        if (DialogRoot != null)
        {
            await DialogRoot.SetOpen(!DialogRoot.IsOpen);
        }
    }
    
    internal void ReturnFocus()
    {
        // In a real implementation, you'd focus the trigger element
        // This would require JS interop
    }
    
    public void Dispose()
    {
        DialogRoot?.UnregisterTrigger(this);
    }
}