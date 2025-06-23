using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CatalystUI.Components;

public partial class TableCell
{
    private ElementReference cellElement;
    private bool isFirstCell = true;
    private bool IsFirstCell => isFirstCell;
    [CascadingParameter] private Table? ParentTable { get; set; }
    [CascadingParameter] private TableRow? ParentRow { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && !string.IsNullOrWhiteSpace(ParentRow?.Href))
        {
            // Check if this cell has a previous sibling
            isFirstCell = await JSRuntime.InvokeAsync<bool>("CatalystUI.checkIsFirstTableCell", cellElement);
            StateHasChanged();
        }
    }

    private string GetCellClasses()
    {
        var classes = new List<string>
        {
            "relative",
            "px-4",
            "first:pl-[--gutter,--spacing(2)]",
            "last:pr-[--gutter,--spacing(2)]"
        };

        if (ParentTable?.Striped == false)
        {
            classes.Add("border-b");
            classes.Add("border-zinc-950/5");
            classes.Add("dark:border-white/5");
        }

        if (ParentTable?.Grid == true)
        {
            classes.Add("border-l");
            classes.Add("border-l-zinc-950/5");
            classes.Add("first:border-l-0");
            classes.Add("dark:border-l-white/5");
        }

        classes.Add(ParentTable?.Dense == true ? "py-2.5" : "py-4");

        if (ParentTable?.Bleed == false)
        {
            classes.Add("sm:first:pl-1");
            classes.Add("sm:last:pr-1");
        }

        return string.Join(" ", classes);
    }
}