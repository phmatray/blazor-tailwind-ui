namespace DaisyBlazor;

/// <summary>
/// MudBlazor-compatible display options for a dialog.
/// </summary>
public sealed class DialogOptions
{
    /// <summary>Maximum width of the dialog box.</summary>
    public MaxWidth? MaxWidth { get; set; }

    /// <summary>Stretch the dialog box to the chosen max width.</summary>
    public bool? FullWidth { get; set; }

    /// <summary>Show an explicit close (x) button in the header.</summary>
    public bool? CloseButton { get; set; }

    /// <summary>Allow dismissing the dialog with the Escape key.</summary>
    public bool? CloseOnEscapeKey { get; set; }

    /// <summary>Allow dismissing the dialog by clicking the backdrop.</summary>
    public bool? BackdropClick { get; set; }

    /// <summary>Center the dialog vertically (no-op placeholder for parity).</summary>
    public bool? NoHeader { get; set; }
}
