using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Heading
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public int Level { get; set; } = 1;

    private string GetHeadingClasses()
    {
        return "catalyst-heading text-2xl/8 font-semibold text-zinc-950 sm:text-xl/8 dark:text-white";
    }
}