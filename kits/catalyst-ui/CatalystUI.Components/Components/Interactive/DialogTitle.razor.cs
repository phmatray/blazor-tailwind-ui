using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DialogTitle
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}