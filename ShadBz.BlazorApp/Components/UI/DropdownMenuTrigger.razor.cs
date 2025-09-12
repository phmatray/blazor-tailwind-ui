using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class DropdownMenuTrigger : ComponentBase
{
    private ElementReference _triggerRef;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [CascadingParameter] private DropdownMenuRoot? DropdownMenuRoot { get; set; }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && DropdownMenuRoot != null)
        {
            DropdownMenuRoot.SetTriggerElement(_triggerRef);
        }
    }
    
    private async Task HandleClick()
    {
        if (DropdownMenuRoot != null)
        {
            await DropdownMenuRoot.ToggleOpen();
        }
    }
    
    private async Task HandleContextMenu(MouseEventArgs e)
    {
        if (DropdownMenuRoot != null)
        {
            await DropdownMenuRoot.SetOpen(true);
        }
    }
}