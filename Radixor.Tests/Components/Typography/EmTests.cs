using Bunit;
using Shouldly;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class EmTests : TestContext
{
    [Fact]
    public void Em_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .AddChildContent("Emphasized text"));
        
        var em = component.Find("em");
        em.ShouldNotBeNull();
        em.TextContent.ShouldBe("Emphasized text");
        em.GetClasses().ShouldContain("rt-Em");
    }
    
    
    
    [Fact]
    public void Em_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long emphasized text that should be truncated"));
        
        component.Find("em").GetClasses().ShouldContain("rt-truncate");
    }
    
    [Theory]
    [InlineData(EmWrap.Wrap, "rt-text-wrap")]
    [InlineData(EmWrap.Nowrap, "rt-text-nowrap")]
    [InlineData(EmWrap.Pretty, "rt-text-pretty")]
    [InlineData(EmWrap.Balance, "rt-text-balance")]
    public void Em_ShouldApplyWrapClass(EmWrap wrap, string expectedClass)
    {
        var component = RenderComponent<Em>(parameters => parameters
            .Add(p => p.Wrap, wrap)
            .AddChildContent("Emphasized text"));
        
        component.Find("em").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Em_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .Add(p => p.Class, "custom-em")
            .AddChildContent("Emphasized text"));
        
        component.Find("em").GetClasses().ShouldContain("custom-em");
    }
    
    [Fact]
    public void Em_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .AddUnmatched("data-testid", "em-element")
            .AddUnmatched("title", "Emphasis")
            .AddChildContent("Emphasized text"));
        
        var em = component.Find("em");
        em.GetAttribute("data-testid").ShouldBe("em-element");
        em.GetAttribute("title").ShouldBe("Emphasis");
    }
}