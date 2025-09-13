using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Radixor.Components.Layout;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Layout;

public class BoxTests : TestContext
{
    [Fact]
    public void Box_ShouldRenderDivByDefault()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .AddChildContent("Test Content"));
        
        component.Find("div").Should().NotBeNull();
        component.Find("div").TextContent.Should().Be("Test Content");
    }
    
    [Fact]
    public void Box_ShouldRenderSpanWhenAsIsSpan()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.As, "span")
            .AddChildContent("Test Content"));
        
        component.Find("span").Should().NotBeNull();
        component.Find("span").TextContent.Should().Be("Test Content");
    }
    
    [Fact]
    public void Box_ShouldApplyDisplayClass()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.Display, "flex")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-display-flex");
    }
    
    [Fact]
    public void Box_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.Mx, "2")
            .Add(p => p.My, "4")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-m-3");
        classes.Should().Contain("rt-r-mx-2");
        classes.Should().Contain("rt-r-my-4");
    }
    
    [Fact]
    public void Box_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.P, "3")
            .Add(p => p.Px, "2")
            .Add(p => p.Py, "4")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-p-3");
        classes.Should().Contain("rt-r-px-2");
        classes.Should().Contain("rt-r-py-4");
    }
    
    [Fact]
    public void Box_ShouldApplyCustomCssClass()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.Class, "custom-class")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("custom-class");
    }
    
    [Fact]
    public void Box_ShouldHaveBaseClass()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-Box");
    }
    
    [Fact]
    public void Box_WithAsChild_ShouldNotRenderWrapper()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.AsChild, true)
            .AddChildContent("<button>Click me</button>"));
        
        component.FindAll("div").Should().BeEmpty();
        component.FindAll("span").Should().BeEmpty();
    }
    
    [Fact]
    public void Box_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .AddUnmatched("data-testid", "test-box")
            .AddUnmatched("aria-label", "Test Box")
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.GetAttribute("data-testid").Should().Be("test-box");
        element.GetAttribute("aria-label").Should().Be("Test Box");
    }
}