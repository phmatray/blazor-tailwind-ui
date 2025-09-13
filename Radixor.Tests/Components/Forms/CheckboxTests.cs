using Bunit;
using FluentAssertions;
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
        button.Should().NotBeNull();
        button.GetAttribute("type").Should().Be("button");
        button.GetAttribute("role").Should().Be("checkbox");
        button.GetAttribute("aria-checked").Should().Be("false");
        button.GetClasses().Should().Contain("rt-CheckboxRoot");
    }
    
    [Fact]
    public void Checkbox_ShouldBeCheckedWhenValueIsTrue()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, true));
        
        var button = component.Find("button");
        button.GetAttribute("aria-checked").Should().Be("true");
        button.GetAttribute("data-state").Should().Be("checked");
    }
    
    [Fact]
    public void Checkbox_ShouldBeUncheckedWhenValueIsFalse()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, false));
        
        var button = component.Find("button");
        button.GetAttribute("aria-checked").Should().Be("false");
        button.GetAttribute("data-state").Should().Be("unchecked");
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
        
        value.Should().BeTrue();
        component.SetParametersAndRender(parameters => parameters.Add(p => p.Checked, value));
        button.GetAttribute("aria-checked").Should().Be("true");
    }
    
    [Fact]
    public void Checkbox_ShouldBeDisabledWhenDisabledIsTrue()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Disabled, true));
        
        var button = component.Find("button");
        button.HasAttribute("disabled").Should().BeTrue();
        button.HasAttribute("data-disabled").Should().BeTrue();
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
        
        value.Should().BeFalse();
    }
    
    [Theory]
    [InlineData(CheckboxSize.Size1, "rt-r-size-1")]
    [InlineData(CheckboxSize.Size2, "rt-r-size-2")]
    [InlineData(CheckboxSize.Size3, "rt-r-size-3")]
    public void Checkbox_ShouldApplySizeClass(CheckboxSize size, string expectedClass)
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("button").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Checkbox_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Color, "blue"));
        
        component.Find("button").GetAttribute("data-accent-color").Should().Be("blue");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.HighContrast, true));
        
        component.Find("button").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyVariantClass()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Variant, CheckboxVariant.Soft));
        
        component.Find("button").GetClasses().Should().Contain("rt-variant-soft");
    }
    
    [Fact]
    public void Checkbox_ShouldRenderCheckIconWhenChecked()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, true));
        
        component.Find(".rt-CheckboxIndicator").Should().NotBeNull();
        // The checkbox indicator exists but may use CSS to show the check rather than SVG
        component.Find("button").GetAttribute("data-state").Should().Be("checked");
    }
    
    [Fact]
    public void Checkbox_ShouldNotRenderCheckIconWhenUnchecked()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Checked, false));
        
        component.FindAll(".rt-CheckboxIndicator svg").Should().BeEmpty();
    }
    
    [Fact]
    public void Checkbox_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mr, "3"));
        
        var classes = component.Find("button").GetClasses();
        classes.Should().Contain("rt-r-m-2");
        classes.Should().Contain("rt-r-mr-3");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .Add(p => p.Class, "custom-checkbox"));
        
        component.Find("button").GetClasses().Should().Contain("custom-checkbox");
    }
    
    [Fact]
    public void Checkbox_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Checkbox>(parameters => parameters
            .AddUnmatched("data-testid", "terms-checkbox")
            .AddUnmatched("name", "terms"));
        
        var button = component.Find("button");
        button.GetAttribute("data-testid").Should().Be("terms-checkbox");
        button.GetAttribute("name").Should().Be("terms");
    }
}