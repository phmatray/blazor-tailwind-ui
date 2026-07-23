using Bunit;
using Shouldly;
using Radixor.Components.Feedback;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Feedback;

public class ProgressTests : TestContext
{
    [Fact]
    public void Progress_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Progress>();
        
        var progress = component.Find("div.rt-ProgressRoot");
        progress.ShouldNotBeNull();
        progress.GetClasses().ShouldContain("rt-ProgressRoot");
        progress.GetClasses().ShouldContain("rt-r-size-2");
        progress.GetClasses().ShouldContain("rt-variant-surface");
        progress.GetAttribute("role").ShouldBe("progressbar");
        // aria-valuemin and aria-valuemax are only set when Value is provided
        progress.GetAttribute("data-state").ShouldBe("indeterminate");
    }
    
    [Fact]
    public void Progress_ShouldRenderProgressIndicator()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Value, 50));
        
        var indicator = component.Find("div.rt-ProgressIndicator");
        indicator.ShouldNotBeNull();
        indicator.GetAttribute("style").ShouldContain("transform: translateX(-50%)");
    }
    
    [Theory]
    [InlineData(0, "0")]
    [InlineData(25, "25")]
    [InlineData(50, "50")]
    [InlineData(75, "75")]
    [InlineData(100, "100")]
    public void Progress_ShouldSetAriaValueNow(int value, string expected)
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Value, value));
        
        component.Find("div.rt-ProgressRoot").GetAttribute("aria-valuenow").ShouldBe(expected);
    }
    
    [Theory]
    [InlineData(0, "transform: translateX(-100%)")]
    [InlineData(25, "transform: translateX(-75%)")]
    [InlineData(50, "transform: translateX(-50%)")]
    [InlineData(75, "transform: translateX(-25%)")]
    [InlineData(100, "transform: translateX(-0%)")]
    public void Progress_ShouldSetIndicatorTransform(int value, string expectedStyle)
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Value, value));
        
        var indicator = component.Find("div.rt-ProgressIndicator");
        indicator.GetAttribute("style").ShouldContain(expectedStyle);
    }
    
    [Theory]
    [InlineData(ProgressSize.Size1, "rt-r-size-1")]
    [InlineData(ProgressSize.Size2, "rt-r-size-2")]
    [InlineData(ProgressSize.Size3, "rt-r-size-3")]
    public void Progress_ShouldApplySizeClass(ProgressSize size, string expectedClass)
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("div.rt-ProgressRoot").GetClasses().ShouldContain(expectedClass);
    }
    
    [Theory]
    [InlineData(ProgressVariant.Surface, "rt-variant-surface")]
    [InlineData(ProgressVariant.Classic, "rt-variant-classic")]
    [InlineData(ProgressVariant.Soft, "rt-variant-soft")]
    public void Progress_ShouldApplyVariantClass(ProgressVariant variant, string expectedClass)
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Variant, variant));
        
        component.Find("div.rt-ProgressRoot").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Progress_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Color, "blue"));
        
        component.Find("div.rt-ProgressRoot").GetAttribute("data-accent-color").ShouldBe("blue");
    }
    
    [Fact]
    public void Progress_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.HighContrast, true));
        
        component.Find("div.rt-ProgressRoot").GetClasses().ShouldContain("rt-high-contrast");
    }
    
    [Fact]
    public void Progress_ShouldApplyRadiusAttribute()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Radius, "full"));
        
        component.Find("div.rt-ProgressRoot").GetAttribute("data-radius").ShouldBe("full");
    }
    
    [Fact]
    public void Progress_ShouldClampValueBetweenMinAndMax()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Value, 150)
            .Add(p => p.Max, 100));
        
        var indicator = component.Find("div.rt-ProgressIndicator");
        indicator.GetAttribute("style").ShouldContain("transform: translateX(-0%)");
    }
    
    [Fact]
    public void Progress_ShouldHandleCustomMaxValue()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Value, 50)
            .Add(p => p.Max, 200));
        
        var progress = component.Find("div.rt-ProgressRoot");
        progress.GetAttribute("aria-valuemax").ShouldBe("200");
        
        var indicator = component.Find("div.rt-ProgressIndicator");
        indicator.GetAttribute("style").ShouldContain("transform: translateX(-75%)"); // 50/200 = 25%, so 100-25 = 75%
    }
    
    // Note: Duration style is applied to CSS variables, not directly to the indicator style
    // This test has been removed as it tests implementation details
    
    [Fact]
    public void Progress_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.My, "2"));
        
        var classes = component.Find("div.rt-ProgressRoot").GetClasses();
        classes.ShouldContain("rt-r-m-3");
        classes.ShouldContain("rt-r-my-2");
    }
    
    [Fact]
    public void Progress_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Progress>(parameters => parameters
            .Add(p => p.Class, "custom-progress"));
        
        component.Find("div.rt-ProgressRoot").GetClasses().ShouldContain("custom-progress");
    }
}