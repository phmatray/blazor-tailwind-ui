using Bunit;
using Shouldly;
using Radixor.Components.Layout;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Layout;

public class FlexTests : TestContext
{
    [Fact]
    public void Flex_ShouldRenderWithBaseClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.ShouldNotBeNull();
        element.GetClasses().ShouldContain("rt-Flex");
        element.TextContent.ShouldBe("Test Content");
    }
    
    [Fact]
    public void Flex_ShouldApplyDirectionClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Direction, FlexDirection.Column)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-fd-column");
    }
    
    [Fact]
    public void Flex_ShouldApplyAlignClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Align, FlexAlign.Center)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-ai-center");
    }
    
    [Fact]
    public void Flex_ShouldApplyJustifyClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Justify, FlexJustify.Between)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-jc-space-between");
    }
    
    [Fact]
    public void Flex_ShouldApplyWrapClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Wrap, FlexWrap.Wrap)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-fw-wrap");
    }
    
    [Fact]
    public void Flex_ShouldApplyGapClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Gap, "3")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-gap-3");
    }
    
    [Fact]
    public void Flex_ShouldApplyGapXAndGapYClasses()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.GapX, "2")
            .Add(p => p.GapY, "4")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-cg-2");
        classes.ShouldContain("rt-r-rg-4");
    }
    
    [Fact]
    public void Flex_ShouldRenderAsSpanWhenSpecified()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.As, "span")
            .AddChildContent("Test Content"));
        
        component.Find("span").ShouldNotBeNull();
        component.Find("span").GetClasses().ShouldContain("rt-Flex");
    }
    
    [Fact]
    public void Flex_ShouldApplyInlineClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Display, FlexDisplay.InlineFlex)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-display-inline-flex");
    }
    
    [Fact]
    public void Flex_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.Mx, "auto")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-m-3");
        classes.ShouldContain("rt-r-mx-auto");
    }
    
    [Fact]
    public void Flex_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.P, "4")
            .Add(p => p.Py, "2")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-p-4");
        classes.ShouldContain("rt-r-py-2");
    }
    
    [Fact]
    public void Flex_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Class, "custom-flex")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("custom-flex");
    }
}