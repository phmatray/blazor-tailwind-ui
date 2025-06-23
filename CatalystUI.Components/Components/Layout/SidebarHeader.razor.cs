using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class SidebarHeader
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}