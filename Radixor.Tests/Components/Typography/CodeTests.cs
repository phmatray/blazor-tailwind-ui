using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class CodeTests : TestContext
{
    [Fact]
    public void Code_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Code>(parameters => parameters
            .AddChildContent("const x = 42;"));
        
        var code = component.Find("code");
        code.Should().NotBeNull();
        code.TextContent.Should().Be("const x = 42;");
        code.GetClasses().Should().Contain("rt-Code");
        code.GetClasses().Should().Contain("rt-r-size-2");
        code.GetClasses().Should().Contain("rt-variant-soft");
    }
    
    [Theory]
    [InlineData(CodeSize.Size1, "rt-r-size-1")]
    [InlineData(CodeSize.Size2, "rt-r-size-2")]
    [InlineData(CodeSize.Size3, "rt-r-size-3")]
    [InlineData(CodeSize.Size4, "rt-r-size-4")]
    [InlineData(CodeSize.Size5, "rt-r-size-5")]
    [InlineData(CodeSize.Size6, "rt-r-size-6")]
    [InlineData(CodeSize.Size7, "rt-r-size-7")]
    [InlineData(CodeSize.Size8, "rt-r-size-8")]
    [InlineData(CodeSize.Size9, "rt-r-size-9")]
    public void Code_ShouldApplySizeClass(CodeSize size, string expectedClass)
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("code"));
        
        component.Find("code").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(CodeVariant.Solid, "rt-variant-solid")]
    [InlineData(CodeVariant.Soft, "rt-variant-soft")]
    [InlineData(CodeVariant.Outline, "rt-variant-outline")]
    [InlineData(CodeVariant.Ghost, "rt-variant-ghost")]
    public void Code_ShouldApplyVariantClass(CodeVariant variant, string expectedClass)
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.Variant, variant)
            .AddChildContent("code"));
        
        component.Find("code").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(CodeWeight.Light, "rt-r-weight-light")]
    [InlineData(CodeWeight.Regular, "rt-r-weight-regular")]
    [InlineData(CodeWeight.Medium, "rt-r-weight-medium")]
    [InlineData(CodeWeight.Bold, "rt-r-weight-bold")]
    public void Code_ShouldApplyWeightClass(CodeWeight weight, string expectedClass)
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.Weight, weight)
            .AddChildContent("code"));
        
        component.Find("code").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Code_ShouldApplyColorAttribute()
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.Color, "cyan")
            .AddChildContent("code"));
        
        component.Find("code").GetAttribute("data-accent-color").Should().Be("cyan");
    }
    
    [Fact]
    public void Code_ShouldApplyHighContrastClass()
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.HighContrast, true)
            .AddChildContent("code"));
        
        component.Find("code").GetClasses().Should().Contain("rt-high-contrast");
    }
    
    [Fact]
    public void Code_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("very long code snippet that should be truncated"));
        
        component.Find("code").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Fact]
    public void Code_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Code>(parameters => parameters
            .Add(p => p.Class, "custom-code")
            .AddChildContent("code"));
        
        component.Find("code").GetClasses().Should().Contain("custom-code");
    }
    
    [Fact]
    public void Code_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Code>(parameters => parameters
            .AddUnmatched("data-language", "javascript")
            .AddUnmatched("title", "JavaScript code")
            .AddChildContent("const x = 42;"));
        
        var code = component.Find("code");
        code.GetAttribute("data-language").Should().Be("javascript");
        code.GetAttribute("title").Should().Be("JavaScript code");
    }
}