using Microsoft.AspNetCore.Components;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class Kbd : SpacingComponentBase
{
    [Parameter] public new RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public KbdSize? Size { get; set; }
    
    private string KbdCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Kbd")
                .AddClass(GetSizeClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass() => Size switch
    {
        KbdSize.Size1 => "rt-r-size-1",
        KbdSize.Size2 => "rt-r-size-2",
        KbdSize.Size3 => "rt-r-size-3",
        KbdSize.Size4 => "rt-r-size-4",
        KbdSize.Size5 => "rt-r-size-5",
        KbdSize.Size6 => "rt-r-size-6",
        KbdSize.Size7 => "rt-r-size-7",
        KbdSize.Size8 => "rt-r-size-8",
        KbdSize.Size9 => "rt-r-size-9",
        _ => ""
    };
}

public enum KbdSize
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