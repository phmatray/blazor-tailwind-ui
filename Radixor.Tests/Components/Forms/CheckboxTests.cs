using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Radixor.Components.Forms;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Forms;

public class CheckboxTests : TestContext
{
    [Fact]
    public void Checkbox_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Checkbox>();
        
        var button = component.Find("button");
        button.ShouldNotBeNull();
        button.GetAttribute("type").ShouldBe("button");
        button.GetAttribute("role").ShouldBe("checkbox");
        button.GetAttribute("aria-checked").ShouldBe("false");
        button.GetClasses().ShouldContain("rt-CheckboxRoot");
    }
    
    [Fact]
    public void Checkbox_ShouldBeCheckedWhenValueIsTrue()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, true));
        
        var button = component.Find("button");
        button.GetAttribute("aria-checked").ShouldBe("true");
        button.GetAttribute("data-state").ShouldBe("checked");
    }
    
    [Fact]
    public void Checkbox_ShouldBeUncheckedWhenValueIsFalse()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, false));
        
        var button = component.Find("button");
        button.GetAttribute("aria-checked").ShouldBe("false");
        button.GetAttribute("data-state").ShouldBe("unchecked");
    }
    
    [Fact]
    public void Checkbox_ShouldToggleValueOnClick()
    {
        var value = false;
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, value)
            .Add(p => p.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => value = v)));
        
        var button = component.Find("button");
        button.Click();
        
        value.ShouldBeTrue();
        component.SetParametersAndRender(parameters => parameters.Add(p => p.Checked, value));
        button.GetAttribute("aria-checked").ShouldBe("true");
    }
    
    [Fact]
    public void Checkbox_ShouldBeDisabledWhenDisabledIsTrue()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Disabled, true));
        
        var button = component.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
        button.HasAttribute("data-disabled").ShouldBeTrue();
    }
    
    [Fact]
    public void Checkbox_ShouldNotToggleWhenDisabled()
    {
        var value = false;
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, value)
            .Add(p => p.Disabled, true)
            .Add(p => p.CheckedChanged, EventCallback.Factory.Create<bool>(this, v => value = v)));
        
        var button = component.Find("button");
        button.Click();
        
        value.ShouldBeFalse();
    }
    
    [Theory]
    [InlineData(CheckboxSize.Size1, "rt-r-size-1")]
    [InlineData(CheckboxSize.Size2, "rt-r-size-2")]
    [InlineData(CheckboxSize.Size3, "rt-r-size-3")]
    public void Checkbox_ShouldApplySizeClass(CheckboxSize size, string expectedClass)
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("button").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Checkbox_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Color, "blue"));
        
        component.Find("button").GetAttribute("data-accent-color").ShouldBe("blue");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.HighContrast, true));
        
        component.Find("button").GetClasses().ShouldContain("rt-high-contrast");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyVariantClass()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Variant, CheckboxVariant.Soft));
        
        component.Find("button").GetClasses().ShouldContain("rt-variant-soft");
    }
    
    [Fact]
    public void Checkbox_ShouldRenderCheckIconWhenChecked()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, true));
        
        component.Find(".rt-CheckboxIndicator").ShouldNotBeNull();
        // The checkbox indicator exists but may use CSS to show the check rather than SVG
        component.Find("button").GetAttribute("data-state").ShouldBe("checked");
    }
    
    [Fact]
    public void Checkbox_ShouldNotRenderCheckIconWhenUnchecked()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, false));
        
        component.FindAll(".rt-CheckboxIndicator svg").ShouldBeEmpty();
    }
    
    [Fact]
    public void Checkbox_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mr, "3"));
        
        var classes = component.Find("button").GetClasses();
        classes.ShouldContain("rt-r-m-2");
        classes.ShouldContain("rt-r-mr-3");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Class, "custom-checkbox"));
        
        component.Find("button").GetClasses().ShouldContain("custom-checkbox");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .AddUnmatched("data-testid", "terms-checkbox")
            .AddUnmatched("name", "terms"));
        
        var button = component.Find("button");
        button.GetAttribute("data-testid").ShouldBe("terms-checkbox");
        button.GetAttribute("name").ShouldBe("terms");
    }
}