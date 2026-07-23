namespace DaisyBlazor.Data;

/// <summary>
/// Snapshot of the grid's paging + sort state passed to a server-data loader.
/// Member names mirror MudBlazor's <c>GridState&lt;T&gt;</c>.
/// </summary>
/// <typeparam name="T">Row item type.</typeparam>
public sealed class GridState<T>
{
    /// <summary>Zero-based page index.</summary>
    public int Page { get; set; }

    /// <summary>Number of rows per page.</summary>
    public int PageSize { get; set; }

    /// <summary>Active sort definitions (in priority order).</summary>
    public IReadOnlyList<SortDefinition<T>> SortDefinitions { get; set; } = [];
}

/// <summary>
/// Result returned by a server-data loader: the rows for the requested page and the
/// total number of rows across all pages. Member names mirror MudBlazor's <c>GridData&lt;T&gt;</c>.
/// </summary>
/// <typeparam name="T">Row item type.</typeparam>
public sealed class GridData<T>
{
    /// <summary>Rows for the requested page.</summary>
    public IEnumerable<T> Items { get; set; } = [];

    /// <summary>Total number of rows across all pages.</summary>
    public int TotalItems { get; set; }
}

/// <summary>
/// Describes a single active sort. Mirrors MudBlazor's <c>SortDefinition&lt;T&gt;</c>.
/// </summary>
/// <typeparam name="T">Row item type.</typeparam>
public sealed class SortDefinition<T>
{
    /// <summary>Creates a sort definition.</summary>
    public SortDefinition(string sortBy, bool descending, int index, Func<T, object> sortFunc)
    {
        SortBy = sortBy;
        Descending = descending;
        Index = index;
        SortFunc = sortFunc;
    }

    /// <summary>Identifier of the sorted column (member name / title).</summary>
    public string SortBy { get; set; }

    /// <summary>True when sorting descending.</summary>
    public bool Descending { get; set; }

    /// <summary>Priority index (0 = primary sort).</summary>
    public int Index { get; set; }

    /// <summary>Extracts the sortable value from a row.</summary>
    public Func<T, object> SortFunc { get; set; }
}

/// <summary>Context passed to a <see cref="TemplateColumn{T}"/> cell template.</summary>
/// <typeparam name="T">Row item type.</typeparam>
public sealed class CellContext<T>
{
    /// <summary>The row item.</summary>
    public T Item { get; set; } = default!;
}

/// <summary>Event arguments for a grid row click. Mirrors MudBlazor's <c>DataGridRowClickEventArgs&lt;T&gt;</c>.</summary>
/// <typeparam name="T">Row item type.</typeparam>
public sealed class DataGridRowClickEventArgs<T>
{
    /// <summary>The clicked row item.</summary>
    public T Item { get; set; } = default!;

    /// <summary>Zero-based index of the clicked row within the current page.</summary>
    public int RowIndex { get; set; }
}
