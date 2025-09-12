using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class AlertDialogCancel : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [CascadingParameter] private AlertDialogRoot? AlertDialogRoot { get; set; }
    
    private async Task HandleClick()
    {
        if (AlertDialogRoot != null)
        {
            await AlertDialogRoot.SetOpen(false);
        }
    }
}