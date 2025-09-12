using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class DialogClose : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [CascadingParameter] private DialogRoot? DialogRoot { get; set; }
    
    private async Task HandleClick()
    {
        if (DialogRoot != null)
        {
            await DialogRoot.SetOpen(false);
        }
    }
}