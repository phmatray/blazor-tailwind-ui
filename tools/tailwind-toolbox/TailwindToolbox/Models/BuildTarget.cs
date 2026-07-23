namespace TailwindToolbox.Models;

/// <summary>
/// Represents an MSBuild .targets file for Tailwind compilation.
/// </summary>
public sealed record BuildTarget
{
    /// <summary>
    /// Path to .targets file (must end with .targets).
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// MSBuild target name (defaults to "BuildTailwindCSS").
    /// </summary>
    public required string TargetName { get; init; }

    /// <summary>
    /// Source CSS file path (relative to project root).
    /// </summary>
    public required string InputCssPath { get; init; }

    /// <summary>
    /// Compiled CSS destination (typically wwwroot/css/app.css).
    /// </summary>
    public required string OutputCssPath { get; init; }

    /// <summary>
    /// MSBuild target to run before (defaults to "BeforeBuild").
    /// </summary>
    public string RunBeforeTargets { get; init; } = "BeforeBuild";

    /// <summary>
    /// Whether to minify output (true for Release, false for Debug).
    /// </summary>
    public bool IsMinified { get; init; }
}
