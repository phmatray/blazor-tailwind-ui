using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class PaginationList
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}