using Bunit;
using FluentAssertions;
using Radixor.Components.Layout;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Layout;

public class ContainerTests : TestContext
{
    [Fact]
    public void Container_ShouldRenderWithBaseClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.Should().NotBeNull();
        element.GetClasses().Should().Contain("rt-Container");
        element.TextContent.Should().Be("Test Content");
    }
    
    [Fact]
    public void Container_ShouldApplySizeClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.Size, ContainerSize.Size2)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-size-2");
    }
    
    [Fact]
    public void Container_ShouldApplyDisplayClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.Display, ContainerDisplay.Block)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("rt-r-display-block");
    }
    
    [Fact]
    public void Container_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.M, "auto")
            .Add(p => p.Mx, "4")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-m-auto");
        classes.Should().Contain("rt-r-mx-4");
    }
    
    [Fact]
    public void Container_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.P, "5")
            .Add(p => p.Px, "3")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.Should().Contain("rt-r-p-5");
        classes.Should().Contain("rt-r-px-3");
    }
    
    [Fact]
    public void Container_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.Class, "custom-container")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().Should().Contain("custom-container");
    }
    
    [Fact]
    public void Container_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .AddUnmatched("data-testid", "main-container")
            .AddUnmatched("role", "main")
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.GetAttribute("data-testid").Should().Be("main-container");
        element.GetAttribute("role").Should().Be("main");
    }
}