using Spectre.Console.Cli;
using TailwindToolbox.Commands;
using TailwindToolbox.Services;
using TailwindToolbox.Tests.Helpers;
using Xunit;

namespace TailwindToolbox.Tests.Integration;

/// <summary>
/// Integration tests for CreateTargetCommand.
/// </summary>
public sealed class CreateTargetCommandIntegrationTests : IDisposable
{
    private readonly string _testProjectDir;

    public CreateTargetCommandIntegrationTests()
    {
        _testProjectDir = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testProjectDir);
    }

    [Fact]
    public async Task Execute_CreatesTargetFileAndUpdatesCsproj()
    {
        // Arrange
        CreateBlazorProject();
        CreateStylesDirectory();

        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        // Verify .targets file was created
        var targetsFile = Path.Combine(_testProjectDir, "TailwindBuild.targets");
        Assert.True(File.Exists(targetsFile));

        // Verify .csproj was updated with Import
        var csprojPath = Path.Combine(_testProjectDir, "TestProject.csproj");
        var csprojContent = await File.ReadAllTextAsync(csprojPath);
        Assert.Contains("<Import Project=\"TailwindBuild.targets\"", csprojContent);
    }

    [Fact]
    public async Task Execute_WithCustomTargetName_UsesCustomName()
    {
        // Arrange
        CreateBlazorProject();
        CreateStylesDirectory();

        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            TargetName = "CompileTailwindCSS"
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        var targetsFile = Path.Combine(_testProjectDir, "TailwindBuild.targets");
        var content = await File.ReadAllTextAsync(targetsFile);
        Assert.Contains("CompileTailwindCSS", content);
    }

    [Fact]
    public async Task Execute_WithDryRun_DoesNotCreateFiles()
    {
        // Arrange
        CreateBlazorProject();
        CreateStylesDirectory();

        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            DryRun = true
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        // Verify .targets file was NOT created
        var targetsFile = Path.Combine(_testProjectDir, "TailwindBuild.targets");
        Assert.False(File.Exists(targetsFile));
    }

    [Fact]
    public async Task Execute_WithExistingTargetFile_AndNoForce_ReturnsError()
    {
        // Arrange
        CreateBlazorProject();
        CreateStylesDirectory();
        CreateExistingTargetsFile();

        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            Force = false
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(1, exitCode); // Error due to existing file
    }

    [Fact]
    public async Task Execute_WithExistingTargetFile_AndForce_Overwrites()
    {
        // Arrange
        CreateBlazorProject();
        CreateStylesDirectory();
        CreateExistingTargetsFile();

        var originalContent = await File.ReadAllTextAsync(
            Path.Combine(_testProjectDir, "TailwindBuild.targets"));

        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            Force = true
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        var targetsFile = Path.Combine(_testProjectDir, "TailwindBuild.targets");
        var newContent = await File.ReadAllTextAsync(targetsFile);
        Assert.NotEqual(originalContent, newContent);
    }

    [Fact]
    public async Task Execute_WithMinifyAlways_IncludesMinifyFlag()
    {
        // Arrange
        CreateBlazorProject();
        CreateStylesDirectory();

        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            Minify = "always"
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode);

        var targetsFile = Path.Combine(_testProjectDir, "TailwindBuild.targets");
        var content = await File.ReadAllTextAsync(targetsFile);
        Assert.Contains("--minify", content);
    }

    [Fact]
    public async Task Execute_WithNonBlazorProject_ReturnsError()
    {
        // Arrange - Empty directory with no .csproj
        var command = CreateCommand();
        var context = CreateCommandContext();
        var settings = new CreateTargetCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(2, exitCode); // Project not found error
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

    private CreateTargetCommand CreateCommand()
    {
        var projectDetector = new ProjectDetector();
        var fileGenerator = new FileGenerator();
        var targetFileGenerator = new TargetFileGenerator(fileGenerator);

        return new CreateTargetCommand(projectDetector, targetFileGenerator);
    }

    private CommandContext CreateCommandContext()
    {
        return new CommandContext([], TestRemainingArguments.Empty, "create-target", null);
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

    private void CreateStylesDirectory()
    {
        var stylesDir = Path.Combine(_testProjectDir, "Styles");
        Directory.CreateDirectory(stylesDir);

        var appCssContent = """
            @tailwind base;
            @tailwind components;
            @tailwind utilities;
            """;

        File.WriteAllText(Path.Combine(stylesDir, "app.css"), appCssContent);
    }

    private void CreateExistingTargetsFile()
    {
        var existingContent = """
            <Project>
              <Target Name="OldTarget">
                <Message Text="Old target file" />
              </Target>
            </Project>
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "TailwindBuild.targets"), existingContent);
    }
}
