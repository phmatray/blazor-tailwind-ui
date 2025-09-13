using Microsoft.AspNetCore.Components;

namespace Radixor.Components.Overlays;

public partial class DropdownMenuLabel : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}