using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Subheading
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public int Level { get; set; } = 2;

    private string GetSubheadingClasses()
    {
        return "catalyst-subheading text-base/7 font-semibold text-zinc-950 sm:text-sm/6 dark:text-white";
    }
}