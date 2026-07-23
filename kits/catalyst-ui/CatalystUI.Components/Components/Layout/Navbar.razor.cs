using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Navbar
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}