using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class AlertDialogAction : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [CascadingParameter] private AlertDialogRoot? AlertDialogRoot { get; set; }
    
    private async Task HandleClick(MouseEventArgs e)
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }
        
        if (AlertDialogRoot != null)
        {
            await AlertDialogRoot.SetOpen(false);
        }
    }
}