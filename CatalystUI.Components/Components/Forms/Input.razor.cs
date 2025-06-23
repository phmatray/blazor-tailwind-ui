using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CatalystUI.Components;

public partial class Input<TValue>
{
    private ElementReference Element;
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string InputType { get; set; } = "text";
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public string? AutoComplete { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnFocus { get; set; }
    [Parameter] public EventCallback<FocusEventArgs> OnBlur { get; set; }
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

    private void OnInput(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
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
            else if (typeof(TValue) == typeof(decimal) || typeof(TValue) == typeof(decimal?))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result = default;
                    return true;
                }

                if (decimal.TryParse(value, out var decimalValue))
                {
                    result = (TValue)(object)decimalValue;
                    return true;
                }
            }
            else if (typeof(TValue) == typeof(double) || typeof(TValue) == typeof(double?))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result = default;
                    return true;
                }

                if (double.TryParse(value, out var doubleValue))
                {
                    result = (TValue)(object)doubleValue;
                    return true;
                }
            }
            else if (typeof(TValue) == typeof(DateTime) || typeof(TValue) == typeof(DateTime?))
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    result = default;
                    return true;
                }

                if (DateTime.TryParse(value, out var dateValue))
                {
                    result = (TValue)(object)dateValue;
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

    private string GetInputClasses()
    {
        var classes = new List<string>
        {
            "catalyst-input",
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