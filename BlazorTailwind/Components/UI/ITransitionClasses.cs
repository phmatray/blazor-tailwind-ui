namespace BlazorTailwind.Components.UI;

public interface ITransitionClasses
{
    /// <summary>
    /// Classes to add to the transitioning element during the entire enter phase.
    /// </summary>
    string Enter { get; set; }

    /// <summary>
    /// Classes to add to the transitioning element before the enter phase starts.
    /// </summary>
    string EnterFrom { get; set; }

    /// <summary>
    /// Classes to add to the transitioning element immediately after the enter phase starts.
    /// </summary>
    string EnterTo { get; set; }

    /// <summary>
    /// Classes to add to the transitioning element once the transition is done. These classes will persist after that, until it's time to leave.
    /// </summary>
    string Entered { get; set; }

    /// <summary>
    /// Classes to add to the transitioning element during the entire leave phase.
    /// </summary>
    string Leave { get; set; }

    /// <summary>
    /// Classes to add to the transitioning element before the leave phase starts.
    /// </summary>
    string LeaveFrom { get; set; }

    /// <summary>
    /// Classes to add to the transitioning element immediately after the leave phase starts.
    /// </summary>
    string LeaveTo { get; set; }

    string AdditionalClasses { get; set; }
}