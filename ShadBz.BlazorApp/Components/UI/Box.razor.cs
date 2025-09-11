using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Box : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    // Core Box properties
    [Parameter] public string As { get; set; } = "div"; // "div" or "span"
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public string? Display { get; set; }
    
    // Layout properties - Size
    [Parameter] public string? Width { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public string? MaxWidth { get; set; }
    [Parameter] public string? Height { get; set; }
    [Parameter] public string? MinHeight { get; set; }
    [Parameter] public string? MaxHeight { get; set; }
    
    // Layout properties - Position
    [Parameter] public string? Position { get; set; }
    [Parameter] public string? Inset { get; set; }
    [Parameter] public string? Top { get; set; }
    [Parameter] public string? Right { get; set; }
    [Parameter] public string? Bottom { get; set; }
    [Parameter] public string? Left { get; set; }
    
    // Layout properties - Overflow
    [Parameter] public string? Overflow { get; set; }
    [Parameter] public string? OverflowX { get; set; }
    [Parameter] public string? OverflowY { get; set; }
    
    // Layout properties - Flexbox
    [Parameter] public string? FlexBasis { get; set; }
    [Parameter] public string? FlexShrink { get; set; }
    [Parameter] public string? FlexGrow { get; set; }
    [Parameter] public string? AlignSelf { get; set; }
    [Parameter] public string? JustifySelf { get; set; }
    
    // Layout properties - Grid
    [Parameter] public string? GridArea { get; set; }
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
    
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }
    
    protected string CssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Box")
                .AddClass(GetDisplayClass())
                .AddClass(GetLayoutClasses())
                .AddClass(GetMarginClasses())
                .AddClass(GetPaddingClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetDisplayClass()
    {
        return !string.IsNullOrEmpty(Display) ? $"rt-r-display-{Display}" : "";
    }
    
    private string GetLayoutClasses()
    {
        var classes = new List<string>();
        
        // Size classes
        if (!string.IsNullOrEmpty(Width))
            classes.Add(IsSpaceScaleValue(Width) ? $"rt-r-w-{Width}" : "rt-r-w");
        if (!string.IsNullOrEmpty(MinWidth))
            classes.Add(IsSpaceScaleValue(MinWidth) ? $"rt-r-min-w-{MinWidth}" : "rt-r-min-w");
        if (!string.IsNullOrEmpty(MaxWidth))
            classes.Add(IsSpaceScaleValue(MaxWidth) ? $"rt-r-max-w-{MaxWidth}" : "rt-r-max-w");
        if (!string.IsNullOrEmpty(Height))
            classes.Add(IsSpaceScaleValue(Height) ? $"rt-r-h-{Height}" : "rt-r-h");
        if (!string.IsNullOrEmpty(MinHeight))
            classes.Add(IsSpaceScaleValue(MinHeight) ? $"rt-r-min-h-{MinHeight}" : "rt-r-min-h");
        if (!string.IsNullOrEmpty(MaxHeight))
            classes.Add(IsSpaceScaleValue(MaxHeight) ? $"rt-r-max-h-{MaxHeight}" : "rt-r-max-h");
        
        // Position classes
        if (!string.IsNullOrEmpty(Position))
            classes.Add($"rt-r-position-{Position}");
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
        
        // Overflow classes
        if (!string.IsNullOrEmpty(Overflow))
            classes.Add($"rt-r-overflow-{Overflow}");
        if (!string.IsNullOrEmpty(OverflowX))
            classes.Add($"rt-r-overflow-x-{OverflowX}");
        if (!string.IsNullOrEmpty(OverflowY))
            classes.Add($"rt-r-overflow-y-{OverflowY}");
        
        // Flexbox classes
        if (!string.IsNullOrEmpty(FlexBasis))
            classes.Add(IsSpaceScaleValue(FlexBasis) ? $"rt-r-fb-{FlexBasis}" : "rt-r-fb");
        if (!string.IsNullOrEmpty(FlexShrink))
            classes.Add($"rt-r-fs-{FlexShrink}");
        if (!string.IsNullOrEmpty(FlexGrow))
            classes.Add($"rt-r-fg-{FlexGrow}");
        if (!string.IsNullOrEmpty(AlignSelf))
            classes.Add($"rt-r-as-{AlignSelf}");
        if (!string.IsNullOrEmpty(JustifySelf))
            classes.Add($"rt-r-js-{JustifySelf}");
        
        // Grid classes
        if (!string.IsNullOrEmpty(GridArea))
            classes.Add("rt-r-ga");
        if (!string.IsNullOrEmpty(GridColumn))
            classes.Add($"rt-r-gc-{GridColumn}");
        if (!string.IsNullOrEmpty(GridColumnStart))
            classes.Add($"rt-r-gcs-{GridColumnStart}");
        if (!string.IsNullOrEmpty(GridColumnEnd))
            classes.Add($"rt-r-gce-{GridColumnEnd}");
        if (!string.IsNullOrEmpty(GridRow))
            classes.Add($"rt-r-gr-{GridRow}");
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
            classes.Add(IsSpaceScaleValue(M) ? $"rt-r-m-{M}" : "rt-r-m");
        if (!string.IsNullOrEmpty(Mx))
            classes.Add(IsSpaceScaleValue(Mx) ? $"rt-r-mx-{Mx}" : "rt-r-mx");
        if (!string.IsNullOrEmpty(My))
            classes.Add(IsSpaceScaleValue(My) ? $"rt-r-my-{My}" : "rt-r-my");
        if (!string.IsNullOrEmpty(Mt))
            classes.Add(IsSpaceScaleValue(Mt) ? $"rt-r-mt-{Mt}" : "rt-r-mt");
        if (!string.IsNullOrEmpty(Mr))
            classes.Add(IsSpaceScaleValue(Mr) ? $"rt-r-mr-{Mr}" : "rt-r-mr");
        if (!string.IsNullOrEmpty(Mb))
            classes.Add(IsSpaceScaleValue(Mb) ? $"rt-r-mb-{Mb}" : "rt-r-mb");
        if (!string.IsNullOrEmpty(Ml))
            classes.Add(IsSpaceScaleValue(Ml) ? $"rt-r-ml-{Ml}" : "rt-r-ml");
        
        return string.Join(" ", classes);
    }
    
    private string GetPaddingClasses()
    {
        var classes = new List<string>();
        
        if (!string.IsNullOrEmpty(P))
            classes.Add(IsSpaceScaleValue(P) ? $"rt-r-p-{P}" : "rt-r-p");
        if (!string.IsNullOrEmpty(Px))
            classes.Add(IsSpaceScaleValue(Px) ? $"rt-r-px-{Px}" : "rt-r-px");
        if (!string.IsNullOrEmpty(Py))
            classes.Add(IsSpaceScaleValue(Py) ? $"rt-r-py-{Py}" : "rt-r-py");
        if (!string.IsNullOrEmpty(Pt))
            classes.Add(IsSpaceScaleValue(Pt) ? $"rt-r-pt-{Pt}" : "rt-r-pt");
        if (!string.IsNullOrEmpty(Pr))
            classes.Add(IsSpaceScaleValue(Pr) ? $"rt-r-pr-{Pr}" : "rt-r-pr");
        if (!string.IsNullOrEmpty(Pb))
            classes.Add(IsSpaceScaleValue(Pb) ? $"rt-r-pb-{Pb}" : "rt-r-pb");
        if (!string.IsNullOrEmpty(Pl))
            classes.Add(IsSpaceScaleValue(Pl) ? $"rt-r-pl-{Pl}" : "rt-r-pl");
        
        return string.Join(" ", classes);
    }
    
    private bool IsSpaceScaleValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        
        // Check if it's a space scale value (0-9) or negative space scale (-1 to -9)
        return value == "0" || 
               (value.Length == 1 && char.IsDigit(value[0])) ||
               (value.Length == 2 && value[0] == '-' && char.IsDigit(value[1]));
    }
    
    protected Dictionary<string, object> GetBoxAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = CssClass;
        return attributes;
    }
    
    protected Dictionary<string, object> GetAllAttributes()
    {
        var attributes = new Dictionary<string, object>();
        
        // Add style for custom property values
        var styles = new List<string>();
        
        // Layout custom properties
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
            
        // Position custom properties
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
            
        // Margin custom properties
        if (!string.IsNullOrEmpty(M) && !IsSpaceScaleValue(M))
            styles.Add($"--m: {M}");
        if (!string.IsNullOrEmpty(Mx) && !IsSpaceScaleValue(Mx))
        {
            styles.Add($"--ml: {Mx}");
            styles.Add($"--mr: {Mx}");
        }
        if (!string.IsNullOrEmpty(My) && !IsSpaceScaleValue(My))
        {
            styles.Add($"--mt: {My}");
            styles.Add($"--mb: {My}");
        }
        if (!string.IsNullOrEmpty(Mt) && !IsSpaceScaleValue(Mt))
            styles.Add($"--mt: {Mt}");
        if (!string.IsNullOrEmpty(Mr) && !IsSpaceScaleValue(Mr))
            styles.Add($"--mr: {Mr}");
        if (!string.IsNullOrEmpty(Mb) && !IsSpaceScaleValue(Mb))
            styles.Add($"--mb: {Mb}");
        if (!string.IsNullOrEmpty(Ml) && !IsSpaceScaleValue(Ml))
            styles.Add($"--ml: {Ml}");
            
        // Padding custom properties
        if (!string.IsNullOrEmpty(P) && !IsSpaceScaleValue(P))
            styles.Add($"--p: {P}");
        if (!string.IsNullOrEmpty(Px) && !IsSpaceScaleValue(Px))
        {
            styles.Add($"--pl: {Px}");
            styles.Add($"--pr: {Px}");
        }
        if (!string.IsNullOrEmpty(Py) && !IsSpaceScaleValue(Py))
        {
            styles.Add($"--pt: {Py}");
            styles.Add($"--pb: {Py}");
        }
        if (!string.IsNullOrEmpty(Pt) && !IsSpaceScaleValue(Pt))
            styles.Add($"--pt: {Pt}");
        if (!string.IsNullOrEmpty(Pr) && !IsSpaceScaleValue(Pr))
            styles.Add($"--pr: {Pr}");
        if (!string.IsNullOrEmpty(Pb) && !IsSpaceScaleValue(Pb))
            styles.Add($"--pb: {Pb}");
        if (!string.IsNullOrEmpty(Pl) && !IsSpaceScaleValue(Pl))
            styles.Add($"--pl: {Pl}");
        
        if (styles.Any())
            attributes["style"] = string.Join("; ", styles);
        
        // Merge with additional attributes
        if (AdditionalAttributes != null)
        {
            foreach (var attr in AdditionalAttributes)
            {
                if (attr.Key == "style" && attributes.ContainsKey("style"))
                {
                    // Merge styles
                    attributes["style"] = $"{attributes["style"]}; {attr.Value}";
                }
                else
                {
                    attributes[attr.Key] = attr.Value;
                }
            }
        }
        
        return attributes;
    }
}