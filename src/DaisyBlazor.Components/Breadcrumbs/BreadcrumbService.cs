using Microsoft.Extensions.Localization;

namespace DaisyBlazor;

/// <summary>
/// daisyUI-returning breadcrumb service. Generic over the consumer's resource type so each
/// application can use its own <c>IStringLocalizer&lt;TResource&gt;</c>. The trail itself comes from
/// an app-supplied <see cref="IBreadcrumbSource"/>, keeping DaisyBlazor decoupled from any feature
/// or routing framework.
/// </summary>
/// <typeparam name="TResource">The resource type used to resolve localized breadcrumb titles.</typeparam>
public sealed class BreadcrumbService<TResource> : IBreadcrumbService
    where TResource : class
{
    private readonly IBreadcrumbSource _source;
    private readonly IStringLocalizer<TResource> _localizer;
    private readonly BreadcrumbServiceOptions _options;

    /// <summary>Creates a new <see cref="BreadcrumbService{TResource}"/>.</summary>
    /// <param name="source">The app-supplied source of the (un-localized) breadcrumb trail for a route.</param>
    /// <param name="localizer">The string localizer used to resolve breadcrumb titles.</param>
    /// <param name="options">Options controlling the Home breadcrumb's title key and icon.</param>
    /// <exception cref="ArgumentNullException">Thrown when any argument is <see langword="null"/>.</exception>
    public BreadcrumbService(
        IBreadcrumbSource source,
        IStringLocalizer<TResource> localizer,
        BreadcrumbServiceOptions options)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(localizer);
        ArgumentNullException.ThrowIfNull(options);

        _source = source;
        _localizer = localizer;
        _options = options;
    }

    /// <inheritdoc />
    public IReadOnlyList<BreadcrumbItem> GetBreadcrumbs(string currentRoute)
    {
        ArgumentNullException.ThrowIfNull(currentRoute);

        IReadOnlyList<BreadcrumbDefinition> definitions = _source.GetBreadcrumbs(currentRoute);
        if (definitions.Count == 0)
            return [];

        List<BreadcrumbItem> breadcrumbs =
        [
            new(_localizer[_options.HomeTitleKey], Href: "/", Icon: _options.HomeIcon)
        ];

        foreach (BreadcrumbDefinition def in definitions)
        {
            breadcrumbs.Add(new BreadcrumbItem(
                _localizer[def.TitleKey],
                Href: def.Href,
                Disabled: def.Href is null,
                Icon: def.Icon));
        }

        return breadcrumbs;
    }
}

/// <summary>Options for the daisyUI breadcrumb adapter.</summary>
public sealed class BreadcrumbServiceOptions
{
    /// <summary>Resource key used to localize the Home breadcrumb. Defaults to <c>"Breadcrumb_Home"</c>.</summary>
    public string HomeTitleKey { get; set; } = "Breadcrumb_Home";

    /// <summary>Icon for the Home breadcrumb. Defaults to <c>Icons.Material.Filled.Home</c>.</summary>
    public string HomeIcon { get; set; } = Icons.Material.Filled.Home;
}
