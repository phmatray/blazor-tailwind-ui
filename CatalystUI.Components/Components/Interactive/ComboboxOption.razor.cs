using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class ComboboxOption<TValue>
{
    [CascadingParameter] private Combobox<TValue>? ParentCombobox { get; set; }
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public new bool Disabled { get; set; }
    private bool isHovered = false;
    private bool IsSelected => ParentCombobox?.Value?.Equals(Value) ?? false;

    private async Task HandleClick()
    {
        if (!Disabled && ParentCombobox != null)
        {
            await ParentCombobox.SelectOption(Value);
        }
    }

    private void HandleMouseEnter()
    {
        isHovered = true;
    }

    private string GetOptionClasses()
    {
        return CombineClasses(
            // Basic layout
            "group/option grid w-full cursor-default grid-cols-[1fr_theme(spacing.5)] items-baseline gap-x-2 rounded-lg py-2.5 pr-2 pl-3.5 sm:grid-cols-[1fr_theme(spacing.4)] sm:py-1.5 sm:pr-2 sm:pl-3",
            // Typography
            "text-base/6 text-zinc-950 sm:text-sm/6 dark:text-white forced-colors:text-[CanvasText]",
            // Hover/Focus
            isHovered || IsSelected ? "bg-blue-500 text-white" : "",
            // Disabled
            Disabled ? "opacity-50 cursor-not-allowed" : "",
            // Transitions
            "transition-colors"
        );
    }

    private string GetContentClasses()
    {
        return CombineClasses(
            // Base
            "flex min-w-0 items-center",
            // Icons
            "*:data-[slot=icon]:size-5 *:data-[slot=icon]:shrink-0 sm:*:data-[slot=icon]:size-4",
            "*:data-[slot=icon]:text-zinc-500 dark:*:data-[slot=icon]:text-zinc-400",
            isHovered || IsSelected ? "*:data-[slot=icon]:text-white" : "",
            // Avatars
            "*:data-[slot=avatar]:-mx-0.5 *:data-[slot=avatar]:size-6 sm:*:data-[slot=avatar]:size-5"
        );
    }
}