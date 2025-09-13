using Microsoft.AspNetCore.Components;

namespace Radixor.Components.Overlays;

public partial class ContextMenuLabel : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}