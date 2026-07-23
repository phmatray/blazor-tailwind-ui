using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DialogBody
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}