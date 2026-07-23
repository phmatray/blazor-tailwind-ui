namespace DaisyBlazor;

/// <summary>
/// Scoped <see cref="ISnackbar"/> implementation. Holds the visible toast list and notifies
/// <see cref="SnackbarProvider"/> via <see cref="OnChange"/>. Auto-dismiss timing is owned by the provider.
/// </summary>
public sealed class SnackbarService : ISnackbar
{
    private readonly List<SnackbarMessage> _shown = [];
    private readonly Lock _gate = new();

    /// <inheritdoc />
    public IReadOnlyList<SnackbarMessage> Shown
    {
        get
        {
            lock (_gate)
            {
                return _shown.ToArray();
            }
        }
    }

    /// <inheritdoc />
    public event Action? OnChange;

    /// <inheritdoc />
    public void Add(string message, Severity severity = Severity.Normal, Action<SnackbarOptions>? configure = null)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        SnackbarOptions options = new();
        configure?.Invoke(options);

        lock (_gate)
        {
            if (options.PreventDuplicates &&
                _shown.Any(m => m.Severity == severity && string.Equals(m.Message, message, StringComparison.Ordinal)))
            {
                return;
            }

            _shown.Add(new SnackbarMessage(Guid.NewGuid(), message, severity, options));
        }

        OnChange?.Invoke();
    }

    /// <inheritdoc />
    public void Remove(SnackbarMessage message)
    {
        bool removed;
        lock (_gate)
        {
            removed = _shown.RemoveAll(m => m.Id == message.Id) > 0;
        }

        if (removed)
        {
            OnChange?.Invoke();
        }
    }

    /// <inheritdoc />
    public void Clear()
    {
        bool any;
        lock (_gate)
        {
            any = _shown.Count > 0;
            _shown.Clear();
        }

        if (any)
        {
            OnChange?.Invoke();
        }
    }
}
