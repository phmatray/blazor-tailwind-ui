using Bunit;
using FluentAssertions;
using Radixor.Components.Layout;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Layout;

public class GridTests : TestContext
{
    [Fact]
    public void Grid_ShouldRenderWithBaseClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.Should().NotBeNull();
        element.GetClasses().Should().Contain("rt-Grid");
        element.TextContent.Should().Be("Test Content");
    }
    
    [Fact]
    public void Grid_ShouldApplyColumnsClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Columns, "3")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-gtc-3");
    }
    
    [Fact]
    public void Grid_ShouldApplyRowsClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Rows, "2")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-gtr-2");
    }
    
    [Fact]
    public void Grid_ShouldApplyFlowClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Flow, GridFlow.Column)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-gaf-column");
    }
    
    [Fact]
    public void Grid_ShouldApplyAlignClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Align, GridAlign.Center)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-ai-center");
    }
    
    [Fact]
    public void Grid_ShouldApplyJustifyClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Justify, GridJustify.Center)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-jc-center");
    }
    
    [Fact]
    public void Grid_ShouldApplyGapClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Gap, "4")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-gap-4");
    }
    
    [Fact]
    public void Grid_ShouldApplyGapXAndGapYClasses()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.GapX, "3")
            .Add(p => p.GapY, "5")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-cg-3");
        classes.Should().Contain("rt-r-rg-5");
    }
    
    [Fact]
    public void Grid_ShouldApplyInlineClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Display, GridDisplay.InlineGrid)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-display-inline-grid");
    }
    
    [Fact]
    public void Grid_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mt, "4")
            .Add(p => p.Mb, "4")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-m-2");
        classes.Should().Contain("rt-r-mt-4");
        classes.Should().Contain("rt-r-mb-4");
    }
    
    [Fact]
    public void Grid_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.P, "3")
            .Add(p => p.Px, "5")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-p-3");
        classes.Should().Contain("rt-r-px-5");
    }
    
    [Fact]
    public void Grid_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Class, "custom-grid")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("custom-grid");
    }
    
    [Fact]
    public void Grid_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .AddUnmatched("data-testid", "grid-container")
            .AddUnmatched("aria-label", "Grid Layout")
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.GetAttribute("data-testid").Should().Be("grid-container");
        element.GetAttribute("aria-label").Should().Be("Grid Layout");
    }
}