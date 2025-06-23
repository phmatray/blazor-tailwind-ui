using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class SidebarSection
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}