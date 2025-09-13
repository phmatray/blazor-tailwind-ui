using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Typography;

public enum QuoteWrap
{
    Wrap,
    Nowrap,
    Pretty,
    Balance
}

public partial class Quote : SpacingComponentBase
{
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public QuoteWrap? Wrap { get; set; }
    
    private string QuoteCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Quote")
                .AddClass("rt-truncate", Truncate)
                .AddClass(GetWrapClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetWrapClass() => Wrap switch
    {
        QuoteWrap.Wrap => "rt-text-wrap",
        QuoteWrap.Nowrap => "rt-text-nowrap",
        QuoteWrap.Pretty => "rt-text-pretty",
        QuoteWrap.Balance => "rt-text-balance",
        _ => ""
    };
}