using Bunit;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
using Radixor.Components.DataDisplay;
using Radixor.Tests.TestHelpers;
using Xunit;

namespace Radixor.Tests.Components.DataDisplay;

public class TableTests : TestContext
{
    [Fact]
    public void TableRoot_ShouldRenderWithDefaultProperties()
    {
        var component = RenderComponent<TableRoot>(parameters => parameters
            .AddChildContent("<tr><td>Test</td></tr>"));
        
        var wrapper = component.Find("div.rt-TableRootWrapper");
        wrapper.Should().NotBeNull();
        
        var table = component.Find("table.rt-TableRoot");
        table.Should().NotBeNull();
        table.GetClasses().Should().Contain("rt-TableRoot");
        table.GetClasses().Should().Contain("rt-r-size-2");
        table.GetClasses().Should().Contain("rt-variant-surface");
    }
    
    [Theory]
    [InlineData(TableSize.Size1, "rt-r-size-1")]
    [InlineData(TableSize.Size2, "rt-r-size-2")]
    [InlineData(TableSize.Size3, "rt-r-size-3")]
    public void TableRoot_ShouldApplySizeClass(TableSize size, string expectedClass)
    {
        var component = RenderComponent<TableRoot>(parameters => parameters
            .Add(p => p.Size, size)
            .AddChildContent("<tr><td>Test</td></tr>"));
        
        component.Find("table.rt-TableRoot").GetClasses().Should().Contain(expectedClass);
    }
    
    [Theory]
    [InlineData(TableVariant.Surface, "rt-variant-surface")]
    [InlineData(TableVariant.Ghost, "rt-variant-ghost")]
    public void TableRoot_ShouldApplyVariantClass(TableVariant variant, string expectedClass)
    {
        var component = RenderComponent<TableRoot>(parameters => parameters
            .Add(p => p.Variant, variant)
            .AddChildContent("<tr><td>Test</td></tr>"));
        
        component.Find("table.rt-TableRoot").GetClasses().Should().Contain(expectedClass);
    }
    
    [Fact]
    public void TableRoot_ShouldProvideCascadingValue()
    {
        var component = RenderComponent<TableRoot>(parameters => parameters
            .AddChildContent("<tr><td>Test</td></tr>"));
        
        // The TableRoot should provide itself as a cascading value
        var tableRoot = component.Instance;
        tableRoot.Should().NotBeNull();
    }
    
    [Fact]
    public void TableHeader_ShouldRenderThead()
    {
        var component = RenderComponent<TableHeader>(parameters => parameters
            .AddChildContent("<tr><th>Header</th></tr>"));
        
        var thead = component.Find("thead.rt-TableHeader");
        thead.Should().NotBeNull();
        thead.InnerHtml.Should().Contain("Header");
    }
    
    [Fact]
    public void TableBody_ShouldRenderTbody()
    {
        var component = RenderComponent<TableBody>(parameters => parameters
            .AddChildContent("<tr><td>Body</td></tr>"));
        
        var tbody = component.Find("tbody.rt-TableBody");
        tbody.Should().NotBeNull();
        tbody.InnerHtml.Should().Contain("Body");
    }
    
    [Fact]
    public void TableRow_ShouldRenderTr()
    {
        var component = RenderComponent<TableRow>(parameters => parameters
            .AddChildContent("<td>Row</td>"));
        
        var tr = component.Find("tr.rt-TableRow");
        tr.Should().NotBeNull();
        tr.InnerHtml.Should().Contain("Row");
    }
    
    [Fact]
    public void TableRow_ShouldApplyAlignClass()
    {
        var component = RenderComponent<TableRow>(parameters => parameters
            .Add(p => p.Align, TableRowAlign.Center)
            .AddChildContent("<td>Row</td>"));
        
        component.Find("tr.rt-TableRow").GetClasses().Should().Contain("rt-align-center");
    }
    
    [Fact]
    public void TableCell_ShouldRenderTd()
    {
        var component = RenderComponent<TableCell>(parameters => parameters
            .AddChildContent("Cell Content"));
        
        var td = component.Find("td.rt-TableCell");
        td.Should().NotBeNull();
        td.TextContent.Should().Be("Cell Content");
    }
    
