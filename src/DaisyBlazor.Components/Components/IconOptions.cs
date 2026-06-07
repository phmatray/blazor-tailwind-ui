namespace DaisyBlazor;

/// <summary>
/// Configures how <see cref="Icon"/> (and every component that renders icons through it)
/// emits glyphs. Cascade an instance near the application root to swap the icon font:
/// <code>
/// &lt;CascadingValue Value="_icons" IsFixed="true"&gt;
///     &lt;Router ... /&gt;
/// &lt;/CascadingValue&gt;
/// @code { readonly IconOptions _icons = new() { FontClass = "material-symbols-rounded" }; }
/// </code>
/// When no <see cref="IconOptions"/> is cascaded, icons render with the default Material
/// Symbols font class, so existing applications need no change. Material Symbols renders via
/// ligatures, so a <see cref="FontClass"/> swap is transparent only for a ligature-compatible
/// font that shares Material's ligature names (e.g. another Material Symbols variant). For a
/// fully custom icon set (Lucide, Heroicons, inline SVG), pass an SVG as <see cref="Icon"/>'s
/// child content instead of a ligature <c>Name</c>.
/// </summary>
public sealed class IconOptions
{
    /// <summary>The CSS class identifying the icon font. Defaults to <c>"material-symbols-outlined"</c>.</summary>
    public string FontClass { get; set; } = DefaultFontClass;

    /// <summary>The icon-font class used when no <see cref="IconOptions"/> is cascaded.</summary>
    public const string DefaultFontClass = "material-symbols-outlined";
}
