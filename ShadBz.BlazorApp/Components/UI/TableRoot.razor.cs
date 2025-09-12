using Microsoft.AspNetCore.Components;

namespace ShadBz.BlazorApp.Components.UI;

public enum TableSize
{
    Size1,
    Size2,
    Size3
}

public enum TableVariant
{
    Surface,
    Ghost
}

public enum TableLayout
{
    Auto,
    Fixed
}

public partial class TableRoot : ComponentBase
{
    [Parameter] public TableSize Size { get; set; } = TableSize.Size2;
    [Parameter] public TableVariant Variant { get; set; } = TableVariant.Surface;
    [Parameter] public TableLayout Layout { get; set; } = TableLayout.Auto;
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public string? MaxWidth { get; set; }
    
    internal TableSize GetSize() => Size;
    internal TableVariant GetVariant() => Variant;
}