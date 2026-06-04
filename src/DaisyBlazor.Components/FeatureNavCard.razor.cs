using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace DaisyBlazor;

/// <summary>
/// Feature-navigation card with avatar, title, subtitle, description and an optional
/// "go to" action button.
/// </summary>
public partial class FeatureNavCard(NavigationManager navigation)
{
    /// <summary>Color of the leading avatar.</summary>
    [Parameter, EditorRequired]
    public Color AvatarColor { get; set; }

    /// <summary>Icon inside the avatar.</summary>
    [Parameter, EditorRequired]
    public string Icon { get; set; } = null!;

    /// <summary>Card title.</summary>
    [Parameter, EditorRequired]
    public string Title { get; set; } = null!;

    /// <summary>Subtitle shown beneath the title.</summary>
    [Parameter, EditorRequired]
    public string Subtitle { get; set; } = null!;

    /// <summary>Body description of the feature.</summary>
    [Parameter, EditorRequired]
    public string Description { get; set; } = null!;

    /// <summary>Navigation target. When set, the whole card becomes clickable.</summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>Optional action-button label (only rendered when <see cref="Href"/> is set).</summary>
    [Parameter]
    public string? ActionLabel { get; set; }

    /// <summary>Color of the action button. Defaults to <see cref="AvatarColor"/>.</summary>
    [Parameter]
    public Color? ActionColor { get; set; }

    /// <summary>
    /// Optional avatar gradient start. When both gradient colours are set, the avatar
    /// renders as a gradient tile (echoing the feature hero) instead of a flat
    /// <see cref="AvatarColor"/> avatar.
    /// </summary>
    [Parameter]
    public string? GradientStart { get; set; }

    /// <summary>Optional avatar gradient end. See <see cref="GradientStart"/>.</summary>
    [Parameter]
    public string? GradientEnd { get; set; }

    /// <summary>Optional extra content rendered below the description.</summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private bool IsClickable => Href is not null;

    private bool HasGradient => GradientStart is not null && GradientEnd is not null;

    private Color ResolvedActionColor => ActionColor ?? AvatarColor;

    private string AvatarStyle =>
        $"background:linear-gradient(135deg,{GradientStart} 0%,{GradientEnd} 100%);"
        + $"--nav-card-glow:color-mix(in srgb,{GradientEnd} 55%,transparent);";

    /// <summary>Gradient strip and hover tint shared by the accent bar and the card itself.</summary>
    private string AccentStyle =>
        $"--nav-card-from:{GradientStart};--nav-card-to:{GradientEnd};"
        + $"--nav-card-glow:color-mix(in srgb,{GradientEnd} 45%,transparent);";

    private string CardClass =>
        "grow nav-card" + (IsClickable ? " cursor-pointer hover-card" : string.Empty);

    private void OnCardClick()
    {
        if (Href is not null)
        {
            navigation.NavigateTo(Href);
        }
    }

    /// <summary>
    /// Accessibility attributes splatted onto the card so a clickable card behaves like a
    /// button for keyboard and screen-reader users (role, tab stop, label, key handler).
    /// </summary>
    private IReadOnlyDictionary<string, object>? CardAttributes => IsClickable
        ? new Dictionary<string, object>
        {
            ["role"] = "button",
            ["tabindex"] = "0",
            ["aria-label"] = ActionLabel is not null ? $"{Title} – {ActionLabel}" : Title,
            ["onkeydown"] = EventCallback.Factory.Create<KeyboardEventArgs>(this, HandleKeyDown),
        }
        : null;

    private void HandleKeyDown(KeyboardEventArgs e)
    {
        if (Href is null)
        {
            return;
        }

        if (e.Key is "Enter" or " " or "Spacebar")
        {
            navigation.NavigateTo(Href);
        }
    }
}
