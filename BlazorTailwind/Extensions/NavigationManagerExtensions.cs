using Microsoft.AspNetCore.Components;

namespace BlazorTailwind.Extensions;

public static class NavigationManagerExtensions
{
    public static bool IsActivePage(this NavigationManager navigationManager, string href)
    {
        var uri = new Uri(navigationManager.Uri);
        return uri.AbsolutePath == href;
    }
}