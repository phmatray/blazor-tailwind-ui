namespace DaisyBlazor;

/// <summary>
/// Context passed to <c>Menu.ActivatorContent</c>. Mirrors the subset of MudBlazor's
/// menu activator context used by consuming markup (notably <c>menu.ToggleAsync</c>).
/// </summary>
public sealed class MenuActivator(Func<Task> toggle)
{
    private readonly Func<Task> _toggle = toggle;

    /// <summary>Opens the menu if closed, closes it if open.</summary>
    public Task ToggleAsync() => _toggle();
}
