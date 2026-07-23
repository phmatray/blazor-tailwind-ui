using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CatalystUI.Components;

public partial class Combobox<TValue>
{
    private ElementReference comboboxElement;
    private ElementReference inputElement;
    private string query = "";
    private bool isOpen = false;
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue?> ValueChanged { get; set; }
    [Parameter] public IEnumerable<TValue> Options { get; set; } = Enumerable.Empty<TValue>();
    [Parameter] public Func<TValue?, string> DisplayValue { get; set; } = (v) => v?.ToString() ?? "";
    [Parameter] public Func<TValue, string, bool>? Filter { get; set; }
    [Parameter] public RenderFragment<TValue> OptionTemplate { get; set; } = default!;
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? AriaLabel { get; set; }
    [Parameter] public bool AutoFocus { get; set; }
    [Parameter] public ComboboxAnchor Anchor { get; set; } = ComboboxAnchor.Bottom;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    public enum ComboboxAnchor
    {
        Top,
        Bottom
    }

    private string Query
    {
        get => Value != null ? DisplayValue(Value) : query;
        set
        {
            query = value;
            if (!string.IsNullOrEmpty(value))
            {
                IsOpen = true;
            }
        }
    }

    private bool IsOpen
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

    private IEnumerable<TValue> FilteredOptions
    {
        get
        {
            if (string.IsNullOrEmpty(query))
                return Options;

            return Options.Where(option =>
            {
                if (Filter != null)
                    return Filter(option, query);

                var displayText = DisplayValue(option);
                return displayText?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false;
            });
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && AutoFocus)
        {
            await inputElement.FocusAsync();
        }
    }

    internal async Task SelectOption(TValue option)
    {
        Value = option;
        query = "";
        IsOpen = false;
        await ValueChanged.InvokeAsync(Value);
        await inputElement.FocusAsync();
    }

    private void HandleFocus()
    {
        if (!string.IsNullOrEmpty(query) || Options.Any())
        {
            IsOpen = true;
        }
    }

    private void HandleBlur()
    {
        // Delay to allow click on option
        Task.Delay(200).ContinueWith(_ =>
        {
            IsOpen = false;
            query = "";
            InvokeAsync(StateHasChanged);
        });
    }

    private void ToggleDropdown()
    {
        IsOpen = !IsOpen;
    }

    private string GetControlClasses()
    {
        return CombineClasses(
            // Basic layout
            "relative block w-full",
            // Background color + shadow applied to inset pseudo element
            "before:absolute before:inset-px before:rounded-[calc(theme(borderRadius.lg)-1px)] before:bg-white before:shadow-sm",
            // Background color is moved to control and shadow is removed in dark mode
            "dark:before:hidden",
            // Focus ring
            "after:pointer-events-none after:absolute after:inset-0 after:rounded-lg after:ring-transparent after:ring-inset sm:focus-within:after:ring-2 sm:focus-within:after:ring-blue-500",
            // Disabled state
            Disabled ? "opacity-50 before:bg-zinc-950/5 before:shadow-none" : ""
        );
    }

    private string GetInputClasses()
    {
        return CombineClasses(
            // Basic layout
            "relative block w-full appearance-none rounded-lg py-[calc(theme(spacing[2.5])-1px)] sm:py-[calc(theme(spacing[1.5])-1px)]",
            // Horizontal padding
            "pr-[calc(theme(spacing.10)-1px)] pl-[calc(theme(spacing[3.5])-1px)] sm:pr-[calc(theme(spacing.9)-1px)] sm:pl-[calc(theme(spacing.3)-1px)]",
            // Typography
            "text-base/6 text-zinc-950 placeholder:text-zinc-500 sm:text-sm/6 dark:text-white",
            // Border
            "border border-zinc-950/10 hover:border-zinc-950/20 dark:border-white/10 dark:hover:border-white/20",
            // Background color
            "bg-transparent dark:bg-white/5",
            // Hide default focus styles
            "focus:outline-none",
            // Disabled state
            Disabled
                ? "border-zinc-950/20 dark:border-white/15 dark:bg-white/[2.5%] hover:border-zinc-950/20 dark:hover:border-white/15"
                : "",
            // System icons
            "dark:[color-scheme:dark]"
        );
    }

    private string GetOptionsClasses()
    {
        var anchorClasses = Anchor == ComboboxAnchor.Top ? "bottom-full mb-2" : "top-full mt-2";

        return CombineClasses(
            // Positioning
            "absolute z-50",
            anchorClasses,
            "left-0 right-0",
            // Base styles
            "min-w-[calc(100%+8px)] scroll-py-1 rounded-xl p-1 select-none",
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