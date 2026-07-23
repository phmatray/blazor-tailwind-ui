namespace DaisyBlazor;

/// <summary>
/// Supplies the raw (un-localized) breadcrumb trail for a route. Implemented by the consuming
/// application so that <see cref="BreadcrumbService{TResource}"/> stays free of any feature/routing
/// framework: plug in feature modules, a route table, attributes, or a hard-coded map — DaisyBlazor
/// only needs the ordered list of <see cref="BreadcrumbDefinition"/> for the current route.
/// </summary>
public interface IBreadcrumbSource
{
    /// <summary>
    /// Returns the breadcrumb trail for <paramref name="currentRoute"/>, excluding the Home crumb
    /// (which <see cref="BreadcrumbService{TResource}"/> prepends). Return an empty list when the
    /// route has no trail.
    /// </summary>
    /// <param name="currentRoute">The current page route (e.g. <c>"/vouchers/lounge/42"</c>).</param>
    IReadOnlyList<BreadcrumbDefinition> GetBreadcrumbs(string currentRoute);
}

/// <summary>A single un-localized breadcrumb entry returned by an <see cref="IBreadcrumbSource"/>.</summary>
/// <param name="TitleKey">Resource key resolved via <c>IStringLocalizer&lt;TResource&gt;</c> to the display text.</param>
/// <param name="Href">Navigation href. <see langword="null"/> means the current page (non-clickable).</param>
/// <param name="Icon">Optional Material Symbols ligature rendered before the text.</param>
public sealed record BreadcrumbDefinition(string TitleKey, string? Href = null, string? Icon = null);
