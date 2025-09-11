using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Grid : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
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
    
    // Gap properties
    [Parameter] public string? Gap { get; set; }
    [Parameter] public string? GapX { get; set; }
    [Parameter] public string? GapY { get; set; }
    
    // Layout properties (inherited from Box)
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public string? MaxWidth { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? MinHeight { get; set; }
    [Parameter] public string? MaxHeight { get; set; }
    [Parameter] public string? Position { get; set; }
    [Parameter] public string? Inset { get; set; }
    [Parameter] public string? Top { get; set; }
    [Parameter] public string? Right { get; set; }
    [Parameter] public string? Bottom { get; set; }
    [Parameter] public string? Left { get; set; }
    [Parameter] public string? Overflow { get; set; }
    [Parameter] public string? OverflowX { get; set; }
    [Parameter] public string? OverflowY { get; set; }
    [Parameter] public string? FlexBasis { get; set; }
    [Parameter] public string? FlexShrink { get; set; }
    [Parameter] public string? FlexGrow { get; set; }
    [Parameter] public string? GridColumn { get; set; }
    [Parameter] public string? GridColumnStart { get; set; }
    [Parameter] public string? GridColumnEnd { get; set; }
    [Parameter] public string? GridRow { get; set; }
    [Parameter] public string? GridRowStart { get; set; }
    [Parameter] public string? GridRowEnd { get; set; }
    
    // Margin properties
    [Parameter] public string? M { get; set; }
    [Parameter] public string? Mx { get; set; }
    [Parameter] public string? My { get; set; }
    [Parameter] public string? Mt { get; set; }
    [Parameter] public string? Mr { get; set; }
    [Parameter] public string? Mb { get; set; }
    [Parameter] public string? Ml { get; set; }
    
    // Padding properties
    [Parameter] public string? P { get; set; }
    [Parameter] public string? Px { get; set; }
    [Parameter] public string? Py { get; set; }
    [Parameter] public string? Pt { get; set; }
    [Parameter] public string? Pr { get; set; }
    [Parameter] public string? Pb { get; set; }
    [Parameter] public string? Pl { get; set; }
    
    // CSS customization
    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
    
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
                .AddClass(GetGapClasses())
                .AddClass(GetLayoutClasses())
                .AddClass(GetMarginClasses())
                .AddClass(GetPaddingClasses())
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
        if (IsGridCountValue(Columns))
            return $"rt-r-gtc-{Columns}";
        
        // Custom value
        return "rt-r-gtc";
    }
    
    private string GetRowsClass()
    {
        if (string.IsNullOrEmpty(Rows)) return "";
        
        // Check if it's a numeric value (1-9)
        if (IsGridCountValue(Rows))
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
    
    private string GetGapClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(Gap))
        {
            if (IsSpaceScaleValue(Gap))
                classes.Add($"rt-r-gap-{Gap}");
            else
                classes.Add("rt-r-gap");
        }
        
        if (!string.IsNullOrEmpty(GapX))
        {
            if (IsSpaceScaleValue(GapX))
                classes.Add($"rt-r-cg-{GapX}");
            else
                classes.Add("rt-r-cg");
        }
        
        if (!string.IsNullOrEmpty(GapY))
        {
            if (IsSpaceScaleValue(GapY))
                classes.Add($"rt-r-rg-{GapY}");
            else
                classes.Add("rt-r-rg");
        }
        
        return string.Join(" ", classes);
    }
    
    private string GetLayoutClasses()
    {
        var classes = new List<string>();
        
        // Width
        if (!string.IsNullOrEmpty(Width))
            classes.Add(IsSpaceScaleValue(Width) ? $"rt-r-w-{Width}" : "rt-r-w");
        if (!string.IsNullOrEmpty(MinWidth))
            classes.Add(IsSpaceScaleValue(MinWidth) ? $"rt-r-min-w-{MinWidth}" : "rt-r-min-w");
        if (!string.IsNullOrEmpty(MaxWidth))
            classes.Add(IsSpaceScaleValue(MaxWidth) ? $"rt-r-max-w-{MaxWidth}" : "rt-r-max-w");
        
        // Height
        if (!string.IsNullOrEmpty(Height))
            classes.Add(IsSpaceScaleValue(Height) ? $"rt-r-h-{Height}" : "rt-r-h");
        if (!string.IsNullOrEmpty(MinHeight))
            classes.Add(IsSpaceScaleValue(MinHeight) ? $"rt-r-min-h-{MinHeight}" : "rt-r-min-h");
        if (!string.IsNullOrEmpty(MaxHeight))
            classes.Add(IsSpaceScaleValue(MaxHeight) ? $"rt-r-max-h-{MaxHeight}" : "rt-r-max-h");
        
        // Position
        if (!string.IsNullOrEmpty(Position))
            classes.Add($"rt-r-position-{Position}");
        
        // Inset
        if (!string.IsNullOrEmpty(Inset))
            classes.Add(IsSpaceScaleValue(Inset) ? $"rt-r-inset-{Inset}" : "rt-r-inset");
        if (!string.IsNullOrEmpty(Top))
            classes.Add(IsSpaceScaleValue(Top) ? $"rt-r-top-{Top}" : "rt-r-top");
        if (!string.IsNullOrEmpty(Right))
            classes.Add(IsSpaceScaleValue(Right) ? $"rt-r-right-{Right}" : "rt-r-right");
        if (!string.IsNullOrEmpty(Bottom))
            classes.Add(IsSpaceScaleValue(Bottom) ? $"rt-r-bottom-{Bottom}" : "rt-r-bottom");
        if (!string.IsNullOrEmpty(Left))
            classes.Add(IsSpaceScaleValue(Left) ? $"rt-r-left-{Left}" : "rt-r-left");
        
        // Overflow
        if (!string.IsNullOrEmpty(Overflow))
            classes.Add($"rt-r-overflow-{Overflow}");
        if (!string.IsNullOrEmpty(OverflowX))
            classes.Add($"rt-r-ox-{OverflowX}");
        if (!string.IsNullOrEmpty(OverflowY))
            classes.Add($"rt-r-oy-{OverflowY}");
        
        // Flex properties
        if (!string.IsNullOrEmpty(FlexBasis))
            classes.Add(IsSpaceScaleValue(FlexBasis) ? $"rt-r-fb-{FlexBasis}" : "rt-r-fb");
        if (!string.IsNullOrEmpty(FlexShrink))
            classes.Add($"rt-r-fs-{FlexShrink}");
        if (!string.IsNullOrEmpty(FlexGrow))
            classes.Add($"rt-r-fg-{FlexGrow}");
        
        // Grid properties
        if (!string.IsNullOrEmpty(GridColumn))
            classes.Add("rt-r-gc");
        if (!string.IsNullOrEmpty(GridColumnStart))
            classes.Add($"rt-r-gcs-{GridColumnStart}");
        if (!string.IsNullOrEmpty(GridColumnEnd))
            classes.Add($"rt-r-gce-{GridColumnEnd}");
        if (!string.IsNullOrEmpty(GridRow))
            classes.Add("rt-r-gr");
        if (!string.IsNullOrEmpty(GridRowStart))
            classes.Add($"rt-r-grs-{GridRowStart}");
        if (!string.IsNullOrEmpty(GridRowEnd))
            classes.Add($"rt-r-gre-{GridRowEnd}");
        
        return string.Join(" ", classes);
    }
    
    private string GetMarginClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(M))
            classes.Add($"rt-r-m-{M}");
        if (!string.IsNullOrEmpty(Mx))
            classes.Add($"rt-r-mx-{Mx}");
        if (!string.IsNullOrEmpty(My))
            classes.Add($"rt-r-my-{My}");
        if (!string.IsNullOrEmpty(Mt))
            classes.Add($"rt-r-mt-{Mt}");
        if (!string.IsNullOrEmpty(Mr))
            classes.Add($"rt-r-mr-{Mr}");
        if (!string.IsNullOrEmpty(Mb))
            classes.Add($"rt-r-mb-{Mb}");
        if (!string.IsNullOrEmpty(Ml))
            classes.Add($"rt-r-ml-{Ml}");
        
        return string.Join(" ", classes);
    }
    
    private string GetPaddingClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(P))
            classes.Add($"rt-r-p-{P}");
        if (!string.IsNullOrEmpty(Px))
            classes.Add($"rt-r-px-{Px}");
        if (!string.IsNullOrEmpty(Py))
            classes.Add($"rt-r-py-{Py}");
        if (!string.IsNullOrEmpty(Pt))
            classes.Add($"rt-r-pt-{Pt}");
        if (!string.IsNullOrEmpty(Pr))
            classes.Add($"rt-r-pr-{Pr}");
        if (!string.IsNullOrEmpty(Pb))
            classes.Add($"rt-r-pb-{Pb}");
        if (!string.IsNullOrEmpty(Pl))
            classes.Add($"rt-r-pl-{Pl}");
        
        return string.Join(" ", classes);
    }
    
    protected string GetInlineStyles()
    {
        var styles = new List<string>();
        
        // Add grid template areas
        if (!string.IsNullOrEmpty(Areas))
            styles.Add($"--grid-template-areas: {Areas}");
        
        // Add custom columns value
        if (!string.IsNullOrEmpty(Columns) && !IsGridCountValue(Columns))
        {
            var columnsValue = ParseGridValue(Columns);
            styles.Add($"--grid-template-columns: {columnsValue}");
        }
        
        // Add custom rows value
        if (!string.IsNullOrEmpty(Rows) && !IsGridCountValue(Rows))
        {
            var rowsValue = ParseGridValue(Rows);
            styles.Add($"--grid-template-rows: {rowsValue}");
        }
        
        // Add custom gap values
        if (!string.IsNullOrEmpty(Gap) && !IsSpaceScaleValue(Gap))
            styles.Add($"--gap: {Gap}");
        if (!string.IsNullOrEmpty(GapX) && !IsSpaceScaleValue(GapX))
            styles.Add($"--column-gap: {GapX}");
        if (!string.IsNullOrEmpty(GapY) && !IsSpaceScaleValue(GapY))
            styles.Add($"--row-gap: {GapY}");
        
        // Add custom layout values
        if (!string.IsNullOrEmpty(Width) && !IsSpaceScaleValue(Width))
            styles.Add($"--width: {Width}");
        if (!string.IsNullOrEmpty(MinWidth) && !IsSpaceScaleValue(MinWidth))
            styles.Add($"--min-width: {MinWidth}");
        if (!string.IsNullOrEmpty(MaxWidth) && !IsSpaceScaleValue(MaxWidth))
            styles.Add($"--max-width: {MaxWidth}");
        if (!string.IsNullOrEmpty(Height) && !IsSpaceScaleValue(Height))
            styles.Add($"--height: {Height}");
        if (!string.IsNullOrEmpty(MinHeight) && !IsSpaceScaleValue(MinHeight))
            styles.Add($"--min-height: {MinHeight}");
        if (!string.IsNullOrEmpty(MaxHeight) && !IsSpaceScaleValue(MaxHeight))
            styles.Add($"--max-height: {MaxHeight}");
        
        // Position values
        if (!string.IsNullOrEmpty(Inset) && !IsSpaceScaleValue(Inset))
            styles.Add($"--inset: {Inset}");
        if (!string.IsNullOrEmpty(Top) && !IsSpaceScaleValue(Top))
            styles.Add($"--top: {Top}");
        if (!string.IsNullOrEmpty(Right) && !IsSpaceScaleValue(Right))
            styles.Add($"--right: {Right}");
        if (!string.IsNullOrEmpty(Bottom) && !IsSpaceScaleValue(Bottom))
            styles.Add($"--bottom: {Bottom}");
        if (!string.IsNullOrEmpty(Left) && !IsSpaceScaleValue(Left))
            styles.Add($"--left: {Left}");
        
        // Flex values
        if (!string.IsNullOrEmpty(FlexBasis) && !IsSpaceScaleValue(FlexBasis))
            styles.Add($"--flex-basis: {FlexBasis}");
        
        // Grid values
        if (!string.IsNullOrEmpty(GridColumn))
            styles.Add($"--grid-column: {GridColumn}");
        if (!string.IsNullOrEmpty(GridRow))
            styles.Add($"--grid-row: {GridRow}");
        
        // Add user-provided styles
        if (!string.IsNullOrEmpty(Style))
            styles.Add(Style);
        
        return string.Join("; ", styles);
    }
    
    private bool IsSpaceScaleValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        
        // Check if it's a number 0-9 or negative number -1 to -9
        return value == "0" || 
               (value.Length == 1 && char.IsDigit(value[0])) ||
               (value.Length == 2 && value[0] == '-' && char.IsDigit(value[1]));
    }
    
    private bool IsGridCountValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        
        // Check if it's a single digit 1-9
        return value.Length == 1 && char.IsDigit(value[0]) && value[0] != '0';
    }
    
    private string ParseGridValue(string value)
    {
        // If it's just a number, convert to repeat syntax
        if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
        {
            return $"repeat({value}, minmax(0, 1fr))";
        }
        
        // Otherwise return as-is
        return value;
    }
    
    protected Dictionary<string, object> GetGridAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = CssClass;
        
        var inlineStyles = GetInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected Dictionary<string, object> GetAllAttributes()
    {
        var attributes = new Dictionary<string, object>();
        
        // Merge with additional attributes
        if (AdditionalAttributes != null)
        {
            foreach (var attr in AdditionalAttributes)
            {
                attributes[attr.Key] = attr.Value;
            }
        }
        
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