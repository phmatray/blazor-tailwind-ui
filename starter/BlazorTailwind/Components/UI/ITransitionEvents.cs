namespace BlazorTailwind.Components.UI;

public interface ITransitionEvents
{
    /// <summary>
    /// Callback which is called before we start the enter transition.
    /// </summary>
    Action? BeforeEnter { get; set; }

    /// <summary>
    /// Callback which is called after we finished the enter transition.
    /// </summary>
    Action? AfterEnter { get; set; }

    /// <summary>
    /// Callback which is called before we start the leave transition.
    /// </summary>
    Action? BeforeLeave { get; set; }

    /// <summary>
    /// Callback which is called after we finished the leave transition.
    /// </summary>
    Action? AfterLeave { get; set; }
}