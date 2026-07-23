using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Data;

/// <summary>
/// A column whose cell content is supplied by an arbitrary template. Mirrors MudBlazor's
/// <c>TemplateColumn&lt;T&gt;</c>. Not sortable by default; supply <see cref="SortBy"/> to enable sorting.
/// </summary>
/// <typeparam name="T">Row item type.</typeparam>
public sealed class TemplateColumn<T> : Column<T>
{
    /// <summary>Cell template receiving a <see cref="CellContext{T}"/> (access the row via <c>context.Item</c>).</summary>
    [Parameter]
    public RenderFragment<CellContext<T>>? CellTemplate { get; set; }

    /// <summary>Alias for <see cref="CellTemplate"/> when content is provided as the column's body.</summary>
    [Parameter]
    public RenderFragment<CellContext<T>>? ChildContent { get; set; }

    /// <summary>Optional sort selector. When set (and <see cref="Column{T}.Sortable"/>), the column becomes sortable.</summary>
    [Parameter]
    public Func<T, object>? SortBy { get; set; }

    public override Func<T, object?>? GetSortFunc()
    {
        if (SortBy is not null)
        {
            return item => SortBy(item);
        }

        return null;
    }

    public override RenderFragment RenderCell(T item) => builder =>
    {
        RenderFragment<CellContext<T>>? template = CellTemplate ?? ChildContent;
        if (template is not null)
        {
            CellContext<T> context = new() { Item = item };
            builder.AddContent(0, template(context));
        }
    };
}
