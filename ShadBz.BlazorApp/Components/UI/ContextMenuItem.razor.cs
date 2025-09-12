using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public partial class ContextMenuItem : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? Shortcut { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnSelect { get; set; }
    [CascadingParameter] private ContextMenuRoot? ContextMenuRoot { get; set; }
    
    private string ItemCssClass => new CssBuilder("rt-ContextMenuItem")
        .AddClass("rt-disabled", Disabled)
        .AddClass($"rt-variant-{Color}", !string.IsNullOrEmpty(Color))
        .Build();
    
    private async Task HandleClick(MouseEventArgs e)
    {
        if (!Disabled)
        {
            if (OnSelect.HasDelegate)
            {
                await OnSelect.InvokeAsync(e);
            }
            
            if (ContextMenuRoot != null)
            {
                await ContextMenuRoot.SetOpen(false);
            }
        }
    }
}