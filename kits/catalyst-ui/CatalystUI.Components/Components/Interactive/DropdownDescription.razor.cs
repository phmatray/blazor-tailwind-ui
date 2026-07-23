using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownDescription
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}