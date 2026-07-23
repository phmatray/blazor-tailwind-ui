using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class SidebarBody
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}