using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CatalystUI.Components.Services;

public class CatalystUIJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public CatalystUIJsInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/CatalystUI.Components/js/catalyst-ui.js").AsTask());
    }

    public async ValueTask FocusAsync(ElementReference element)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("CatalystUI.focus", element);
    }

    public async ValueTask<IJSObjectReference> TrapFocusAsync(ElementReference element)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference>("CatalystUI.trapFocus", element);
    }

    public async ValueTask<IJSObjectReference> AddClickOutsideHandlerAsync(ElementReference element, DotNetObjectReference<object> dotNetReference, string methodName)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference>("CatalystUI.addClickOutsideHandler", element, dotNetReference, methodName);
    }

    public async ValueTask LockScrollAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("CatalystUI.lockScroll");
    }

    public async ValueTask UnlockScrollAsync()
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("CatalystUI.unlockScroll");
    }

    public async ValueTask<ElementDimensions?> GetDimensionsAsync(ElementReference element)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<ElementDimensions?>("CatalystUI.getDimensions", element);
    }

    public async ValueTask SetCssVariableAsync(ElementReference element, string name, string value)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("CatalystUI.setCssVariable", element, name, value);
    }

    public async ValueTask ScrollToElementAsync(ElementReference element, ScrollOptions? options = null)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("CatalystUI.scrollToElement", element, options ?? new ScrollOptions());
    }

    public async ValueTask<bool> CopyToClipboardAsync(string text)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("CatalystUI.copyToClipboard", text);
    }

    public async ValueTask<bool> IsInViewportAsync(ElementReference element)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<bool>("CatalystUI.isInViewport", element);
    }

    public async ValueTask<IJSObjectReference?> AnimateAsync(ElementReference element, object keyframes, AnimationOptions options)
    {
        var module = await moduleTask.Value;
        return await module.InvokeAsync<IJSObjectReference?>("CatalystUI.animate", element, keyframes, options);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}

public record ElementDimensions(double Width, double Height, double Top, double Left, double Bottom, double Right);

public class ScrollOptions
{
    public string Behavior { get; set; } = "smooth";
    public string Block { get; set; } = "start";
    public string Inline { get; set; } = "nearest";
}

public class AnimationOptions
{
    public int Duration { get; set; } = 300;
    public string Easing { get; set; } = "ease";
    public string Fill { get; set; } = "forwards";
    public int? Delay { get; set; }
    public int? Iterations { get; set; }
}