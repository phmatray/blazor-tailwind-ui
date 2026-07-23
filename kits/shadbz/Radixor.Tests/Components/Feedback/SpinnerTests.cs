using Bunit;
using Shouldly;
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
        spinner.ShouldNotBeNull();
        spinner.GetClasses().ShouldContain("rt-Spinner");
        spinner.GetClasses().ShouldContain("rt-r-size-2");
    }
    
    [Theory]
    [InlineData(SpinnerSize.Size1, "rt-r-size-1")]
    [InlineData(SpinnerSize.Size2, "rt-r-size-2")]
    [InlineData(SpinnerSize.Size3, "rt-r-size-3")]
    public void Spinner_ShouldApplySizeClass(SpinnerSize size, string expectedClass)
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Size, size));
        
        component.Find("span.rt-Spinner").GetClasses().ShouldContain(expectedClass);
    }
    
    [Fact]
    public void Spinner_ShouldRenderInnerElements()
    {
        var component = RenderComponent<Spinner>();
        
        // Should have spinner leaves for animation
        var spinnerLeaves = component.FindAll("span.rt-SpinnerLeaf");
        spinnerLeaves.Count().ShouldBe(8);
    }
    
    [Fact]
    public void Spinner_ShouldHaveAriaLabel()
    {
        var component = RenderComponent<Spinner>();
        
        var spinner = component.Find("span.rt-Spinner");
        spinner.GetAttribute("aria-label").ShouldBe("Loading");
    }
    
    [Fact]
    public void Spinner_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Class, "custom-spinner"));
        
        component.Find("span.rt-Spinner").GetClasses().ShouldContain("custom-spinner");
    }
    
    [Fact]
    public void Spinner_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .AddUnmatched("data-testid", "loading-spinner")
            .AddUnmatched("role", "status"));
        
        var spinner = component.Find("span.rt-Spinner");
        spinner.GetAttribute("data-testid").ShouldBe("loading-spinner");
        spinner.GetAttribute("role").ShouldBe("status");
    }
    
    [Fact]
    public void Spinner_ShouldSetLoadingProp()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Loading, true));
        
        component.Find("span.rt-Spinner").ShouldNotBeNull();
    }
    
    [Fact]
    public void Spinner_ShouldNotRenderWhenLoadingIsFalse()
    {
        var component = RenderComponent<Spinner>(parameters => parameters
            .Add(p => p.Loading, false));
        
        component.FindAll("span.rt-Spinner").ShouldBeEmpty();
    }
}