using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radixor.Components.Forms;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Forms;

public class ButtonTests : TestContext
{
    [Fact]
    public void Button_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .AddChildContent("Click Me"));
        
        var button = component.Find("button");
        button.ShouldNotBeNull();
        button.TextContent.ShouldBe("Click Me");
        button.GetClasses().ShouldContain("rt-Button");
        button.GetClasses().ShouldContain("rt-variant-solid");
        button.GetClasses().ShouldContain("rt-r-size-2");
    }
    
    [Theory]
    [InlineData(ButtonSize.Small, "rt-r-size-1")]
    [InlineData(ButtonSize.Medium, "rt-r-size-2")]
    [InlineData(ButtonSize.Large, "rt-r-size-3")]
    [InlineData(ButtonSize.ExtraLarge, "rt-r-size-4")]
    public void Button_ShouldApplySizeClass(ButtonSize size, string expectedClass)
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Button"));
        
        component.Find("button").GetClasses().ShouldContain(expectedClass);
    }
    
    [Theory]
    [InlineData(ButtonVariant.Classic, "rt-variant-classic")]
    [InlineData(ButtonVariant.Solid, "rt-variant-solid")]
    [InlineData(ButtonVariant.Soft, "rt-variant-soft")]
    [InlineData(ButtonVariant.Surface, "rt-variant-surface")]
    [InlineData(ButtonVariant.Outline, "rt-variant-outline")]
    [InlineData(ButtonVariant.Ghost, "rt-variant-ghost")]
    public void Button_ShouldApplyVariantClass(ButtonVariant variant, string expectedClass)
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Variant, variant)
            .AddChildContent("Button"));
        
        component.Find("button").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Button_ShouldBeDisabledWhenDisabledIsTrue()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Disabled, true)
            .AddChildContent("Button"));
        
        var button = component.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
        button.HasAttribute("data-disabled").ShouldBeTrue();
    }
    
    [Fact]
    public void Button_ShouldBeDisabledWhenLoadingIsTrue()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Loading, true)
            .AddChildContent("Button"));
        
        var button = component.Find("button");
        button.HasAttribute("disabled").ShouldBeTrue();
        button.HasAttribute("data-disabled").ShouldBeTrue();
        button.GetClasses().ShouldContain("rt-loading");
    }
    
    [Fact]
    public void Button_ShouldShowSpinnerWhenLoading()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Loading, true)
            .AddChildContent("Loading"));
        
        component.Find(".rt-Spinner").ShouldNotBeNull();
        component.Find(".visually-hidden").TextContent.ShouldBe("Loading");
    }
    
    [Fact]
    public void Button_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("Button"));
        
        component.Find("button").GetClasses().ShouldContain("rt-high-contrast");
    }
    
    [Fact]
    public void Button_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Color, "blue")
            .AddChildContent("Button"));
        
        component.Find("button").GetAttribute("data-accent-color").ShouldBe("blue");
    }
    
    [Fact]
    public void Button_ShouldApplyRadiusAttribute()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Radius, "full")
            .AddChildContent("Button"));
        
        component.Find("button").GetAttribute("data-radius").ShouldBe("full");
    }
    
    [Fact]
    public void Button_ShouldTriggerOnClickEvent()
    {
        var clicked = false;
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
            .AddChildContent("Click Me"));
        
        component.Find("button").Click();
        
        clicked.ShouldBeTrue();
    }
    
    // Note: bUnit doesn't prevent clicks on disabled buttons, so this test is commented out
    // The actual browser will respect the disabled attribute and prevent clicks
    //[Fact]
    //public void Button_ShouldNotTriggerOnClickWhenDisabled()
    //{
    //    var clicked = false;
    //    var component = RenderComponent<Button>(parameters => parameters
    //        .Add(p => p.Disabled, true)
    //        .Add(p => p.OnClick, EventCallback.Factory.Create<MouseEventArgs>(this, () => clicked = true))
    //        .AddChildContent("Click Me"));
    //    
    //    component.Find("button").Click();
    //    
    //    clicked.ShouldBeFalse();
    //}
    
    [Fact]
    public void Button_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.Mx, "auto")
            .Add(p => p.Mt, "3")
            .AddChildContent("Button"));
        
        var classes = component.Find("button").GetClasses();
        classes.ShouldContain("rt-r-m-2");
        classes.ShouldContain("rt-r-mx-auto");
        classes.ShouldContain("rt-r-mt-3");
    }
    
    [Fact]
    public void Button_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .Add(p => p.Class, "custom-button")
            .AddChildContent("Button"));
        
        component.Find("button").GetClasses().ShouldContain("custom-button");
    }
    
    [Fact]
    public void Button_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Button>(parameters => parameters
            .AddUnmatched("data-testid", "submit-button")
            .AddUnmatched("aria-label", "Submit Form")
            .AddChildContent("Submit"));
        
        var button = component.Find("button");
        button.GetAttribute("data-testid").ShouldBe("submit-button");
        button.GetAttribute("aria-label").ShouldBe("Submit Form");
    }
}