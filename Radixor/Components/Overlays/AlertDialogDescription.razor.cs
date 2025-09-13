using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

namespace Radixor.Components.Overlays;

public partial class AlertDialogDescription : SpacingComponentBase
{
    [Parameter] public new RenderFragment? ChildContent { get; set; }
}