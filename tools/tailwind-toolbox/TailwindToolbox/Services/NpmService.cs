using System.Diagnostics;
using System.Text.Json;
using TailwindToolbox.Utilities;

namespace TailwindToolbox.Services;

/// <summary>
/// Service for executing npm commands via System.Diagnostics.Process.
/// </summary>
public sealed class NpmService : INpmService
{
    public async Task<NpmResult> InstallPackageAsync(string packageName, string version, string workingDirectory, TimeSpan? timeout = null)
    {
        var versionSpec = version == "latest" ? packageName : $"{packageName}@{version}";
        var command = $"install {versionSpec} --save";
        return await ExecuteNpmCommandAsync(command, workingDirectory, timeout ?? TimeSpan.FromMinutes(2));
    }

    public async Task<NpmBatchResult> InstallPackagesAsync(Dictionary<string, string> packages, string workingDirectory)
    {
        var installedCount = 0;
        foreach (var (packageName, version) in packages)
        {
            var result = await InstallPackageAsync(packageName, version, workingDirectory);
            if (result.Success)
            {
                installedCount++;
            }
        }

        return new NpmBatchResult(installedCount == packages.Count, installedCount);
    }

    public async Task<ToolCheckResult> CheckNodeInstalledAsync()
    {
        try
        {
            var result = await ExecuteCommandAsync("node", "--version", ".", TimeSpan.FromSeconds(5));
            if (result.ExitCode == 0)
            {
                var version = result.StandardOutput.Trim();
                return new ToolCheckResult(true, version);
            }
        }
        catch
        {
            // Node not found
        }

        return new ToolCheckResult(false, null);
    }

    public async Task<ToolCheckResult> CheckNpmInstalledAsync()
    {
        try
        {
            var result = await ExecuteCommandAsync("npm", "--version", ".", TimeSpan.FromSeconds(5));
            if (result.ExitCode == 0)
            {
                var version = result.StandardOutput.Trim();
                return new ToolCheckResult(true, version);
            }
        }
        catch
        {
            // npm not found
        }

        return new ToolCheckResult(false, null);
    }

    public async Task<NpmResult> ExecuteNpmCommandAsync(string command, string workingDirectory)
    {
        return await ExecuteNpmCommandAsync(command, workingDirectory, TimeSpan.FromMinutes(2));
    }

    private async Task<NpmResult> ExecuteNpmCommandAsync(string command, string workingDirectory, TimeSpan timeout)
    {
        // Check if npm is installed first
        var npmCheck = await CheckNpmInstalledAsync();
        if (!npmCheck.IsInstalled)
        {
            throw new InvalidOperationException("npm is not installed or not in PATH");
        }

        return await ExecuteCommandAsync("npm", command, workingDirectory, timeout);
    }

    public async Task<string?> GetInstalledVersionAsync(string packageName, string workingDirectory)
    {
        try
        {
            var result = await ExecuteNpmCommandAsync("list --json --depth=0", workingDirectory);
            if (result.Success)
            {
                var json = JsonDocument.Parse(result.StandardOutput);
                if (json.RootElement.TryGetProperty("dependencies", out var deps))
                {
                    if (deps.TryGetProperty(packageName, out var pkg))
                    {
                        if (pkg.TryGetProperty("version", out var version))
                        {
                            return version.GetString();
                        }
                    }
                }
            }
        }
        catch
        {
            // Package not found or JSON parsing failed
        }

        return null;
    }

    public async Task<List<Models.DependencyVersion>> CheckForUpdatesAsync(string workingDirectory)
    {
        var updates = new List<Models.DependencyVersion>();

        try
        {
            var result = await ExecuteNpmCommandAsync("outdated --json", workingDirectory);

            // npm outdated returns exit code 1 when updates are found
            if (result.StandardOutput.Length > 0)
            {
                var json = JsonDocument.Parse(result.StandardOutput);

                foreach (var package in json.RootElement.EnumerateObject())
                {
                    var packageName = package.Name;
                    string? current = null;
                    string? latest = null;

                    if (package.Value.TryGetProperty("current", out var currentProp))
                    {
                        current = currentProp.GetString();
                    }

                    if (package.Value.TryGetProperty("latest", out var latestProp))
                    {
                        latest = latestProp.GetString();
                    }

                    if (current != null && latest != null)
                    {
                        updates.Add(new Models.DependencyVersion
                        {
                            PackageName = packageName,
                            InstalledVersion = current,
                            RequiredVersion = current,
                            LatestVersion = latest
                        });
                    }
                }
            }
        }
        catch
        {
            // npm outdated failed or JSON parsing failed
        }

        return updates;
    }

    public bool DetectBreakingChanges(string currentVersion, string latestVersion)
    {
        return SemverUtility.DetectBreakingChanges(currentVersion, latestVersion);
    }

    public async Task<NpmResult> UpdatePackageAsync(string packageName, string version, string workingDirectory)
    {
        return await InstallPackageAsync(packageName, version, workingDirectory);
    }

    private static async Task<NpmResult> ExecuteCommandAsync(
        string fileName,
        string arguments,
        string workingDirectory,
        TimeSpan timeout)
    {
        var processStartInfo = new ProcessStartInfo
        {
            FileName = fileName,
            Arguments = arguments,
            WorkingDirectory = workingDirectory,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = new Process { StartInfo = processStartInfo };
        var outputBuilder = new System.Text.StringBuilder();
        var errorBuilder = new System.Text.StringBuilder();

        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                outputBuilder.AppendLine(e.Data);
            }
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                errorBuilder.AppendLine(e.Data);
            }
        };

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        var completed = await process.WaitForExitAsync(timeout);
        if (!completed)
        {
            process.Kill(true);
            return new NpmResult(false, -1, "", "Command timed out");
        }

        var exitCode = process.ExitCode;
        var stdout = outputBuilder.ToString();
        var stderr = errorBuilder.ToString();

        return new NpmResult(exitCode == 0, exitCode, stdout, stderr);
    }
}

internal static class ProcessExtensions
{
    public static async Task<bool> WaitForExitAsync(this Process process, TimeSpan timeout)
    {
        using var cts = new CancellationTokenSource(timeout);
        try
        {
            await process.WaitForExitAsync(cts.Token);
            return true;
        }
        catch (OperationCanceledException)
        {
            return false;
        }
    }
}
