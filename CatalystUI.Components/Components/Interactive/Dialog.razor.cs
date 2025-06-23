using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CatalystUI.Components;

public partial class Dialog
{
    private ElementReference dialogElement;
    [Parameter] public bool Open { get; set; }
    [Parameter] public EventCallback<bool> OpenChanged { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public DialogSize Size { get; set; } = DialogSize.Lg;
    [Parameter] public bool CloseOnBackdropClick { get; set; } = true;
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    public enum DialogSize
    {
        Xs,
        Sm,
        Md,
        Lg,
        Xl,
        Xxl,
        Xxxl,
        Xxxxl,
        Xxxxxl
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Open && firstRender)
        {
            // Lock scroll when dialog opens
            await JSRuntime.InvokeVoidAsync("CatalystUI.lockScroll");

            // Focus trap
            await JSRuntime.InvokeVoidAsync("CatalystUI.trapFocus", dialogElement);
        }
    }

    private async Task HandleBackdropClick()
    {
        if (CloseOnBackdropClick)
        {
            await CloseDialog();
        }
    }

    public async Task CloseDialog()
    {
        Open = false;
        await OpenChanged.InvokeAsync(Open);

        // Unlock scroll when dialog closes
        await JSRuntime.InvokeVoidAsync("CatalystUI.unlockScroll");
    }

    private string GetBackdropClasses()
    {
        return CombineClasses(
            "fixed inset-0 flex w-screen justify-center overflow-y-auto",
            "bg-zinc-950/25 dark:bg-zinc-950/50",
            "px-2 py-2 sm:px-6 sm:py-8 lg:px-8 lg:py-16",
            "transition duration-100 focus:outline-0",
            Open ? "opacity-100 ease-out" : "opacity-0 ease-in"
        );
    }

    private string GetPanelClasses()
    {
        var sizeClass = Size switch
        {
            DialogSize.Xs => "sm:max-w-xs",
            DialogSize.Sm => "sm:max-w-sm",
            DialogSize.Md => "sm:max-w-md",
            DialogSize.Lg => "sm:max-w-lg",
            DialogSize.Xl => "sm:max-w-xl",
            DialogSize.Xxl => "sm:max-w-2xl",
            DialogSize.Xxxl => "sm:max-w-3xl",
            DialogSize.Xxxxl => "sm:max-w-4xl",
            DialogSize.Xxxxxl => "sm:max-w-5xl",
            _ => "sm:max-w-lg"
        };

        return CombineClasses(
            sizeClass,
            "row-start-2 w-full min-w-0",
            "rounded-t-3xl sm:rounded-2xl",
            "bg-white dark:bg-zinc-900",
            "p-8",
            "shadow-lg ring-1 ring-zinc-950/10 dark:ring-white/10",
            "sm:mb-auto",
            "forced-colors:outline",
            "transition duration-100 will-change-transform",
            Open
                ? "translate-y-0 opacity-100 scale-100 ease-out"
                : "translate-y-12 opacity-0 sm:translate-y-0 sm:scale-95 ease-in"
        );
    }

    public void Dispose()
    {
        // Cleanup if needed
    }
}