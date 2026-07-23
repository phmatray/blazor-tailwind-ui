using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Typography;

public enum EmWrap
{
    Wrap,
    Nowrap,
    Pretty,
    Balance
}

public partial class Em : SpacingComponentBase
{
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public EmWrap? Wrap { get; set; }
    
    private string EmCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Em")
                .AddClass("rt-truncate", Truncate)
                .AddClass(GetWrapClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetWrapClass() => Wrap switch
    {
        EmWrap.Wrap => "rt-text-wrap",
        EmWrap.Nowrap => "rt-text-nowrap",
        EmWrap.Pretty => "rt-text-pretty",
        EmWrap.Balance => "rt-text-balance",
        _ => ""
    };
}