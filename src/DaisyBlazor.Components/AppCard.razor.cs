using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DaisyBlazor;

/// <summary>
/// Compact gradient-icon card used as an app/shortcut tile on the home page.
/// </summary>
public partial class AppCard(NavigationManager navigation, IJSRuntime js)
{
    /// <summary>Displayed name beneath the icon.</summary>
    [Parameter]
    public string Name { get; set; } = string.Empty;

    /// <summary>Icon shown inside the gradient tile.</summary>
    [Parameter]
    public string Icon { get; set; } = Icons.Material.Filled.Apps;

    /// <summary>Navigation target on click. Ignored when <see cref="OnCardClick"/> is set.</summary>
    [Parameter]
    public string Route { get; set; } = "/";

    /// <summary>Starting color of the icon-tile gradient.</summary>
    [Parameter]
    public string GradientStart { get; set; } = "#667eea";

    /// <summary>Ending color of the icon-tile gradient.</summary>
    [Parameter]
    public string GradientEnd { get; set; } = "#764ba2";

    /// <summary>Optional badge content rendered in the corner.</summary>
    [Parameter]
    public string? Badge { get; set; }

    /// <summary>Optional explicit click handler. Overrides <see cref="Route"/> navigation.</summary>
    [Parameter]
    public EventCallback OnCardClick { get; set; }

    /// <summary>When true, navigation opens <see cref="Route"/> in a new browser tab.</summary>
    [Parameter]
    public bool IsExternal { get; set; }

    private bool IsHovered { get; set; }

    private async Task OnClick()
    {
        if (OnCardClick.HasDelegate)
        {
            await OnCardClick.InvokeAsync();
        }
        else if (!string.IsNullOrEmpty(Route))
        {
            if (IsExternal)
            {
                await js.InvokeVoidAsync("window.open", Route, "_blank");
            }
            else
            {
                navigation.NavigateTo(Route);
            }
        }
    }
}
