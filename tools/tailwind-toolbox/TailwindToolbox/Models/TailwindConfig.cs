namespace TailwindToolbox.Models;

/// <summary>
/// Represents the tailwind.config.js configuration structure.
/// </summary>
public sealed record TailwindConfig
{
    /// <summary>
    /// Path to tailwind.config.js file.
    /// </summary>
    public required string FilePath { get; init; }

    /// <summary>
    /// Glob patterns for content files (e.g., .razor, .html, .cshtml).
    /// </summary>
    public required List<string> ContentPaths { get; init; }

    /// <summary>
    /// Theme customizations (optional).
    /// </summary>
    public Dictionary<string, object>? Theme { get; init; }

    /// <summary>
    /// Tailwind plugins (optional, valid npm package names).
    /// </summary>
    public List<string>? Plugins { get; init; }

    /// <summary>
    /// Dark mode strategy ("media" or "class", optional).
    /// </summary>
    public string? DarkMode { get; init; }
}
