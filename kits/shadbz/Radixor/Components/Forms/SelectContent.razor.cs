using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

using Radixor.Components.Common;
namespace Radixor.Components.Forms;

public partial class SelectContent<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private SelectRoot<TValue>? SelectRoot { get; set; }
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public SelectContentVariant? Variant { get; set; } = SelectContentVariant.Solid;
    [Parameter] public string? Color { get; set; }
    [Parameter] public bool HighContrast { get; set; }
    [Parameter] public string? Position { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SelectRoot?.RegisterContent(this);
    }
    
    public void Dispose()
    {
        SelectRoot?.UnregisterContent(this);
    }
    
    internal void NotifyStateChanged()
    {
        StateHasChanged();
    }
    
    private Dictionary<string, object> GetContentAttributes()
    {
        var attributes = new Dictionary<string, object>
        {
            ["class"] = GetContentCssClass()
        };
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (HighContrast)
            attributes["data-high-contrast"] = "";
        
        if (!string.IsNullOrEmpty(Position))
            attributes["data-position"] = Position;
        
        return attributes;
    }
    
    private string GetContentCssClass()
    {
        var builder = new CssBuilder("rt-SelectContent")
            .AddClass(GetSizeClass())
            .AddClass(GetVariantClass());
        
        return builder.Build();
    }
    
    private string GetSizeClass()
    {
        var size = SelectRoot?.GetSize() ?? SelectSize.Size2;
        return size switch
        {
            SelectSize.Size1 => "rt-r-size-1",
            SelectSize.Size2 => "rt-r-size-2",
            SelectSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            SelectContentVariant.Solid => "rt-variant-solid",
            SelectContentVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
}

public enum SelectContentVariant
{
    Solid,
    Soft
}