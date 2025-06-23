using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Checkbox
{
    private ElementReference Element;
    [Parameter] public bool Value { get; set; }
    [Parameter] public EventCallback<bool> ValueChanged { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? ContainerClass { get; set; }
    [Parameter] public string? LabelClass { get; set; }
    private string LabelClasses => CombineClasses("text-sm/6", "text-zinc-950", "dark:text-white", LabelClass);

    private async Task OnChange(ChangeEventArgs e)
    {
        if (!Disabled)
        {
            var newValue = e.Value is bool boolValue ? boolValue : false;
            Value = newValue;
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private string GetCheckboxClasses()
    {
        return string.Join(" ", new[]
        {
            "catalyst-checkbox",
            "h-4",
            "w-4",
            "rounded",
            "border",
            "border-zinc-300",
            "text-indigo-600",
            "shadow-sm",
            "catalyst-transition-colors",
            "focus:ring-2",
            "focus:ring-indigo-600",
            "focus:ring-offset-2",
            "dark:border-zinc-700",
            "dark:text-indigo-500",
            "dark:focus:ring-indigo-500",
            "dark:focus:ring-offset-zinc-900",
            Disabled ? "cursor-not-allowed" : "cursor-pointer hover:border-zinc-400 dark:hover:border-zinc-600"
        });
    }

    public async ValueTask FocusAsync()
    {
        await Element.FocusAsync();
    }
}