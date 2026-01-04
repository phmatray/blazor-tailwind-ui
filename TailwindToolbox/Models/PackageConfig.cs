namespace TailwindToolbox.Models;

/// <summary>
/// Represents package.json for npm dependency management.
/// </summary>
public sealed record PackageConfig
{
    /// <summary>
    /// Path to package.json file.
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// Package name (defaults to project name, lowercase with hyphens).
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Package version (semantic version, e.g., "1.0.0").
    /// </summary>
    public required string Version { get; init; }

    /// <summary>
    /// Production dependencies (must include "tailwindcss").
    /// </summary>
    public required Dictionary<string, string> Dependencies { get; init; }

    /// <summary>
    /// Development dependencies (optional, e.g., autoprefixer).
    /// </summary>
    public Dictionary<string, string>? DevDependencies { get; init; }

    /// <summary>
    /// npm scripts (must include Tailwind build script).
    /// </summary>
    public required Dictionary<string, string> Scripts { get; init; }
}
