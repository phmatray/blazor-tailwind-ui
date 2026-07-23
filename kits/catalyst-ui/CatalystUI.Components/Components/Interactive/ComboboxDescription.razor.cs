using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class ComboboxDescription
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}