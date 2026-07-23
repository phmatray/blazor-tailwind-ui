using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
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
        avatar.ShouldNotBeNull();
        avatar.GetClasses().ShouldContain("rt-Avatar");
        avatar.GetClasses().ShouldContain("rt-r-size-3");
        avatar.GetClasses().ShouldContain("rt-variant-soft");
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
        
        component.Find("span.rt-Avatar").GetClasses().ShouldContain(expectedClass);
    }
    
    [Theory]
    [InlineData(AvatarVariant.Solid, "rt-variant-solid")]
    [InlineData(AvatarVariant.Soft, "rt-variant-soft")]
    public void Avatar_ShouldApplyVariantClass(AvatarVariant variant, string expectedClass)
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Variant, variant));
        
        component.Find("span.rt-Avatar").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Avatar_ShouldRenderImage()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Src, "avatar.jpg")
            .Add(p => p.Alt, "User Avatar"));
        
        var img = component.Find("img.rt-AvatarImage");
        img.ShouldNotBeNull();
        img.GetAttribute("src").ShouldBe("avatar.jpg");
        img.GetAttribute("alt").ShouldBe("User Avatar");
    }
    
    [Fact]
    public void Avatar_ShouldRenderFallback()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Fallback, (RenderFragment)(b => b.AddContent(0, "JD"))));
        
        var fallback = component.Find("span.rt-AvatarFallback");
        fallback.ShouldNotBeNull();
        fallback.TextContent.ShouldBe("JD");
    }
    
    [Fact]
    public void Avatar_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Color, "indigo"));
        
        component.Find("span.rt-Avatar").GetAttribute("data-accent-color").ShouldBe("indigo");
    }
    
    [Fact]
    public void Avatar_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.HighContrast, true));
        
        component.Find("span.rt-Avatar").GetClasses().ShouldContain("rt-high-contrast");
    }
    
    [Fact]
    public void Avatar_ShouldApplyRadiusAttribute()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Radius, "full"));
        
        component.Find("span.rt-Avatar").GetAttribute("data-radius").ShouldBe("full");
    }
    
    [Fact]
    public void Avatar_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mx, "3"));
        
        var classes = component.Find("span.rt-Avatar").GetClasses();
        classes.ShouldContain("rt-r-m-2");
        classes.ShouldContain("rt-r-mx-3");
    }
    
    [Fact]
    public void Avatar_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Avatar>(parameters => parameters
            .Add(p => p.Class, "custom-avatar"));
        
        component.Find("span.rt-Avatar").GetClasses().ShouldContain("custom-avatar");
    }
}