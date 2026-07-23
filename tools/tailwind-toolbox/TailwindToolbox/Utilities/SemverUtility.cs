namespace TailwindToolbox.Utilities;

/// <summary>
/// Utility class for semantic versioning operations.
/// </summary>
public static class SemverUtility
{
    /// <summary>
    /// Compares two semantic versions.
    /// </summary>
    /// <param name="version1">The first version to compare.</param>
    /// <param name="version2">The second version to compare.</param>
    /// <returns>
    /// A value less than 0 if version1 is less than version2,
    /// 0 if they are equal,
    /// or a value greater than 0 if version1 is greater than version2.
    /// </returns>
    public static int CompareVersions(string version1, string version2)
    {
        // Normalize versions by removing common prefixes
        var v1 = NormalizeVersion(version1);
        var v2 = NormalizeVersion(version2);

        var v1Parts = v1.Split('.');
        var v2Parts = v2.Split('.');

        for (int i = 0; i < Math.Max(v1Parts.Length, v2Parts.Length); i++)
        {
            var v1Part = i < v1Parts.Length && int.TryParse(v1Parts[i], out var p1) ? p1 : 0;
            var v2Part = i < v2Parts.Length && int.TryParse(v2Parts[i], out var p2) ? p2 : 0;

            if (v1Part != v2Part)
            {
                return v1Part.CompareTo(v2Part);
            }
        }

        return 0;
    }

    /// <summary>
    /// Detects if updating from current version to target version contains breaking changes (major version bump).
    /// </summary>
    /// <param name="currentVersion">The current version.</param>
    /// <param name="targetVersion">The target version.</param>
    /// <returns>True if the update contains breaking changes, false otherwise.</returns>
    public static bool DetectBreakingChanges(string currentVersion, string targetVersion)
    {
        var current = NormalizeVersion(currentVersion);
        var target = NormalizeVersion(targetVersion);

        var currentParts = current.Split('.');
        var targetParts = target.Split('.');

        if (currentParts.Length > 0 && targetParts.Length > 0)
        {
            if (int.TryParse(currentParts[0], out var currentMajor) &&
                int.TryParse(targetParts[0], out var targetMajor))
            {
                return targetMajor > currentMajor;
            }
        }

        return false;
    }

    /// <summary>
    /// Extracts the major version number from a semantic version string.
    /// </summary>
    /// <param name="version">The version string.</param>
    /// <returns>The major version number, or 0 if parsing fails.</returns>
    public static int GetMajorVersion(string version)
    {
        var normalized = NormalizeVersion(version);
        var parts = normalized.Split('.');

        if (parts.Length > 0 && int.TryParse(parts[0], out var major))
        {
            return major;
        }

        return 0;
    }

    /// <summary>
    /// Extracts the minor version number from a semantic version string.
    /// </summary>
    /// <param name="version">The version string.</param>
    /// <returns>The minor version number, or 0 if parsing fails.</returns>
    public static int GetMinorVersion(string version)
    {
        var normalized = NormalizeVersion(version);
        var parts = normalized.Split('.');

        if (parts.Length > 1 && int.TryParse(parts[1], out var minor))
        {
            return minor;
        }

        return 0;
    }

    /// <summary>
    /// Extracts the patch version number from a semantic version string.
    /// </summary>
    /// <param name="version">The version string.</param>
    /// <returns>The patch version number, or 0 if parsing fails.</returns>
    public static int GetPatchVersion(string version)
    {
        var normalized = NormalizeVersion(version);
        var parts = normalized.Split('.');

        if (parts.Length > 2 && int.TryParse(parts[2], out var patch))
        {
            return patch;
        }

        return 0;
    }

    /// <summary>
    /// Normalizes a version string by removing common prefixes like 'v', '^', '~'.
    /// </summary>
    /// <param name="version">The version string to normalize.</param>
    /// <returns>The normalized version string.</returns>
    private static string NormalizeVersion(string version)
    {
        return version.TrimStart('v', 'V', '^', '~', '=', '>', '<', ' ');
    }
}
