using TailwindToolbox.Commands;
using TailwindToolbox.Models;
using TailwindToolbox.Services;
using TailwindToolbox.Tests.Helpers;
using Spectre.Console.Cli;
using Xunit;

namespace TailwindToolbox.Tests.Integration;

/// <summary>
/// Integration tests for SetupCommand.
/// Tests the full setup flow with real file system operations in a temp directory.
/// </summary>
public class SetupCommandIntegrationTests : IDisposable
{
    private readonly string _testProjectDirectory;
    private readonly string _csprojPath;

    public SetupCommandIntegrationTests()
    {
        _testProjectDirectory = CreateTempDirectory();
        _csprojPath = Path.Combine(_testProjectDirectory, "TestBlazorApp.csproj");
        CreateBlazorProject();
    }

    [Fact]
    public async Task Execute_WithFreshBlazorProject_CreatesAllConfigurationFiles()
    {
        // Arrange
        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            SkipNpmInstall = true // Skip actual npm install in tests
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);
        Assert.True(File.Exists(Path.Combine(_testProjectDirectory, "tailwind.config.js")));
        Assert.True(File.Exists(Path.Combine(_testProjectDirectory, "package.json")));
        Assert.True(File.Exists(Path.Combine(_testProjectDirectory, "Styles", "app.css")));
        Assert.True(File.Exists(Path.Combine(_testProjectDirectory, "TailwindBuild.targets")));
    }

    [Fact]
    public async Task Execute_UpdatesCsprojWithImport()
    {
        // Arrange
        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            SkipNpmInstall = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);
        var csprojContent = await File.ReadAllTextAsync(_csprojPath);
        Assert.Contains("<Import Project=\"TailwindBuild.targets\" />", csprojContent);
    }

    [Fact]
    public async Task Execute_UpdatesGitignoreWithNodeModules()
    {
        // Arrange
        var gitignorePath = Path.Combine(_testProjectDirectory, ".gitignore");
        await File.WriteAllTextAsync(gitignorePath, "bin/\nobj/\n");

        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            SkipNpmInstall = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);
        var gitignoreContent = await File.ReadAllTextAsync(gitignorePath);
        Assert.Contains("node_modules/", gitignoreContent);
    }

    [Fact(Skip = "Requires interactive console (AnsiConsole.Confirm) - test manually or refactor for DI")]
    public async Task Execute_WithExistingConfig_AndNoForceFlag_ReturnsUserCancelledExitCode()
    {
        // Arrange
        var tailwindConfigPath = Path.Combine(_testProjectDirectory, "tailwind.config.js");
        await File.WriteAllTextAsync(tailwindConfigPath, "module.exports = {}");

        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            Force = false,
            SkipNpmInstall = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        // Exit code 2 means user cancelled (existing config detected, no force flag)
        Assert.Equal(2, exitCode);
    }

    [Fact]
    public async Task Execute_WithExistingConfig_AndForceFlag_OverwritesConfig()
    {
        // Arrange
        var tailwindConfigPath = Path.Combine(_testProjectDirectory, "tailwind.config.js");
        await File.WriteAllTextAsync(tailwindConfigPath, "OLD CONTENT");

        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            Force = true,
            SkipNpmInstall = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);
        var content = await File.ReadAllTextAsync(tailwindConfigPath);
        Assert.DoesNotContain("OLD CONTENT", content);
        Assert.Contains("module.exports", content);
    }

    [Fact]
    public async Task Execute_WithDryRun_DoesNotCreateFiles()
    {
        // Arrange
        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            DryRun = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);
        Assert.False(File.Exists(Path.Combine(_testProjectDirectory, "tailwind.config.js")));
        Assert.False(File.Exists(Path.Combine(_testProjectDirectory, "package.json")));
    }

    [Fact]
    public async Task Execute_WithCustomCssOutput_UsesCustomPath()
    {
        // Arrange
        var command = CreateSetupCommand();
        var customCssPath = "custom/output/app.css";
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            CssOutputPath = customCssPath,
            SkipNpmInstall = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);
        var targetsPath = Path.Combine(_testProjectDirectory, "TailwindBuild.targets");
        var targetsContent = await File.ReadAllTextAsync(targetsPath);
        Assert.Contains(customCssPath, targetsContent);
    }

    [Fact]
    public async Task Execute_WithNonBlazorProject_ReturnsErrorExitCode()
    {
        // Arrange
        var nonBlazorDir = CreateTempDirectory();
        var nonBlazorCsproj = Path.Combine(nonBlazorDir, "Regular.csproj");
        await File.WriteAllTextAsync(nonBlazorCsproj, """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
            </Project>
            """);

        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = nonBlazorDir
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(1, exitCode); // Error exit code

        // Cleanup
        Directory.Delete(nonBlazorDir, true);
    }

    [Fact]
    public async Task Execute_CreatesProjectStructure_MatchingBlazorProjectType()
    {
        // Arrange
        var command = CreateSetupCommand();
        var settings = new SetupCommand.Settings
        {
            ProjectDirectory = _testProjectDirectory,
            SkipNpmInstall = true
        };

        // Act
        var context = new CommandContext(Array.Empty<string>(), TestRemainingArguments.Empty, "setup", null);
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        // Verify Styles directory was created
        Assert.True(Directory.Exists(Path.Combine(_testProjectDirectory, "Styles")));

        // Verify tailwind.config.js has correct content paths for Blazor
        var tailwindConfig = await File.ReadAllTextAsync(
            Path.Combine(_testProjectDirectory, "tailwind.config.js"));
        Assert.Contains(".razor", tailwindConfig);
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
        File.WriteAllText(_csprojPath, csprojContent);

        // Create wwwroot directory (typical for Blazor projects)
        Directory.CreateDirectory(Path.Combine(_testProjectDirectory, "wwwroot"));
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static SetupCommand CreateSetupCommand()
    {
        var projectDetector = new ProjectDetector();
        var fileGenerator = new FileGenerator();
        var npmService = new NpmService();
        var targetFileGenerator = new TargetFileGenerator(fileGenerator);

        return new SetupCommand(projectDetector, fileGenerator, npmService, targetFileGenerator);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testProjectDirectory))
        {
            Directory.Delete(_testProjectDirectory, true);
        }
    }
}
