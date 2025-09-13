using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class StrongTests : TestContext
{
    [Fact]
    public void Strong_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .AddChildContent("Bold text"));
        
        var strong = component.Find("strong");
        strong.Should().NotBeNull();
        strong.TextContent.Should().Be("Bold text");
        strong.GetClasses().Should().Contain("rt-Strong");
    }
    
    
    
    [Fact]
    public void Strong_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long strong text that should be truncated"));
        
        component.Find("strong").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Theory]
    [InlineData(StrongWrap.Wrap, "rt-text-wrap")]
    [InlineData(StrongWrap.Nowrap, "rt-text-nowrap")]
    [InlineData(StrongWrap.Pretty, "rt-text-pretty")]
    [InlineData(StrongWrap.Balance, "rt-text-balance")]
    public void Strong_ShouldApplyWrapClass(StrongWrap wrap, string expectedClass)
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .Add(p => p.Wrap, wrap)
            .AddChildContent("Strong text"));
        
        component.Find("strong").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Strong_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .Add(p => p.Class, "custom-strong")
            .AddChildContent("Strong text"));
        
        component.Find("strong").GetClasses().Should().Contain("custom-strong");
    }
    
    [Fact]
    public void Strong_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .AddUnmatched("data-testid", "strong-element")
            .AddUnmatched("title", "Important text")
            .AddChildContent("Strong text"));
        
        var strong = component.Find("strong");
        strong.GetAttribute("data-testid").Should().Be("strong-element");
        strong.GetAttribute("title").Should().Be("Important text");
    }
}