using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class ListboxDescription
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}