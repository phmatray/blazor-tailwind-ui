using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DialogDescription
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}