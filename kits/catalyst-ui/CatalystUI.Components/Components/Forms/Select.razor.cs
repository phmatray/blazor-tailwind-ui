using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Select<TValue>
{
    private ElementReference Element;
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? ContainerClass { get; set; }
    [Parameter] public bool Invalid { get; set; }

    private string? CurrentValueAsString
    {
        get => Value?.ToString();
        set
        {
            if (TryParseValueFromString(value, out var result))
            {
                Value = result;
                _ = ValueChanged.InvokeAsync(Value);
            }
        }
    }

    private void OnChange(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
    }

    private bool TryParseValueFromString(string? value, out TValue? result)
    {
        try
        {
            if (typeof(TValue) == typeof(string))
            {
                result = (TValue?)(object?)value;
                return true;
            }
            else if (typeof(TValue) == typeof(int) || typeof(TValue) == typeof(int?))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result = default;
                    return true;
                }

                if (int.TryParse(value, out var intValue))
                {
                    result = (TValue)(object)intValue;
                    return true;
                }
            }
            else if (typeof(TValue).IsEnum)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result = default;
                    return true;
                }

                if (Enum.TryParse(typeof(TValue), value, out var enumValue))
                {
                    result = (TValue)enumValue;
                    return true;
                }
            }
        }
        catch
        {
            // Parsing failed
        }

        result = default;
        return false;
    }

    private string GetSelectClasses()
    {
        var classes = new List<string>
        {
            "catalyst-select",
            "block",
            "w-full",
            "appearance-none",
            "rounded-lg",
            "border-0",
            "py-1.5",
            "pl-3",
            "pr-10",
            "text-sm/6",
            "text-zinc-950",
            "shadow-sm",
            "ring-1",
            "ring-inset",
            "catalyst-transition-colors",
            "dark:text-white"
        };

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

        classes.AddRange(new[]
        {
            "hover:ring-zinc-400",
            "dark:hover:ring-zinc-600",
            "dark:bg-white/5"
        });

        return string.Join(" ", classes);
    }

    public async ValueTask FocusAsync()
    {
        await Element.FocusAsync();
    }
}