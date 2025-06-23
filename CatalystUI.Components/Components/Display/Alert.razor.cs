using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public partial class Alert
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? Title { get; set; }
    [Parameter] public AlertType Type { get; set; } = AlertType.Info;
    [Parameter] public bool ShowIcon { get; set; } = true;
    [Parameter] public bool Dismissible { get; set; }
    [Parameter] public EventCallback OnDismiss { get; set; }

    private async Task HandleDismiss()
    {
        if (OnDismiss.HasDelegate)
        {
            await OnDismiss.InvokeAsync();
        }
    }

    private string GetAlertClasses()
    {
        var classes = new List<string>
        {
            "catalyst-alert",
            "rounded-lg",
            "p-4"
        };

        classes.Add(GetTypeClasses());

        return string.Join(" ", classes);
    }

    private string GetTypeClasses()
    {
        return Type switch
        {
            AlertType.Success => "bg-green-50 text-green-800 dark:bg-green-900/20 dark:text-green-400",
            AlertType.Warning => "bg-yellow-50 text-yellow-800 dark:bg-yellow-900/20 dark:text-yellow-400",
            AlertType.Error => "bg-red-50 text-red-800 dark:bg-red-900/20 dark:text-red-400",
            AlertType.Info => "bg-blue-50 text-blue-800 dark:bg-blue-900/20 dark:text-blue-400",
            _ => "bg-blue-50 text-blue-800 dark:bg-blue-900/20 dark:text-blue-400"
        };
    }

    private string GetIconClasses()
    {
        var baseClasses = "h-5 w-5";
        var colorClass = Type switch
        {
            AlertType.Success => "text-green-400 dark:text-green-500",
            AlertType.Warning => "text-yellow-400 dark:text-yellow-500",
            AlertType.Error => "text-red-400 dark:text-red-500",
            AlertType.Info => "text-blue-400 dark:text-blue-500",
            _ => "text-blue-400 dark:text-blue-500"
        };

        return $"{baseClasses} {colorClass}";
    }

    private string GetTitleClasses()
    {
        return "text-sm font-medium";
    }

    private string GetContentClasses()
    {
        return !string.IsNullOrWhiteSpace(Title) ? "mt-2 text-sm" : "text-sm";
    }

    private string GetDismissButtonClasses()
    {
        var baseClasses = "inline-flex rounded-md p-1.5 focus:outline-none focus:ring-2 focus:ring-offset-2";
        var colorClasses = Type switch
        {
            AlertType.Success =>
                "text-green-500 hover:bg-green-100 focus:ring-green-600 focus:ring-offset-green-50 dark:text-green-400 dark:hover:bg-green-800/50 dark:focus:ring-green-500 dark:focus:ring-offset-green-900/20",
            AlertType.Warning =>
                "text-yellow-500 hover:bg-yellow-100 focus:ring-yellow-600 focus:ring-offset-yellow-50 dark:text-yellow-400 dark:hover:bg-yellow-800/50 dark:focus:ring-yellow-500 dark:focus:ring-offset-yellow-900/20",
            AlertType.Error =>
                "text-red-500 hover:bg-red-100 focus:ring-red-600 focus:ring-offset-red-50 dark:text-red-400 dark:hover:bg-red-800/50 dark:focus:ring-red-500 dark:focus:ring-offset-red-900/20",
            AlertType.Info =>
                "text-blue-500 hover:bg-blue-100 focus:ring-blue-600 focus:ring-offset-blue-50 dark:text-blue-400 dark:hover:bg-blue-800/50 dark:focus:ring-blue-500 dark:focus:ring-offset-blue-900/20",
            _ =>
                "text-blue-500 hover:bg-blue-100 focus:ring-blue-600 focus:ring-offset-blue-50 dark:text-blue-400 dark:hover:bg-blue-800/50 dark:focus:ring-blue-500 dark:focus:ring-offset-blue-900/20"
        };

        return $"{baseClasses} {colorClasses}";
    }

    public enum AlertType
    {
        Info,
        Success,
        Warning,
        Error
    }
}