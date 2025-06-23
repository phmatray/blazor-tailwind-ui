using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class TableHeader
{
    [CascadingParameter] private Table? ParentTable { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetHeaderClasses()
    {
        var classes = new List<string>
        {
            "border-b",
            "border-b-zinc-950/10",
            "px-4",
            "py-2",
            "font-medium",
            "first:pl-[--gutter,--spacing(2)]",
            "last:pr-[--gutter,--spacing(2)]",
            "dark:border-b-white/10"
        };

        if (ParentTable?.Grid == true)
        {
            classes.Add("border-l");
            classes.Add("border-l-zinc-950/5");
            classes.Add("first:border-l-0");
            classes.Add("dark:border-l-white/5");
        }

        if (ParentTable?.Bleed == false)
        {
            classes.Add("sm:first:pl-1");
            classes.Add("sm:last:pr-1");
        }

        return string.Join(" ", classes);
    }
}