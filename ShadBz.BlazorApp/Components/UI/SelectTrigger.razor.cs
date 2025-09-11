using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class SelectTrigger<TValue> : SpacingComponentBase, IDisposable
{
    [CascadingParameter] private SelectRoot<TValue>? SelectRoot { get; set; }
    
    [Parameter] public SelectVariant? Variant { get; set; } = SelectVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    [Parameter] public string? Placeholder { get; set; } = "Select an option";
    
    private string? DisplayText => SelectRoot?.GetDisplayText();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        SelectRoot?.RegisterTrigger(this);
    }
    
    public void Dispose()
    {
        SelectRoot?.UnregisterTrigger(this);
    }
    
    internal void NotifyStateChanged()
    {
        StateHasChanged();
    }
    
    private async Task HandleClick(MouseEventArgs e)
    {
        if (SelectRoot != null)
        {
            await SelectRoot.ToggleDropdown();
        }
    }
    
    private string TriggerCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-SelectTrigger rt-reset")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass(GetMarginClasses())
                .AddClass("rt-open", SelectRoot?.IsOpen == true)
                .AddClass(Class);
            
            return builder.Build();
        }
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
            SelectVariant.Classic => "rt-variant-classic",
            SelectVariant.Surface => "rt-variant-surface",
            SelectVariant.Soft => "rt-variant-soft",
            SelectVariant.Ghost => "rt-variant-ghost",
            _ => ""
        };
    }
    
    private Dictionary<string, object> GetTriggerAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = TriggerCssClass;
        attributes["type"] = "button";
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        if (SelectRoot?.Disabled == true)
        {
            attributes["disabled"] = true;
            attributes["data-disabled"] = "";
        }
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
}

public enum SelectVariant
{
    Classic,
    Surface,
    Soft,
    Ghost
}