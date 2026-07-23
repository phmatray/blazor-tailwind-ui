namespace DaisyBlazor;

/// <summary>
/// Mirrors MudBlazor's TableRowClickEventArgs&lt;T&gt; so existing
/// <c>OnRowClick="@(args =&gt; ... args.Item ...)"</c> handlers keep compiling.
/// </summary>
public sealed class TableRowClickEventArgs<T>
{
    /// <summary>The clicked row's data item (may be null).</summary>
    public T? Item { get; init; }

    /// <summary>Zero-based index of the clicked row in the current page.</summary>
    public int RowIndex { get; init; }
}
