using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DaisyBlazor.Theming;

/// <summary>
/// daisyUI theme provider. Manages the selected theme (any enabled daisyUI theme, or the
/// special <c>"system"</c> value which follows the OS light/dark setting), applies it to the
/// document <c>data-theme</c> attribute via <see cref="IJSRuntime"/>, and persists the choice
/// to localStorage + cookie. Cascades itself so descendants can read <see cref="Preference"/> /
/// <see cref="Theme"/> / <see cref="IsDarkMode"/> and call <see cref="SetThemeAsync"/>.
/// <para>
/// Brand-neutral: the light/dark theme applied for <c>"system"</c>, the set of selectable themes,
/// and the set of themes considered "dark" are all parameter-driven, so any application can plug in
/// its own daisyUI themes without modifying this component.
/// </para>
/// </summary>
public partial class ThemeProvider
{
    /// <summary>The special preference value that follows the OS light/dark setting.</summary>
    public const string SystemPreference = "system";

    /// <summary>The 35 built-in daisyUI v5 themes, in display order. Used as the default for <see cref="Themes"/>.</summary>
    public static readonly IReadOnlyList<string> DaisyUiThemes =
    [
        "light", "dark", "cupcake", "bumblebee", "emerald", "corporate", "synthwave", "retro",
        "cyberpunk", "valentine", "halloween", "garden", "forest", "aqua", "lofi", "pastel",
        "fantasy", "wireframe", "black", "luxury", "dracula", "cmyk", "autumn", "business",
        "acid", "lemonade", "night", "coffee", "winter", "dim", "nord", "sunset",
        "caramellatte", "abyss", "silk"
    ];

    /// <summary>The built-in daisyUI themes that use a dark color scheme. Default for <see cref="DarkThemes"/>.</summary>
    public static readonly IReadOnlyCollection<string> DaisyUiDarkThemes =
    [
        "dark", "synthwave", "halloween", "forest", "black", "luxury", "dracula", "business",
        "night", "coffee", "dim", "sunset", "abyss"
    ];

    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    /// <summary>Content rendered inside the provider; receives <c>this</c> as a cascading value.</summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>Theme applied for <see cref="SystemPreference"/> when the OS is in light mode. Defaults to <c>light</c>.</summary>
    [Parameter]
    public string SystemLightTheme { get; set; } = "light";

    /// <summary>Theme applied for <see cref="SystemPreference"/> when the OS is in dark mode. Defaults to <c>dark</c>.</summary>
    [Parameter]
    public string SystemDarkTheme { get; set; } = "dark";

    /// <summary>
    /// All selectable themes, in display order. Must stay in sync with the <c>themes:</c> list enabled
    /// in your Tailwind/daisyUI css. Defaults to all 35 built-in daisyUI themes (<see cref="DaisyUiThemes"/>).
    /// </summary>
    [Parameter]
    public IReadOnlyList<string> Themes { get; set; } = DaisyUiThemes;

    /// <summary>Themes considered "dark" (used to resolve <see cref="IsDarkMode"/>). Defaults to <see cref="DaisyUiDarkThemes"/>.</summary>
    [Parameter]
    public IReadOnlyCollection<string> DarkThemes { get; set; } = DaisyUiDarkThemes;

    /// <summary>localStorage key for the saved preference. Defaults to <c>daisy-theme</c>.</summary>
    [Parameter]
    public string StorageKey { get; set; } = "daisy-theme";

    /// <summary>Cookie name for the preference (so the server can read it during SSR). Defaults to <c>daisy-theme</c>.</summary>
    [Parameter]
    public string CookieKey { get; set; } = "daisy-theme";

    /// <summary>Cookie name for the resolved theme (so SSR can paint the right theme). Defaults to <c>daisy-darkmode</c>.</summary>
    [Parameter]
    public string DarkModeCookieKey { get; set; } = "daisy-darkmode";

