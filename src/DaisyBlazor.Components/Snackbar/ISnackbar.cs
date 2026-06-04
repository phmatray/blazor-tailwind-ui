namespace DaisyBlazor;

/// <summary>
/// MudBlazor-compatible snackbar contract. <c>Snackbar.Add("msg", Severity.Success)</c> stays working.
/// </summary>
public interface ISnackbar
{
    /// <summary>Currently visible toasts.</summary>
    IReadOnlyList<SnackbarMessage> Shown { get; }

    /// <summary>Raised whenever the visible set changes so a provider can re-render.</summary>
    event Action? OnChange;

    /// <summary>Queue a new toast.</summary>
    void Add(string message, Severity severity = Severity.Normal, Action<SnackbarOptions>? configure = null);

    /// <summary>Remove a specific toast.</summary>
    void Remove(SnackbarMessage message);

    /// <summary>Remove all toasts.</summary>
    void Clear();
}
