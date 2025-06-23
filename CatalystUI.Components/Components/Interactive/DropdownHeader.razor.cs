using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownHeader
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}