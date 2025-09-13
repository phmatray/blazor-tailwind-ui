using Bunit;
using FluentAssertions;
using Radixor.Components.Feedback;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Feedback;

public class SpinnerTests : TestContext
{
    [Fact]
    public void Spinner_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Spinner>();
        
        var spinner = component.Find("span.rt-Spinner");
        spinner.Should().NotBeNull();
        spinner.GetClasses().Should().Contain("rt-Spinner");
        spinner.GetClasses().Should().Contain("rt-r-size-2");
    }
    
    [Theory]
    [InlineData(SpinnerSize.Size1, "rt-r-size-1")]
    [InlineData(SpinnerSize.Size2, "rt-r-size-2")]
    [InlineData(SpinnerSize.Size3, "rt-r-size-3")]
    public void Spinner_ShouldApplySizeClass(SpinnerSize size, string expectedClass)
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("span.rt-Spinner").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Spinner_ShouldRenderInnerElements()
    {
        var component = RenderComponent<Spinner>();
        
        // Should have spinner leaves for animation
        var spinnerLeaves = component.FindAll("span.rt-SpinnerLeaf");
        spinnerLeaves.Should().HaveCount(8);
    }
    
    [Fact]
    public void Spinner_ShouldHaveAriaLabel()
    {
        var component = RenderComponent<Spinner>();
        
        var spinner = component.Find("span.rt-Spinner");
        spinner.GetAttribute("aria-label").Should().Be("Loading");
    }
    
    [Fact]
    public void Spinner_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Class, "custom-spinner"));
        
        component.Find("span.rt-Spinner").GetClasses().Should().Contain("custom-spinner");
    }
    
    [Fact]
    public void Spinner_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .AddUnmatched("data-testid", "loading-spinner")
            .AddUnmatched("role", "status"));
        
        var spinner = component.Find("span.rt-Spinner");
        spinner.GetAttribute("data-testid").Should().Be("loading-spinner");
        spinner.GetAttribute("role").Should().Be("status");
    }
    
    [Fact]
    public void Spinner_ShouldSetLoadingProp()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Loading, true));
        
        component.Find("span.rt-Spinner").Should().NotBeNull();
    }
    
    [Fact]
    public void Spinner_ShouldNotRenderWhenLoadingIsFalse()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Loading, false));
        
        component.FindAll("span.rt-Spinner").Should().BeEmpty();
    }
}