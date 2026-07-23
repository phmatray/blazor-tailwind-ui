using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Radixor.Components.Common;
namespace Radixor.Components.Forms;

public partial class SelectItem<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private SelectRoot<TValue>? SelectRoot { get; set; }
    
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; } = new();
    
    private bool IsSelected => SelectRoot != null && 
                              EqualityComparer<TValue>.Default.Equals(SelectRoot.GetValue(), Value);
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SelectRoot?.RegisterItem(this);
    }
    
    public void Dispose()
    {
        SelectRoot?.UnregisterItem(this);
    }
    
    internal string? GetDisplayText()
    {
        if (ChildContent != null)
        {
            // For simplicity, we'll use the Value's ToString() method
            // In a real implementation, you might want to extract text from RenderFragment
            return Value?.ToString();
        }
        return Value?.ToString();
    }
    
    private async Task HandleClick(MouseEventArgs e)
    {
        if (!Disabled && SelectRoot != null)
        {
            await SelectRoot.SetValue(Value);
        }
    }
    
    private string ItemCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-SelectItem")
                .AddClass("rt-SelectItem--selected", IsSelected)
                .AddClass("rt-SelectItem--disabled", Disabled);
            
            return builder.Build();
        }
    }
}