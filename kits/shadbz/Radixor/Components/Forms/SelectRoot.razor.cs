using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Radixor.Components.Forms;

public partial class SelectRoot<TValue> : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public SelectSize? Size { get; set; } = SelectSize.Size2;
    [Parameter] public TValue? Value { get; set; }
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public TValue? DefaultValue { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string? Name { get; set; }
    [Parameter] public bool Required { get; set; }
    
    private TValue? _internalValue;
    private bool _isOpen;
    private SelectTrigger<TValue>? _trigger;
    private SelectContent<TValue>? _content;
    private readonly List<SelectItem<TValue>> _items = new();
    
    internal bool IsOpen => _isOpen;
    internal SelectSize GetSize() => Size ?? SelectSize.Size2;
    
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
            }
            
            StateHasChanged();
        }
        
        await CloseDropdown();
    }
    
    internal void RegisterTrigger(SelectTrigger<TValue> trigger)
    {
        _trigger = trigger;
    }
    
    internal void UnregisterTrigger(SelectTrigger<TValue> trigger)
    {
        if (_trigger == trigger)
        {
            _trigger = null;
        }
    }
    
    internal void RegisterContent(SelectContent<TValue> content)
    {
        _content = content;
    }
    
    internal void UnregisterContent(SelectContent<TValue> content)
    {
        if (_content == content)
        {
            _content = null;
        }
    }
    
    internal void RegisterItem(SelectItem<TValue> item)
    {
        if (!_items.Contains(item))
        {
            _items.Add(item);
        }
    }
    
    internal void UnregisterItem(SelectItem<TValue> item)
    {
        _items.Remove(item);
    }
    
    internal async Task ToggleDropdown()
    {
        if (_isOpen)
        {
            await CloseDropdown();
        }
        else
        {
            await OpenDropdown();
        }
    }
    
    internal async Task OpenDropdown()
    {
        if (!Disabled)
        {
            _isOpen = true;
            StateHasChanged();
            _trigger?.NotifyStateChanged();
            _content?.NotifyStateChanged();
        }
    }
    
    internal async Task CloseDropdown()
    {
        _isOpen = false;
        StateHasChanged();
        _trigger?.NotifyStateChanged();
        _content?.NotifyStateChanged();
    }
    
    internal string? GetDisplayText()
    {
        var currentValue = GetValue();
        if (currentValue == null)
        {
            return null;
        }
        
        foreach (var item in _items)
        {
            if (EqualityComparer<TValue>.Default.Equals(item.Value, currentValue))
            {
                return item.GetDisplayText();
            }
        }
        
        return currentValue.ToString();
    }
    
    protected override void OnInitialized()
    {
        base.OnInitialized();
        if (DefaultValue != null && !ValueChanged.HasDelegate)
        {
            _internalValue = DefaultValue;
        }
    }
}

public enum SelectSize
{
    Size1,
    Size2,
    Size3
}