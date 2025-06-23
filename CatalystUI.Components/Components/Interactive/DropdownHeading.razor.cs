using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownHeading
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}