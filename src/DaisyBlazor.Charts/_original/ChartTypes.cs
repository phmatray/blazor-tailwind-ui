using System.Collections.Generic;
using System.Linq;

namespace Bsca.Blazor.Components;

/// <summary>
/// Chart kind, mirroring the subset of <c>MudBlazor.ChartType</c> used across HorizonHub.
/// </summary>
public enum ChartType
{
    /// <summary>Pie chart.</summary>
    Pie,

    /// <summary>Donut chart.</summary>
    Donut,

    /// <summary>Line chart.</summary>
    Line,

    /// <summary>Vertical bar (column) chart.</summary>
    Bar,

    /// <summary>Stacked vertical bar chart.</summary>
    StackedBar,

    /// <summary>Heat map.</summary>
    HeatMap
}

/// <summary>
/// Holds the numeric values for a single <see cref="ChartSeries{T}"/>.
/// Mirrors the shape consuming pages rely on (<c>new ChartData&lt;double&gt;(list)</c>,
/// <c>new ChartData&lt;double&gt;(scalar)</c>, and a readable <see cref="Values"/> collection).
/// </summary>
/// <typeparam name="T">Numeric value type (typically <see cref="double"/>).</typeparam>
public sealed class ChartData<T>
{
    /// <summary>Creates an empty data set.</summary>
    public ChartData()
    {
        Values = new List<T>();
    }

    /// <summary>Creates a data set from a list of values.</summary>
    public ChartData(IEnumerable<T> values)
    {
        Values = values?.ToList() ?? new List<T>();
    }

    /// <summary>Creates a single-value data set (used for pie/donut slices).</summary>
    public ChartData(T value)
    {
        Values = new List<T> { value };
    }

    /// <summary>The ordered values backing the series.</summary>
    public List<T> Values { get; set; }
}

/// <summary>
/// A named data series, mirroring <c>MudBlazor.ChartSeries</c> but generic over the value type.
/// </summary>
/// <typeparam name="T">Numeric value type (typically <see cref="double"/>).</typeparam>
public sealed class ChartSeries<T>
{
    /// <summary>Series display name (legend / tooltip label).</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Series values.</summary>
    public ChartData<T> Data { get; set; } = new();
}

/// <summary>
/// Internal flattened data point fed to ApexCharts (label + numeric value for one series entry).
/// </summary>
public sealed class ChartPoint
{
    /// <summary>The category/axis label for this point.</summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>The numeric value for this point.</summary>
    public double Value { get; set; }
}
