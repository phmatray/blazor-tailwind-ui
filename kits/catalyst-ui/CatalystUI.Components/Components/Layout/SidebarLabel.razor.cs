using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class SidebarLabel
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}