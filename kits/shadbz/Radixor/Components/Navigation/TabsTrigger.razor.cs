using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Radixor.Components.Navigation;

public partial class TabsTrigger<TValue> : ComponentBase, IDisposable
{
    [CascadingParameter] private TabsRoot<TValue>? TabsRoot { get; set; }
    
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public bool Disabled { get; set; }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        TabsRoot?.RegisterTrigger(this);
    }
    
    private async Task HandleClick(MouseEventArgs args)
    {
        if (!Disabled && TabsRoot != null)
        {
            await TabsRoot.SetValue(Value);
        }
    }
    
    internal void NotifyStateChanged()
    {
        StateHasChanged();
    }
    
    public void Dispose()
    {
        TabsRoot?.UnregisterTrigger(this);
    }
}