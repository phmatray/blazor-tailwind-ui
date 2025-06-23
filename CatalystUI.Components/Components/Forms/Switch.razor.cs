using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Switch
{
    private ElementReference Element;
    [Parameter] public bool Value { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? ContainerClass { get; set; }
    [Parameter] public string? LabelClass { get; set; }
    private string LabelClasses => CombineClasses("text-sm/6", "text-zinc-950", "dark:text-white", LabelClass);

    private async Task Toggle()
    {
        if (!Disabled)
        {
            Value = !Value;
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private string GetSwitchClasses()
    {
        var baseClasses = new[]
        {
            "catalyst-switch",
            "relative",
            "inline-flex",
            "h-6",
            "w-11",
            "flex-shrink-0",
            "rounded-full",
            "border-2",
            "border-transparent",
            "catalyst-transition-colors",
            "focus:ring-2",
            "focus:ring-indigo-600",
            "focus:ring-offset-2",
            "dark:focus:ring-indigo-500",
            "dark:focus:ring-offset-zinc-900"
        };

        var stateClasses = Value
            ? new[] { "bg-indigo-600", "dark:bg-indigo-500" }
            : new[] { "bg-zinc-200", "dark:bg-zinc-700" };

        var interactionClasses = Disabled
            ? new[] { "cursor-not-allowed" }
            : new[] { "cursor-pointer" };

        return string.Join(" ", baseClasses.Concat(stateClasses).Concat(interactionClasses));
    }

    private string GetThumbClasses()
    {
        var baseClasses = new[]
        {
            "catalyst-switch-thumb",
            "pointer-events-none",
            "inline-block",
            "h-5",
            "w-5",
            "rounded-full",
            "bg-white",
            "shadow-lg",
            "ring-0",
            "catalyst-transition-transform"
        };

        var translateClass = Value ? "translate-x-5" : "translate-x-0";

        return string.Join(" ", baseClasses.Append(translateClass));
    }

    public async ValueTask FocusAsync()
    {
        await Element.FocusAsync();
    }
}