    /// <summary>
    /// Initial preference read by the consumer (typically from the server-side cookie during SSR) to avoid
    /// a flash of the wrong theme on first paint. May be a theme name or <c>"system"</c>. Pass <c>null</c> to skip.
    /// </summary>
    [Parameter]
    public string? InitialPreference { get; set; }

    /// <summary>Kept for backwards compatibility; no longer used to seed state (the resolved theme cookie is preferred).</summary>
    [Parameter]
    public bool? InitialIsDarkMode { get; set; }

    /// <summary>Raised after the applied theme changes (e.g. to refresh a theme picker).</summary>
    [Parameter]
    public EventCallback<string> OnThemeChanged { get; set; }

    /// <summary>The persisted choice: a theme name, or <see cref="SystemPreference"/>.</summary>
    private string _preference = SystemPreference;

    /// <summary>The actual daisyUI theme currently applied to <c>data-theme</c>.</summary>
    private string _resolved = "light";

    private bool _isSystemDarkMode;

    /// <summary>Current selection: a theme name, or <c>"system"</c>.</summary>
    public string Preference => _preference;

    /// <summary>The daisyUI theme actually applied (resolves <c>"system"</c> to a concrete theme).</summary>
    public string Theme => _resolved;

    /// <summary>Whether the applied theme is a dark theme.</summary>
    public bool IsDarkMode => DarkThemes.Contains(_resolved);

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        _resolved = SystemLightTheme;

        if (!string.IsNullOrEmpty(InitialPreference))
        {
            _preference = InitialPreference;
        }

        // Best-effort resolve before the first interactive render (refined in OnAfterRenderAsync).
        _resolved = ResolveTheme(_preference, string.Equals(_preference, SystemDarkTheme, StringComparison.OrdinalIgnoreCase));
    }

    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            return;
        }

        try
        {
            _isSystemDarkMode = await JS.InvokeAsync<bool>(
                "eval",
                "window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches");

            string? stored = await JS.InvokeAsync<string?>("localStorage.getItem", StorageKey);
            if (!string.IsNullOrEmpty(stored))
            {
                _preference = stored;
            }
        }
        catch
        {
            // localStorage / matchMedia not available
        }

        _resolved = ResolveTheme(_preference, _isSystemDarkMode);
        await ApplyThemeAttributeAsync();
        await PersistAsync();
        StateHasChanged();
    }

    /// <summary>Resolves a preference (theme name or "system") to a concrete daisyUI theme.</summary>
    private string ResolveTheme(string preference, bool systemPrefersDark)
    {
        if (string.Equals(preference, SystemPreference, StringComparison.OrdinalIgnoreCase))
        {
            return systemPrefersDark ? SystemDarkTheme : SystemLightTheme;
        }

        return Themes.Contains(preference) ? preference : SystemLightTheme;
    }

    /// <summary>
    /// Selects a theme and persists it. Pass any value from <see cref="Themes"/>, or
    /// <see cref="SystemPreference"/> to follow the OS setting.
    /// </summary>
    public async Task SetThemeAsync(string preference)
    {
        if (_preference == preference)
        {
            return;
        }

        _preference = preference;
        _resolved = ResolveTheme(_preference, _isSystemDarkMode);
        await ApplyThemeAttributeAsync();
        await PersistAsync();
        StateHasChanged();

        if (OnThemeChanged.HasDelegate)
        {
            await OnThemeChanged.InvokeAsync(_resolved);
        }
    }

    private async Task ApplyThemeAttributeAsync()
    {
        try
        {
            await JS.InvokeVoidAsync("document.documentElement.setAttribute", "data-theme", _resolved);
        }
        catch
        {
            // DOM not available
        }
    }

    private async Task PersistAsync()
    {
        try
        {
            await JS.InvokeVoidAsync("localStorage.setItem", StorageKey, _preference);
            await JS.InvokeVoidAsync(
                "eval",
                $"document.cookie = '{CookieKey}={_preference};path=/;max-age=31536000';" +
                $"document.cookie = '{DarkModeCookieKey}={_resolved};path=/;max-age=31536000'");
        }
        catch
        {
            // localStorage / cookie not available
        }
    }
}
