using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class AlertDialogTrigger : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter] private AlertDialogRoot? AlertDialogRoot { get; set; }
    
    private async Task HandleClick()
    {
        if (AlertDialogRoot != null)
        {
            await AlertDialogRoot.SetOpen(true);
        }
    }
}