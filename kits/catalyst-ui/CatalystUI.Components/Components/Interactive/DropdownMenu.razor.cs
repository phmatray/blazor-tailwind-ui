using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownMenu
{
    private ElementReference menuElement;
    private bool IsVisible = false;
    [CascadingParameter] private Dropdown? ParentDropdown { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public MenuAnchor Anchor { get; set; } = MenuAnchor.Bottom;

    public enum MenuAnchor
    {
        Top,
        Bottom,
        Left,
        Right
    }

    protected override void OnInitialized()
    {
        if (ParentDropdown != null)
        {
            ParentDropdown.MenuInstance = this;
        }
    }

    internal async Task Show()
    {
        IsVisible = true;
        StateHasChanged();
        await Task.Yield(); // Let the browser render
    }

    internal async Task Hide()
    {
        IsVisible = false;
        StateHasChanged();
        await Task.Yield();
    }

    private string GetMenuClasses()
    {
        var anchorClasses = Anchor switch
        {
            MenuAnchor.Top => "bottom-full mb-2",
            MenuAnchor.Bottom => "top-full mt-2",
            MenuAnchor.Left => "right-full mr-2",
            MenuAnchor.Right => "left-full ml-2",
            _ => "top-full mt-2"
        };

        return CombineClasses(
            // Positioning
            "absolute z-50",
            anchorClasses,
            // Base styles
            "isolate w-max rounded-xl p-1",
            // Invisible border for accessibility
            "outline outline-transparent focus:outline-hidden",
            // Handle scrolling
            "overflow-y-auto max-h-[calc(100vh-theme(spacing.6)-theme(spacing.2))]",
            // Background
            "bg-white/75 backdrop-blur-xl dark:bg-zinc-800/75",
            // Shadows
            "shadow-lg ring-1 ring-zinc-950/10 dark:ring-white/10 dark:ring-inset",
            // Grid support
            "supports-[grid-template-columns:subgrid]:grid supports-[grid-template-columns:subgrid]:grid-cols-[auto_1fr_1.5rem_0.5rem_auto]",
            // Transitions
            "transition duration-100",
            IsVisible ? "opacity-100 translate-y-0" : "opacity-0 -translate-y-1"
        );
    }
}