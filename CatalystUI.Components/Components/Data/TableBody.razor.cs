using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class TableBody
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}