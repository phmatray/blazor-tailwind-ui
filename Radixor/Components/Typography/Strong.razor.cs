using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Typography;

public enum StrongWrap
{
    Wrap,
    Nowrap,
    Pretty,
    Balance
}

public partial class Strong : SpacingComponentBase
{
    [Parameter] public new RenderFragment? ChildContent { get; set; }
    [Parameter] public bool AsChild { get; set; }
    [Parameter] public bool Truncate { get; set; }
    [Parameter] public StrongWrap? Wrap { get; set; }
    
    private string StrongCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-Strong")
                .AddClass("rt-truncate", Truncate)
                .AddClass(GetWrapClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetWrapClass() => Wrap switch
    {
        StrongWrap.Wrap => "rt-text-wrap",
        StrongWrap.Nowrap => "rt-text-nowrap",
        StrongWrap.Pretty => "rt-text-pretty",
        StrongWrap.Balance => "rt-text-balance",
        _ => ""
    };
}