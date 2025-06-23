using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CatalystUI.Components;

public partial class SidebarItem
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Current { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public bool TouchTarget { get; set; } = true;

    private async Task HandleClick(MouseEventArgs e)
    {
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(e);
        }
    }

    private string GetItemClasses()
    {
        return CombineClasses(
            // Base
            "flex w-full items-center gap-3 rounded-lg px-2 py-2.5 text-left text-base/6 font-medium text-zinc-950 sm:py-2 sm:text-sm/5",
            // Leading icon/icon-only
            "*:data-[slot=icon]:size-6 *:data-[slot=icon]:shrink-0 *:data-[slot=icon]:fill-zinc-500 sm:*:data-[slot=icon]:size-5",
            // Trailing icon (down chevron or similar)
            "*:last:data-[slot=icon]:ml-auto *:last:data-[slot=icon]:size-5 sm:*:last:data-[slot=icon]:size-4",
            // Avatar
            "*:data-[slot=avatar]:-m-0.5 *:data-[slot=avatar]:size-7 sm:*:data-[slot=avatar]:size-6",
            // Hover
            "hover:bg-zinc-950/5 hover:*:data-[slot=icon]:fill-zinc-950",
            // Active
            "active:bg-zinc-950/5 active:*:data-[slot=icon]:fill-zinc-950",
            // Current
            Current ? "*:data-[slot=icon]:fill-zinc-950" : "",
            // Dark mode
            "dark:text-white dark:*:data-[slot=icon]:fill-zinc-400",
            "dark:hover:bg-white/5 dark:hover:*:data-[slot=icon]:fill-white",
            "dark:active:bg-white/5 dark:active:*:data-[slot=icon]:fill-white",
            Current ? "dark:*:data-[slot=icon]:fill-white" : ""
        );
    }
}