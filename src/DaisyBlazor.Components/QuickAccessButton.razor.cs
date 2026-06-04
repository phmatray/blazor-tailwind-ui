using Microsoft.AspNetCore.Components;

namespace DaisyBlazor;

/// <summary>
/// Outlined full-width button with optional badge, tooltip and loading state.
/// Used as a quick-access shortcut on feature home pages.
/// </summary>
public partial class QuickAccessButton
{
    /// <summary>Button label.</summary>
    [Parameter, EditorRequired]
    public string Label { get; set; } = null!;

    /// <summary>Leading icon (use values from <see cref="Icons"/>).</summary>
    [Parameter, EditorRequired]
    public string Icon { get; set; } = null!;

    /// <summary>Color applied to the button.</summary>
    [Parameter]
    public Color Color { get; set; } = Color.Default;

    /// <summary>Optional navigation target. When set, the button behaves as a link.</summary>
    [Parameter]
    public string? Href { get; set; }

    /// <summary>Click handler. Combined with <see cref="Href"/>, <see cref="Href"/> wins for navigation.</summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    /// <summary>When true, disables the button.</summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>When true, shows a progress spinner and disables the button.</summary>
    [Parameter]
    public bool Loading { get; set; }

    /// <summary>Optional badge text. When set, the button is wrapped in a <see cref="Badge"/> and disabled.</summary>
    [Parameter]
    public string? BadgeText { get; set; }

    /// <summary>Tooltip shown over the badge when <see cref="BadgeText"/> is set.</summary>
    [Parameter]
    public string? TooltipText { get; set; }

    private bool IsDisabled => Disabled || Loading || BadgeText is not null;
}
