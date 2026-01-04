using TailwindToolbox.Models;
using TailwindToolbox.Services;

namespace TailwindToolbox.Tests.Unit;

/// <summary>
/// Unit tests for ProjectDetector service.
/// Tests the .csproj parsing logic and project type detection.
/// </summary>
public class ProjectDetectorTests
{
    [Fact]
    public async Task DetectProjectAsync_WithBlazorServerProject_ReturnsServerType()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "TestProject.csproj");
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """;
        File.WriteAllText(csprojPath, csprojContent);

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(BlazorProjectType.Server, result.ProjectType);
        Assert.Equal("TestProject", result.ProjectName);
        Assert.Equal("net10.0", result.DotNetVersion);
        Assert.Equal(tempDir, result.ProjectDirectory);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithBlazorWebAssemblyProject_ReturnsWebAssemblyType()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "TestWasmProject.csproj");
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """;
        File.WriteAllText(csprojPath, csprojContent);

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(BlazorProjectType.WebAssembly, result.ProjectType);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithBlazorHybridProject_ReturnsHybridType()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "TestHybridProject.csproj");
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components.WebView" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """;
        File.WriteAllText(csprojPath, csprojContent);

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(BlazorProjectType.Hybrid, result.ProjectType);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithNoProjectFile_ReturnsNull()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.Null(result);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithNonBlazorProject_ReturnsUnknownType()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "RegularProject.csproj");
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
            </Project>
            """;
        File.WriteAllText(csprojPath, csprojContent);

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(BlazorProjectType.Unknown, result.ProjectType);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithTailwindConfigPresent_SetsTailwindConfigFlag()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "TestProject.csproj");
        var tailwindConfigPath = Path.Combine(tempDir, "tailwind.config.js");

        File.WriteAllText(csprojPath, """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """);
        File.WriteAllText(tailwindConfigPath, "module.exports = {}");

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasTailwindConfig);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithPackageJsonPresent_SetsPackageJsonFlag()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "TestProject.csproj");
        var packageJsonPath = Path.Combine(tempDir, "package.json");

        File.WriteAllText(csprojPath, """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """);
        File.WriteAllText(packageJsonPath, "{}");

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.HasPackageJson);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task DetectProjectAsync_WithWwwRootFolder_SetsWwwRootPath()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "TestProject.csproj");
        var wwwrootPath = Path.Combine(tempDir, "wwwroot");
        Directory.CreateDirectory(wwwrootPath);

        File.WriteAllText(csprojPath, """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <ItemGroup>
                <PackageReference Include="Microsoft.AspNetCore.Components" Version="10.0.0" />
              </ItemGroup>
            </Project>
            """);

        var detector = CreateProjectDetector();

        // Act
        var result = await detector.DetectProjectAsync(tempDir);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.WwwRootPath);
        Assert.Equal(wwwrootPath, result.WwwRootPath);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static IProjectDetector CreateProjectDetector()
    {
        return new ProjectDetector();
    }
}
