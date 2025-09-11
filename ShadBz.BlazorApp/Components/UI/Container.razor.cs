using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Container : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    // Core Container properties
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public ContainerSize? Size { get; set; } = ContainerSize.Size4; // Default size="4"
    [Parameter] public ContainerDisplay? Display { get; set; }
    [Parameter] public ContainerAlign? Align { get; set; }
    
    // Width properties for inner container
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public string? MaxWidth { get; set; }
    
    // Height properties for inner container
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? MinHeight { get; set; }
    [Parameter] public string? MaxHeight { get; set; }
    
    // Layout properties
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
    
    // Padding properties (for outer container)
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
            var builder = new CssBuilder("rt-Container")
                .AddClass(GetSizeClass())
                .AddClass(GetDisplayClass())
                .AddClass(GetAlignClass())
                .AddClass(GetLayoutClasses())
                .AddClass(GetMarginClasses())
                .AddClass(GetPaddingClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    protected string InnerCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-ContainerInner")
                .AddClass(GetInnerWidthClasses())
                .AddClass(GetInnerHeightClasses());
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            ContainerSize.Size1 => "rt-r-size-1",
            ContainerSize.Size2 => "rt-r-size-2",
            ContainerSize.Size3 => "rt-r-size-3",
            ContainerSize.Size4 => "rt-r-size-4",
            _ => ""
        };
    }
    
    private string GetDisplayClass()
    {
        return Display switch
        {
            ContainerDisplay.None => "rt-r-display-none",
            ContainerDisplay.Initial => "rt-r-display-flex",
            _ => ""
        };
    }
    
    private string GetAlignClass()
    {
        return Align switch
        {
            ContainerAlign.Left => "rt-r-ai-start",
            ContainerAlign.Center => "rt-r-ai-center",
            ContainerAlign.Right => "rt-r-ai-end",
            _ => ""
        };
    }
    
    private string GetInnerWidthClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(Width))
            classes.Add(IsSpaceScaleValue(Width) ? $"rt-r-w-{Width}" : "rt-r-w");
        if (!string.IsNullOrEmpty(MinWidth))
            classes.Add(IsSpaceScaleValue(MinWidth) ? $"rt-r-min-w-{MinWidth}" : "rt-r-min-w");
        if (!string.IsNullOrEmpty(MaxWidth))
            classes.Add(IsSpaceScaleValue(MaxWidth) ? $"rt-r-max-w-{MaxWidth}" : "rt-r-max-w");
        
        return string.Join(" ", classes);
    }
    
    private string GetInnerHeightClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(Height))
            classes.Add(IsSpaceScaleValue(Height) ? $"rt-r-h-{Height}" : "rt-r-h");
        if (!string.IsNullOrEmpty(MinHeight))
            classes.Add(IsSpaceScaleValue(MinHeight) ? $"rt-r-min-h-{MinHeight}" : "rt-r-min-h");
        if (!string.IsNullOrEmpty(MaxHeight))
            classes.Add(IsSpaceScaleValue(MaxHeight) ? $"rt-r-max-h-{MaxHeight}" : "rt-r-max-h");
        
        return string.Join(" ", classes);
    }
    
    private string GetLayoutClasses()
    {
        var classes = new List<string>();
        
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
    
    protected string GetInnerStyles()
    {
        var styles = new List<string>();
        
        // Add custom width values
        if (!string.IsNullOrEmpty(Width) && !IsSpaceScaleValue(Width))
            styles.Add($"--width: {Width}");
        if (!string.IsNullOrEmpty(MinWidth) && !IsSpaceScaleValue(MinWidth))
            styles.Add($"--min-width: {MinWidth}");
        if (!string.IsNullOrEmpty(MaxWidth) && !IsSpaceScaleValue(MaxWidth))
            styles.Add($"--max-width: {MaxWidth}");
        
        // Add custom height values
        if (!string.IsNullOrEmpty(Height) && !IsSpaceScaleValue(Height))
            styles.Add($"--height: {Height}");
        if (!string.IsNullOrEmpty(MinHeight) && !IsSpaceScaleValue(MinHeight))
            styles.Add($"--min-height: {MinHeight}");
        if (!string.IsNullOrEmpty(MaxHeight) && !IsSpaceScaleValue(MaxHeight))
            styles.Add($"--max-height: {MaxHeight}");
        
        return string.Join("; ", styles);
    }
    
    protected string GetOuterStyles()
    {
        var styles = new List<string>();
        
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
    
    protected Dictionary<string, object> GetContainerAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = CssClass;
        
        var outerStyles = GetOuterStyles();
        if (!string.IsNullOrEmpty(outerStyles))
            attributes["style"] = outerStyles;
        
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

public enum ContainerSize
{
    Size1, // 448px max-width
    Size2, // 688px max-width
    Size3, // 880px max-width
    Size4  // 1136px max-width (default)
}

public enum ContainerDisplay
{
    None,
    Initial // Maps to "flex" in CSS
}

public enum ContainerAlign
{
    Left,   // Maps to "start" in CSS
    Center,
    Right   // Maps to "end" in CSS
}