using Microsoft.AspNetCore.Components;

namespace ShadBz.BlazorApp.Components.UI;

public partial class TextFieldSlot : ComponentBase, IDisposable
{
    [CascadingParameter] private TextFieldRoot? TextFieldRoot { get; set; }
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public SlotSide Side { get; set; } = SlotSide.Left;
    [Parameter] public string? Color { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TextFieldRoot?.RegisterSlot(this);
    }
    
    public void Dispose()
    {
        TextFieldRoot?.UnregisterSlot(this);
    }
}

public enum SlotSide
{
    Left,
    Right
}