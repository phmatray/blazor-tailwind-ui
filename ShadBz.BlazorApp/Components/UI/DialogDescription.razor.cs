using Microsoft.AspNetCore.Components;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

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