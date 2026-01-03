using TailwindToolbox.Services;
using Xunit;

namespace TailwindToolbox.Tests.Contract;

/// <summary>
/// Contract tests for NpmService.
/// Validates npm commands execute correctly with real npm/node runtime.
/// These tests require Node.js and npm to be installed on the test machine.
/// </summary>
public class NpmServiceContractTests : IDisposable
{
    private readonly string _testDirectory;

    public NpmServiceContractTests()
    {
        _testDirectory = CreateTempDirectory();
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NpmInstall_WithTailwindCss_InstallsSuccessfully()
    {
        // Arrange
        var service = CreateNpmService();
        var packageJsonPath = Path.Combine(_testDirectory, "package.json");
        await File.WriteAllTextAsync(packageJsonPath, """
            {
              "name": "test-project",
              "version": "1.0.0",
              "dependencies": {}
            }
            """);

        // Act
        var result = await service.InstallPackageAsync("tailwindcss", "4.0.0", _testDirectory);

        // Assert
        Assert.True(result.Success, $"npm install failed: {result.StandardError}");
        Assert.Equal(0, result.ExitCode);
        Assert.True(Directory.Exists(Path.Combine(_testDirectory, "node_modules")));
        Assert.True(Directory.Exists(Path.Combine(_testDirectory, "node_modules", "tailwindcss")));
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NpmInstall_WithAutoprefixer_InstallsSuccessfully()
    {
        // Arrange
        var service = CreateNpmService();
        var packageJsonPath = Path.Combine(_testDirectory, "package.json");
        await File.WriteAllTextAsync(packageJsonPath, """
            {
              "name": "test-project",
              "version": "1.0.0",
              "dependencies": {}
            }
            """);

        // Act
        var result = await service.InstallPackageAsync("autoprefixer", "10.4.16", _testDirectory);

        // Assert
        Assert.True(result.Success);
        Assert.True(Directory.Exists(Path.Combine(_testDirectory, "node_modules", "autoprefixer")));
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NodeVersion_ReturnsValidSemanticVersion()
    {
        // Arrange
        var service = CreateNpmService();

        // Act
        var result = await service.CheckNodeInstalledAsync();

        // Assert
        Assert.True(result.IsInstalled);
        Assert.NotNull(result.Version);
        Assert.Matches(@"v?\d+\.\d+\.\d+", result.Version); // Matches semantic versioning
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NpmVersion_ReturnsValidSemanticVersion()
    {
        // Arrange
        var service = CreateNpmService();

        // Act
        var result = await service.CheckNpmInstalledAsync();

        // Assert
        Assert.True(result.IsInstalled);
        Assert.NotNull(result.Version);
        Assert.Matches(@"\d+\.\d+\.\d+", result.Version);
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NpxTailwindCss_ExecutesSuccessfully()
    {
        // Arrange
        var service = CreateNpmService();

        // First install tailwindcss
        var packageJsonPath = Path.Combine(_testDirectory, "package.json");
        await File.WriteAllTextAsync(packageJsonPath, """
            {
              "name": "test-project",
              "version": "1.0.0",
              "dependencies": {}
            }
            """);
        await service.InstallPackageAsync("tailwindcss", "4.0.0", _testDirectory);

        // Create input CSS file
        var inputCssPath = Path.Combine(_testDirectory, "input.css");
        await File.WriteAllTextAsync(inputCssPath, "@tailwind base;\n@tailwind components;\n@tailwind utilities;");

        // Create tailwind config
        var configPath = Path.Combine(_testDirectory, "tailwind.config.js");
        await File.WriteAllTextAsync(configPath, "module.exports = { content: [] }");

        var outputCssPath = Path.Combine(_testDirectory, "output.css");

        // Act
        var result = await service.ExecuteNpmCommandAsync(
            $"npx tailwindcss -i {inputCssPath} -o {outputCssPath}",
            _testDirectory);

        // Assert
        Assert.True(result.Success, $"npx tailwindcss failed: {result.StandardError}");
        Assert.True(File.Exists(outputCssPath));
        var outputContent = await File.ReadAllTextAsync(outputCssPath);
        Assert.NotEmpty(outputContent);
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NpmList_WithInstalledPackage_ReturnsPackageInfo()
    {
        // Arrange
        var service = CreateNpmService();
        var packageJsonPath = Path.Combine(_testDirectory, "package.json");
        await File.WriteAllTextAsync(packageJsonPath, """
            {
              "name": "test-project",
              "version": "1.0.0",
              "dependencies": {}
            }
            """);
        await service.InstallPackageAsync("tailwindcss", "4.0.0", _testDirectory);

        // Act
        var version = await service.GetInstalledVersionAsync("tailwindcss", _testDirectory);

        // Assert
        Assert.NotNull(version);
        Assert.StartsWith("4.0", version);
    }

    [Fact(Skip = "Requires npm installed on test machine")]
    public async Task NpmInstall_WithInvalidPackageName_Fails()
    {
        // Arrange
        var service = CreateNpmService();
        var packageJsonPath = Path.Combine(_testDirectory, "package.json");
        await File.WriteAllTextAsync(packageJsonPath, """
            {
              "name": "test-project",
              "version": "1.0.0",
              "dependencies": {}
            }
            """);

        // Act
        var result = await service.InstallPackageAsync(
            "this-package-definitely-does-not-exist-xyz-123",
            "1.0.0",
            _testDirectory);

        // Assert
        Assert.False(result.Success);
        Assert.NotEqual(0, result.ExitCode);
        Assert.NotEmpty(result.StandardError);
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxContractTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static INpmService CreateNpmService()
    {
        return new NpmService();
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            try
            {
                Directory.Delete(_testDirectory, true);
            }
            catch
            {
                // Ignore cleanup errors (node_modules can be tricky to delete on Windows)
            }
        }
    }
}
