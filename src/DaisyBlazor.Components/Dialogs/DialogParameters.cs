using System.Linq.Expressions;
using System.Reflection;

namespace DaisyBlazor;

/// <summary>
/// MudBlazor-compatible bag of parameter values forwarded to a hosted dialog component.
/// Supports the dictionary indexer (<c>parameters["Name"] = value</c>) and collection
/// initializer (<c>{ "Name", value }</c>) syntaxes.
/// </summary>
public class DialogParameters : Dictionary<string, object?>
{
    /// <summary>Add or replace a parameter value by name.</summary>
    public new void Add(string parameterName, object? value) => this[parameterName] = value;

    /// <summary>Get a stored value cast to <typeparamref name="T"/>, or <c>default</c> when absent.</summary>
    public T? Get<T>(string parameterName) =>
        TryGetValue(parameterName, out object? value) && value is T typed ? typed : default;
}

/// <summary>
/// Strongly-typed <see cref="DialogParameters"/> for a specific dialog component
/// <typeparamref name="T"/>. Enables the <c>{ x => x.Prop, value }</c> initializer used by MudBlazor.
/// </summary>
public class DialogParameters<T> : DialogParameters
{
    /// <summary>Add or replace a parameter value using a property selector expression.</summary>
    public void Add<TProperty>(Expression<Func<T, TProperty>> property, TProperty value)
    {
        this[ResolveName(property)] = value;
    }

    private static string ResolveName<TProperty>(Expression<Func<T, TProperty>> property)
    {
        if (property.Body is MemberExpression member && member.Member is PropertyInfo info)
        {
            return info.Name;
        }

        if (property.Body is UnaryExpression unary &&
            unary.Operand is MemberExpression unaryMember &&
            unaryMember.Member is PropertyInfo unaryInfo)
        {
            return unaryInfo.Name;
        }

        throw new ArgumentException("Expression must select a property.", nameof(property));
    }
}
