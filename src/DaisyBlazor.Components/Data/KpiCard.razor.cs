using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Data;

/// <summary>
/// Color-bordered KPI tile: caption + large value + optional subtitle.
/// </summary>
public partial class KpiCard
{
    /// <summary>Caption shown above the value. Already-resolved text (pass <c>@L["…"]</c> at the call site to localize).</summary>
    [Parameter, EditorRequired]
    public string Label { get; set; } = null!;

    /// <summary>Main metric value.</summary>
    [Parameter, EditorRequired]
    public string Value { get; set; } = null!;

    /// <summary>CSS color used for the left border and the value text. Defaults to a light blue.</summary>
    [Parameter]
    public string Color { get; set; } = "#4fc3f7";

    /// <summary>Optional smaller caption beneath the value.</summary>
    [Parameter]
    public string? Subtitle { get; set; }

    /// <summary>Additional CSS classes applied to the tile.</summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>Inline style appended to the tile.</summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>Arbitrary attributes splatted onto the tile.</summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }
}
