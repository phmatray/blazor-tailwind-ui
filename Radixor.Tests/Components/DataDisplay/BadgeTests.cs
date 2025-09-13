using Bunit;
using FluentAssertions;
using Radixor.Components.DataDisplay;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.DataDisplay;

public class BadgeTests : TestContext
{
    [Fact]
    public void Badge_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .AddChildContent("New"));
        
        var badge = component.Find("span.rt-Badge");
        badge.Should().NotBeNull();
        badge.TextContent.Should().Be("New");
        badge.GetClasses().Should().Contain("rt-Badge");
        badge.GetClasses().Should().Contain("rt-r-size-1");
        badge.GetClasses().Should().Contain("rt-variant-soft");
    }
    
    [Theory]
    [InlineData(BadgeSize.Size1, "rt-r-size-1")]
    [InlineData(BadgeSize.Size2, "rt-r-size-2")]
    [InlineData(BadgeSize.Size3, "rt-r-size-3")]
    public void Badge_ShouldApplySizeClass(BadgeSize size, string expectedClass)
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Badge"));
        
        component.Find("span.rt-Badge").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(BadgeVariant.Solid, "rt-variant-solid")]
    [InlineData(BadgeVariant.Soft, "rt-variant-soft")]
    [InlineData(BadgeVariant.Surface, "rt-variant-surface")]
    [InlineData(BadgeVariant.Outline, "rt-variant-outline")]
    public void Badge_ShouldApplyVariantClass(BadgeVariant variant, string expectedClass)
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.Variant, variant)
            .AddChildContent("Badge"));
        
        component.Find("span.rt-Badge").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Badge_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.Color, "red")
            .AddChildContent("Alert"));
        
        component.Find("span.rt-Badge").GetAttribute("data-accent-color").Should().Be("red");
    }
    
    [Fact]
    public void Badge_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("Badge"));
        
        component.Find("span.rt-Badge").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Badge_ShouldApplyRadiusAttribute()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.Radius, "full")
            .AddChildContent("Badge"));
        
        component.Find("span.rt-Badge").GetAttribute("data-radius").Should().Be("full");
    }
    
    [Fact]
    public void Badge_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.M, "1")
            .Add(p => p.Ml, "2")
            .AddChildContent("Badge"));
        
        var classes = component.Find("span.rt-Badge").GetClasses();
        classes.Should().Contain("rt-r-m-1");
        classes.Should().Contain("rt-r-ml-2");
    }
    
    [Fact]
    public void Badge_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .Add(p => p.Class, "custom-badge")
            .AddChildContent("Badge"));
        
        component.Find("span.rt-Badge").GetClasses().Should().Contain("custom-badge");
    }
    
    [Fact]
    public void Badge_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Badge>(parameters => parameters
            .AddUnmatched("data-testid", "status-badge")
            .AddUnmatched("title", "Status indicator")
            .AddChildContent("Active"));
        
        var badge = component.Find("span.rt-Badge");
        badge.GetAttribute("data-testid").Should().Be("status-badge");
        badge.GetAttribute("title").Should().Be("Status indicator");
    }
}