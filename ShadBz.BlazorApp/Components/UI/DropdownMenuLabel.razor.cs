using Microsoft.AspNetCore.Components;

namespace ShadBz.BlazorApp.Components.UI;

public partial class DropdownMenuLabel : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}