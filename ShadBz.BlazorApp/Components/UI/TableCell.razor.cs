using Microsoft.AspNetCore.Components;

namespace ShadBz.BlazorApp.Components.UI;

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