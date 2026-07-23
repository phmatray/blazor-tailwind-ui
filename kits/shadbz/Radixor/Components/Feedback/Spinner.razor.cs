using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Feedback;

public partial class Spinner : SpacingComponentBase
{
    [Parameter] public SpinnerSize? Size { get; set; } = SpinnerSize.Size2;
    [Parameter] public bool Loading { get; set; } = true;
    
    private string SpinnerCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Spinner")
                .AddClass(GetSizeClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            SpinnerSize.Size1 => "rt-r-size-1",
            SpinnerSize.Size2 => "rt-r-size-2",
            SpinnerSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetSpinnerAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = SpinnerCssClass;
        attributes["aria-label"] = "Loading";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum SpinnerSize
{
    Size1,
    Size2,
    Size3
}