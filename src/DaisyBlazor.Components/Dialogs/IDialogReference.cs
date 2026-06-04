namespace DaisyBlazor;

/// <summary>
/// Handle to a shown dialog. Await <see cref="Result"/> to get the outcome once the dialog closes.
/// </summary>
public interface IDialogReference
{
    /// <summary>Completes when the dialog closes, carrying its <see cref="DialogResult"/>.</summary>
    Task<DialogResult> Result { get; }

    /// <summary>Programmatically close the dialog with the given result.</summary>
    void Close(DialogResult result);

    /// <summary>Programmatically close the dialog with a default successful result.</summary>
    void Close();
}
