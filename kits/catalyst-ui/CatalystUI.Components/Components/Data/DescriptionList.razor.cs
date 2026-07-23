using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DescriptionList
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetDescriptionListClasses()
    {
        return
            "catalyst-description-list grid grid-cols-1 text-base/6 sm:grid-cols-[min(50%,theme(spacing.80))_auto] sm:text-sm/6";
    }
}