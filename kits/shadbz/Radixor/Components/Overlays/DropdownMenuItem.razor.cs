using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

using Radixor.Components.Common;
namespace Radixor.Components.Overlays;

public partial class DropdownMenuItem : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? Shortcut { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnSelect { get; set; }
    [CascadingParameter] private DropdownMenuRoot? DropdownMenuRoot { get; set; }
    
    private string ItemCssClass => new CssBuilder("rt-DropdownMenuItem")
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
            
            if (DropdownMenuRoot != null)
            {
                await DropdownMenuRoot.SetOpen(false);
            }
        }
    }
}