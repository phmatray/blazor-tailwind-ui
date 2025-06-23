using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class PaginationPage
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter, EditorRequired] public string Href { get; set; } = "";
    [Parameter] public bool Current { get; set; }
}