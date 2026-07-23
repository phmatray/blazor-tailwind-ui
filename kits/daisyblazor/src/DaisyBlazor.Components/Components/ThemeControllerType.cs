namespace DaisyBlazor;

/// <summary>Specifies the HTML input type used by the daisyUI theme-controller.</summary>
public enum ThemeControllerType
{
    /// <summary>
    /// Renders as <c>&lt;input type="checkbox"&gt;</c>. Toggles between the default theme
    /// (unchecked) and <c>Value</c> (checked). Ideal for a two-theme Light/Dark switch.
    /// </summary>
    Checkbox,

    /// <summary>
    /// Renders as <c>&lt;input type="radio"&gt;</c>. Selects <c>Value</c> when checked.
    /// Multiple controllers sharing the same <c>Name</c> form a mutually exclusive group,
    /// letting the user pick from any number of daisyUI themes.
    /// </summary>
    Radio
}
