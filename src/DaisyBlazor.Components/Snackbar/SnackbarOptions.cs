namespace DaisyBlazor;

/// <summary>
/// Per-message snackbar configuration, mirroring the MudBlazor options surface used across DaisyBlazor.
/// </summary>
public sealed class SnackbarOptions
{
    /// <summary>How long (ms) the toast stays visible before it auto-dismisses.</summary>
    public int VisibleStateDuration { get; set; } = 5000;

    /// <summary>Show a manual close button on the toast.</summary>
    public bool ShowCloseIcon { get; set; } = true;

    /// <summary>When true, an identical message will not be added again while it is still shown.</summary>
    public bool PreventDuplicates { get; set; } = true;
}
