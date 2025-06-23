using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class ListboxLabel
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}