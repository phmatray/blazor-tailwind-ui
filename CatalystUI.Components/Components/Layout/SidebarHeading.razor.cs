using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class SidebarHeading
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}