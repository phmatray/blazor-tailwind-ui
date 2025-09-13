using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

using Radixor.Components.Common;
namespace Radixor.Components.Overlays;

public enum TooltipSide
{
    Top,
    Right,
    Bottom,
    Left
}

public enum TooltipAlign
{
    Start,
    Center,
    End
}

public partial class TooltipContent : ComponentBase, IDisposable
{
    [Inject] private IJSRuntime? JSRuntime { get; set; }
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public TooltipSide? Side { get; set; } = TooltipSide.Top;
    [Parameter] public TooltipAlign? Align { get; set; } = TooltipAlign.Center;
    [Parameter] public int SideOffset { get; set; } = 5;
    [Parameter] public int AlignOffset { get; set; } = 0;
    [Parameter] public bool AvoidCollisions { get; set; } = true;
    
    [CascadingParameter] private TooltipRoot? TooltipRoot { get; set; }
    
    private DotNetObjectReference<TooltipContent>? _dotNetRef;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && JSRuntime != null)
        {
            _dotNetRef = DotNetObjectReference.Create(this);
        }
        
        await base.OnAfterRenderAsync(firstRender);
    }
    
    private string ContentCssClass => new CssBuilder("rt-TooltipContent")
        .AddClass(GetSideClass())
        .AddClass(GetAlignClass())
        .Build();
    
    private string GetSideClass() => Side switch
    {
        TooltipSide.Top => "rt-variant-top",
        TooltipSide.Right => "rt-variant-right",
        TooltipSide.Bottom => "rt-variant-bottom",
        TooltipSide.Left => "rt-variant-left",
        _ => "rt-variant-top"
    };
    
    private string GetAlignClass() => Align switch
    {
        TooltipAlign.Start => "rt-align-start",
        TooltipAlign.Center => "rt-align-center",
        TooltipAlign.End => "rt-align-end",
        _ => "rt-align-center"
    };
    
    private string GetPositionStyle()
    {
        // Simplified positioning - in production, you'd calculate based on trigger element
        var sideOffset = SideOffset != 0 ? $"margin-top: -{SideOffset}px;" : "";
        var alignOffset = AlignOffset != 0 ? $"margin-left: {AlignOffset}px;" : "";
        return $"position: absolute; z-index: 100; {sideOffset} {alignOffset}";
    }
    
    public void Dispose()
    {
        _dotNetRef?.Dispose();
    }
}