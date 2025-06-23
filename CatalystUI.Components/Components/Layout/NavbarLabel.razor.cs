using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class NavbarLabel
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}