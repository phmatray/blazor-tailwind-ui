using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class KbdTests : TestContext
{
    [Fact]
    public void Kbd_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Kbd>(parameters => parameters
            .AddChildContent("Ctrl"));
        
        var kbd = component.Find("kbd.rt-Kbd");
        kbd.Should().NotBeNull();
        kbd.TextContent.Should().Be("Ctrl");
        kbd.GetClasses().Should().Contain("rt-Kbd");
        // Default size is not set in the component, so no size class is applied by default
    }
    
    [Theory]
    [InlineData(KbdSize.Size1, "rt-r-size-1")]
    [InlineData(KbdSize.Size2, "rt-r-size-2")]
    [InlineData(KbdSize.Size3, "rt-r-size-3")]
    [InlineData(KbdSize.Size4, "rt-r-size-4")]
    [InlineData(KbdSize.Size5, "rt-r-size-5")]
    [InlineData(KbdSize.Size6, "rt-r-size-6")]
    [InlineData(KbdSize.Size7, "rt-r-size-7")]
    [InlineData(KbdSize.Size8, "rt-r-size-8")]
    [InlineData(KbdSize.Size9, "rt-r-size-9")]
    public void Kbd_ShouldApplySizeClass(KbdSize size, string expectedClass)
    {
        var component = RenderComponent<Kbd>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Key"));
        
        component.Find("kbd.rt-Kbd").GetClasses().Should().Contain(expectedClass);
    }
    
    // Note: KbdVariant is not implemented in the current component
    // This test has been removed until the feature is implemented
    
    // Note: Color and HighContrast properties are not implemented in the current Kbd component
    // These tests have been removed until the features are implemented
    
    [Fact]
    public void Kbd_ShouldRenderKeyboardShortcut()
    {
        var component = RenderComponent<Kbd>(parameters => parameters
            .AddChildContent("⌘+S"));
        
        var kbd = component.Find("kbd.rt-Kbd");
        kbd.TextContent.Should().Be("⌘+S");
    }
    
    [Fact]
    public void Kbd_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Kbd>(parameters => parameters
            .Add(p => p.Class, "custom-kbd")
            .AddChildContent("Key"));
        
        component.Find("kbd.rt-Kbd").GetClasses().Should().Contain("custom-kbd");
    }
    
    [Fact]
    public void Kbd_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Kbd>(parameters => parameters
            .AddUnmatched("data-testid", "kbd-element")
            .AddUnmatched("title", "Keyboard shortcut")
            .AddChildContent("Enter"));
        
        var kbd = component.Find("kbd.rt-Kbd");
        kbd.GetAttribute("data-testid").Should().Be("kbd-element");
        kbd.GetAttribute("title").Should().Be("Keyboard shortcut");
    }
}