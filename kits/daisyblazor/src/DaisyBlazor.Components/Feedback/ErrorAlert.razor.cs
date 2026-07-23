using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Feedback;

/// <summary>
/// Filled error <see cref="Alert"/>; renders nothing when <see cref="Message"/> is null.
/// </summary>
public partial class ErrorAlert
{
    /// <summary>Error text. When null, the alert is not rendered.</summary>
    [Parameter]
    public string? Message { get; set; }

    /// <summary>CSS class applied to the alert. Defaults to <c>mb-4</c>.</summary>
    [Parameter]
    public string Class { get; set; } = "mb-4";
}
