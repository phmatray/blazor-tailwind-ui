using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Components.Common;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class HeadingTests : TestContext
{
    [Fact]
    public void Heading_ShouldRenderH1ByDefault()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .AddChildContent("Test Heading"));
        
        var h1 = component.Find("h1");
        h1.Should().NotBeNull();
        h1.TextContent.Should().Be("Test Heading");
        h1.GetClasses().Should().Contain("rt-Heading");
    }
    
    [Theory]
    [InlineData("h1")]
    [InlineData("h2")]
    [InlineData("h3")]
    [InlineData("h4")]
    [InlineData("h5")]
    [InlineData("h6")]
    public void Heading_ShouldRenderCorrectHtmlElement(string asElement)
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.As, asElement)
            .AddChildContent("Test Heading"));
        
        var element = component.Find(asElement);
        element.Should().NotBeNull();
        element.GetClasses().Should().Contain("rt-Heading");
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
    public void Heading_ShouldApplySizeClass(string size, string expectedClass)
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Heading"));
        
        component.Find("h1").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(TextWeight.Light, "rt-r-weight-light")]
    [InlineData(TextWeight.Regular, "rt-r-weight-regular")]
    [InlineData(TextWeight.Medium, "rt-r-weight-medium")]
    [InlineData(TextWeight.Bold, "rt-r-weight-bold")]
    public void Heading_ShouldApplyWeightClass(TextWeight weight, string expectedClass)
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Weight, weight)
            .AddChildContent("Heading"));
        
        component.Find("h1").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(TextAlign.Left, "rt-r-ta-left")]
    [InlineData(TextAlign.Center, "rt-r-ta-center")]
    [InlineData(TextAlign.Right, "rt-r-ta-right")]
    public void Heading_ShouldApplyAlignClass(TextAlign align, string expectedClass)
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Align, align)
            .AddChildContent("Heading"));
        
        component.Find("h1").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Heading_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Color, "red")
            .AddChildContent("Heading"));
        
        component.Find("h1").GetAttribute("data-accent-color").Should().Be("red");
    }
    
    [Fact]
    public void Heading_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("Heading"));
        
        component.Find("h1").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Heading_ShouldApplyTrimClass()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Trim, true)
            .AddChildContent("Heading"));
        
        var classes = component.Find("h2").GetClasses();
        classes.Should().Contain("rt-leading-trim-start");
        classes.Should().Contain("rt-leading-trim-end");
    }
    
    [Fact]
    public void Heading_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long heading that needs truncation"));
        
        component.Find("h1").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Theory]
    [InlineData(TextWrap.Wrap, "rt-r-tw-wrap")]
    [InlineData(TextWrap.Nowrap, "rt-r-tw-nowrap")]
    [InlineData(TextWrap.Pretty, "rt-r-tw-pretty")]
    [InlineData(TextWrap.Balance, "rt-r-tw-balance")]
    public void Heading_ShouldApplyWrapClass(TextWrap wrap, string expectedClass)
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Wrap, wrap)
            .AddChildContent("Heading"));
        
        component.Find("h1").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Heading_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.Mt, "4")
            .Add(p => p.Mb, "4")
            .AddChildContent("Heading"));
        
        var classes = component.Find("h2").GetClasses();
        classes.Should().Contain("rt-r-m-3");
        classes.Should().Contain("rt-r-mt-4");
        classes.Should().Contain("rt-r-mb-4");
    }
    
    [Fact]
    public void Heading_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Heading>(parameters => parameters
            .Add(p => p.Class, "custom-heading")
            .AddChildContent("Heading"));
        
        component.Find("h1").GetClasses().Should().Contain("custom-heading");
    }
}