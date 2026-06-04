namespace DaisyBlazor;

/// <summary>A single breadcrumb entry rendered in a daisyUI breadcrumbs trail.</summary>
/// <param name="Text">The localized display text.</param>
/// <param name="Href">Navigation href. <see langword="null"/> means the current page (non-clickable).</param>
/// <param name="Disabled">Whether the item is non-clickable (e.g. the current page).</param>
/// <param name="Icon">Optional Material Symbols ligature to render before the text.</param>
public sealed record BreadcrumbItem(string Text, string? Href, bool Disabled = false, string? Icon = null);
