using DaisyBlazor;
using DaisyBlazor.Charts;
using Microsoft.AspNetCore.Components;

namespace DaisyBlazor.Components.Tests;

/// <summary>
/// Smoke tests asserting that DaisyBlazor components emit the expected daisyUI classes.
/// Doubles as a guard that the component APIs render without throwing.
/// </summary>
public class ComponentRenderTests : BunitContext
{
    [Fact]
    public void Button_renders_btn_with_color_and_content()
    {
        IRenderedComponent<Button> cut = Render<Button>(ps => ps
            .Add(p => p.Color, Color.Primary)
            .AddChildContent("Save"));

        cut.Markup.ShouldContain("btn");
        cut.Markup.ShouldContain("btn-primary");
        cut.Markup.ShouldContain("Save");
    }

    [Fact]
    public void Button_with_href_renders_anchor()
    {
        IRenderedComponent<Button> cut = Render<Button>(ps => ps
            .Add(p => p.Href, "/go")
            .AddChildContent("Link"));

        cut.Find("a").GetAttribute("href").ShouldBe("/go");
    }

    [Theory]
    [InlineData(Severity.Info, "alert-info")]
    [InlineData(Severity.Success, "alert-success")]
    [InlineData(Severity.Warning, "alert-warning")]
    [InlineData(Severity.Error, "alert-error")]
    public void Alert_renders_severity_class(Severity severity, string expected)
    {
        IRenderedComponent<Alert> cut = Render<Alert>(ps => ps
            .Add(p => p.Severity, severity)
            .AddChildContent("msg"));

        cut.Markup.ShouldContain("alert");
        cut.Markup.ShouldContain(expected);
    }

    [Theory]
    [InlineData(LoadingType.Spinner, "loading-spinner")]
    [InlineData(LoadingType.Dots, "loading-dots")]
    [InlineData(LoadingType.Ring, "loading-ring")]
    [InlineData(LoadingType.Bars, "loading-bars")]
    [InlineData(LoadingType.Infinity, "loading-infinity")]
    public void Loading_renders_animation_class(LoadingType type, string expected)
    {
        IRenderedComponent<Loading> cut = Render<Loading>(ps => ps.Add(p => p.Type, type));

        cut.Markup.ShouldContain("loading");
        cut.Markup.ShouldContain(expected);
    }

    [Fact]
    public void Kbd_renders_kbd_class()
    {
        IRenderedComponent<Kbd> cut = Render<Kbd>(ps => ps.AddChildContent("Ctrl"));

        cut.Find("kbd").ClassList.ShouldContain("kbd");
        cut.Markup.ShouldContain("Ctrl");
    }

    [Fact]
    public void RadialProgress_sets_value_custom_property()
    {
        IRenderedComponent<RadialProgress> cut = Render<RadialProgress>(ps => ps.Add(p => p.Value, 70));

        cut.Markup.ShouldContain("radial-progress");
        cut.Find("div").GetAttribute("style").ShouldContain("--value:70");
    }

    [Fact]
    public void Range_renders_range_with_color_and_value()
    {
        IRenderedComponent<Range> cut = Render<Range>(ps => ps
            .Add(p => p.Value, 50)
            .Add(p => p.Color, Color.Success));

        var input = cut.Find("input");
        input.ClassList.ShouldContain("range");
        input.ClassList.ShouldContain("range-success");
        input.GetAttribute("value").ShouldBe("50");
    }

    [Fact]
    public void Status_renders_status_with_color()
    {
        IRenderedComponent<Status> cut = Render<Status>(ps => ps.Add(p => p.Color, Color.Success));

        cut.Markup.ShouldContain("status");
        cut.Markup.ShouldContain("status-success");
    }

    [Fact]
    public void Badge_renders_badge_class()
    {
        IRenderedComponent<Badge> cut = Render<Badge>(ps => ps.AddChildContent("9"));

        cut.Markup.ShouldContain("badge");
    }

    [Fact]
    public void Tabs_marks_active_tab()
    {
        IRenderedComponent<Tabs> cut = Render<Tabs>(ps => ps
            .Add(p => p.ActiveIndex, 1)
            .AddChildContent<Tab>(tab => tab.Add(t => t.Title, "One").AddChildContent("first"))
            .AddChildContent<Tab>(tab => tab.Add(t => t.Title, "Two").AddChildContent("second")));

        cut.Markup.ShouldContain("tab-active");
        cut.Markup.ShouldContain("tab-content");
    }
}

/// <summary>Tests for the dependency-free SVG charts.</summary>
public class ChartRenderTests : BunitContext
{
    [Fact]
    public void Sparkline_renders_svg_path()
    {
        IRenderedComponent<Sparkline> cut = Render<Sparkline>(ps => ps
            .Add(p => p.Data, new double[] { 1, 4, 2, 6, 3, 7 }));

        cut.Find("svg").ShouldNotBeNull();
        cut.Markup.ShouldContain("<path");
    }

    [Fact]
    public void PieChart_renders_a_slice_per_point()
    {
        IRenderedComponent<PieChart> cut = Render<PieChart>(ps => ps
            .Add(p => p.Data, new List<ChartDataPoint>
            {
                new("A", 30),
                new("B", 50),
                new("C", 20),
            }));

        cut.Find("svg").ShouldNotBeNull();
        cut.Markup.ShouldContain("<path");
    }
}
