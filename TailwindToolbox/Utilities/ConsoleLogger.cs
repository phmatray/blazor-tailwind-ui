using Spectre.Console;

namespace TailwindToolbox.Utilities;

/// <summary>
/// Console logger utility that respects verbose and no-color settings.
/// </summary>
public sealed class ConsoleLogger
{
    private readonly bool _verbose;
    private readonly bool _noColor;

    public ConsoleLogger(bool verbose, bool noColor)
    {
        _verbose = verbose;
        _noColor = noColor;

        // Disable colors if requested
        if (noColor)
        {
            AnsiConsole.Profile.Capabilities.ColorSystem = ColorSystem.NoColors;
        }
    }

    /// <summary>
    /// Log verbose information (only shown when --verbose is enabled).
    /// </summary>
    public void Verbose(string message)
    {
        if (_verbose)
        {
            WriteMessage("[dim]VERBOSE:[/] [dim]{0}[/]", message);
        }
    }

    /// <summary>
    /// Log informational message (always shown).
    /// </summary>
    public void Info(string message)
    {
        WriteMessage("[blue]INFO:[/] {0}", message);
    }

    /// <summary>
    /// Log success message (always shown).
    /// </summary>
    public void Success(string message)
    {
        WriteMessage("[green]✓[/] {0}", message);
    }

    /// <summary>
    /// Log warning message (always shown).
    /// </summary>
    public void Warning(string message)
    {
        WriteMessage("[yellow]WARNING:[/] {0}", message);
    }

    /// <summary>
    /// Log error message (always shown).
    /// </summary>
    public void Error(string message)
    {
        WriteMessage("[red]ERROR:[/] {0}", message);
    }

    /// <summary>
    /// Log error with exception details (verbose shows full stack trace).
    /// </summary>
    public void Error(string message, Exception exception)
    {
        WriteMessage("[red]ERROR:[/] {0}", message);

        if (_verbose)
        {
            WriteMessage("[dim]Exception Type:[/] {0}", exception.GetType().FullName ?? "Unknown");
            WriteMessage("[dim]Stack Trace:[/]");
            WriteMessage("[dim]{0}[/]", exception.StackTrace ?? "N/A");

            if (exception.InnerException != null)
            {
                WriteMessage("[dim]Inner Exception:[/] {0}", exception.InnerException.Message);
            }
        }
        else
        {
            WriteMessage("[dim]{0}[/]", exception.Message);
        }
    }

    private void WriteMessage(string format, params object[] args)
    {
        if (_noColor)
        {
            // Strip markup and write plain text
            var message = string.Format(format, args);
            var plainMessage = StripMarkup(message);
            Console.WriteLine(plainMessage);
        }
        else
        {
            AnsiConsole.MarkupLine(format, args);
        }
    }

    private static string StripMarkup(string text)
    {
        // Simple markup removal (removes content between [ and ])
        var result = text;
        while (result.Contains('[') && result.Contains(']'))
        {
            var start = result.IndexOf('[');
            var end = result.IndexOf(']', start);
            if (end > start)
            {
                result = result.Remove(start, end - start + 1);
            }
            else
            {
                break;
            }
        }
        return result;
    }
}
