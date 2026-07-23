using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Field
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetFieldClasses()
    {
        return CombineClasses(
            "[&>[data-slot=label]+[data-slot=control]]:mt-3",
            "[&>[data-slot=label]+[data-slot=description]]:mt-1",
            "[&>[data-slot=description]+[data-slot=control]]:mt-3",
            "[&>[data-slot=control]+[data-slot=description]]:mt-3",
            "[&>[data-slot=control]+[data-slot=error]]:mt-3",
            "*:data-[slot=label]:font-medium"
        );
    }
}