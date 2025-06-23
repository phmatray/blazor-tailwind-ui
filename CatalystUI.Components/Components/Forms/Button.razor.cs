using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CatalystUI.Components;

public partial class Button
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
    [Parameter] public string Type { get; set; } = "button";
    [Parameter] public string? Href { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Solid;
    [Parameter] public ButtonColor Color { get; set; } = ButtonColor.Dark;
    private bool IsLink => !string.IsNullOrWhiteSpace(Href);

    private async Task HandleClick(MouseEventArgs args)
    {
        if (!Disabled && OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }

    private string GetButtonClasses()
    {
        var classes = new List<string>
        {
            // Base styles
            "catalyst-btn",
            "relative",
            "isolate",
            "inline-flex",
            "items-baseline",
            "justify-center",
            "gap-x-2",
            "rounded-lg",
            "border",
            "text-base/6",
            "font-semibold",
            // Sizing
            "px-[calc(var(--spacing-3-5)-1px)]",
            "py-[calc(var(--spacing-2-5)-1px)]",
            "sm:px-[calc(var(--spacing-3)-1px)]",
            "sm:py-[calc(var(--spacing-1-5)-1px)]",
            "sm:text-sm/6",
            // Transitions
            "catalyst-transition-colors",
            "catalyst-transition-shadow",
            // Focus styles
            "catalyst-focus"
        };

        // Add variant-specific classes
        switch (Variant)
        {
            case ButtonVariant.Solid:
                classes.AddRange(GetSolidClasses());
                break;
            case ButtonVariant.Outline:
                classes.AddRange(GetOutlineClasses());
                break;
            case ButtonVariant.Plain:
                classes.AddRange(GetPlainClasses());
                break;
        }

        // Add color-specific classes for solid variant
        if (Variant == ButtonVariant.Solid)
        {
            classes.AddRange(GetColorClasses());
        }

        return string.Join(" ", classes);
    }

    private IEnumerable<string> GetSolidClasses()
    {
        return new[]
        {
            "border-transparent",
            "shadow-sm",
            "hover:shadow-md",
            "active:shadow-sm"
        };
    }

    private IEnumerable<string> GetOutlineClasses()
    {
        return new[]
        {
            "border-zinc-950/10",
            "text-zinc-950",
            "hover:bg-zinc-950/[0.025]",
            "active:bg-zinc-950/[0.025]",
            "dark:border-white/15",
            "dark:text-white",
            "dark:hover:bg-white/5",
            "dark:active:bg-white/5"
        };
    }

    private IEnumerable<string> GetPlainClasses()
    {
        return new[]
        {
            "border-transparent",
            "text-zinc-950",
            "hover:bg-zinc-950/5",
            "active:bg-zinc-950/5",
            "dark:text-white",
            "dark:hover:bg-white/10",
            "dark:active:bg-white/10"
        };
    }

    private IEnumerable<string> GetColorClasses()
    {
        return Color switch
        {
            ButtonColor.Dark => new[]
            {
                "text-white",
                "bg-zinc-900",
                "hover:bg-zinc-800",
                "dark:bg-zinc-800",
                "dark:hover:bg-zinc-700"
            },
            ButtonColor.DarkZinc => new[]
            {
                "text-white",
                "bg-zinc-900",
                "hover:bg-zinc-800",
                "dark:bg-zinc-600",
                "dark:hover:bg-zinc-500"
            },
            ButtonColor.Light => new[]
            {
                "text-zinc-950",
                "bg-white",
                "hover:bg-zinc-50",
                "dark:text-white",
                "dark:bg-zinc-800",
                "dark:hover:bg-zinc-700"
            },
            ButtonColor.DarkWhite => new[]
            {
                "text-white",
                "bg-zinc-900",
                "hover:bg-zinc-800",
                "dark:text-zinc-950",
                "dark:bg-white",
                "dark:hover:bg-zinc-100"
            },
            ButtonColor.White => new[]
            {
                "text-zinc-950",
                "bg-white",
                "hover:bg-zinc-50",
                "border-zinc-950/10"
            },
            ButtonColor.Zinc => new[]
            {
                "text-white",
                "bg-zinc-600",
                "hover:bg-zinc-500"
            },
            ButtonColor.Indigo => new[]
            {
                "text-white",
                "bg-indigo-500",
                "hover:bg-indigo-400"
            },
            ButtonColor.Cyan => new[]
            {
                "text-cyan-950",
                "bg-cyan-300",
                "hover:bg-cyan-200"
            },
            ButtonColor.Red => new[]
            {
                "text-white",
                "bg-red-600",
                "hover:bg-red-500"
            },
            _ => Array.Empty<string>()
        };
    }

    public enum ButtonVariant
    {
        Solid,
        Outline,
        Plain
    }

    public enum ButtonColor
    {
        Dark,
        DarkZinc,
        Light,
        DarkWhite,
        White,
        Zinc,
        Indigo,
        Cyan,
        Red
    }
}