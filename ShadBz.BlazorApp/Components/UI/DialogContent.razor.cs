using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShadBz.BlazorApp.Components.UI.Base;

namespace ShadBz.BlazorApp.Components.UI;

public partial class DialogContent : SpacingComponentBase, IDisposable
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public DialogSize? Size { get; set; } = DialogSize.Size2;
    [Parameter] public string? MaxWidth { get; set; }
    [Parameter] public string? MinWidth { get; set; }
    [Parameter] public EventCallback OnOpenAutoFocus { get; set; }
    [Parameter] public EventCallback OnCloseAutoFocus { get; set; }
    [Parameter] public EventCallback<KeyboardEventArgs> OnEscapeKeyDown { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnPointerDownOutside { get; set; }
    
    [CascadingParameter] private DialogRoot? DialogRoot { get; set; }
    
    private string ContentCssClass
    {
        get
        {
            var builder = new CssBuilder("rt-BaseDialogContent rt-DialogContent")
                .AddClass(GetSizeClass())
                .AddClass(GetMarginClasses())
                .AddClass(Class);
            
            return builder.Build();
        }
    }
    
    private string OverlayCssClass => "rt-BaseDialogOverlay rt-DialogOverlay";
    
    private string GetSizeClass()
    {
        return Size switch
        {
            DialogSize.Size1 => "rt-r-size-1",
            DialogSize.Size2 => "rt-r-size-2",
            DialogSize.Size3 => "rt-r-size-3",
            DialogSize.Size4 => "rt-r-size-4",
            _ => "rt-r-size-2"
        };
    }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        DialogRoot?.RegisterContent(this);
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && DialogRoot?.IsOpen == true && OnOpenAutoFocus.HasDelegate)
        {
            await OnOpenAutoFocus.InvokeAsync();
        }
    }
    
    private async Task HandleOverlayClick()
    {
        if (DialogRoot != null)
        {
            await DialogRoot.HandleOverlayClick();
        }
        
        if (OnPointerDownOutside.HasDelegate)
        {
            await OnPointerDownOutside.InvokeAsync();
        }
    }
    
    private async Task HandleEscapeKey(KeyboardEventArgs e)
    {
        if (e.Key == "Escape")
        {
            if (OnEscapeKeyDown.HasDelegate)
            {
                await OnEscapeKeyDown.InvokeAsync(e);
            }
            
            if (DialogRoot != null)
            {
                await DialogRoot.HandleEscapeKey();
            }
        }
    }
    
    private Dictionary<string, object> GetContentAttributes()
    {
        var attributes = GetAllAttributes();
        attributes["class"] = ContentCssClass;
        
        if (!string.IsNullOrEmpty(MaxWidth))
            attributes["style"] = $"max-width: {MaxWidth};";
        
        if (!string.IsNullOrEmpty(MinWidth))
            attributes["style"] = (attributes.ContainsKey("style") ? attributes["style"] + " " : "") + $"min-width: {MinWidth};";
        
        return attributes;
    }
    
    public void Dispose()
    {
        DialogRoot?.UnregisterContent(this);
    }
}

public enum DialogSize
{
    Size1,
    Size2,
    Size3,
    Size4
}