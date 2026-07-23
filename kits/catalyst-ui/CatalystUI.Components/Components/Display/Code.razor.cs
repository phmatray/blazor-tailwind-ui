using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Code
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetCodeClasses()
    {
        return
            "catalyst-code rounded-sm border border-zinc-950/10 bg-zinc-950/[0.025] px-0.5 text-sm font-medium text-zinc-950 sm:text-[0.8125rem] dark:border-white/20 dark:bg-white/5 dark:text-white";
    }
}