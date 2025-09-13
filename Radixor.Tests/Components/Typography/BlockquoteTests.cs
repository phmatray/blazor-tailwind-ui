using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class BlockquoteTests : TestContext
{
    [Fact]
    public void Blockquote_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .AddChildContent("This is a quote"));
        
        var blockquote = component.Find("blockquote");
        blockquote.Should().NotBeNull();
        blockquote.TextContent.Should().Be("This is a quote");
        blockquote.GetClasses().Should().Contain("rt-Blockquote");
        blockquote.GetClasses().Should().Contain("rt-r-size-2");
    }
    
    [Theory]
    [InlineData(BlockquoteSize.Size1, "rt-r-size-1")]
    [InlineData(BlockquoteSize.Size2, "rt-r-size-2")]
    [InlineData(BlockquoteSize.Size3, "rt-r-size-3")]
    [InlineData(BlockquoteSize.Size4, "rt-r-size-4")]
    [InlineData(BlockquoteSize.Size5, "rt-r-size-5")]
    [InlineData(BlockquoteSize.Size6, "rt-r-size-6")]
    [InlineData(BlockquoteSize.Size7, "rt-r-size-7")]
    [InlineData(BlockquoteSize.Size8, "rt-r-size-8")]
    [InlineData(BlockquoteSize.Size9, "rt-r-size-9")]
    public void Blockquote_ShouldApplySizeClass(BlockquoteSize size, string expectedClass)
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Quote"));
        
        component.Find("blockquote").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(BlockquoteWeight.Light, "rt-r-weight-light")]
    [InlineData(BlockquoteWeight.Regular, "rt-r-weight-regular")]
    [InlineData(BlockquoteWeight.Medium, "rt-r-weight-medium")]
    [InlineData(BlockquoteWeight.Bold, "rt-r-weight-bold")]
    public void Blockquote_ShouldApplyWeightClass(BlockquoteWeight weight, string expectedClass)
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.Weight, weight)
            .AddChildContent("Quote"));
        
        component.Find("blockquote").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Blockquote_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.Color, "purple")
            .AddChildContent("Quote"));
        
        component.Find("blockquote").GetAttribute("data-accent-color").Should().Be("purple");
    }
    
    [Fact]
    public void Blockquote_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("Quote"));
        
        component.Find("blockquote").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Blockquote_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long quote that should be truncated"));
        
        component.Find("blockquote").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Theory]
    [InlineData(BlockquoteWrap.Wrap, "rt-text-wrap")]
    [InlineData(BlockquoteWrap.Nowrap, "rt-text-nowrap")]
    [InlineData(BlockquoteWrap.Pretty, "rt-text-pretty")]
    [InlineData(BlockquoteWrap.Balance, "rt-text-balance")]
    public void Blockquote_ShouldApplyWrapClass(BlockquoteWrap wrap, string expectedClass)
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.Wrap, wrap)
            .AddChildContent("Quote"));
        
        component.Find("blockquote").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Blockquote_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.My, "4")
            .AddChildContent("Quote"));
        
        var classes = component.Find("blockquote").GetClasses();
        classes.Should().Contain("rt-r-m-3");
        classes.Should().Contain("rt-r-my-4");
    }
    
    [Fact]
    public void Blockquote_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .Add(p => p.Class, "custom-blockquote")
            .AddChildContent("Quote"));
        
        component.Find("blockquote").GetClasses().Should().Contain("custom-blockquote");
    }
}