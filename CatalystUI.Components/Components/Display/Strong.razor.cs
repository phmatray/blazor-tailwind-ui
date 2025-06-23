using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Strong
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetStrongClasses()
    {
        return "catalyst-strong font-medium text-zinc-950 dark:text-white";
    }
}