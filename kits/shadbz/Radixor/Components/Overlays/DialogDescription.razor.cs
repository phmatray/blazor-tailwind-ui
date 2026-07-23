using Microsoft.AspNetCore.Components;
using Radixor.Components.Base;

using Radixor.Components.Common;
namespace Radixor.Components.Overlays;

public partial class DialogDescription : SpacingComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? As { get; set; } = "p";
    
    private string DescriptionCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-DialogDescription")
                .AddClass("rt-r-size-2")
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
}