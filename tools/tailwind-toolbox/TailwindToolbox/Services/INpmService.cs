namespace TailwindToolbox.Services;

/// <summary>
/// Service for executing npm commands and managing npm packages.
/// </summary>
public interface INpmService
{
    /// <summary>
    /// Installs an npm package.
    /// </summary>
    Task<NpmResult> InstallPackageAsync(string packageName, string version, string workingDirectory, TimeSpan? timeout = null);

    /// <summary>
    /// Installs multiple npm packages.
    /// </summary>
    Task<NpmBatchResult> InstallPackagesAsync(Dictionary<string, string> packages, string workingDirectory);

    /// <summary>
    /// Checks if Node.js is installed.
    /// </summary>
    Task<ToolCheckResult> CheckNodeInstalledAsync();

    /// <summary>
    /// Checks if npm is installed.
    /// </summary>
    Task<ToolCheckResult> CheckNpmInstalledAsync();

    /// <summary>
    /// Executes an npm command.
    /// </summary>
    Task<NpmResult> ExecuteNpmCommandAsync(string command, string workingDirectory);

    /// <summary>
    /// Gets the installed version of a package.
    /// </summary>
    Task<string?> GetInstalledVersionAsync(string packageName, string workingDirectory);

    /// <summary>
    /// Checks for available updates for installed packages.
    /// </summary>
    Task<List<Models.DependencyVersion>> CheckForUpdatesAsync(string workingDirectory);

    /// <summary>
    /// Detects if updating from current version to latest version contains breaking changes.
    /// </summary>
    bool DetectBreakingChanges(string currentVersion, string latestVersion);

    /// <summary>
    /// Updates a package to a specific version.
    /// </summary>
    Task<NpmResult> UpdatePackageAsync(string packageName, string version, string workingDirectory);
}

public sealed record NpmResult(bool Success, int ExitCode, string StandardOutput, string StandardError);

public sealed record NpmBatchResult(bool Success, int InstalledCount);

public sealed record ToolCheckResult(bool IsInstalled, string? Version);
