using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace DaisyBlazor.Data;

/// <summary>
/// Abstract base for a <see cref="DataGrid{T}"/> column. Concrete columns
/// (<see cref="PropertyColumn{T, TProperty}"/>, <see cref="TemplateColumn{T}"/>)
/// register themselves with the owning grid on first render.
/// </summary>
/// <typeparam name="T">Row item type.</typeparam>
public abstract class Column<T> : ComponentBase
{
    private bool _registered;

    /// <summary>Owning grid, supplied via cascading value.</summary>
    [CascadingParameter]
    public DataGrid<T>? Owner { get; set; }

    /// <summary>Column header text.</summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>Whether the column is sortable.</summary>
    [Parameter]
    public bool Sortable { get; set; }

    /// <summary>Optional CSS class applied to every body cell in this column.</summary>
    [Parameter]
    public string? CellClass { get; set; }

    /// <summary>Optional CSS class applied to the header cell.</summary>
    [Parameter]
    public string? HeaderClass { get; set; }

    /// <summary>Stable identifier used as the sort key. Defaults to <see cref="Title"/>.</summary>
    public virtual string Identifier => Title ?? string.Empty;

    /// <summary>Whether this column can actually be sorted (sortable AND a sort func exists).</summary>
    public virtual bool CanSort => Sortable && GetSortFunc() is not null;

    /// <summary>Returns the value used to sort rows by this column, or null when not sortable.</summary>
    public abstract Func<T, object?>? GetSortFunc();

    /// <summary>Renders the cell content for the given row.</summary>
    public abstract RenderFragment RenderCell(T item);

    protected override void OnInitialized()
    {
        if (!_registered && Owner is not null)
        {
            Owner.AddColumn(this);
            _registered = true;
        }
    }

    // Columns are configuration nodes; they never render visible markup themselves.
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
    }
}