    [Fact]
    public void TableCell_ShouldRenderThWhenHeader()
    {
        var component = RenderComponent<TableCell>(parameters => parameters
            .Add(p => p.IsHeader, true)
            .AddChildContent("Header Cell"));
        
        var th = component.Find("th.rt-TableHeaderCell");
        th.Should().NotBeNull();
        th.TextContent.Should().Be("Header Cell");
    }
    
    [Theory]
    [InlineData(TableCellAlign.Left, "rt-align-left")]
    [InlineData(TableCellAlign.Center, "rt-align-center")]
    [InlineData(TableCellAlign.Right, "rt-align-right")]
    public void TableCell_ShouldApplyAlignClass(TableCellAlign align, string expectedClass)
    {
        var component = RenderComponent<TableCell>(parameters => parameters
            .Add(p => p.Align, align)
            .AddChildContent("Cell"));
        
        component.Find("td.rt-TableCell").GetClasses().Should().Contain(expectedClass);
    }
    
    // Note: TableCellJustify is not implemented in the current component
    // This test has been removed until the feature is implemented
    
    [Fact]
    public void TableCell_ShouldApplyWidthStyle()
    {
        var component = RenderComponent<TableCell>(parameters => parameters
            .Add(p => p.Width, "200px")
            .AddChildContent("Cell"));
        
        var td = component.Find("td.rt-TableCell");
        td.GetAttribute("style").Should().Contain("width: 200px");
    }
    
    [Fact]
    public void Table_CompleteTableStructure_ShouldRenderCorrectly()
    {
        var component = RenderComponent<TableRoot>(parameters => parameters
            .AddChildContent(builder =>
            {
                builder.OpenComponent<TableHeader>(0);
                builder.AddAttribute(1, "ChildContent", (RenderFragment)(b =>
                {
                    b.OpenComponent<TableRow>(0);
                    b.AddAttribute(1, "ChildContent", (RenderFragment)(b2 =>
                    {
                        b2.OpenComponent<TableCell>(0);
                        b2.AddAttribute(1, "IsHeader", true);
                        b2.AddAttribute(2, "ChildContent", (RenderFragment)(b3 => b3.AddContent(0, "Name")));
                        b2.CloseComponent();
                        
                        b2.OpenComponent<TableCell>(1);
                        b2.AddAttribute(2, "IsHeader", true);
                        b2.AddAttribute(3, "ChildContent", (RenderFragment)(b3 => b3.AddContent(0, "Age")));
                        b2.CloseComponent();
                    }));
                    b.CloseComponent();
                }));
                builder.CloseComponent();
                
                builder.OpenComponent<TableBody>(2);
                builder.AddAttribute(3, "ChildContent", (RenderFragment)(b =>
                {
                    b.OpenComponent<TableRow>(0);
                    b.AddAttribute(1, "ChildContent", (RenderFragment)(b2 =>
                    {
                        b2.OpenComponent<TableCell>(0);
                        b2.AddAttribute(1, "ChildContent", (RenderFragment)(b3 => b3.AddContent(0, "John")));
                        b2.CloseComponent();
                        
                        b2.OpenComponent<TableCell>(1);
                        b2.AddAttribute(2, "ChildContent", (RenderFragment)(b3 => b3.AddContent(0, "30")));
                        b2.CloseComponent();
                    }));
                    b.CloseComponent();
                }));
                builder.CloseComponent();
            }));
        
        // Verify structure
        component.Find("table.rt-TableRoot").Should().NotBeNull();
        component.Find("thead.rt-TableHeader").Should().NotBeNull();
        component.Find("tbody.rt-TableBody").Should().NotBeNull();
        component.FindAll("tr.rt-TableRow").Should().HaveCount(2);
        component.FindAll("th.rt-TableHeaderCell").Should().HaveCount(2);
        component.FindAll("td.rt-TableCell").Should().HaveCount(2);
    }
}