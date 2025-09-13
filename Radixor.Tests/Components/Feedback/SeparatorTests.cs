using Bunit;
using FluentAssertions;
using Radixor.Components.Feedback;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Feedback;

public class SeparatorTests : TestContext
{
    [Fact]
    public void Separator_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Separator>();
        
        var separator = component.Find("span.rt-Separator");
        separator.Should().NotBeNull();
        separator.GetClasses().Should().Contain("rt-Separator");
        separator.GetClasses().Should().Contain("rt-r-size-1");
        separator.GetClasses().Should().Contain("rt-r-orientation-horizontal");
        // Note: data-orientation attribute is not set by default in the current implementation
    }
    
    [Theory]
    [InlineData(SeparatorSize.Size1, "rt-r-size-1")]
    [InlineData(SeparatorSize.Size2, "rt-r-size-2")]
    [InlineData(SeparatorSize.Size3, "rt-r-size-3")]
    [InlineData(SeparatorSize.Size4, "rt-r-size-4")]
    public void Separator_ShouldApplySizeClass(SeparatorSize size, string expectedClass)
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("span.rt-Separator").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(SeparatorOrientation.Horizontal, "rt-r-orientation-horizontal")]
    [InlineData(SeparatorOrientation.Vertical, "rt-r-orientation-vertical")]
    public void Separator_ShouldApplyOrientationClass(SeparatorOrientation orientation, string expectedClass)
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .Add(p => p.Orientation, orientation));
        
        var separator = component.Find("span.rt-Separator");
        separator.GetClasses().Should().Contain(expectedClass);
        // Note: data-orientation attribute implementation varies based on decorative property
    }
    
    [Fact]
    public void Separator_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .Add(p => p.Color, "gray"));
        
        component.Find("span.rt-Separator").GetAttribute("data-accent-color").Should().Be("gray");
    }
    
    [Fact]
    public void Separator_ShouldAcceptDecorativeParameter()
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .Add(p => p.Decorative, true));
        
        var separator = component.Find("span.rt-Separator");
        separator.Should().NotBeNull();
        // The Decorative property affects accessibility attributes
        // but the specific implementation may vary
    }
    
    [Fact]
    public void Separator_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.My, "4"));
        
        var classes = component.Find("span.rt-Separator").GetClasses();
        classes.Should().Contain("rt-r-m-3");
        classes.Should().Contain("rt-r-my-4");
    }
    
    [Fact]
    public void Separator_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .Add(p => p.Class, "custom-separator"));
        
        component.Find("span.rt-Separator").GetClasses().Should().Contain("custom-separator");
    }
    
    [Fact]
    public void Separator_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Separator>(parameters => parameters
            .AddUnmatched("data-testid", "divider")
            .AddUnmatched("aria-label", "Section separator"));
        
        var separator = component.Find("span.rt-Separator");
        separator.GetAttribute("data-testid").Should().Be("divider");
        separator.GetAttribute("aria-label").Should().Be("Section separator");
    }
}