using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorTailwind.Components.UI;

public class TwTransitionChild : TwTransitionBase
{
    [CascadingParameter]
    public TwTransition? Parent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // Produces the following HTML if Show is true:
        // <div class="@TransitionClasses @AdditionalClasses" @attributes="InputAttributes">
        //     @ChildContent
        // </div>
        
        builder.OpenElement(0, As);
        builder.AddAttribute(1, "class", $"{TransitionClasses} {AdditionalClasses}");
        builder.AddMultipleAttributes(2, AdditionalAttributes);
        builder.AddContent(3, ChildContent);
        builder.CloseElement();
    }

    public Task Toggle()
    {
        return Parent == null
            ? Task.CompletedTask
            : InvokeAsync(() => Parent.Show ? ShowAsync() : HideAsync());
    }

    protected override void OnInitialized()
    {
        Parent?.AddTransition(this);
    }
}