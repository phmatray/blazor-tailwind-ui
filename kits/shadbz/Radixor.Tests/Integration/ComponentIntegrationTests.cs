using Bunit;
using Shouldly;
using Microsoft.AspNetCore.Components;
using Radixor.Components.DataDisplay;
using Radixor.Components.Feedback;
using Radixor.Components.Forms;
using Radixor.Components.Layout;
using Radixor.Components.Typography;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Integration;

public class ComponentIntegrationTests : TestContext
{
    [Fact]
    public void Card_WithNestedComponents_ShouldRenderCorrectly()
    {
        var component = RenderComponent<Card>(parameters => parameters
            .AddChildContent(builder =>
            {
                // Add Flex layout
                builder.OpenComponent<Flex>(0);
                builder.AddAttribute(1, "Direction", FlexDirection.Column);
                builder.AddAttribute(2, "Gap", "3");
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(b =>
                {
                    // Add Heading
                    b.OpenComponent<Heading>(0);
                    b.AddAttribute(1, "Size", "4");
                    b.AddAttribute(2, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "Card Title")));
                    b.CloseComponent();
                    
                    // Add Text
                    b.OpenComponent<Text>(1);
                    b.AddAttribute(2, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "Card description text")));
                    b.CloseComponent();
                    
                    // Add Button
                    b.OpenComponent<Button>(2);
                    b.AddAttribute(3, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "Click Me")));
                    b.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        
        // Verify structure
        component.Find("div.rt-Card").ShouldNotBeNull();
        component.Find("div.rt-Flex").ShouldNotBeNull();
        component.Find("h1.rt-Heading").TextContent.ShouldBe("Card Title");
        component.Find("span.rt-Text").TextContent.ShouldBe("Card description text");
        component.Find("button.rt-Button").TextContent.ShouldBe("Click Me");
    }
    
    [Fact]
    public void Form_WithMultipleInputs_ShouldRenderCorrectly()
    {
        var checkboxValue = false;
        var switchValue = false;
        var buttonClicked = false;
        
        var component = RenderComponent<Flex>(parameters => parameters
            .Add(p => p.Direction, FlexDirection.Column)
            .Add(p => p.Gap, "4")
            .AddChildContent(builder =>
            {
                // Add Checkbox
                builder.OpenComponent<Checkbox>(0);
                builder.AddAttribute(1, "Checked", checkboxValue);
                builder.AddAttribute(2, "CheckedChanged", EventCallback.Factory.Create<bool>(this, v => checkboxValue = v));
                builder.CloseComponent();
                
                // Add Switch
                builder.OpenComponent<Switch>(3);
                builder.AddAttribute(4, "Checked", switchValue);
                builder.AddAttribute(5, "CheckedChanged", EventCallback.Factory.Create<bool>(this, v => switchValue = v));
                builder.CloseComponent();
                
                // Add Button
                builder.OpenComponent<Button>(6);
                builder.AddAttribute(7, "OnClick", EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => buttonClicked = true));
                builder.AddAttribute(8, "ChildContent", (RenderFragment)(b => b.AddContent(0, "Submit")));
                builder.CloseComponent();
            }));
        
        // Verify all components render
        component.Find("button.rt-CheckboxRoot").ShouldNotBeNull();
        component.Find("button.rt-SwitchRoot").ShouldNotBeNull();
        component.Find("button.rt-Button").ShouldNotBeNull();
        
        // Test interactions
        component.Find("button.rt-CheckboxRoot").Click();
        checkboxValue.ShouldBeTrue();
        
        component.Find("button.rt-SwitchRoot").Click();
        switchValue.ShouldBeTrue();
        
        component.Find("button.rt-Button").Click();
        buttonClicked.ShouldBeTrue();
    }
    
    [Fact]
    public void Grid_WithCards_ShouldRenderResponsiveLayout()
    {
        var component = RenderComponent<Grid>(parameters => parameters
            .Add(p => p.Columns, "3")
            .Add(p => p.Gap, "4")
            .AddChildContent(builder =>
            {
                for (int i = 1; i <= 6; i++)
                {
                    var cardNumber = i; // Capture variable for closure
                    builder.OpenComponent<Card>(i - 1);
                    builder.AddAttribute(i * 10, "ChildContent", (RenderFragment)(b =>
                    {
                        b.OpenComponent<Text>(0);
                        b.AddAttribute(1, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, $"Card {cardNumber}")));
                        b.CloseComponent();
                    }));
                    builder.CloseComponent();
                }
            }));
        
        // Verify grid structure
        component.Find("div.rt-Grid").ShouldNotBeNull();
        component.Find("div.rt-Grid").GetClasses().ShouldContain("rt-r-gtc-3");
        component.Find("div.rt-Grid").GetClasses().ShouldContain("rt-r-gap-4");
        
        // Verify all cards rendered
        var cards = component.FindAll("div.rt-Card");
        cards.Count().ShouldBe(6);
        
        // Verify card content
        for (int i = 1; i <= 6; i++)
        {
            cards[i - 1].TextContent.ShouldBe($"Card {i}");
        }
    }
    
    [Fact]
    public void Avatar_WithBadge_ShouldRenderCorrectly()
    {
        var component = RenderComponent<Box>(parameters => parameters
            .Add(p => p.Display, "inline-flex")
            .AddChildContent(builder =>
            {
                // Add Avatar
                builder.OpenComponent<Avatar>(0);
                builder.AddAttribute(1, "Src", "avatar.jpg");
                builder.AddAttribute(2, "Alt", "User");
                builder.AddAttribute(3, "Fallback", (RenderFragment)(b => b.AddContent(0, "JD")));
                builder.CloseComponent();
                
                // Add Badge positioned on avatar
                builder.OpenComponent<Badge>(4);
                builder.AddAttribute(5, "Color", "green");
                builder.AddAttribute(6, "Class", "absolute-position");
                builder.AddAttribute(7, "ChildContent", (RenderFragment)(b => b.AddContent(0, "Online")));
                builder.CloseComponent();
            }));
        
        // Verify both components render
        component.Find("span.rt-Avatar").ShouldNotBeNull();
        component.Find("span.rt-Badge").ShouldNotBeNull();
        component.Find("span.rt-Badge").TextContent.ShouldBe("Online");
    }
    
    [Fact]
    public void LoadingState_WithSkeletonAndContent_ShouldToggleCorrectly()
    {
        var loading = true;
        
        var component = RenderComponent<Skeleton>(parameters => parameters
            .Add(p => p.Loading, loading)
            .Add(p => p.Width, "200px")
            .Add(p => p.Height, "100px")
            .AddChildContent(builder =>
            {
                builder.OpenComponent<Card>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(b =>
                {
                    b.OpenComponent<Text>(0);
                    b.AddAttribute(1, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "Loaded Content")));
                    b.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        
        // When loading, should show skeleton
        component.Find("span.rt-Skeleton").ShouldNotBeNull();
        component.FindAll("div.rt-Card").ShouldBeEmpty();
        
        // Update loading state
        component.SetParametersAndRender(parameters => parameters
            .Add(p => p.Loading, false));
        
        // When not loading, should show content
        component.FindAll("span.rt-Skeleton").ShouldBeEmpty();
        component.Find("div.rt-Card").ShouldNotBeNull();
        component.Find("span.rt-Text").TextContent.ShouldBe("Loaded Content");
    }
    
    [Fact]
    public void Typography_CombinedElements_ShouldRenderCorrectly()
    {
        var component = RenderComponent<Blockquote>(parameters => parameters
            .AddChildContent(builder =>
            {
                builder.OpenComponent<Text>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(b =>
                {
                    b.AddContent(0, "This is a ");
                    
                    b.OpenComponent<Strong>(1);
                    b.AddAttribute(2, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "strong")));
                    b.CloseComponent();
                    
                    b.AddContent(3, " statement with ");
                    
                    b.OpenComponent<Em>(4);
                    b.AddAttribute(5, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "emphasis")));
                    b.CloseComponent();
                    
                    b.AddContent(6, " and some ");
                    
                    b.OpenComponent<Code>(7);
                    b.AddAttribute(8, "ChildContent", (RenderFragment)(b2 => b2.AddContent(0, "code")));
                    b.CloseComponent();
                    
                    b.AddContent(9, ".");
                }));
                builder.CloseComponent();
            }));
        
        // Verify all typography elements render
        component.Find("blockquote.rt-Blockquote").ShouldNotBeNull();
        component.Find("span.rt-Text").ShouldNotBeNull();
        component.Find("strong.rt-Strong").TextContent.ShouldBe("strong");
        component.Find("em.rt-Em").TextContent.ShouldBe("emphasis");
        component.Find("code.rt-Code").TextContent.ShouldBe("code");
        
        // Verify complete text
        component.Find("blockquote").TextContent.ShouldBe("This is a strong statement with emphasis and some code.");
    }
}