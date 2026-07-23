using Bunit;
using Shouldly;
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
        strong.ShouldNotBeNull();
        strong.TextContent.ShouldBe("Bold text");
        strong.GetClasses().ShouldContain("rt-Strong");
    }
    
    
    
    [Fact]
    public void Strong_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long strong text that should be truncated"));
        
        component.Find("strong").GetClasses().ShouldContain("rt-truncate");
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
        
        component.Find("strong").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Strong_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .Add(p => p.Class, "custom-strong")
            .AddChildContent("Strong text"));
        
        component.Find("strong").GetClasses().ShouldContain("custom-strong");
    }
    
    [Fact]
    public void Strong_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Strong>(parameters => parameters
            .AddUnmatched("data-testid", "strong-element")
            .AddUnmatched("title", "Important text")
            .AddChildContent("Strong text"));
        
        var strong = component.Find("strong");
        strong.GetAttribute("data-testid").ShouldBe("strong-element");
        strong.GetAttribute("title").ShouldBe("Important text");
    }
}