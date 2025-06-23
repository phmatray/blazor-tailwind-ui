using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class ComboboxLabel
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}