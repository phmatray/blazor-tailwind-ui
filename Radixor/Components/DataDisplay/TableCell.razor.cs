using Microsoft.AspNetCore.Components;

namespace Radixor.Components.DataDisplay;

public enum TableCellAlign
{
    Left,
    Center,
    Right
}

public partial class TableCell : ComponentBase
{
    [CascadingParameter] private TableRoot? TableRoot { get; set; }
}