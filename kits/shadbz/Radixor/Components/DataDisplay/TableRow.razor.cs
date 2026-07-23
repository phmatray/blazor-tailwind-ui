using Microsoft.AspNetCore.Components;

namespace Radixor.Components.DataDisplay;

public enum TableRowAlign
{
    Start,
    Center,
    End,
    Baseline
}

public partial class TableRow : ComponentBase
{
    [CascadingParameter] private TableRoot? TableRoot { get; set; }
}