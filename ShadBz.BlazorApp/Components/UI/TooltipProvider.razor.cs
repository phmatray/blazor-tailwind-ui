using Microsoft.AspNetCore.Components;

namespace ShadBz.BlazorApp.Components.UI;

public partial class TooltipProvider : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public int DelayDuration { get; set; } = 700;
    [Parameter] public int SkipDelayDuration { get; set; } = 300;
    
    public int GetDelayDuration() => DelayDuration;
    public int GetSkipDelayDuration() => SkipDelayDuration;
}