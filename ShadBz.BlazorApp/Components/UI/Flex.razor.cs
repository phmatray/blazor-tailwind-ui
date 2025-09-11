using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Flex : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    // Core Flex properties
    [Parameter] public string As { get; set; } = "div"; // "div" or "span"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public FlexDisplay? Display { get; set; }
    [Parameter] public FlexDirection? Direction { get; set; }
    [Parameter] public FlexAlign? Align { get; set; }
    [Parameter] public FlexJustify? Justify { get; set; }
    [Parameter] public FlexWrap? Wrap { get; set; }
    
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
            var builder = new CssBuilder("rt-Flex")
                .AddClass(GetDisplayClass())
                .AddClass(GetDirectionClass())
                .AddClass(GetAlignClass())
                .AddClass(GetJustifyClass())
                .AddClass(GetWrapClass())
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
            FlexDisplay.None => "rt-r-display-none",
            FlexDisplay.InlineFlex => "rt-r-display-inline-flex",
            FlexDisplay.Flex => "rt-r-display-flex",
            _ => ""
        };
    }
    
    private string GetDirectionClass()
    {
        return Direction switch
        {
            FlexDirection.Row => "rt-r-fd-row",
            FlexDirection.Column => "rt-r-fd-column",
            FlexDirection.RowReverse => "rt-r-fd-row-reverse",
            FlexDirection.ColumnReverse => "rt-r-fd-column-reverse",
            _ => ""
        };
    }
    
    private string GetAlignClass()
    {
        return Align switch
        {
            FlexAlign.Start => "rt-r-ai-start",
            FlexAlign.Center => "rt-r-ai-center",
            FlexAlign.End => "rt-r-ai-end",
            FlexAlign.Baseline => "rt-r-ai-baseline",
            FlexAlign.Stretch => "rt-r-ai-stretch",
            _ => ""
        };
    }
    
    private string GetJustifyClass()
    {
        return Justify switch
        {
            FlexJustify.Start => "rt-r-jc-start",
            FlexJustify.Center => "rt-r-jc-center",
            FlexJustify.End => "rt-r-jc-end",
            FlexJustify.Between => "rt-r-jc-space-between",
            _ => ""
        };
    }
    
    private string GetWrapClass()
    {
        return Wrap switch
        {
            FlexWrap.NoWrap => "rt-r-fw-nowrap",
            FlexWrap.Wrap => "rt-r-fw-wrap",
            FlexWrap.WrapReverse => "rt-r-fw-wrap-reverse",
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
    
    protected Dictionary<string, object> GetFlexAttributes()
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

public enum FlexDisplay
{
    None,
    InlineFlex,
    Flex
}

public enum FlexDirection
{
    Row,
    Column,
    RowReverse,
    ColumnReverse
}

public enum FlexAlign
{
    Start,
    Center,
    End,
    Baseline,
    Stretch
}

public enum FlexJustify
{
    Start,
    Center,
    End,
    Between
}

public enum FlexWrap
{
    NoWrap,
    Wrap,
    WrapReverse
}