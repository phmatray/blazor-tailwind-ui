namespace DaisyBlazor;

/// <summary>Service for generating breadcrumbs based on the current route.</summary>
public interface IBreadcrumbService
{
    /// <summary>Gets breadcrumb items for the given route, suitable for a daisyUI breadcrumbs trail.</summary>
    /// <param name="currentRoute">The current page route (e.g. <c>"/vouchers/lounge/42"</c>).</param>
    /// <returns>A localized list of breadcrumb items starting with Home, or an empty list if no feature handles the route.</returns>
    IReadOnlyList<BreadcrumbItem> GetBreadcrumbs(string currentRoute);
}
