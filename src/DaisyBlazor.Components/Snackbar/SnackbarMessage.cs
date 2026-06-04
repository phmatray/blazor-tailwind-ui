namespace DaisyBlazor;

/// <summary>
/// A single snackbar/toast entry tracked by <see cref="ISnackbar"/>.
/// </summary>
public sealed record SnackbarMessage(Guid Id, string Message, Severity Severity, SnackbarOptions Options);
