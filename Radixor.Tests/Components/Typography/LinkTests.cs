using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Components.Common;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class LinkTests : TestContext
{
    [Fact]
    public void Link_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Href, "https://example.com")
            .AddChildContent("Click here"));
        
        var link = component.Find("a.rt-Link");
        link.Should().NotBeNull();
        link.TextContent.Should().Be("Click here");
        link.GetAttribute("href").Should().Be("https://example.com");
        link.GetClasses().Should().Contain("rt-Link");
        link.GetClasses().Should().Contain("rt-underline-auto");
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
    public void Link_ShouldApplySizeClass(string size, string expectedClass)
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Size, size)
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        component.Find("a.rt-Link").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(LinkUnderline.Auto, "rt-underline-auto")]
    [InlineData(LinkUnderline.Always, "rt-underline-always")]
    [InlineData(LinkUnderline.Hover, "rt-underline-hover")]
    [InlineData(LinkUnderline.None, "rt-underline-none")]
    public void Link_ShouldApplyUnderlineClass(LinkUnderline underline, string expectedClass)
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Underline, underline)
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        component.Find("a.rt-Link").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(TextWeight.Light, "rt-r-weight-light")]
    [InlineData(TextWeight.Regular, "rt-r-weight-regular")]
    [InlineData(TextWeight.Medium, "rt-r-weight-medium")]
    [InlineData(TextWeight.Bold, "rt-r-weight-bold")]
    public void Link_ShouldApplyWeightClass(TextWeight weight, string expectedClass)
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Weight, weight)
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        component.Find("a.rt-Link").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Link_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Color, "blue")
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        component.Find("a.rt-Link").GetAttribute("data-accent-color").Should().Be("blue");
    }
    
    [Fact]
    public void Link_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        component.Find("a.rt-Link").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Link_ShouldApplyTargetAttribute()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Target, "_blank")
            .Add(p => p.Href, "https://example.com")
            .AddChildContent("External Link"));
        
        component.Find("a.rt-Link").GetAttribute("target").Should().Be("_blank");
    }
    
    [Fact]
    public void Link_ShouldApplyRelAttribute()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Rel, "noopener noreferrer")
            .Add(p => p.Href, "https://example.com")
            .Add(p => p.Target, "_blank")
            .AddChildContent("Safe External Link"));
        
        component.Find("a.rt-Link").GetAttribute("rel").Should().Be("noopener noreferrer");
    }
    
    [Fact]
    public void Link_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Truncate, true)
            .Add(p => p.Href, "#")
            .AddChildContent("Very long link text that should be truncated"));
        
        component.Find("a.rt-Link").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Fact]
    public void Link_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mx, "3")
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        var classes = component.Find("a.rt-Link").GetClasses();
        classes.Should().Contain("rt-r-m-2");
        classes.Should().Contain("rt-r-mx-3");
    }
    
    [Fact]
    public void Link_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Link>(parameters => parameters
            .Add(p => p.Class, "custom-link")
            .Add(p => p.Href, "#")
            .AddChildContent("Link"));
        
        component.Find("a.rt-Link").GetClasses().Should().Contain("custom-link");
    }
}