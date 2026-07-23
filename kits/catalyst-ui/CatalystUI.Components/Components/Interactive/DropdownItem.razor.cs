using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownItem
{
    [CascadingParameter] private Dropdown? ParentDropdown { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }

    private async Task HandleClick()
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }

        // Close dropdown after clicking an item
        if (ParentDropdown != null && !Disabled)
        {
            await ParentDropdown.CloseMenu();
        }
    }

    private string GetItemClasses()
    {
        return CombineClasses(
            // Base styles
            "group cursor-default rounded-lg px-3.5 py-2.5 focus:outline-hidden sm:px-3 sm:py-1.5",
            // Text styles
            "text-left text-base/6 text-zinc-950 sm:text-sm/6 dark:text-white forced-colors:text-[CanvasText]",
            // Hover/Focus
            "hover:bg-blue-500 hover:text-white focus:bg-blue-500 focus:text-white",
            // Disabled state
            Disabled ? "opacity-50 cursor-not-allowed" : "",
            // Forced colors mode
            "forced-color-adjust-none forced-colors:hover:bg-[Highlight] forced-colors:hover:text-[HighlightText] forced-colors:focus:bg-[Highlight] forced-colors:focus:text-[HighlightText]",
            // Grid layout
            "col-span-full grid grid-cols-[auto_1fr_1.5rem_0.5rem_auto] items-center supports-[grid-template-columns:subgrid]:grid-cols-subgrid",
            // Icons
            "*:data-[slot=icon]:col-start-1 *:data-[slot=icon]:row-start-1 *:data-[slot=icon]:mr-2.5 *:data-[slot=icon]:-ml-0.5 *:data-[slot=icon]:size-5 sm:*:data-[slot=icon]:mr-2 sm:*:data-[slot=icon]:size-4",
            "*:data-[slot=icon]:text-zinc-500 hover:*:data-[slot=icon]:text-white focus:*:data-[slot=icon]:text-white dark:*:data-[slot=icon]:text-zinc-400",
            // Avatar
            "*:data-[slot=avatar]:mr-2.5 *:data-[slot=avatar]:-ml-1 *:data-[slot=avatar]:size-6 sm:*:data-[slot=avatar]:mr-2 sm:*:data-[slot=avatar]:size-5"
        );
    }
}