using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorTailwind.Components.UI;

public class Transition : ComponentBase
{
    /// <summary>
    /// Whether the children should be shown or hidden.
    /// </summary>
    [Parameter, EditorRequired] public bool Show { get; set; }

    /// <summary>
    /// The element to render in place of the Transition itself.
    /// </summary>
    [Parameter] public string As { get; set; } = "div";
    
    /// <summary>
    /// Whether the transition should run on initial mount.
    /// </summary>
    [Parameter] public bool Appear { get; set; } = false;
    
    /// <summary>
    /// Whether the element should be unmounted or hidden based on the show state.
    /// </summary>
    [Parameter] public bool Unmount { get; set; } = true;
    
    /// <summary>
    /// Whether the element should be unmounted or hidden based on the show state.
    /// </summary>
    [Parameter] public string? Enter { get; set; }
    
    /// <summary>
    /// Classes to add to the transitioning element before the enter phase starts.
    /// </summary>
    [Parameter] public string? EnterFrom { get; set; }
    
    /// <summary>
    /// Classes to add to the transitioning element immediately after the enter phase starts.
    /// </summary>
    [Parameter] public string? EnterTo { get; set; }
    
    /// <summary>
    /// Classes to add to the transitioning element once the transition is done. These classes will persist after that, until it's time to leave.
    /// </summary>
    [Parameter] public string? Entered { get; set; }
    
    /// <summary>
    /// Classes to add to the transitioning element during the entire leave phase.
    /// </summary>
    [Parameter] public string? Leave { get; set; }
    
    /// <summary>
    /// Classes to add to the transitioning element before the leave phase starts.
    /// </summary>
    [Parameter] public string? LeaveFrom { get; set; }
    
    /// <summary>
    /// Classes to add to the transitioning element immediately after the leave phase starts.
    /// </summary>
    [Parameter] public string? LeaveTo { get; set; }

    /// <summary>
    /// Callback which is called before we start the enter transition.
    /// </summary>
    [Parameter] public Action BeforeEnter { get; set; } = () => { };
    
    /// <summary>
    /// Callback which is called after we finished the enter transition.
    /// </summary>
    [Parameter] public Action AfterEnter { get; set; } = () => { };
    
    /// <summary>
    /// Callback which is called before we start the leave transition.
    /// </summary>
    [Parameter] public Action BeforeLeave { get; set; } = () => { };
    
    /// <summary>
    /// Callback which is called after we finished the leave transition.
    /// </summary>
    [Parameter] public Action AfterLeave { get; set; } = () => { };
    
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }
    
    private bool IsEntering { get; set; }
    private bool IsLeaving { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        SetInitialAnimationState();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        UpdateAnimationState();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (IsEntering)
        {
            IsEntering = false;
            AfterEnter.Invoke();
        }
        else if (IsLeaving)
        {
            IsLeaving = false;
            AfterLeave.Invoke();
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Show)
        {
            builder.OpenElement(0, As);
        
            var classNames = GetCurrentCssClass();
            if (!string.IsNullOrWhiteSpace(classNames))
            {
                builder.AddAttribute(1, "class", classNames);
            }
        
            if (AdditionalAttributes != null)
            {
                foreach (var kvp in AdditionalAttributes)
                {
                    builder.AddAttribute(2, kvp.Key, kvp.Value);
                }
            }
        
            builder.AddContent(3, ChildContent);
            builder.CloseElement();
        }
    }

    private void SetInitialAnimationState()
    {
        if (Show)
        {
            IsEntering = Appear;
            IsLeaving = false;
        }
        else
        {
            IsEntering = false;
            IsLeaving = false;
        }
    }


    private void UpdateAnimationState()
    {
        if (Show && !IsEntering)
        {
            IsEntering = true;
            IsLeaving = false;
            BeforeEnter.Invoke();
        }
        else if (!Show && !IsLeaving)
        {
            IsEntering = false;
            IsLeaving = true;
            BeforeLeave.Invoke();
        }
    }

    private string GetCurrentCssClass()
    {
        var enterClasses = IsEntering ? $"{Enter} {EnterFrom}".Trim() : EnterTo;
        var leaveClasses = IsLeaving ? $"{Leave} {LeaveFrom}".Trim() : LeaveTo;
        var currentClasses = Show ? enterClasses : leaveClasses;

        return AdditionalAttributes?
            .TryGetValue("class", out var attribute) is true
            ? $"{attribute} {currentClasses}"
            : currentClasses ?? string.Empty;
    }
}