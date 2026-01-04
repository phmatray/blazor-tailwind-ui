namespace TailwindToolbox.Models;

/// <summary>
/// Represents a detected Blazor project with metadata about project type, version, and file locations.
/// </summary>
public sealed class BlazorProject
{
    /// <summary>
    /// Absolute path to .csproj file.
    /// </summary>
    public required string ProjectFilePath { get; init; }

    /// <summary>
    /// Directory containing .csproj (derived from ProjectFilePath).
    /// </summary>
    public required string ProjectDirectory { get; init; }

    /// <summary>
    /// Name of the project (extracted from .csproj filename).
    /// </summary>
    public required string ProjectName { get; init; }

    /// <summary>
    /// Target framework (e.g., "net10.0").
    /// </summary>
    public required string DotNetVersion { get; init; }

    /// <summary>
    /// Blazor project type (Server, WebAssembly, Hybrid, or Unknown).
    /// </summary>
    public required BlazorProjectType ProjectType { get; init; }

    /// <summary>
    /// Whether tailwind.config.js exists in project directory.
    /// </summary>
    public bool HasTailwindConfig { get; init; }

    /// <summary>
    /// Whether package.json exists in project directory.
    /// </summary>
    public bool HasPackageJson { get; init; }

    /// <summary>
    /// Whether .targets file exists in project directory.
    /// </summary>
    public bool HasBuildTargets { get; init; }

    /// <summary>
    /// Path to wwwroot folder (null if not found).
    /// </summary>
    public string? WwwRootPath { get; init; }
}
