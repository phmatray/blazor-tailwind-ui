using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class PaginationGap
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}