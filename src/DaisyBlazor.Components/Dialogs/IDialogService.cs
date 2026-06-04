using Microsoft.AspNetCore.Components;

namespace DaisyBlazor;

/// <summary>
/// MudBlazor-compatible dialog service. A <see cref="DialogProvider"/> mounted once in the app
/// listens to <see cref="OnDialogShown"/> and renders the requested component.
/// </summary>
public interface IDialogService
{
    /// <summary>Raised when a new dialog is shown so the provider can render it.</summary>
    event Action<DialogReference>? OnDialogShown;

    /// <summary>
    /// Show dialog component <typeparamref name="T"/> with the given title, parameters and options.
    /// </summary>
    Task<IDialogReference> ShowAsync<T>(
        string title,
        DialogParameters? parameters = null,
        DialogOptions? options = null)
        where T : ComponentBase;

    /// <summary>
    /// Show a built-in confirm dialog. Returns <c>true</c> for yes, <c>false</c> for no,
    /// or <c>null</c> when canceled / dismissed.
    /// </summary>
    Task<bool?> ShowMessageBoxAsync(
        string title,
        string message,
        string yesText = "OK",
        string? noText = null,
        string? cancelText = null);
}
