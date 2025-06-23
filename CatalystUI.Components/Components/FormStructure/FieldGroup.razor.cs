using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class FieldGroup
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}