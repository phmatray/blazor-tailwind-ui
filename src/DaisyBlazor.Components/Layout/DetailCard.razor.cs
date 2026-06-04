using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Layout;

/// <summary>
/// Paper-wrapped detail card with an icon+title header and a dense, hoverable
/// daisyUI table body. Pair with <see cref="DetailRow"/>.
/// </summary>
public partial class DetailCard
{
    /// <summary>Card title shown in the header row.</summary>
    [Parameter, EditorRequired]
    public string Title { get; set; } = null!;

    /// <summary>Optional icon shown to the left of <see cref="Title"/>.</summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>Body content — typically a sequence of <see cref="DetailRow"/> elements.</summary>
    [Parameter, EditorRequired]
    public RenderFragment ChildContent { get; set; } = null!;

    /// <summary>Paper elevation. Defaults to 1.</summary>
    [Parameter]
    public int Elevation { get; set; } = 1;

    /// <summary>When true, applies dense styling to the inner table. Defaults to true.</summary>
    [Parameter]
    public bool Dense { get; set; } = true;

    /// <summary>Optional CSS class forwarded to the root paper.</summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>Optional inline style forwarded to the root paper.</summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>Unmatched attributes splatted onto the root paper.</summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }
}
