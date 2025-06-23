using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownButton
{
    [CascadingParameter] private Dropdown? ParentDropdown { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private async Task HandleClick()
    {
        if (ParentDropdown != null)
        {
            await ParentDropdown.ToggleMenu();
        }
    }
}