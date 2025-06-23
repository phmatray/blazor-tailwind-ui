using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CatalystUI.Components;

public partial class Textarea
{
    private ElementReference Element;
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public int Rows { get; set; } = 3;
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }
    [Parameter] public bool Invalid { get; set; }
    [Parameter] public bool Resizable { get; set; } = true;

    private async Task OnInput(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        await ValueChanged.InvokeAsync(Value);
    }

    private async Task OnChange(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        await ValueChanged.InvokeAsync(Value);
    }

    private string GetTextareaClasses()
    {
        var classes = new List<string>
        {
            "catalyst-textarea",
            "block",
            "w-full",
            "rounded-lg",
            "border-0",
            "py-1.5",
            "px-3",
            "text-sm/6",
            "text-zinc-950",
            "shadow-sm",
            "ring-1",
            "ring-inset",
            "placeholder:text-zinc-500",
            "catalyst-transition-colors",
            "dark:text-white"
        };

        if (!Resizable)
        {
            classes.Add("resize-none");
        }

        if (Invalid)
        {
            classes.AddRange(new[]
            {
                "ring-red-500",
                "focus:ring-red-500",
                "dark:ring-red-400",
                "dark:focus:ring-red-400"
            });
        }
        else
        {
            classes.AddRange(new[]
            {
                "ring-zinc-300",
                "focus:ring-2",
                "focus:ring-inset",
                "focus:ring-indigo-600",
                "dark:ring-zinc-700",
                "dark:focus:ring-indigo-500"
            });
        }

        if (!ReadOnly)
        {
            classes.AddRange(new[]
            {
                "hover:ring-zinc-400",
                "dark:hover:ring-zinc-600"
            });
        }

        classes.AddRange(new[]
        {
            "dark:bg-white/5",
            "dark:placeholder:text-zinc-400"
        });

        return string.Join(" ", classes);
    }

    public async ValueTask FocusAsync()
    {
        await Element.FocusAsync();
    }
}