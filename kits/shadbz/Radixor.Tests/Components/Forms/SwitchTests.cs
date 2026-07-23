using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Radixor.Components.Forms;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Forms;

public class SwitchTests : TestContext
{
    [Fact]
    public void Switch_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Switch>();
        
        var button = component.Find("button");
        button.ShouldNotBeNull();
        button.GetAttribute("type").ShouldBe("button");
        button.GetAttribute("role").ShouldBe("switch");
        button.GetAttribute("aria-checked").ShouldBe("false");
        button.GetClasses().ShouldContain("rt-SwitchRoot");
    }
    
    [Fact]
    public void Switch_ShouldBeCheckedWhenValueIsTrue()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Checked, true));
        
        var button = component.Find("button");
        button.GetAttribute("aria-checked").ShouldBe("true");
        button.GetAttribute("data-state").ShouldBe("checked");
    }
    
    [Fact]
    public void Switch_ShouldBeUncheckedWhenValueIsFalse()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Checked, false));
        
        var button = component.Find("button");
        button.GetAttribute("aria-checked").ShouldBe("false");
        button.GetAttribute("data-state").ShouldBe("unchecked");
    }
    
    [Fact]
    public void Switch_ShouldToggleValueOnClick()
    {
        var value = false;
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Checked, value)
            .Add(p => p.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => value = v)));
        
        var button = component.Find("button");
        button.Click();
        
        value.ShouldBeTrue();
        component.SetParametersAndRender(parameters => parameters.Add(p => p.Checked, value));
        button.GetAttribute("aria-checked").ShouldBe("true");
    }
    
    [Fact]
    public void Switch_ShouldBeDisabledWhenDisabledIsTrue()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Disabled, true));
        
        var button = component.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
        button.HasAttribute("data-disabled").ShouldBeTrue();
    }
    
    [Fact]
    public void Switch_ShouldNotToggleWhenDisabled()
    {
        var value = false;
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Checked, value)
            .Add(p => p.Disabled, true)
            .Add(p => p.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => value = v)));
        
        var button = component.Find("button");
        button.Click();
        
        value.ShouldBeFalse();
    }
    
    [Theory]
    [InlineData(SwitchSize.Size1, "rt-r-size-1")]
    [InlineData(SwitchSize.Size2, "rt-r-size-2")]
    [InlineData(SwitchSize.Size3, "rt-r-size-3")]
    public void Switch_ShouldApplySizeClass(SwitchSize size, string expectedClass)
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("button").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Switch_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Color, "green"));
        
        component.Find("button").GetAttribute("data-accent-color").ShouldBe("green");
    }
    
    [Fact]
    public void Switch_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.HighContrast, true));
        
        component.Find("button").GetClasses().ShouldContain("rt-high-contrast");
    }
    
    [Fact]
    public void Switch_ShouldApplyVariantClass()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Variant, SwitchVariant.Soft));
        
        component.Find("button").GetClasses().ShouldContain("rt-variant-soft");
    }
    
    [Fact]
    public void Switch_ShouldApplyRadiusAttribute()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Radius, "full"));
        
        component.Find("button").GetAttribute("data-radius").ShouldBe("full");
    }
    
    [Fact]
    public void Switch_ShouldRenderThumb()
    {
        var component = RenderComponent<Switch>();
        
        component.Find(".rt-SwitchThumb").ShouldNotBeNull();
    }
    
    [Fact]
    public void Switch_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.M, "3")
            .Add(p => p.Ml, "2"));
        
        var classes = component.Find("button").GetClasses();
        classes.ShouldContain("rt-r-m-3");
        classes.ShouldContain("rt-r-ml-2");
    }
    
    [Fact]
    public void Switch_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .Add(p => p.Class, "custom-switch"));
        
        component.Find("button").GetClasses().ShouldContain("custom-switch");
    }
    
    [Fact]
    public void Switch_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Switch>(parameters => parameters
            .AddUnmatched("data-testid", "dark-mode-switch")
            .AddUnmatched("name", "darkMode"));
        
        var button = component.Find("button");
        button.GetAttribute("data-testid").ShouldBe("dark-mode-switch");
        button.GetAttribute("name").ShouldBe("darkMode");
    }
}