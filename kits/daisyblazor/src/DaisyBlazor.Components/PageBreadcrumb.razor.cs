using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace DaisyBlazor;

/// <summary>
/// Reads the route from <see cref="NavigationManager"/> and renders breadcrumbs via
/// the registered <see cref="IBreadcrumbService"/>.
/// </summary>
public partial class PageBreadcrumb
{
    /// <summary>Optional CSS class forwarded to the underlying breadcrumbs.</summary>
    [Parameter]
    public string? Class { get; set; }

    private List<BreadcrumbItem> _breadcrumbs = [];

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += OnLocationChanged;
        UpdateBreadcrumbs();
    }

    private void OnLocationChanged(object? sender, LocationChangedEventArgs e)
    {
        UpdateBreadcrumbs();
        StateHasChanged();
    }

    private void UpdateBreadcrumbs()
    {
        string route = new Uri(NavigationManager.Uri).AbsolutePath;
        _breadcrumbs = [.. BreadcrumbService.GetBreadcrumbs(route)];
    }

    /// <inheritdoc />
    public void Dispose()
    {
        NavigationManager.LocationChanged -= OnLocationChanged;
    }
}
