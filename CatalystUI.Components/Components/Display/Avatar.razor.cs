using System.Drawing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CatalystUI.Components;

public partial class Avatar
{
    [Parameter] public string? Src { get; set; }
    [Parameter] public bool Square { get; set; }
    [Parameter] public string? Initials { get; set; }
    [Parameter] public string? Alt { get; set; }
    [Parameter] public AvatarSize Size { get; set; } = AvatarSize.Medium;
    [Parameter] public bool IsButton { get; set; }
    [Parameter] public string? Href { get; set; }
    [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

    private async Task HandleClick(MouseEventArgs args)
    {
        if (!Disabled && OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync(args);
        }
    }


    private string GetAvatarClasses()
    {
        var classes = new List<string>
        {
            "catalyst-avatar",
            "inline-grid",
            "shrink-0",
            "align-middle",
            "[--avatar-radius:20%]",
            "*:col-start-1",
            "*:row-start-1",
            "outline",
            "-outline-offset-1",
            "outline-black/10",
            "dark:outline-white/10"
        };

        // Size classes
        classes.Add(GetSizeClass());

        // Border radius
        if (Square)
        {
            classes.Add("rounded-[--avatar-radius]");
            classes.Add("*:rounded-[--avatar-radius]");
        }
        else
        {
            classes.Add("rounded-full");
            classes.Add("*:rounded-full");
        }

        return string.Join(" ", classes);
    }

    private string GetButtonClasses()
    {
        var classes = new List<string>
        {
            "relative",
            "inline-grid",
            "catalyst-focus"
        };

        classes.Add(Square ? "rounded-[20%]" : "rounded-full");

        return string.Join(" ", classes);
    }

    private string GetSvgClasses()
    {
        return "size-full fill-current p-[5%] font-medium uppercase select-none " + GetTextSizeClass();
    }

    private string GetSizeClass()
    {
        return Size switch
        {
            AvatarSize.Small => "size-8",
            AvatarSize.Medium => "size-10",
            AvatarSize.Large => "size-12",
            AvatarSize.ExtraLarge => "size-16",
            _ => "size-10"
        };
    }

    private string GetTextSizeClass()
    {
        return Size switch
        {
            AvatarSize.Small => "text-[32px]",
            AvatarSize.Medium => "text-[40px]",
            AvatarSize.Large => "text-[48px]",
            AvatarSize.ExtraLarge => "text-[64px]",
            _ => "text-[40px]"
        };
    }

    public enum AvatarSize
    {
        Small,
        Medium,
        Large,
        ExtraLarge
    }
}
