using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CatalystUI.Components;

public partial class Dropdown
{
    private ElementReference dropdownElement;
    private IJSObjectReference? clickOutsideHandler;
    internal bool IsOpen { get; set; }
    internal DropdownMenu? MenuInstance { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    internal async Task ToggleMenu()
    {
        IsOpen = !IsOpen;

        if (IsOpen && MenuInstance != null)
        {
            await MenuInstance.Show();

            // Set up click outside handler
            var dotNetRef = DotNetObjectReference.Create(this);
            clickOutsideHandler = await JSRuntime.InvokeAsync<IJSObjectReference>(
                "CatalystUI.addClickOutsideHandler",
                dropdownElement,
                dotNetRef,
                nameof(CloseMenu));
        }
        else
        {
            await CloseMenu();
        }
    }

    [JSInvokable]
    public async Task CloseMenu()
    {
        IsOpen = false;
        if (MenuInstance != null)
        {
            await MenuInstance.Hide();
        }

        // Dispose click outside handler
        if (clickOutsideHandler != null)
        {
            await clickOutsideHandler.InvokeVoidAsync("dispose");
            clickOutsideHandler = null;
        }

        StateHasChanged();
    }

    public void Dispose()
    {
        clickOutsideHandler?.InvokeVoidAsync("dispose");
    }
}