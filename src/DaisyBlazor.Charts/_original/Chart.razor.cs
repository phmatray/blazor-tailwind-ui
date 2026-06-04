using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ApexCharts;
using Microsoft.AspNetCore.Components;

namespace Bsca.Blazor.Components;

/// <summary>
/// daisyUI/ApexCharts replacement for <c>MudChart</c>. Renders pie, donut, line, bar,
/// stacked bar and heat-map charts from a list of <see cref="ChartSeries{T}"/> plus
/// category labels. Backed by Blazor-ApexCharts.
/// </summary>
/// <typeparam name="T">Numeric value type carried by the series (typically <see cref="double"/>).</typeparam>
public partial class Chart<T> : ComponentBase
{
    /// <summary>The kind of chart to render.</summary>
    [Parameter]
    public ChartType ChartType { get; set; } = ChartType.Line;

    /// <summary>Named data series. For pie/donut, each series typically holds a single value.</summary>
    [Parameter]
    public List<ChartSeries<T>> ChartSeries { get; set; } = new();

    /// <summary>Category / x-axis labels.</summary>
    [Parameter]
    public string[] ChartLabels { get; set; } = Array.Empty<string>();

    /// <summary>Alias for <see cref="ChartLabels"/> (MudBlazor's x-axis label name).</summary>
    [Parameter]
    public string[]? XAxisLabels { get; set; }

    /// <summary>Raw numeric input (MudBlazor compatibility; used when no <see cref="ChartSeries"/> are supplied).</summary>
    [Parameter]
    public double[]? InputData { get; set; }

    /// <summary>Labels matching <see cref="InputData"/> (MudBlazor compatibility).</summary>
    [Parameter]
    public string[]? InputLabels { get; set; }

    /// <summary>CSS width (e.g. "100%", "320px"). Defaults to full width.</summary>
    [Parameter]
    public string? Width { get; set; } = "100%";

    /// <summary>CSS height (e.g. "260px"). Defaults to "300px".</summary>
    [Parameter]
    public string? Height { get; set; } = "300px";

    /// <summary>Optional ApexCharts options passthrough (advanced customization).</summary>
    [Parameter]
    public ApexChartOptions<ChartPoint>? Options { get; set; }

    /// <summary>Extra CSS classes for the wrapper element.</summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>Inline style for the wrapper element.</summary>
    [Parameter]
    public string? Style { get; set; }

    /// <summary>Unmatched attributes splatted onto the wrapper element.</summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? UserAttributes { get; set; }

    /// <summary>Wrapper class string.</summary>
    private string CssClassValue => CssClass.Merge("w-full", Class);

    /// <summary>Resolved category labels (prefers explicit labels, falls back to input labels).</summary>
    private string[] ResolvedLabels =>
        (XAxisLabels is { Length: > 0 } ? XAxisLabels
            : ChartLabels is { Length: > 0 } ? ChartLabels
            : InputLabels) ?? Array.Empty<string>();

    /// <summary>True for circular charts that use a single flattened series.</summary>
    private bool IsCircular => ChartType is ChartType.Pie or ChartType.Donut;

    /// <summary>Resolved ApexCharts series type for the configured <see cref="ChartType"/>.</summary>
    private SeriesType ResolvedSeriesType => ChartType switch
    {
        ChartType.Pie => SeriesType.Pie,
        ChartType.Donut => SeriesType.Donut,
        ChartType.Bar => SeriesType.Bar,
        ChartType.StackedBar => SeriesType.Bar,
        ChartType.HeatMap => SeriesType.Heatmap,
        _ => SeriesType.Line
    };

    /// <summary>Whether the underlying chart should stack its series.</summary>
    private bool IsStacked => ChartType == ChartType.StackedBar;

    /// <summary>
    /// Resolved options, ensuring stacking is applied for <see cref="ChartType.StackedBar"/>
    /// while still honoring any caller-provided <see cref="Options"/>.
    /// </summary>
    private ApexChartOptions<ChartPoint>? ResolvedOptions
    {
        get
        {
            if (Options is null && !IsStacked)
            {
                return null;
            }

            ApexChartOptions<ChartPoint> options = Options ?? new ApexChartOptions<ChartPoint>();
            if (IsStacked)
            {
                options.Chart ??= new ApexCharts.Chart();
                options.Chart.Stacked = true;
            }

            return options;
        }
    }

    /// <summary>
    /// Flattens <see cref="ChartSeries"/> (or <see cref="InputData"/>) into the per-series
    /// point lists consumed by the markup. For circular charts a single series is produced
    /// with one slice per label.
    /// </summary>
    private IReadOnlyList<RenderSeries> BuildSeries()
    {
        string[] labels = ResolvedLabels;

        if (IsCircular)
        {
            List<ChartPoint> points = new();

            if (ChartSeries is { Count: > 0 })
            {
                for (int i = 0; i < ChartSeries.Count; i++)
                {
                    ChartSeries<T> series = ChartSeries[i];
                    string label = i < labels.Length ? labels[i] : series.Name;
                    double value = series.Data?.Values is { Count: > 0 }
                        ? ToDouble(series.Data.Values[0])
                        : 0d;
                    points.Add(new ChartPoint { Label = label, Value = value });
                }
            }
            else if (InputData is { Length: > 0 })
            {
                for (int i = 0; i < InputData.Length; i++)
                {
                    string label = i < labels.Length ? labels[i] : (i + 1).ToString(CultureInfo.InvariantCulture);
                    points.Add(new ChartPoint { Label = label, Value = InputData[i] });
                }
            }

            return new List<RenderSeries> { new("", points) };
        }

        List<RenderSeries> result = new();

        if (ChartSeries is { Count: > 0 })
        {
            foreach (ChartSeries<T> series in ChartSeries)
            {
                List<ChartPoint> points = new();
                List<T> values = series.Data?.Values ?? new List<T>();
                for (int i = 0; i < values.Count; i++)
                {
                    string label = i < labels.Length ? labels[i] : (i + 1).ToString(CultureInfo.InvariantCulture);
                    points.Add(new ChartPoint { Label = label, Value = ToDouble(values[i]) });
                }

                result.Add(new RenderSeries(series.Name, points));
            }
        }
        else if (InputData is { Length: > 0 })
        {
            List<ChartPoint> points = new();
            for (int i = 0; i < InputData.Length; i++)
            {
                string label = i < labels.Length ? labels[i] : (i + 1).ToString(CultureInfo.InvariantCulture);
                points.Add(new ChartPoint { Label = label, Value = InputData[i] });
            }

            result.Add(new RenderSeries(string.Empty, points));
        }

        return result;
    }

    /// <summary>True when there is no data to plot.</summary>
    private bool HasData =>
        (ChartSeries is { Count: > 0 } && ChartSeries.Any(s => s.Data?.Values is { Count: > 0 }))
        || InputData is { Length: > 0 };

    private static double ToDouble(T value) =>
        value is null ? 0d : Convert.ToDouble(value, CultureInfo.InvariantCulture);

    /// <summary>A single resolved series ready for ApexCharts rendering.</summary>
    private sealed record RenderSeries(string Name, List<ChartPoint> Points);
}
