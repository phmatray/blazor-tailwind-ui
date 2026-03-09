using Bunit;
using Shouldly;
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
        element.ShouldNotBeNull();
        element.GetClasses().ShouldContain("rt-Container");
        element.TextContent.ShouldBe("Test Content");
    }
    
    [Fact]
    public void Container_ShouldApplySizeClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.Size, ContainerSize.Size2)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-size-2");
    }
    
    [Fact]
    public void Container_ShouldApplyDisplayClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.Display, ContainerDisplay.Block)
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("rt-r-display-block");
    }
    
    [Fact]
    public void Container_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.M, "auto")
            .Add(p => p.Mx, "4")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-m-auto");
        classes.ShouldContain("rt-r-mx-4");
    }
    
    [Fact]
    public void Container_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.P, "5")
            .Add(p => p.Px, "3")
            .AddChildContent("Test Content"));
        
        var classes = component.Find("div").GetClasses();
        classes.ShouldContain("rt-r-p-5");
        classes.ShouldContain("rt-r-px-3");
    }
    
    [Fact]
    public void Container_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .Add(p => p.Class, "custom-container")
            .AddChildContent("Test Content"));
        
        component.Find("div").GetClasses().ShouldContain("custom-container");
    }
    
    [Fact]
    public void Container_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Container>(parameters => parameters
            .AddUnmatched("data-testid", "main-container")
            .AddUnmatched("role", "main")
            .AddChildContent("Test Content"));
        
        var element = component.Find("div");
        element.GetAttribute("data-testid").ShouldBe("main-container");
        element.GetAttribute("role").ShouldBe("main");
    }
}