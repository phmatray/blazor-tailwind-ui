using BlazorTailwind.Components.Icons.Outline24;
using BlazorTailwind.Components.Layout;
using Microsoft.AspNetCore.Components;

namespace BlazorTailwind.Services;

public class NavigationService(
    NavigationManager navigationManager)
{
    public NavigationItem[] GetMainMenu() =>
    [
        new NavigationItem
        {
            Name = "Home",
            Href = "/",
            Icon = typeof(HomeIcon),
            Current = true
        },
        new NavigationItem
        {
            Name = "Counter",
            Href = "counter",
            Icon = typeof(UsersIcon),
            Current = false
        },
        new NavigationItem
        {
            Name = "Weather",
            Href = "weather",
            Icon = typeof(FolderIcon),
            Current = false
        }
    ];

    public NavigationItem[] GetExternalLinks() =>
    [
        new NavigationItem
        {
            Name = "ASP.NET",
            Href = "https://learn.microsoft.com/aspnet/core/",
            Initial = "A",
            Current = false
        },
        new NavigationItem
        {
            Name = "Tailwind CSS",
            Href = "https://tailwindcss.com/",
            Initial = "T",
            Current = false
        },
        new NavigationItem
        {
            Name = "Fluxor",
            Href = "https://github.com/mrpmorris/Fluxor",
            Initial = "F",
            Current = false
        }
    ];
    
    public NavigationItem[] GetSettings() =>
    [
        new NavigationItem
        {
            Name = "Your Profile",
            Href = "#"
        },
        new NavigationItem
        {
            Name = "Sign out",
            Href = "#"
        }
    ];
    
    public bool IsActivePage(string href)
    {
        var uri = new Uri(navigationManager.Uri);
        return uri.AbsolutePath == href;
    }
}