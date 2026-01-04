using Spectre.Console.Cli;
using TailwindToolbox.Commands;
using TailwindToolbox.Services;
using TailwindToolbox.Tests.Helpers;
using Xunit;

namespace TailwindToolbox.Tests.Integration;

/// <summary>
/// Integration tests for UpdateCommand.
/// </summary>
public sealed class UpdateCommandIntegrationTests : IDisposable
{
    private readonly string _testProjectDir;

    public UpdateCommandIntegrationTests()
    {
        _testProjectDir = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testProjectDir);
    }

    [Fact(Skip = "Contract test - requires npm and network access")]
    public async Task Execute_WithPatchUpdate_UpdatesSuccessfully()
    {
        // Arrange - Create project with outdated patch version
        CreateBlazorProject();
        CreatePackageJsonWithOldVersion("4.0.0"); // Assume 4.0.5 is latest

        var command = CreateUpdateCommand();
        var context = CreateCommandContext();
        var settings = new UpdateCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode); // Should succeed without warnings
    }

    [Fact(Skip = "Contract test - requires npm and network access")]
    public async Task Execute_WithBreakingChanges_DisplaysWarning()
    {
        // Arrange - Create project with old major version
        CreateBlazorProject();
        CreatePackageJsonWithOldVersion("3.4.1"); // Assume 4.x is latest

        var command = CreateUpdateCommand();
        var context = CreateCommandContext();
        var settings = new UpdateCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        // Exit code depends on user interaction
        // In automated test, may return 2 (user cancelled) or 0 (accepted)
        Assert.True(exitCode == 0 || exitCode == 2);
    }

    [Fact]
    public async Task Execute_WithDryRun_DoesNotModifyFiles()
    {
        // Arrange
        CreateBlazorProject();
        CreatePackageJsonWithOldVersion("4.0.0");

        var originalContent = File.ReadAllText(Path.Combine(_testProjectDir, "package.json"));

        var command = CreateUpdateCommand();
        var context = CreateCommandContext();
        var settings = new UpdateCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            DryRun = true
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        var currentContent = File.ReadAllText(Path.Combine(_testProjectDir, "package.json"));
        Assert.Equal(originalContent, currentContent); // File should be unchanged
    }

    [Fact(Skip = "Contract test - requires npm and network access")]
    public async Task Execute_WithSkipBreaking_OnlyUpdatesMinorAndPatch()
    {
        // Arrange
        CreateBlazorProject();
        CreatePackageJsonWithOldVersion("3.4.1"); // Assume 4.x is latest

        var command = CreateUpdateCommand();
        var context = CreateCommandContext();
        var settings = new UpdateCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            SkipBreaking = true
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        // Verify version is still 3.x (latest 3.x patch)
        var packageJson = File.ReadAllText(Path.Combine(_testProjectDir, "package.json"));
        Assert.Contains("\"tailwindcss\": \"3.", packageJson);
    }

    [Fact]
    public async Task Execute_WithNoUpdatesAvailable_ReturnsExitCode3()
    {
        // Arrange
        CreateBlazorProject();
        CreatePackageJsonWithLatestVersion(); // Already up to date

        var command = CreateUpdateCommand();
        var context = CreateCommandContext();
        var settings = new UpdateCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(3, exitCode); // No updates available
    }

    public void Dispose()
    {
        if (Directory.Exists(_testProjectDir))
        {
            try
            {
                Directory.Delete(_testProjectDir, true);
            }
            catch
            {
                // Best effort cleanup
            }
        }
    }

    private UpdateCommand CreateUpdateCommand()
    {
        var projectDetector = new ProjectDetector();
        var npmService = new NpmService();

        return new UpdateCommand(projectDetector, npmService);
    }

    private CommandContext CreateCommandContext()
    {
        return new CommandContext([], TestRemainingArguments.Empty, "update", null);
    }

    private void CreateBlazorProject()
    {
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
                <Nullable>enable</Nullable>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "TestProject.csproj"), csprojContent);

        // Create wwwroot directory
        Directory.CreateDirectory(Path.Combine(_testProjectDir, "wwwroot"));
    }

    private void CreatePackageJsonWithOldVersion(string version)
    {
        var packageJsonContent = $$"""
            {
              "name": "testproject",
              "version": "1.0.0",
              "dependencies": {
                "tailwindcss": "{{version}}"
              },
              "devDependencies": {
                "autoprefixer": "10.4.16"
              }
            }
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "package.json"), packageJsonContent);
    }

    private void CreatePackageJsonWithLatestVersion()
    {
        var packageJsonContent = """
            {
              "name": "testproject",
              "version": "1.0.0",
              "dependencies": {
                "tailwindcss": "4.0.5"
              },
              "devDependencies": {
                "autoprefixer": "10.4.20"
              }
            }
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "package.json"), packageJsonContent);
    }
}
