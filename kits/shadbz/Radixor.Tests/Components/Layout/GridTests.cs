using Bunit;
using Shouldly;
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
        element.ShouldNotBeNull();
        element.GetClasses().ShouldContain("rt-Grid");
        element.TextContent.ShouldBe("Test Content");
    }
    
    [Fact]
    public void Grid_ShouldApplyColumnsClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Columns, "3")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-gtc-3");
    }
    
    [Fact]
    public void Grid_ShouldApplyRowsClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Rows, "2")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-gtr-2");
    }
    
    [Fact]
    public void Grid_ShouldApplyFlowClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Flow, GridFlow.Column)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-gaf-column");
    }
    
    [Fact]
    public void Grid_ShouldApplyAlignClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Align, GridAlign.Center)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-ai-center");
    }
    
    [Fact]
    public void Grid_ShouldApplyJustifyClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Justify, GridJustify.Center)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-jc-center");
    }
    
    [Fact]
    public void Grid_ShouldApplyGapClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Gap, "4")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-gap-4");
    }
    
    [Fact]
    public void Grid_ShouldApplyGapXAndGapYClasses()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.GapX, "3")
            .Add(p => p.GapY, "5")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-cg-3");
        classes.ShouldContain("rt-r-rg-5");
    }
    
    [Fact]
    public void Grid_ShouldApplyInlineClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Display, GridDisplay.InlineGrid)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-display-inline-grid");
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
        classes.ShouldContain("rt-r-m-2");
        classes.ShouldContain("rt-r-mt-4");
        classes.ShouldContain("rt-r-mb-4");
    }
    
    [Fact]
    public void Grid_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.P, "3")
            .Add(p => p.Px, "5")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-p-3");
        classes.ShouldContain("rt-r-px-5");
    }
    
    [Fact]
    public void Grid_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Class, "custom-grid")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("custom-grid");
    }
    
    [Fact]
    public void Grid_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .AddUnmatched("data-testid", "grid-container")
            .AddUnmatched("aria-label", "Grid Layout")
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.GetAttribute("data-testid").ShouldBe("grid-container");
        element.GetAttribute("aria-label").ShouldBe("Grid Layout");
    }
}