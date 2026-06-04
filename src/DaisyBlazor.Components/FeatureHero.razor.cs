using Microsoft.AspNetCore.Components;

namespace DaisyBlazor;

/// <summary>
/// Gradient identity banner for a feature home page. Carries the feature's own two
/// colours (the same gradient shown on the global home screen), a frosted icon tile,
/// an uppercase eyebrow, a title, a subtitle, a trailing actions slot, and an optional
/// inline stat strip. See <c>docs/home-page-design-principles.md</c>.
/// </summary>
public partial class FeatureHero
{
    /// <summary>Material icon shown in the frosted leading tile.</summary>
    [Parameter, EditorRequired]
    public string Icon { get; set; } = null!;

    /// <summary>Hero title (rendered in the display font, white).</summary>
    [Parameter, EditorRequired]
    public string Title { get; set; } = null!;

    /// <summary>Optional calm one-liner describing the feature.</summary>
    [Parameter]
    public string? Subtitle { get; set; }

    /// <summary>Optional uppercase, letter-spaced section label above the title.</summary>
    [Parameter]
    public string? Eyebrow { get; set; }

    /// <summary>Gradient start colour (CSS colour, e.g. <c>#0ea5e9</c>).</summary>
    [Parameter]
    public string GradientStart { get; set; } = "#1976d2";

    /// <summary>Gradient end colour (CSS colour, e.g. <c>#38bdf8</c>).</summary>
    [Parameter]
    public string GradientEnd { get; set; } = "#42a5f5";

    /// <summary>Optional trailing action area (buttons, chips) aligned to the hero edge.</summary>
    [Parameter]
    public RenderFragment? Actions { get; set; }

    /// <summary>Optional inline stat strip rendered beneath a hairline divider.</summary>
    [Parameter]
    public RenderFragment? Stats { get; set; }

    /// <summary>When true, renders a tighter hero (less vertical padding).</summary>
    [Parameter]
    public bool Dense { get; set; }

    /// <summary>Captures additional attributes (e.g. <c>class</c>) on the root element.</summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }

    private string CssClass
    {
        get
        {
            string css = "feature-hero" + (Dense ? " feature-hero--dense" : string.Empty);
            if (UserAttributes is not null
                && UserAttributes.TryGetValue("class", out object? extra)
                && extra is string s
                && !string.IsNullOrWhiteSpace(s))
            {
                css += " " + s;
            }

            return css;
        }
    }

    private string HeroStyle
    {
        get
        {
            string style = $"--hero-start:{GradientStart};--hero-end:{GradientEnd};";
            if (UserAttributes is not null
                && UserAttributes.TryGetValue("style", out object? extra)
                && extra is string s
                && !string.IsNullOrWhiteSpace(s))
            {
                style += s;
            }

            return style;
        }
    }
}
