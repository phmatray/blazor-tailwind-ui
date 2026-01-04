using TailwindToolbox.Utilities;

namespace TailwindToolbox.Models;

/// <summary>
/// Represents an npm package with version information.
/// </summary>
public sealed record DependencyVersion
{
    /// <summary>
    /// npm package name (must be valid npm package identifier).
    /// </summary>
    public required string PackageName { get; init; }

    /// <summary>
    /// Currently installed version (null if not installed).
    /// </summary>
    public string? InstalledVersion { get; init; }

    /// <summary>
    /// Minimum required version (semantic version or range).
    /// </summary>
    public required string RequiredVersion { get; init; }

    /// <summary>
    /// Latest available version (null if not fetched).
    /// </summary>
    public string? LatestVersion { get; init; }

    /// <summary>
    /// Whether the installed version meets the requirement.
    /// </summary>
    public bool IsCompatible =>
        InstalledVersion != null &&
        SemverUtility.CompareVersions(InstalledVersion, RequiredVersion) >= 0;

    /// <summary>
    /// Whether a newer version is available.
    /// </summary>
    public bool HasUpdate =>
        LatestVersion != null &&
        InstalledVersion != null &&
        SemverUtility.CompareVersions(LatestVersion, InstalledVersion) > 0;
}
