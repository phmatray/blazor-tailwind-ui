using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class TooltipTrigger : ComponentBase
{
    private ElementReference _triggerRef;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [CascadingParameter] private TooltipRoot? TooltipRoot { get; set; }
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender && TooltipRoot != null)
        {
            TooltipRoot.SetTriggerElement(_triggerRef);
        }
    }
    
    private async Task HandleMouseEnter()
    {
        if (TooltipRoot != null)
        {
            await TooltipRoot.SetOpen(true);
        }
    }
    
    private async Task HandleMouseLeave()
    {
        if (TooltipRoot != null)
        {
            await TooltipRoot.SetOpen(false, immediate: true);
        }
    }
    
    private async Task HandleFocus()
    {
        if (TooltipRoot != null)
        {
            await TooltipRoot.SetOpen(true, immediate: true);
        }
    }
    
    private async Task HandleBlur()
    {
        if (TooltipRoot != null)
        {
            await TooltipRoot.SetOpen(false, immediate: true);
        }
    }
}