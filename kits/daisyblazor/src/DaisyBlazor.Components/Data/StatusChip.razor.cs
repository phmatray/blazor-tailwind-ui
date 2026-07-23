using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Data;

/// <summary>
/// A filled <see cref="Chip{T}"/> variant for status indicators (e.g. flight status, report state).
/// </summary>
public partial class StatusChip
{
    /// <summary>Text displayed inside the chip.</summary>
    [Parameter, EditorRequired]
    public string Label { get; set; } = null!;

    /// <summary>Color applied to the chip.</summary>
    [Parameter, EditorRequired]
    public Color Color { get; set; }

    /// <summary>Chip size; defaults to <see cref="DaisyBlazor.Size.Small"/>.</summary>
    [Parameter]
    public Size Size { get; set; } = Size.Small;

    /// <summary>Optional leading icon (use values from <see cref="Icons"/>).</summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>Additional CSS classes applied to the chip.</summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>Inline style applied to the chip.</summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>Arbitrary attributes splatted onto the chip.</summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }
}
