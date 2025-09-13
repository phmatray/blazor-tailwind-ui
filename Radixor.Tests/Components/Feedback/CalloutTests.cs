using Bunit;
using FluentAssertions;
using Radixor.Components.Feedback;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Feedback;

public class CalloutTests : TestContext
{
    [Fact]
    public void Callout_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .AddChildContent("Important information"));
        
        var callout = component.Find("div.rt-Callout");
        callout.Should().NotBeNull();
        callout.TextContent.Should().Contain("Important information");
        callout.GetClasses().Should().Contain("rt-Callout");
        callout.GetClasses().Should().Contain("rt-r-size-2");
        callout.GetClasses().Should().Contain("rt-variant-soft");
    }
    
    [Theory]
    [InlineData(CalloutSize.Size1, "rt-r-size-1")]
    [InlineData(CalloutSize.Size2, "rt-r-size-2")]
    [InlineData(CalloutSize.Size3, "rt-r-size-3")]
    public void Callout_ShouldApplySizeClass(CalloutSize size, string expectedClass)
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Callout"));
        
        component.Find("div.rt-Callout").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(CalloutVariant.Soft, "rt-variant-soft")]
    [InlineData(CalloutVariant.Surface, "rt-variant-surface")]
    [InlineData(CalloutVariant.Outline, "rt-variant-outline")]
    public void Callout_ShouldApplyVariantClass(CalloutVariant variant, string expectedClass)
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.Variant, variant)
            .AddChildContent("Callout"));
        
        component.Find("div.rt-Callout").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Callout_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.Color, "amber")
            .AddChildContent("Callout"));
        
        component.Find("div.rt-Callout").GetAttribute("data-accent-color").Should().Be("amber");
    }
    
    [Fact]
    public void Callout_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("Callout"));
        
        component.Find("div.rt-Callout").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Callout_ShouldRenderIcon()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.Icon, "ℹ️")
            .AddChildContent("Information"));
        
        var icon = component.Find(".rt-CalloutIcon");
        icon.Should().NotBeNull();
        icon.TextContent.Should().Contain("ℹ️");
    }
    
    [Fact]
    public void Callout_ShouldRenderWithRootElement()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .AddChildContent("Content"));
        
        component.Find(".rt-Callout").Should().NotBeNull();
    }
    
    [Fact]
    public void Callout_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.My, "4")
            .AddChildContent("Callout"));
        
        var classes = component.Find("div.rt-Callout").GetClasses();
        classes.Should().Contain("rt-r-m-3");
        classes.Should().Contain("rt-r-my-4");
    }
    
    [Fact]
    public void Callout_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .Add(p => p.Class, "custom-callout")
            .AddChildContent("Callout"));
        
        component.Find("div.rt-Callout").GetClasses().Should().Contain("custom-callout");
    }
    
    [Fact]
    public void Callout_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Callout>(parameters => parameters
            .AddUnmatched("data-testid", "info-callout")
            .AddUnmatched("role", "alert")
            .AddChildContent("Alert message"));
        
        var callout = component.Find("div.rt-Callout");
        callout.GetAttribute("data-testid").Should().Be("info-callout");
        callout.GetAttribute("role").Should().Be("alert");
    }
}