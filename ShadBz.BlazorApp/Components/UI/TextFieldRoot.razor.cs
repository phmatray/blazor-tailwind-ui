using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class TextFieldRoot : SpacingComponentBase
{
    [Parameter] public TextFieldSize? Size { get; set; } = TextFieldSize.Size2;
    [Parameter] public TextFieldVariant? Variant { get; set; } = TextFieldVariant.Surface;
    [Parameter] public string? Color { get; set; }
    [Parameter] public string? Radius { get; set; }
    
    // Input properties
    [Parameter] public string? Type { get; set; } = "text";
    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public string? Value { get; set; }
    [Parameter] public EventCallback<string?> ValueChanged { get; set; }
    [Parameter] public EventCallback<ChangeEventArgs> OnChange { get; set; }
    [Parameter] public EventCallback<ChangeEventArgs> OnInput { get; set; }
    
    // Validation and constraints
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool ReadOnly { get; set; }
    [Parameter] public bool Required { get; set; }
    [Parameter] public int? MinLength { get; set; }
    [Parameter] public int? MaxLength { get; set; }
    [Parameter] public string? Min { get; set; }
    [Parameter] public string? Max { get; set; }
    [Parameter] public string? Step { get; set; }
    [Parameter] public string? Pattern { get; set; }
    [Parameter] public string? AutoComplete { get; set; }
    
    // Identity
    [Parameter] public string? Name { get; set; }
    [Parameter] public string? Id { get; set; }
    
    // Slots
    [Parameter] public RenderFragment? LeftSlot { get; set; }
    [Parameter] public RenderFragment? RightSlot { get; set; }
    
    // For slot children
    internal List<TextFieldSlot> Slots { get; } = new();
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
    }
    
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        
        if (firstRender)
        {
            // Arrange slots
            foreach (var slot in Slots)
            {
                if (slot.Side == SlotSide.Left)
                {
                    LeftSlot = slot.ChildContent;
                }
                else if (slot.Side == SlotSide.Right)
                {
                    RightSlot = slot.ChildContent;
                }
            }
            
            if (Slots.Count > 0)
            {
                StateHasChanged();
            }
        }
    }
    
    protected string RootCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-TextFieldRoot")
                .AddClass(GetSizeClass())
                .AddClass(GetVariantClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string GetSizeClass()
    {
        return Size switch
        {
            TextFieldSize.Size1 => "rt-r-size-1",
            TextFieldSize.Size2 => "rt-r-size-2",
            TextFieldSize.Size3 => "rt-r-size-3",
            _ => ""
        };
    }
    
    private string GetVariantClass()
    {
        return Variant switch
        {
            TextFieldVariant.Classic => "rt-variant-classic",
            TextFieldVariant.Surface => "rt-variant-surface",
            TextFieldVariant.Soft => "rt-variant-soft",
            _ => ""
        };
    }
    
    protected Dictionary<string, object> GetRootAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = RootCssClass;
        
        if (!string.IsNullOrEmpty(Color))
            attributes["data-accent-color"] = Color;
        
        if (!string.IsNullOrEmpty(Radius))
            attributes["data-radius"] = Radius;
        
        if (Disabled)
            attributes["data-disabled"] = "";
        
        var inlineStyles = BuildInlineStyles();
        if (!string.IsNullOrEmpty(inlineStyles))
            attributes["style"] = inlineStyles;
        
        return attributes;
    }
    
    protected async Task HandleChange(ChangeEventArgs e)
    {
        Value = e.Value?.ToString();
        
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        
        if (OnChange.HasDelegate)
        {
            await OnChange.InvokeAsync(e);
        }
    }
    
    protected async Task HandleInput(ChangeEventArgs e)
    {
        if (OnInput.HasDelegate)
        {
            await OnInput.InvokeAsync(e);
        }
    }
    
    internal void RegisterSlot(TextFieldSlot slot)
    {
        if (!Slots.Contains(slot))
        {
            Slots.Add(slot);
        }
    }
    
    internal void UnregisterSlot(TextFieldSlot slot)
    {
        Slots.Remove(slot);
    }
}

public enum TextFieldSize
{
    Size1,
    Size2,
    Size3
}

public enum TextFieldVariant
{
    Classic,
    Surface,
    Soft
}

