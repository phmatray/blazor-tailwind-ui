using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownSection
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}