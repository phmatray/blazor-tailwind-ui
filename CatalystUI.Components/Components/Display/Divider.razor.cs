using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Divider
{
    [Parameter] public bool Soft { get; set; }

    private string GetDividerClasses()
    {
        var classes = new List<string>
        {
            "catalyst-divider",
            "w-full",
            "border-t"
        };

        if (Soft)
        {
            classes.Add("border-zinc-950/5");
            classes.Add("dark:border-white/5");
        }
        else
        {
            classes.Add("border-zinc-950/10");
            classes.Add("dark:border-white/10");
        }

        return string.Join(" ", classes);
    }
}