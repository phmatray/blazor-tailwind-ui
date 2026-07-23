using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Typography;

public enum BlockquoteSize
{
    Size1,
    Size2,
    Size3,
    Size4,
    Size5,
    Size6,
    Size7,
    Size8,
    Size9
}

public enum BlockquoteWeight
{
    Light,
    Regular,
    Medium,
    Bold
}

public enum BlockquoteWrap
{
    Wrap,
    Nowrap,
    Pretty,
    Balance
}

public partial class Blockquote : SpacingComponentBase
{
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public BlockquoteSize? Size { get; set; } = BlockquoteSize.Size2;
    [Parameter] public BlockquoteWeight? Weight { get; set; }
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public BlockquoteWrap? Wrap { get; set; }
    
    private string BlockquoteCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Blockquote")
                .AddClass(GetSizeClass())
                .AddClass(GetWeightClass())
                .AddClass("rt-high-contrast", HighContrast)
                .AddClass("rt-truncate", Truncate)
                .AddClass(GetWrapClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass() => Size switch
    {
        BlockquoteSize.Size1 => "rt-r-size-1",
        BlockquoteSize.Size2 => "rt-r-size-2",
        BlockquoteSize.Size3 => "rt-r-size-3",
        BlockquoteSize.Size4 => "rt-r-size-4",
        BlockquoteSize.Size5 => "rt-r-size-5",
        BlockquoteSize.Size6 => "rt-r-size-6",
        BlockquoteSize.Size7 => "rt-r-size-7",
        BlockquoteSize.Size8 => "rt-r-size-8",
        BlockquoteSize.Size9 => "rt-r-size-9",
        _ => ""
    };
    
    private string GetWeightClass() => Weight switch
    {
        BlockquoteWeight.Light => "rt-r-weight-light",
        BlockquoteWeight.Regular => "rt-r-weight-regular",
        BlockquoteWeight.Medium => "rt-r-weight-medium",
        BlockquoteWeight.Bold => "rt-r-weight-bold",
        _ => ""
    };
    
    private string GetWrapClass() => Wrap switch
    {
        BlockquoteWrap.Wrap => "rt-text-wrap",
        BlockquoteWrap.Nowrap => "rt-text-nowrap",
        BlockquoteWrap.Pretty => "rt-text-pretty",
        BlockquoteWrap.Balance => "rt-text-balance",
        _ => ""
    };
}