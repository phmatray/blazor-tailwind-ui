namespace DaisyBlazor;

// MudBlazor-compatible enums for collection/data components (P5 group).
// Kept in their own file to avoid churn on the shared Enums.cs.

/// <summary>Timeline label/dot position (mirrors MudBlazor TimelinePosition).</summary>
public enum TimelinePosition
{
    Start,
    End,
    Left,
    Right,
    Top,
    Bottom,
    Alternate
}

/// <summary>Per-item timeline alignment (mirrors MudBlazor TimelineAlign).</summary>
public enum TimelineAlign
{
    Default,
    Start,
    End
}

/// <summary>Selection behavior for a toggle/segmented group (mirrors MudBlazor SelectionMode).</summary>
public enum SelectionMode
{
    SingleSelection,
    MultiSelection,
    ToggleSelection
}
