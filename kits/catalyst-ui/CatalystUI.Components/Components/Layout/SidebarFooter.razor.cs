using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class SidebarFooter
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}