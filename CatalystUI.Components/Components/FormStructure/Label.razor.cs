using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Label
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}