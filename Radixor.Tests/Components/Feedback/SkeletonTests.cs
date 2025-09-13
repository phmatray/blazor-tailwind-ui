using Bunit;
using FluentAssertions;
using Radixor.Components.Feedback;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Feedback;

public class SkeletonTests : TestContext
{
    [Fact]
    public void Skeleton_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Skeleton>();
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.Should().NotBeNull();
        skeleton.GetClasses().Should().Contain("rt-Skeleton");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyWidthStyle()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Width, "200px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("style").Should().Contain("width: 200px");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyHeightStyle()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Height, "50px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("style").Should().Contain("height: 50px");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyMinWidthStyle()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.MinWidth, "100px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("style").Should().Contain("min-width: 100px");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyMaxWidthStyle()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.MaxWidth, "300px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("style").Should().Contain("max-width: 300px");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyMinHeightStyle()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.MinHeight, "20px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("style").Should().Contain("min-height: 20px");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyMaxHeightStyle()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.MaxHeight, "100px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("style").Should().Contain("max-height: 100px");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyMultipleStyles()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Width, "200px")
            .Add(p => p.Height, "50px"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        var style = skeleton.GetAttribute("style");
        style.Should().Contain("width: 200px");
        style.Should().Contain("height: 50px");
    }
    
    [Fact]
    public void Skeleton_ShouldSetLoadingProp()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Loading, true));
        
        component.Find("span.rt-Skeleton").Should().NotBeNull();
    }
    
    [Fact]
    public void Skeleton_ShouldRenderChildrenWhenNotLoading()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Loading, false)
            .AddChildContent("Content loaded"));
        
        component.FindAll("span.rt-Skeleton").Should().BeEmpty();
        component.Markup.Should().Contain("Content loaded");
    }
    
    [Fact]
    public void Skeleton_ShouldHideChildrenWhenLoading()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Loading, true)
            .AddChildContent("Content"));
        
        component.Find("span.rt-Skeleton").Should().NotBeNull();
        component.Markup.Should().NotContain("Content");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Class, "custom-skeleton"));
        
        component.Find("span.rt-Skeleton").GetClasses().Should().Contain("custom-skeleton");
    }
    
    [Fact]
    public void Skeleton_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Skeleton>(parameters => parameters
            .AddUnmatched("data-testid", "skeleton-loader")
            .AddUnmatched("aria-busy", "true"));
        
        var skeleton = component.Find("span.rt-Skeleton");
        skeleton.GetAttribute("data-testid").Should().Be("skeleton-loader");
        skeleton.GetAttribute("aria-busy").Should().Be("true");
    }
}