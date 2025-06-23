using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownLabel
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}