using Microsoft.AspNetCore.Components;

namespace DaisyBlazor;

/// <summary>
/// Scoped <see cref="IDialogService"/> implementation. Builds a <see cref="DialogReference"/> per
/// request and raises <see cref="OnDialogShown"/> for the <see cref="DialogProvider"/> to render.
/// </summary>
public sealed class DialogService : IDialogService
{
    /// <inheritdoc />
    public event Action<DialogReference>? OnDialogShown;

    /// <inheritdoc />
    public Task<IDialogReference> ShowAsync<T>(
        string title,
        DialogParameters? parameters = null,
        DialogOptions? options = null)
        where T : ComponentBase
    {
        DialogReference reference = new(
            Guid.NewGuid(),
            title ?? string.Empty,
            typeof(T),
            parameters ?? new DialogParameters(),
            options ?? new DialogOptions());

        OnDialogShown?.Invoke(reference);

        return Task.FromResult<IDialogReference>(reference);
    }

    /// <inheritdoc />
    public async Task<bool?> ShowMessageBoxAsync(
        string title,
        string message,
        string yesText = "OK",
        string? noText = null,
        string? cancelText = null)
    {
        DialogParameters parameters = new()
        {
            ["Message"] = message,
            ["YesText"] = yesText,
            ["NoText"] = noText,
            ["CancelText"] = cancelText
        };

        IDialogReference reference = await ShowAsync<MessageBoxDialog>(title, parameters);
        DialogResult result = await reference.Result;

        if (result.Canceled)
        {
            return null;
        }

        if (result.Data is bool value)
        {
            return value;
        }

        return null;
    }
}
