using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class HyperLink
{
    [Parameter, EditorRequired] public string Href { get; set; } = "#";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public HyperLinkStyle Style { get; set; } = HyperLinkStyle.Default;

    private string GetLinkClasses()
    {
        var classes = new List<string> { "catalyst-link" };

        switch (Style)
        {
            case HyperLinkStyle.Default:
                classes.AddRange(new[]
                {
                    "text-zinc-950",
                    "underline",
                    "decoration-zinc-950/50",
                    "hover:decoration-zinc-950",
                    "dark:text-white",
                    "dark:decoration-white/50",
                    "dark:hover:decoration-white",
                    "catalyst-transition-colors"
                });
                break;
            case HyperLinkStyle.Plain:
                classes.AddRange(new[]
                {
                    "hover:underline",
                    "catalyst-transition-colors"
                });
                break;
            case HyperLinkStyle.Primary:
                classes.AddRange(new[]
                {
                    "text-indigo-600",
                    "hover:text-indigo-500",
                    "dark:text-indigo-400",
                    "dark:hover:text-indigo-300",
                    "font-medium",
                    "catalyst-transition-colors"
                });
                break;
        }

        return string.Join(" ", classes);
    }

    public enum HyperLinkStyle
    {
        Default,
        Plain,
        Primary
    }
}