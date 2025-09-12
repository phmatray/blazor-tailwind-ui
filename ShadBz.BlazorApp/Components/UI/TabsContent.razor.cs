using Microsoft.AspNetCore.Components;
using System;

namespace ShadBz.BlazorApp.Components.UI;

public partial class TabsContent<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private TabsRoot<TValue>? TabsRoot { get; set; }
    
    [Parameter] public TValue? Value { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsRoot?.RegisterContent(this);
    }
    
    internal void NotifyStateChanged()
    {
        StateHasChanged();
    }
    
    public void Dispose()
    {
        TabsRoot?.UnregisterContent(this);
    }
}