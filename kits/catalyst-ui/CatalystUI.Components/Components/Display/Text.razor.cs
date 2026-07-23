using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Text
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetTextClasses()
    {
        return "catalyst-text text-base/6 text-zinc-500 sm:text-sm/6 dark:text-zinc-400";
    }
}