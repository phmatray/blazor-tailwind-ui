using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Components.Common;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class TextTests : TestContext
{
    [Fact]
    public void Text_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .AddChildContent("Sample text"));
        
        var span = component.Find("span");
        span.Should().NotBeNull();
        span.TextContent.Should().Be("Sample text");
        span.GetClasses().Should().Contain("rt-Text");
        span.GetClasses().Should().Contain("rt-r-size-2");
    }
    
    [Theory]
    [InlineData("1", "rt-r-size-1")]
    [InlineData("2", "rt-r-size-2")]
    [InlineData("3", "rt-r-size-3")]
    [InlineData("4", "rt-r-size-4")]
    [InlineData("5", "rt-r-size-5")]
    [InlineData("6", "rt-r-size-6")]
    [InlineData("7", "rt-r-size-7")]
    [InlineData("8", "rt-r-size-8")]
    [InlineData("9", "rt-r-size-9")]
    public void Text_ShouldApplySizeClass(string size, string expectedClass)
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Text"));
        
        component.Find("span").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(TextWeight.Light, "rt-r-weight-light")]
    [InlineData(TextWeight.Regular, "rt-r-weight-regular")]
    [InlineData(TextWeight.Medium, "rt-r-weight-medium")]
    [InlineData(TextWeight.Bold, "rt-r-weight-bold")]
    public void Text_ShouldApplyWeightClass(TextWeight weight, string expectedClass)
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Weight, weight)
            .AddChildContent("Text"));
        
        component.Find("span").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Text_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Color, "blue")
            .AddChildContent("Text"));
        
        component.Find("span").GetAttribute("data-accent-color").Should().Be("blue");
    }
    
    [Fact]
    public void Text_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("Text"));
        
        component.Find("span").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Theory]
    [InlineData(TextAlign.Left, "rt-r-ta-left")]
    [InlineData(TextAlign.Center, "rt-r-ta-center")]
    [InlineData(TextAlign.Right, "rt-r-ta-right")]
    public void Text_ShouldApplyAlignClass(TextAlign align, string expectedClass)
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Align, align)
            .AddChildContent("Text"));
        
        component.Find("span").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Text_ShouldApplyTrimClass()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Trim, true)
            .AddChildContent("Text"));
        
        var classes = component.Find("span").GetClasses();
        classes.Should().Contain("rt-leading-trim-start");
        classes.Should().Contain("rt-leading-trim-end");
    }
    
    [Fact]
    public void Text_ShouldRenderAsDivWhenAsIsDiv()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.As, "div")
            .AddChildContent("Text"));
        
        component.Find("div").Should().NotBeNull();
        component.Find("div").GetClasses().Should().Contain("rt-Text");
    }
    
    [Fact]
    public void Text_ShouldRenderAsParagraphWhenAsIsP()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.As, "p")
            .AddChildContent("Text"));
        
        component.Find("p").Should().NotBeNull();
        component.Find("p").GetClasses().Should().Contain("rt-Text");
    }
    
    [Fact]
    public void Text_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long text that should be truncated"));
        
        component.Find("span").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Theory]
    [InlineData(TextWrap.Wrap, "rt-r-tw-wrap")]
    [InlineData(TextWrap.Nowrap, "rt-r-tw-nowrap")]
    [InlineData(TextWrap.Pretty, "rt-r-tw-pretty")]
    [InlineData(TextWrap.Balance, "rt-r-tw-balance")]
    public void Text_ShouldApplyWrapClass(TextWrap wrap, string expectedClass)
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Wrap, wrap)
            .AddChildContent("Text"));
        
        component.Find("span").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Text_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mt, "3")
            .Add(p => p.Mb, "3")
            .AddChildContent("Text"));
        
        var classes = component.Find("span").GetClasses();
        classes.Should().Contain("rt-r-m-2");
        classes.Should().Contain("rt-r-mt-3");
        classes.Should().Contain("rt-r-mb-3");
    }
    
    [Fact]
    public void Text_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Text>(parameters => parameters
            .Add(p => p.Class, "custom-text")
            .AddChildContent("Text"));
        
        component.Find("span").GetClasses().Should().Contain("custom-text");
    }
}