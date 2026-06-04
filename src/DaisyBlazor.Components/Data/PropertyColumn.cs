using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Data;

/// <summary>
/// A column bound to a member of the row type via an expression. Mirrors MudBlazor's
/// <c>PropertyColumn&lt;T, TProperty&gt;</c>. Renders the (optionally formatted) member value.
/// </summary>
/// <typeparam name="T">Row item type.</typeparam>
/// <typeparam name="TProperty">Member type.</typeparam>
public sealed class PropertyColumn<T, TProperty> : Column<T>
{
    private Func<T, TProperty>? _compiled;
    private string? _memberName;

    /// <summary>Expression selecting the member to display/sort.</summary>
    [Parameter, EditorRequired]
    public Expression<Func<T, TProperty>> Property { get; set; } = default!;

    /// <summary>Optional explicit sort selector (overrides <see cref="Property"/> for sorting).</summary>
    [Parameter]
    public Func<T, object>? SortBy { get; set; }

    /// <summary>Standard/custom format string applied to the value (e.g. "dd/MM/yyyy").</summary>
    [Parameter]
    public string? Format { get; set; }

    public PropertyColumn()
    {
        // PropertyColumns are sortable by default (matches MudBlazor).
        Sortable = true;
    }

    /// <summary>Sort key: the member name resolved from <see cref="Property"/>.</summary>
    public override string Identifier => Title ?? _memberName ?? string.Empty;

    protected override void OnParametersSet()
    {
        _compiled = Property.Compile();
        _memberName = ResolveMemberName(Property);
    }

    public override Func<T, object?>? GetSortFunc()
    {
        if (SortBy is not null)
        {
            return item => SortBy(item);
        }

        if (_compiled is not null)
        {
            return item => _compiled(item);
        }

        return null;
    }

    public override RenderFragment RenderCell(T item) => builder =>
    {
        builder.AddContent(0, FormatValue(item));
    };

    private string? FormatValue(T item)
    {
        if (_compiled is null)
        {
            return null;
        }

        TProperty value = _compiled(item);
        if (value is null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(Format) && value is IFormattable formattable)
        {
            return formattable.ToString(Format, System.Globalization.CultureInfo.CurrentCulture);
        }

        return value.ToString();
    }

    private static string? ResolveMemberName(Expression<Func<T, TProperty>> expression)
    {
        Expression body = expression.Body;

        if (body is UnaryExpression unary)
        {
            body = unary.Operand;
        }

        if (body is MemberExpression member)
        {
            return member.Member.Name;
        }

        return null;
    }
}
