using Microsoft.AspNetCore.Components;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class AlertDialogDescription : SpacingComponentBase
{
    [Parameter] public new RenderFragment? ChildContent { get; set; }
}