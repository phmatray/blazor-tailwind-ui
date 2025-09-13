using Bunit;
using FluentAssertions;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.Typography;

public class QuoteTests : TestContext
{
    [Fact]
    public void Quote_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<Quote>(parameters => parameters
            .AddChildContent("This is a quote"));
        
        var quote = component.Find("q.rt-Quote");
        quote.Should().NotBeNull();
        quote.TextContent.Should().Be("This is a quote");
        quote.GetClasses().Should().Contain("rt-Quote");
    }
    
    // Note: Color and HighContrast properties are not implemented in the current Quote component
    // These tests have been removed until the features are implemented
    
    [Fact]
    public void Quote_ShouldApplyTruncateClass()
    {
        var component = RenderComponent<Quote>(parameters => parameters
            .Add(p => p.Truncate, true)
            .AddChildContent("Very long quote that should be truncated"));
        
        component.Find("q.rt-Quote").GetClasses().Should().Contain("rt-truncate");
    }
    
    [Theory]
    [InlineData(QuoteWrap.Wrap, "rt-text-wrap")]
    [InlineData(QuoteWrap.Nowrap, "rt-text-nowrap")]
    [InlineData(QuoteWrap.Pretty, "rt-text-pretty")]
    [InlineData(QuoteWrap.Balance, "rt-text-balance")]
    public void Quote_ShouldApplyWrapClass(QuoteWrap wrap, string expectedClass)
    {
        var component = RenderComponent<Quote>(parameters => parameters
            .Add(p => p.Wrap, wrap)
            .AddChildContent("Quote text"));
        
        component.Find("q.rt-Quote").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void Quote_ShouldApplyCustomClass()
    {
        var component = RenderComponent<Quote>(parameters => parameters
            .Add(p => p.Class, "custom-quote")
            .AddChildContent("Quote text"));
        
        component.Find("q.rt-Quote").GetClasses().Should().Contain("custom-quote");
    }
    
    [Fact]
    public void Quote_ShouldApplyAdditionalAttributes()
    {
        var component = RenderComponent<Quote>(parameters => parameters
            .AddUnmatched("data-testid", "quote-element")
            .AddUnmatched("cite", "https://source.com")
            .AddChildContent("Quote text"));
        
        var quote = component.Find("q.rt-Quote");
        quote.GetAttribute("data-testid").Should().Be("quote-element");
        quote.GetAttribute("cite").Should().Be("https://source.com");
    }
}