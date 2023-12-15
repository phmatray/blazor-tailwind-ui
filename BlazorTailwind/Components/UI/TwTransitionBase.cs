using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;

namespace BlazorTailwind.Components.UI;

public partial class TwTransitionBase : ComponentBase, ITransitionClasses, ITransitionEvents
{
    [GeneratedRegex(@"duration-(\d+)")]
    private static partial Regex DurationRegex();

    /// <summary>
    /// The element to render in place of the Transition itself.
    /// </summary>
    [Parameter] public string As { get; set; } = "div";
    
    [Parameter] public string Enter { get; set; } = "";
    [Parameter] public string EnterFrom { get; set; } = "";
    [Parameter] public string EnterTo { get; set; } = "";
    [Parameter] public string Entered { get; set; } = "";
    [Parameter] public string Leave { get; set; } = "";
    [Parameter] public string LeaveFrom { get; set; } = "";
    [Parameter] public string LeaveTo { get; set; } = "";
    [Parameter] public string AdditionalClasses { get; set; } = "";

    [Parameter] public Action? BeforeEnter { get; set; }
    [Parameter] public Action? AfterEnter { get; set; }
    [Parameter] public Action? BeforeLeave { get; set; }
    [Parameter] public Action? AfterLeave { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }
    
    [Parameter] public int Duration { get; set; }
    protected StringBuilder TransitionClasses = new();
    
    protected async Task ShowAsync()
    {
        if (string.IsNullOrWhiteSpace(Enter) && string.IsNullOrWhiteSpace(EnterFrom) && string.IsNullOrWhiteSpace(EnterTo))
        {
            return;
        }

        Duration = GetDuration(Enter);
        TransitionClasses.Append(Enter);
        TransitionClasses.Append($" {EnterFrom} ");
        StateHasChanged();
        await Task.Delay(1);
        TransitionClasses.Replace(EnterFrom, EnterTo);
    }

    protected async Task HideAsync()
    {
        Duration = GetDuration(Leave);

        if (Enter != Leave && !string.IsNullOrWhiteSpace(Enter))
        {
            TransitionClasses.Replace(Enter, Leave);
        }
        else
        {
            TransitionClasses.Append(Leave);
        }
        
        if (EnterTo != LeaveFrom && !string.IsNullOrWhiteSpace(EnterTo))
        {
            TransitionClasses.Replace(EnterTo, LeaveFrom);
        }
        else
        {
            TransitionClasses.Append($" {LeaveFrom}");
        }
        
        TransitionClasses.Replace(LeaveFrom, LeaveTo);
        await Task.Delay(Duration);
        TransitionClasses.Clear();
    }
    
    private static int GetDuration(string match)
    {
        var durationMatch = DurationRegex().Match(match);
        
        return durationMatch.Success
            ? Convert.ToInt32(durationMatch.Groups[1].Value)
            : 0;
    }
}