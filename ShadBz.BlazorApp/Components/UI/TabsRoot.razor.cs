using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShadBz.BlazorApp.Components.UI;

public enum TabsOrientation
{
    Horizontal,
    Vertical
}

public enum TabsVariant
{
    Classic,
    Surface,
    Ghost
}

public partial class TabsRoot<TValue> : ComponentBase
{
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public TValue? DefaultValue { get; set; }
    [Parameter] public TabsOrientation Orientation { get; set; } = TabsOrientation.Horizontal;
    [Parameter] public TabsVariant Variant { get; set; } = TabsVariant.Classic;
    [Parameter] public bool ActivationMode { get; set; } = true; // true = automatic, false = manual
    
    private TValue? _internalValue;
    private TabsList<TValue>? _tabsList;
    private readonly List<TabsTrigger<TValue>> _triggers = new();
    private readonly List<TabsContent<TValue>> _contents = new();
    
    internal TValue? GetValue()
    {
        if (ValueChanged.HasDelegate)
        {
            return Value;
        }
        return _internalValue ?? DefaultValue;
    }
    
    internal async Task SetValue(TValue? value)
    {
        if (!EqualityComparer<TValue>.Default.Equals(GetValue(), value))
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                _internalValue = value;
                StateHasChanged();
            }
            
            NotifyStateChanged();
        }
    }
    
    internal void RegisterList(TabsList<TValue> list)
    {
        _tabsList = list;
    }
    
    internal void UnregisterList(TabsList<TValue> list)
    {
        if (_tabsList == list)
        {
            _tabsList = null;
        }
    }
    
    internal void RegisterTrigger(TabsTrigger<TValue> trigger)
    {
        if (!_triggers.Contains(trigger))
        {
            _triggers.Add(trigger);
        }
    }
    
    internal void UnregisterTrigger(TabsTrigger<TValue> trigger)
    {
        _triggers.Remove(trigger);
    }
    
    internal void RegisterContent(TabsContent<TValue> content)
    {
        if (!_contents.Contains(content))
        {
            _contents.Add(content);
        }
    }
    
    internal void UnregisterContent(TabsContent<TValue> content)
    {
        _contents.Remove(content);
    }
    
    internal bool IsTabActive(TValue? value)
    {
        var currentValue = GetValue();
        return EqualityComparer<TValue>.Default.Equals(currentValue, value);
    }
    
    private void NotifyStateChanged()
    {
        _tabsList?.NotifyStateChanged();
        foreach (var trigger in _triggers)
        {
            trigger.NotifyStateChanged();
        }
        foreach (var content in _contents)
        {
            content.NotifyStateChanged();
        }
    }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (!ValueChanged.HasDelegate && DefaultValue != null)
        {
            _internalValue = DefaultValue;
        }
    }
}