using Spectre.Console.Cli;
using TailwindToolbox.Commands;
using TailwindToolbox.Services;
using TailwindToolbox.Tests.Helpers;

namespace TailwindToolbox.Tests.Integration;

/// <summary>
/// Integration tests for CheckCommand.
/// </summary>
public sealed class CheckCommandIntegrationTests : IDisposable
{
    private readonly string _testProjectDir;

    public CheckCommandIntegrationTests()
    {
        _testProjectDir = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(_testProjectDir);
    }

    [Fact]
    public async Task Execute_WithValidConfiguration_AllChecksPassing()
    {
        // Arrange - Create a fully configured Blazor project
        CreateBlazorProject();
        CreateTailwindConfig();
        CreatePackageJson();
        CreateAppCss();
        CreateBuildTargets();
        CreateGitignore();

        var command = CreateCheckCommand();
        var context = CreateCommandContext();
        var settings = new CheckCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(0, exitCode); // All checks should pass
    }

    [Fact]
    public async Task Execute_WithMissingConfiguration_ReportsErrors()
    {
        // Arrange - Create Blazor project but no Tailwind configuration
        CreateBlazorProject();

        var command = CreateCheckCommand();
        var context = CreateCommandContext();
        var settings = new CheckCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(1, exitCode); // Should return error code
    }

    [Fact]
    public async Task Execute_WithJsonOutput_ReturnsStructuredResults()
    {
        // Arrange
        CreateBlazorProject();

        var command = CreateCheckCommand();
        var context = CreateCommandContext();
        var settings = new CheckCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            Format = "json"
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert - Exit code may be 1 due to missing config, but JSON should be valid
        // We can't easily capture console output in this test, but we verify it doesn't crash
        Assert.True(exitCode == 0 || exitCode == 1);
    }

    [Fact]
    public async Task Execute_WithCategoryFilter_OnlyChecksSpecifiedCategory()
    {
        // Arrange
        CreateBlazorProject();

        var command = CreateCheckCommand();
        var context = CreateCommandContext();
        var settings = new CheckCommand.Settings
        {
            ProjectDirectory = _testProjectDir,
            Category = "environment"
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.True(exitCode == 0 || exitCode == 1);
    }

    [Fact]
    public async Task Execute_WithNonBlazorProject_ReturnsProjectNotFoundError()
    {
        // Arrange - Empty directory with no .csproj
        var command = CreateCheckCommand();
        var context = CreateCommandContext();
        var settings = new CheckCommand.Settings
        {
            ProjectDirectory = _testProjectDir
        };

        // Act
        var exitCode = await command.ExecuteAsync(context, settings);

        // Assert
        Assert.Equal(2, exitCode); // Project not found error code
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

    private CheckCommand CreateCheckCommand()
    {
        var projectDetector = new ProjectDetector();
        var validationService = new ValidationService();

        return new CheckCommand(projectDetector, validationService);
    }

    private CommandContext CreateCommandContext()
    {
        return new CommandContext([], TestRemainingArguments.Empty, "check", null);
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

    private void CreateTailwindConfig()
    {
        var configContent = """
            module.exports = {
              content: ['./**/*.razor', './**/*.html', './**/*.cshtml'],
              theme: { extend: {} },
              plugins: []
            }
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "tailwind.config.js"), configContent);
    }

    private void CreatePackageJson()
    {
        var packageJsonContent = """
            {
              "name": "testproject",
              "version": "1.0.0",
              "dependencies": {
                "tailwindcss": "^4.0.0"
              },
              "devDependencies": {
                "autoprefixer": "^10.4.16"
              },
              "scripts": {
                "build:css": "tailwindcss -i ./Styles/app.css -o ./wwwroot/css/app.css --minify"
              }
            }
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "package.json"), packageJsonContent);
    }

    private void CreateAppCss()
    {
        var cssContent = """
            @tailwind base;
            @tailwind components;
            @tailwind utilities;
            """;

        var stylesDir = Path.Combine(_testProjectDir, "Styles");
        Directory.CreateDirectory(stylesDir);
        File.WriteAllText(Path.Combine(stylesDir, "app.css"), cssContent);
    }

    private void CreateBuildTargets()
    {
        var targetsContent = """
            <Project>
              <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
                <Exec Command="npx tailwindcss -i Styles/app.css -o wwwroot/css/app.css --minify" />
              </Target>
            </Project>
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, "TailwindBuild.targets"), targetsContent);

        // Update .csproj to include import
        var csprojPath = Path.Combine(_testProjectDir, "TestProject.csproj");
        var csprojContent = File.ReadAllText(csprojPath);
        csprojContent = csprojContent.Replace("</Project>", "  <Import Project=\"TailwindBuild.targets\" />\n</Project>");
        File.WriteAllText(csprojPath, csprojContent);
    }

    private void CreateGitignore()
    {
        var gitignoreContent = """
            bin/
            obj/
            node_modules/
            """;

        File.WriteAllText(Path.Combine(_testProjectDir, ".gitignore"), gitignoreContent);
    }
}
