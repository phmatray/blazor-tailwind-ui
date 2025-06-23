using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Fieldset
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}