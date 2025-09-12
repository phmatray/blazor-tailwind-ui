using Microsoft.AspNetCore.Components;

namespace ShadBz.BlazorApp.Components.UI;

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