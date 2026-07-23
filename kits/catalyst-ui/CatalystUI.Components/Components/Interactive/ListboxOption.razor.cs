using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class ListboxOption<TValue>
{
    [CascadingParameter] private Listbox<TValue>? ParentListbox { get; set; }
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public new bool Disabled { get; set; }
    private bool isHovered = false;
    private bool IsSelected => ParentListbox?.Value?.Equals(Value) ?? false;

    private bool IsSelectedOption =>
        ParentListbox != null && ParentListbox.Value?.Equals(Value) == true && !ParentListbox.IsOpen;

    private string sharedClasses => CombineClasses(
        // Base
        "flex min-w-0 items-center",
        // Icons
        "*:data-[slot=icon]:size-5 *:data-[slot=icon]:shrink-0 sm:*:data-[slot=icon]:size-4",
        "*:data-[slot=icon]:text-zinc-500 dark:*:data-[slot=icon]:text-zinc-400",
        isHovered ? "*:data-[slot=icon]:text-white" : "",
        // Avatars
        "*:data-[slot=avatar]:-mx-0.5 *:data-[slot=avatar]:size-6 sm:*:data-[slot=avatar]:size-5"
    );

    private async Task HandleClick()
    {
        if (!Disabled && ParentListbox != null)
        {
            await ParentListbox.SelectOption(Value);
        }
    }

    private void HandleMouseEnter()
    {
        isHovered = true;
    }

    private void HandleMouseLeave()
    {
        isHovered = false;
    }

    private string GetOptionClasses()
    {
        return CombineClasses(
            // Basic layout
            "group/option grid cursor-default grid-cols-[theme(spacing.5)_1fr] items-baseline gap-x-2 rounded-lg py-2.5 pr-3.5 pl-2 sm:grid-cols-[theme(spacing.4)_1fr] sm:py-1.5 sm:pr-3 sm:pl-1.5",
            // Typography
            "text-base/6 text-zinc-950 sm:text-sm/6 dark:text-white forced-colors:text-[CanvasText]",
            // Focus/Hover
            isHovered ? "bg-blue-500 text-white" : "",
            // Selected state
            IsSelected ? "group-data-selected/option" : "",
            // Disabled
            Disabled ? "opacity-50 cursor-not-allowed" : ""
        );
    }
}