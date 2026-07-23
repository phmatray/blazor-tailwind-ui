using Microsoft.AspNetCore.Components;
using System;

namespace Radixor.Components.Navigation;

public partial class TabsList<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private TabsRoot<TValue>? TabsRoot { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsRoot?.RegisterList(this);
    }
    
    internal void NotifyStateChanged()
    {
        StateHasChanged();
    }
    
    public void Dispose()
    {
        TabsRoot?.UnregisterList(this);
    }
}