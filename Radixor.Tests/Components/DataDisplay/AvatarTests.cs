using Bunit;
using FluentAssertions;
using Radixor.Components.DataDisplay;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.DataDisplay;

public class AvatarTests : TestContext
{
    [Fact]
    public void Avatar_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Avatar>();
        
        var avatar = component.Find("span.rt-Avatar");
        avatar.Should().NotBeNull();
        avatar.GetClasses().Should().Contain("rt-Avatar");
        avatar.GetClasses().Should().Contain("rt-r-size-3");
        avatar.GetClasses().Should().Contain("rt-variant-soft");
    }
    
    [Theory]
    [InlineData(AvatarSize.Size1, "rt-r-size-1")]
    [InlineData(AvatarSize.Size2, "rt-r-size-2")]
    [InlineData(AvatarSize.Size3, "rt-r-size-3")]
    [InlineData(AvatarSize.Size4, "rt-r-size-4")]
    [InlineData(AvatarSize.Size5, "rt-r-size-5")]
    [InlineData(AvatarSize.Size6, "rt-r-size-6")]
    [InlineData(AvatarSize.Size7, "rt-r-size-7")]
    [InlineData(AvatarSize.Size8, "rt-r-size-8")]
    [InlineData(AvatarSize.Size9, "rt-r-size-9")]
    public void Avatar_ShouldApplySizeClass(AvatarSize size, string expectedClass)
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("span.rt-Avatar").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(AvatarVariant.Solid, "rt-variant-solid")]
    [InlineData(AvatarVariant.Soft, "rt-variant-soft")]
    public void Avatar_ShouldApplyVariantClass(AvatarVariant variant, string expectedClass)
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Variant, variant));
        
        component.Find("span.rt-Avatar").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Avatar_ShouldRenderImage()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Src, "avatar.jpg")
            .Add(p => p.Alt, "User Avatar"));
        
        var img = component.Find("img.rt-AvatarImage");
        img.Should().NotBeNull();
        img.GetAttribute("src").Should().Be("avatar.jpg");
        img.GetAttribute("alt").Should().Be("User Avatar");
    }
    
    [Fact]
    public void Avatar_ShouldRenderFallback()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Fallback, "JD"));
        
        var fallback = component.Find("span.rt-AvatarFallback");
        fallback.Should().NotBeNull();
        fallback.TextContent.Should().Be("JD");
    }
    
    [Fact]
    public void Avatar_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Color, "indigo"));
        
        component.Find("span.rt-Avatar").GetAttribute("data-accent-color").Should().Be("indigo");
    }
    
    [Fact]
    public void Avatar_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.HighContrast, true));
        
        component.Find("span.rt-Avatar").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Avatar_ShouldApplyRadiusAttribute()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Radius, "full"));
        
        component.Find("span.rt-Avatar").GetAttribute("data-radius").Should().Be("full");
    }
    
    [Fact]
    public void Avatar_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mx, "3"));
        
        var classes = component.Find("span.rt-Avatar").GetClasses();
        classes.Should().Contain("rt-r-m-2");
        classes.Should().Contain("rt-r-mx-3");
    }
    
    [Fact]
    public void Avatar_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Class, "custom-avatar"));
        
        component.Find("span.rt-Avatar").GetClasses().Should().Contain("custom-avatar");
    }
}