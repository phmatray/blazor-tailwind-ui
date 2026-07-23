using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class TableRow
{
    [CascadingParameter] private Table? ParentTable { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public string? Target { get; set; }
    [Parameter] public string? Title { get; set; }

    private string GetRowClasses()
    {
        var classes = new List<string>();

        if (!string.IsNullOrWhiteSpace(Href))
        {
            classes.Add("has-[[data-row-link][data-focus]]:outline-2");
            classes.Add("has-[[data-row-link][data-focus]]:-outline-offset-2");
            classes.Add("has-[[data-row-link][data-focus]]:outline-blue-500");
            classes.Add("dark:focus-within:bg-white/[0.025]");
        }

        if (ParentTable?.Striped == true)
        {
            classes.Add("even:bg-zinc-950/[0.025]");
            classes.Add("dark:even:bg-white/[0.025]");
        }

        if (!string.IsNullOrWhiteSpace(Href))
        {
            if (ParentTable?.Striped == true)
            {
                classes.Add("hover:bg-zinc-950/5");
                classes.Add("dark:hover:bg-white/5");
            }
            else
            {
                classes.Add("hover:bg-zinc-950/[0.025]");
                classes.Add("dark:hover:bg-white/[0.025]");
            }
        }

        return string.Join(" ", classes);
    }

    public void Dispose()
    {
        // Cleanup if needed
    }
}