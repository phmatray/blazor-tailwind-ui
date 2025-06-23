using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CatalystUI.Components;

public partial class Listbox<TValue>
{
    private ElementReference listboxElement;
    private bool isOpen = false;
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public Func<TValue?, string>? DisplayValue { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public bool AutoFocus { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    internal bool IsOpen
    {
        get => isOpen;
        set
        {
            if (isOpen != value)
            {
                isOpen = value;
                StateHasChanged();
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && AutoFocus)
        {
            await listboxElement.FocusAsync();
        }
    }

    internal async Task SelectOption(TValue option)
    {
        Value = option;
        IsOpen = false;
        await ValueChanged.InvokeAsync(Value);
    }

    private void ToggleDropdown()
    {
        if (!Disabled)
        {
            IsOpen = !IsOpen;
        }
    }

    private void HandleFocus()
    {
        // Keep track of focus state if needed
    }

    private void HandleBlur()
    {
        // Delay to allow click on option
        Task.Delay(200).ContinueWith(_ =>
        {
            IsOpen = false;
            InvokeAsync(StateHasChanged);
        });
    }

    private string GetButtonClasses()
    {
        return CombineClasses(
            // Basic layout
            "group relative block w-full",
            // Background color + shadow applied to inset pseudo element
            "before:absolute before:inset-px before:rounded-[calc(theme(borderRadius.lg)-1px)] before:bg-white before:shadow-sm",
            // Background color is moved to control and shadow is removed in dark mode
            "dark:before:hidden",
            // Hide default focus styles
            "focus:outline-none",
            // Focus ring
            "after:pointer-events-none after:absolute after:inset-0 after:rounded-lg after:ring-transparent after:ring-inset focus:after:ring-2 focus:after:ring-blue-500",
            // Disabled state
            Disabled ? "opacity-50 before:bg-zinc-950/5 before:shadow-none" : ""
        );
    }

    private string GetSelectedOptionClasses()
    {
        return CombineClasses(
            // Basic layout
            "relative block w-full appearance-none rounded-lg py-[calc(theme(spacing[2.5])-1px)] sm:py-[calc(theme(spacing[1.5])-1px)]",
            // Set minimum height
            "min-h-11 sm:min-h-9",
            // Horizontal padding
            "pr-[calc(theme(spacing.7)-1px)] pl-[calc(theme(spacing[3.5])-1px)] sm:pl-[calc(theme(spacing.3)-1px)]",
            // Typography
            "text-left text-base/6 text-zinc-950 placeholder:text-zinc-500 sm:text-sm/6 dark:text-white forced-colors:text-[CanvasText]",
            // Border
            "border border-zinc-950/10 group-hover:border-zinc-950/20 dark:border-white/10 dark:group-hover:border-white/20",
            IsOpen ? "border-zinc-950/20 dark:border-white/20" : "",
            // Background color
            "bg-transparent dark:bg-white/5",
            // Disabled state
            Disabled
                ? "border-zinc-950/20 opacity-100 dark:border-white/15 dark:bg-white/[2.5%] hover:border-zinc-950/20 dark:hover:border-white/15"
                : ""
        );
    }

    private string GetOptionsClasses()
    {
        return CombineClasses(
            // Positioning
            "absolute z-50 mt-1",
            "left-0 right-0",
            // Base styles
            "w-max min-w-[calc(100%+1.75rem)] scroll-py-1 rounded-xl p-1 select-none",
            // Handle scrolling
            "overflow-y-auto max-h-64",
            // Popover background
            "bg-white/75 backdrop-blur-xl dark:bg-zinc-800/75",
            // Shadows
            "shadow-lg ring-1 ring-zinc-950/10 dark:ring-white/10 dark:ring-inset",
            // Animation
            "transition-opacity duration-100 ease-in"
        );
    }
}