using Bunit;
using FluentAssertions;
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
        em.Should().NotBeNull();
        em.TextContent.Should().Be("Emphasized text");
        em.GetClasses().Should().Contain("rt-Em");
    }
    
    
    
    [Fact]
    public void Em_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long emphasized text that should be truncated"));
        
        component.Find("em").GetClasses().Should().Contain("rt-truncate");
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
        
        component.Find("em").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Em_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .Add(p => p.Class, "custom-em")
            .AddChildContent("Emphasized text"));
        
        component.Find("em").GetClasses().Should().Contain("custom-em");
    }
    
    [Fact]
    public void Em_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Em>(parameters => parameters
            .AddUnmatched("data-testid", "em-element")
            .AddUnmatched("title", "Emphasis")
            .AddChildContent("Emphasized text"));
        
        var em = component.Find("em");
        em.GetAttribute("data-testid").Should().Be("em-element");
        em.GetAttribute("title").Should().Be("Emphasis");
    }
}