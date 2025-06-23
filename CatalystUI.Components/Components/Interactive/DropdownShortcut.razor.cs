using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class DropdownShortcut
{
    [Parameter] public string Keys { get; set; } = "";

    private string[] GetKeys()
    {
        if (string.IsNullOrWhiteSpace(Keys))
            return Array.Empty<string>();

        // Check if it's a multi-key shortcut (e.g., "Cmd+K")
        if (Keys.Contains("+"))
        {
            return Keys.Split('+');
        }

        // Otherwise split into individual characters
        return Keys.ToCharArray().Select(c => c.ToString()).ToArray();
    }

    private string GetKeyClasses(string key)
    {
        return CombineClasses(
            "min-w-[2ch]",
            "text-center",
            "font-sans",
            "text-zinc-400",
            "capitalize",
            "group-hover:text-white",
            "group-focus:text-white",
            "forced-colors:group-hover:text-[HighlightText]",
            "forced-colors:group-focus:text-[HighlightText]",
            key.Length > 1 ? "pl-1" : ""
        );
    }
}