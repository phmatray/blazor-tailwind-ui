using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Description
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}