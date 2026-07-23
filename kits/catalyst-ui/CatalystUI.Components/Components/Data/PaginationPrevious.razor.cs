using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class PaginationPrevious
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Href { get; set; }
}