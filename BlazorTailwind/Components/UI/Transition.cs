using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorTailwind.Components.UI;

public class Transition : ComponentBase
{
    [Parameter] public bool Show { get; set; }
    [Parameter] public bool Appear { get; set; } = false;

    [Parameter] public string? Enter { get; set; }
    [Parameter] public string? EnterFrom { get; set; }
    [Parameter] public string? EnterTo { get; set; }
    [Parameter] public string? Leave { get; set; }
    [Parameter] public string? LeaveFrom { get; set; }
    [Parameter] public string? LeaveTo { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public Action? BeforeEnter { get; set; }
    [Parameter] public Action? AfterEnter { get; set; }
    [Parameter] public Action? BeforeLeave { get; set; }
    [Parameter] public Action? AfterLeave { get; set; }

    [Parameter] public string As { get; set; } = "div";

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    private bool isEntering;
    private bool isLeaving;

    protected override void OnParametersSet()
    {
        if (Show)
        {
            isEntering = true;
            isLeaving = false;
            BeforeEnter?.Invoke();
        }
        else
        {
            isLeaving = true;
            isEntering = false;
            BeforeLeave?.Invoke();
        }
    }
    
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, As);
        builder.AddAttribute(1, "class", GetCurrentCssClass());
        builder.AddContent(2, ChildContent);
        builder.CloseElement();
    }

    private string GetCurrentCssClass()
    {
        var enterClasses = isEntering ? $"{Enter} {EnterFrom}" : EnterTo;
        var leaveClasses = isLeaving ? $"{Leave} {LeaveFrom}" : LeaveTo;
        var currentClasses = Show ? enterClasses : leaveClasses;

        return AdditionalAttributes?
            .TryGetValue("class", out var attribute) is true
            ? $"{attribute} {currentClasses}"
            : currentClasses ?? string.Empty;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (isEntering)
        {
            isEntering = false;
            AfterEnter?.Invoke();
        }
        else if (isLeaving)
        {
            isLeaving = false;
            AfterLeave?.Invoke();
        }
    }
}