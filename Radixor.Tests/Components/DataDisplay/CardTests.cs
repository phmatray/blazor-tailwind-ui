using Bunit;
using FluentAssertions;
using Radixor.Components.DataDisplay;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.DataDisplay;

public class CardTests : TestContext
{
    [Fact]
    public void Card_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .AddChildContent("Card content"));
        
        var card = component.Find("div.rt-Card");
        card.Should().NotBeNull();
        card.TextContent.Should().Be("Card content");
        card.GetClasses().Should().Contain("rt-Card");
        card.GetClasses().Should().Contain("rt-r-size-1");
        card.GetClasses().Should().Contain("rt-variant-surface");
    }
    
    [Theory]
    [InlineData(CardSize.Size1, "rt-r-size-1")]
    [InlineData(CardSize.Size2, "rt-r-size-2")]
    [InlineData(CardSize.Size3, "rt-r-size-3")]
    [InlineData(CardSize.Size4, "rt-r-size-4")]
    [InlineData(CardSize.Size5, "rt-r-size-5")]
    public void Card_ShouldApplySizeClass(CardSize size, string expectedClass)
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("Card"));
        
        component.Find("div.rt-Card").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(CardVariant.Surface, "rt-variant-surface")]
    [InlineData(CardVariant.Classic, "rt-variant-classic")]
    [InlineData(CardVariant.Ghost, "rt-variant-ghost")]
    public void Card_ShouldApplyVariantClass(CardVariant variant, string expectedClass)
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.Variant, variant)
            .AddChildContent("Card"));
        
        component.Find("div.rt-Card").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Card_ShouldRenderAsButtonWhenAsChild()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.AsChild, true)
            .AddChildContent("<button>Click me</button>"));
        
        // When AsChild is true, it should not render the Card wrapper
        component.FindAll("div.rt-Card").Should().BeEmpty();
    }
    
    [Fact]
    public void Card_ShouldApplyPaddingClasses()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.P, "3")
            .Add(p => p.Px, "4")
            .Add(p => p.Py, "2")
            .AddChildContent("Card"));
        
        var classes = component.Find("div.rt-Card").GetClasses();
        classes.Should().Contain("rt-r-p-3");
        classes.Should().Contain("rt-r-px-4");
        classes.Should().Contain("rt-r-py-2");
    }
    
    [Fact]
    public void Card_ShouldApplyMarginClasses()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.M, "2")
            .Add(p => p.My, "3")
            .AddChildContent("Card"));
        
        var classes = component.Find("div.rt-Card").GetClasses();
        classes.Should().Contain("rt-r-m-2");
        classes.Should().Contain("rt-r-my-3");
    }
    
    [Fact]
    public void Card_ShouldApplyLayoutClasses()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.Width, "300px")
            .Add(p => p.Height, "200px")
            .AddChildContent("Card"));
        
        var style = component.Find("div.rt-Card").GetAttribute("style");
        style.Should().Contain("width: 300px");
        style.Should().Contain("height: 200px");
    }
    
    [Fact]
    public void Card_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .Add(p => p.Class, "custom-card")
            .AddChildContent("Card"));
        
        component.Find("div.rt-Card").GetClasses().Should().Contain("custom-card");
    }
    
    [Fact]
    public void Card_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .AddUnmatched("data-testid", "product-card")
            .AddUnmatched("role", "article")
            .AddChildContent("Product"));
        
        var card = component.Find("div.rt-Card");
        card.GetAttribute("data-testid").Should().Be("product-card");
        card.GetAttribute("role").Should().Be("article");
    }
}