using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DialogActions
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}