using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Forms;

public partial class TextAreaField : SpacingComponentBase
{
    [Parameter] public TextAreaSize? Size { get; set; } = TextAreaSize.Size2;
    [Parameter] public TextAreaVariant? Variant { get; set; } = TextAreaVariant.Surface;
    [Parameter] public TextAreaResize? Resize { get; set; } = TextAreaResize.Vertical;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    
    // Textarea properties
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }
    [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }
    [Parameter] public EventCallback<ChangeEventArgs> OnInput { get; set; }
    
    // Validation and constraints
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public int? MinLength { get; set; }
    [Parameter] public int? MaxLength { get; set; }
    
    // Layout
    [Parameter] public int? Rows { get; set; }
    [Parameter] public int? Cols { get; set; }
    
    // Text behavior
    [Parameter] public string? AutoComplete { get; set; }
    [Parameter] public bool? SpellCheck { get; set; }
    [Parameter] public string? Wrap { get; set; }
    
    // Identity
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Id { get; set; }
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-TextAreaRoot")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            TextAreaSize.Size1 => "rt-r-size-1",
            TextAreaSize.Size2 => "rt-r-size-2",
            TextAreaSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            TextAreaVariant.Classic => "rt-variant-classic",
            TextAreaVariant.Surface => "rt-variant-surface",
            TextAreaVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetRootAttributes()
    {
        var attributes = new Dictionary<string, object>
        {
            ["class"] = RootCssClass
        };
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        if (Disabled)
            attributes["data-disabled"] = "";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected string? GetTextAreaStyle()
    {
        return Resize switch
        {
            TextAreaResize.None => "resize: none;",
            TextAreaResize.Vertical => "resize: vertical;",
            TextAreaResize.Horizontal => "resize: horizontal;",
            TextAreaResize.Both => "resize: both;",
            _ => null
        };
    }
    
    protected async Task HandleChange(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        
        if (OnChange.HasDelegate)
        {
            await OnChange.InvokeAsync(e);
        }
    }
    
    protected async Task HandleInput(ChangeEventArgs e)
    {
        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync(e);
        }
    }
}

public enum TextAreaSize
{
    Size1,
    Size2,
    Size3
}

public enum TextAreaVariant
{
    Classic,
    Surface,
    Soft
}

public enum TextAreaResize
{
    None,
    Vertical,
    Horizontal,
    Both
}