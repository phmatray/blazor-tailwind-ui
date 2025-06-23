using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Legend
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}