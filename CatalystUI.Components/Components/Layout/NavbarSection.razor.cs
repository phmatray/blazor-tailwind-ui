using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class NavbarSection
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}