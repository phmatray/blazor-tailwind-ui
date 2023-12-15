using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorTailwind.Components.UI;

public class TwTransition : TwTransitionBase
{
    /// <summary>
    /// Whether the children should be shown or hidden.
    /// </summary>
    [Parameter] public bool Show { get; set; }
    
    /// <summary>
    /// Whether the transition should run on initial mount.
    /// </summary>
    [Parameter] public bool Appear { get; set; } = false;
    
    /// <summary>
    /// Whether the element should be unmounted or hidden based on the show state.
    /// </summary>
    [Parameter] public bool Unmount { get; set; } = true;

    /// <summary>
    /// Callback that is invoked when the show state changes.
    /// </summary>
    [Parameter] public EventCallback<bool> ShowChanged { get; set; }
    
    private bool _oldStatus = false;
    private List<TwTransitionChild> _transitions = [];
    private bool _isFirstRender = true;
    private string _hiddenClass = "";
    
    private bool IsTransitional
        => !string.IsNullOrWhiteSpace(Enter) || !string.IsNullOrWhiteSpace(Leave);
    
    protected override void OnInitialized()
    {
        _oldStatus = Show;
    }
    
    protected override Task OnParametersSetAsync()
    {
        if (_oldStatus != Show && !_isFirstRender)
        {
            _oldStatus = Show;
            
            if (Show)
            {
                SetHiddenClass();
            }
            
            return ToggleAsync();
        }

        SetHiddenClass();
        return Task.CompletedTask;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _isFirstRender = false;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // Produces the following HTML if Show is true:
        // <div class="@_hiddenClass @TransitionClasses.ToString() @AdditionalClasses" @attributes="InputAttributes">
        //     <CascadingValue Value="this" IsFixed="true">
        //         @ChildContent
        //     </CascadingValue>
        // </div>
        
        builder.OpenElement(0, As);
        var classes = $"{_hiddenClass} {TransitionClasses} {AdditionalClasses}".Trim();
        if (classes.Length > 0)
        {
            builder.AddAttribute(1, "class", classes);
        }
        builder.AddMultipleAttributes(2, AdditionalAttributes);
        
        builder.OpenComponent<CascadingValue<TwTransition>>(3);
        builder.AddAttribute(4, "Value", this);
        builder.AddAttribute(5, "IsFixed", true);
        builder.AddAttribute(6, "ChildContent", 
            (RenderFragment)(childBuilder => childBuilder.AddContent(6, ChildContent)));
        
        builder.CloseComponent();
        builder.CloseElement();
    }

    public async Task Toggle()
    {
        await InvokeAsync(() => Show ? ShowAsync() : HideAsync());
    }

    internal async Task ToggleAsync()
    {
        var tasks = new List<Task>();
        
        if (IsTransitional)
        {
            tasks.Add(Toggle());
        }
        
        foreach (var item in _transitions)
        {
            tasks.Add(item.Toggle());
        }
        
        await Task.WhenAll(tasks);
        await ShowChanged.InvokeAsync(Show);

        SetHiddenClass();
    }

    internal void AddTransition(TwTransitionChild transition)
    {
        _transitions.Add(transition);
    }
    
    private void SetHiddenClass()
    {
        _hiddenClass = Show ? "" : "hidden";
        StateHasChanged();
    }
}