using Spectre.Console.Cli;
using System.ComponentModel;

namespace TailwindToolbox.Commands;

/// <summary>
/// Base settings class with global options available to all commands.
/// </summary>
public abstract class BaseCommandSettings : CommandSettings
{
    [Description("Enable verbose logging output")]
    [CommandOption("-v|--verbose")]
    public bool Verbose { get; set; }

    [Description("Disable colored output")]
    [CommandOption("--no-color")]
    public bool NoColor { get; set; }
}
