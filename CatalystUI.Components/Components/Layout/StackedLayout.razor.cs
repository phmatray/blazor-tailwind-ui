using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CatalystUI.Components;

public partial class StackedLayout
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? NavbarContent { get; set; }
    [Parameter] public RenderFragment? SidebarContent { get; set; }
    private bool ShowSidebar = false;

    private void OpenSidebar(MouseEventArgs e)
    {
        ShowSidebar = true;
    }

    private void CloseSidebar()
    {
        ShowSidebar = false;
    }

    private void CloseSidebar(MouseEventArgs e)
    {
        ShowSidebar = false;
    }
}