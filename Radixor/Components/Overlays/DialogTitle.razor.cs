using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Overlays;

public partial class DialogTitle : SpacingComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? As { get; set; } = "h2";
    
    private string TitleCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-DialogTitle")
                .AddClass("rt-r-size-5")
                .AddClass("rt-r-mb-3")
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
}