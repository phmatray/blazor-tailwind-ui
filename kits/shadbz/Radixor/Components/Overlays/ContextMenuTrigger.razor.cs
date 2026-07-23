using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace Radixor.Components.Overlays;

public partial class ContextMenuTrigger : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [CascadingParameter] private ContextMenuRoot? ContextMenuRoot { get; set; }
    
    private async Task HandleContextMenu(MouseEventArgs e)
    {
        if (!Disabled && ContextMenuRoot != null)
        {
            ContextMenuRoot.SetMousePosition(e.ClientX, e.ClientY);
            await ContextMenuRoot.SetOpen(true);
        }
    }
}