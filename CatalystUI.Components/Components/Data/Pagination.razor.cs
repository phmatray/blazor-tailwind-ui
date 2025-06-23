using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Pagination
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string AriaLabel { get; set; } = "Page navigation";
}