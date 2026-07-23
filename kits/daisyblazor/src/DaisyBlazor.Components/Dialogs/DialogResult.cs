namespace DaisyBlazor;

/// <summary>
/// MudBlazor-compatible result returned when a dialog closes.
/// </summary>
public sealed class DialogResult
{
    private DialogResult(object? data, Type? dataType, bool canceled)
    {
        Data = data;
        DataType = dataType;
        Canceled = canceled;
    }

    /// <summary>Payload returned by the dialog (null when canceled or no data).</summary>
    public object? Data { get; }

    /// <summary>CLR type of <see cref="Data"/> when known.</summary>
    public Type? DataType { get; }

    /// <summary><c>true</c> when the dialog was dismissed without confirming.</summary>
    public bool Canceled { get; }

    /// <summary>Create a successful result carrying <paramref name="data"/>.</summary>
    public static DialogResult Ok<T>(T data) => new(data, typeof(T), false);

    /// <summary>Create a successful result with no payload.</summary>
    public static DialogResult Ok() => new(null, null, false);

    /// <summary>Create a canceled result.</summary>
    public static DialogResult Cancel() => new(null, null, true);
}
