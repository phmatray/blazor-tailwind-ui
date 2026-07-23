namespace DaisyBlazor;

/// <summary>
/// Cascaded handle a hosted dialog component uses to close or cancel itself.
/// Mirrors MudBlazor's <c>IMudDialogInstance</c> so existing dialog components migrate unchanged.
/// </summary>
public interface IMudDialogInstance
{
    /// <summary>Title shown in the dialog header.</summary>
    string Title { get; }

    /// <summary>Close the dialog with a successful, payload-less result.</summary>
    void Close();

    /// <summary>Close the dialog with the supplied result.</summary>
    void Close(DialogResult result);

    /// <summary>Close the dialog as canceled.</summary>
    void Cancel();
}
