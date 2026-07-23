using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DescriptionDetails
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetDetailsClasses()
    {
        return
            "catalyst-description-details pt-1 pb-3 text-zinc-950 sm:border-t sm:border-zinc-950/5 sm:py-3 sm:[&:nth-child(2)]:border-none dark:text-white dark:sm:border-white/5";
    }
}