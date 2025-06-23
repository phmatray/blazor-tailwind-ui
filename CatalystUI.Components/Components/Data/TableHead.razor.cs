using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class TableHead
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}