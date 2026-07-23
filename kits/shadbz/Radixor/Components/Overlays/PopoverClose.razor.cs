using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class PopoverClose : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [CascadingParameter] private PopoverRoot? PopoverRoot { get; set; }
    
    private async Task HandleClick()
    {
        if (PopoverRoot != null)
        {
            await PopoverRoot.SetOpen(false);
        }
    }
}