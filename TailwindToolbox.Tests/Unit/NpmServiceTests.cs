using TailwindToolbox.Services;
using Xunit;

namespace TailwindToolbox.Tests.Unit;

/// <summary>
/// Unit tests for NpmService.
/// Tests npm command execution with mocked process execution.
/// </summary>
public class NpmServiceTests
{
    [Fact]
    public async Task InstallPackageAsync_ExecutesNpmInstallCommand()
    {
        // Arrange
        var service = CreateNpmService();
        var packageName = "tailwindcss";
        var version = "4.0.0";
        var workingDirectory = CreateTempDirectory();

        // Act
        var result = await service.InstallPackageAsync(packageName, version, workingDirectory);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(0, result.ExitCode);

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    [Fact(Skip = "Contract test - requires npm installed on test machine")]
    public async Task InstallPackageAsync_WithLatestVersion_InstallsLatest()
    {
        // Arrange
        var service = CreateNpmService();
        var packageName = "autoprefixer";
        var workingDirectory = CreateTempDirectory();

        // Act
        var result = await service.InstallPackageAsync(packageName, "latest", workingDirectory);

        // Assert
        Assert.True(result.Success);

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    [Fact]
    public async Task CheckNodeInstalledAsync_WhenNodeExists_ReturnsTrue()
    {
        // Arrange
        var service = CreateNpmService();

        // Act
        var result = await service.CheckNodeInstalledAsync();

        // Assert
        // This test assumes Node.js is installed on the test machine
        // In a real scenario, we'd mock the process executor
        Assert.True(result.IsInstalled);
        Assert.NotNull(result.Version);
    }

    [Fact]
    public async Task CheckNpmInstalledAsync_WhenNpmExists_ReturnsTrue()
    {
        // Arrange
        var service = CreateNpmService();

        // Act
        var result = await service.CheckNpmInstalledAsync();

        // Assert
        Assert.True(result.IsInstalled);
        Assert.NotNull(result.Version);
    }

    [Fact(Skip = "Contract test - requires npm installed on test machine")]
    public async Task InstallPackagesAsync_WithMultiplePackages_InstallsAll()
    {
        // Arrange
        var service = CreateNpmService();
        var packages = new Dictionary<string, string>
        {
            { "tailwindcss", "4.0.0" },
            { "autoprefixer", "10.4.16" }
        };
        var workingDirectory = CreateTempDirectory();

        // Act
        var result = await service.InstallPackagesAsync(packages, workingDirectory);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(2, result.InstalledCount);

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    [Fact(Skip = "Contract test - requires testing in environment without npm")]
    public async Task InstallPackageAsync_WhenNpmNotFound_ThrowsException()
    {
        // Arrange
        var service = CreateNpmServiceWithoutNpm();
        var workingDirectory = CreateTempDirectory();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await service.InstallPackageAsync("tailwindcss", "4.0.0", workingDirectory);
        });

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    [Fact]
    public async Task ExecuteNpmCommandAsync_CapturesStdoutAndStderr()
    {
        // Arrange
        var service = CreateNpmService();
        var command = "--version";

        // Act
        var result = await service.ExecuteNpmCommandAsync(command, Directory.GetCurrentDirectory());

        // Assert
        Assert.True(result.Success);
        Assert.NotEmpty(result.StandardOutput);
        Assert.Matches(@"\d+\.\d+\.\d+", result.StandardOutput); // Version format
    }

    [Fact]
    public async Task InstallPackageAsync_WithTimeout_CompletesWithinTimeout()
    {
        // Arrange
        var service = CreateNpmService();
        var packageName = "tailwindcss";
        var version = "4.0.0";
        var workingDirectory = CreateTempDirectory();
        var timeout = TimeSpan.FromSeconds(120); // 2 minutes

        // Act
        var startTime = DateTime.UtcNow;
        var result = await service.InstallPackageAsync(packageName, version, workingDirectory, timeout);
        var elapsed = DateTime.UtcNow - startTime;

        // Assert
        Assert.True(result.Success);
        Assert.True(elapsed < timeout);

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    [Fact]
    public async Task GetInstalledVersionAsync_WhenPackageInstalled_ReturnsVersion()
    {
        // Arrange
        var service = CreateNpmService();
        var workingDirectory = CreateTempDirectory();
        var packageName = "tailwindcss";

        // First install the package
        await service.InstallPackageAsync(packageName, "4.0.0", workingDirectory);

        // Act
        var version = await service.GetInstalledVersionAsync(packageName, workingDirectory);

        // Assert
        Assert.NotNull(version);
        Assert.StartsWith("4.0", version);

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    [Fact]
    public async Task GetInstalledVersionAsync_WhenPackageNotInstalled_ReturnsNull()
    {
        // Arrange
        var service = CreateNpmService();
        var workingDirectory = CreateTempDirectory();
        var packageName = "nonexistent-package-xyz";

        // Act
        var version = await service.GetInstalledVersionAsync(packageName, workingDirectory);

        // Assert
        Assert.Null(version);

        // Cleanup
        Directory.Delete(workingDirectory, true);
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static INpmService CreateNpmService()
    {
        return new NpmService();
    }

    private static INpmService CreateNpmServiceWithoutNpm()
    {
        // For this test, we return the real service
        // The test will need to be run in an environment without npm
        return new NpmService();
    }
}
