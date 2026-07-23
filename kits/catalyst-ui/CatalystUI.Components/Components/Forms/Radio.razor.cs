using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Radio<TValue>
{
    private ElementReference Element;
    [Parameter, EditorRequired] public string? Name { get; set; }
    [Parameter, EditorRequired] public TValue? OptionValue { get; set; }
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? ContainerClass { get; set; }
    [Parameter] public string? LabelClass { get; set; }
    private bool IsChecked => EqualityComparer<TValue>.Default.Equals(Value, OptionValue);
    private string LabelClasses => CombineClasses("text-sm/6", "text-zinc-950", "dark:text-white", LabelClass);

    private async Task OnChange(ChangeEventArgs e)
    {
        if (!Disabled)
        {
            await ValueChanged.InvokeAsync(OptionValue);
        }
    }

    private string GetRadioClasses()
    {
        return string.Join(" ", new[]
        {
            "catalyst-radio",
            "h-4",
            "w-4",
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