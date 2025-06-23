using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Table
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Bleed { get; set; }
    [Parameter] public bool Dense { get; set; }
    [Parameter] public bool Grid { get; set; }
    [Parameter] public bool Striped { get; set; }

    private string GetInnerContainerClasses()
    {
        var classes = new List<string>
        {
            "inline-block",
            "min-w-full",
            "align-middle"
        };

        if (!Bleed)
        {
            classes.Add("sm:px-[--gutter]");
        }

        return string.Join(" ", classes);
    }

    public void Dispose()
    {
        // Cleanup if needed
    }
}