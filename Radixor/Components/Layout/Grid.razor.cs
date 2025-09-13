using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Layout;

public partial class Grid : GapComponentBase, IAsChildSupport
{
    // Core Grid properties
    [Parameter] public string As { get; set; } = "div"; // "div" or "span"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public GridDisplay? Display { get; set; }
    [Parameter] public string? Areas { get; set; } // Grid template areas
    [Parameter] public string? Columns { get; set; } // "1"-"9" or custom value like "1fr 2fr"
    [Parameter] public string? Rows { get; set; } // "1"-"9" or custom value
    [Parameter] public GridFlow? Flow { get; set; }
    [Parameter] public GridAlign? Align { get; set; }
    [Parameter] public GridJustify? Justify { get; set; }
    [Parameter] public GridAlignContent? AlignContent { get; set; }
    [Parameter] public GridJustifyItems? JustifyItems { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Grid")
                .AddClass(GetDisplayClass())
                .AddClass(GetAreasClass())
                .AddClass(GetColumnsClass())
                .AddClass(GetRowsClass())
                .AddClass(GetFlowClass())
                .AddClass(GetAlignClass())
                .AddClass(GetJustifyClass())
                .AddClass(GetAlignContentClass())
                .AddClass(GetJustifyItemsClass())
                .AddClass(GetGapClasses())        // From GapComponentBase
                .AddClass(GetLayoutClasses())     // From LayoutComponentBase
                .AddClass(GetMarginClasses())     // From SpacingComponentBase
                .AddClass(GetPaddingClasses())    // From SpacingComponentBase
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetDisplayClass()
    {
        return Display switch
        {
            GridDisplay.None => "rt-r-display-none",
            GridDisplay.InlineGrid => "rt-r-display-inline-grid",
            GridDisplay.Grid => "rt-r-display-grid",
            _ => ""
        };
    }
    
    private string GetAreasClass()
    {
        return !string.IsNullOrEmpty(Areas) ? "rt-r-gta" : "";
    }
    
    private string GetColumnsClass()
    {
        if (string.IsNullOrEmpty(Columns)) return "";
        
        // Check if it's a numeric value (1-9)
        if (CssHelper.IsGridCountValue(Columns))
            return $"rt-r-gtc-{Columns}";
        
        // Custom value
        return "rt-r-gtc";
    }
    
    private string GetRowsClass()
    {
        if (string.IsNullOrEmpty(Rows)) return "";
        
        // Check if it's a numeric value (1-9)
        if (CssHelper.IsGridCountValue(Rows))
            return $"rt-r-gtr-{Rows}";
        
        // Custom value
        return "rt-r-gtr";
    }
    
    private string GetFlowClass()
    {
        return Flow switch
        {
            GridFlow.Row => "rt-r-gaf-row",
            GridFlow.Column => "rt-r-gaf-column",
            GridFlow.Dense => "rt-r-gaf-dense",
            GridFlow.RowDense => "rt-r-gaf-row-dense",
            GridFlow.ColumnDense => "rt-r-gaf-column-dense",
            _ => ""
        };
    }
    
    private string GetAlignClass()
    {
        return Align switch
        {
            GridAlign.Start => "rt-r-ai-start",
            GridAlign.Center => "rt-r-ai-center",
            GridAlign.End => "rt-r-ai-end",
            GridAlign.Baseline => "rt-r-ai-baseline",
            GridAlign.Stretch => "rt-r-ai-stretch",
            _ => ""
        };
    }
    
    private string GetJustifyClass()
    {
        return Justify switch
        {
            GridJustify.Start => "rt-r-jc-start",
            GridJustify.Center => "rt-r-jc-center",
            GridJustify.End => "rt-r-jc-end",
            GridJustify.Between => "rt-r-jc-space-between",
            _ => ""
        };
    }
    
    private string GetAlignContentClass()
    {
        return AlignContent switch
        {
            GridAlignContent.Start => "rt-r-ac-start",
            GridAlignContent.Center => "rt-r-ac-center",
            GridAlignContent.End => "rt-r-ac-end",
            GridAlignContent.Baseline => "rt-r-ac-baseline",
            GridAlignContent.Between => "rt-r-ac-space-between",
            GridAlignContent.Around => "rt-r-ac-space-around",
            GridAlignContent.Evenly => "rt-r-ac-space-evenly",
            GridAlignContent.Stretch => "rt-r-ac-stretch",
            _ => ""
        };
    }
    
    private string GetJustifyItemsClass()
    {
        return JustifyItems switch
        {
            GridJustifyItems.Start => "rt-r-ji-start",
            GridJustifyItems.Center => "rt-r-ji-center",
            GridJustifyItems.End => "rt-r-ji-end",
            GridJustifyItems.Baseline => "rt-r-ji-baseline",
            GridJustifyItems.Stretch => "rt-r-ji-stretch",
            _ => ""
        };
    }
    
    protected string GetInlineStyles()
    {
        var additionalStyles = GetGapStyles(); // From GapComponentBase
        
        // Add Grid-specific custom values
        if (!string.IsNullOrEmpty(Areas))
            additionalStyles["--grid-template-areas"] = Areas;
        
        if (!string.IsNullOrEmpty(Columns) && !CssHelper.IsGridCountValue(Columns))
        {
            var columnsValue = CssHelper.ParseGridValue(Columns);
            additionalStyles["--grid-template-columns"] = columnsValue;
        }
        
        if (!string.IsNullOrEmpty(Rows) && !CssHelper.IsGridCountValue(Rows))
        {
            var rowsValue = CssHelper.ParseGridValue(Rows);
            additionalStyles["--grid-template-rows"] = rowsValue;
        }
        
        return BuildInlineStyles(additionalStyles); // From LayoutComponentBase
    }
    
    protected Dictionary<string, object> GetGridAttributes()
    {
        var attributes = GetAllAttributes(); // From LayoutComponentBase
        attributes["class"] = CssClass;
        
        var inlineStyles = GetInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum GridDisplay
{
    None,
    InlineGrid,
    Grid
}

public enum GridFlow
{
    Row,
    Column,
    Dense,
    RowDense,
    ColumnDense
}

public enum GridAlign
{
    Start,
    Center,
    End,
    Baseline,
    Stretch
}

public enum GridJustify
{
    Start,
    Center,
    End,
    Between
}

public enum GridAlignContent
{
    Start,
    Center,
    End,
    Baseline,
    Between,
    Around,
    Evenly,
    Stretch
}

public enum GridJustifyItems
{
    Start,
    Center,
    End,
    Baseline,
    Stretch
}