using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class PopoverTrigger : ComponentBase
{
    private ElementReference _triggerRef;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [CascadingParameter] private PopoverRoot? PopoverRoot { get; set; }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && PopoverRoot != null)
        {
            PopoverRoot.SetTriggerElement(_triggerRef);
        }
    }
    
    private async Task HandleClick()
    {
        if (PopoverRoot != null)
        {
            await PopoverRoot.ToggleOpen();
        }
    }
}