using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class AuthLayout
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
}