using System.Xml.Linq;
using TailwindToolbox.Services;
using Xunit;

namespace TailwindToolbox.Tests.Unit;

/// <summary>
/// Unit tests for TargetFileGenerator.
/// </summary>
public sealed class TargetFileGeneratorTests
{
    [Fact]
    public async Task GenerateTargetFile_CreatesValidXml()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var outputPath = Path.Combine(tempDir, "TailwindBuild.targets");
        var generator = CreateTargetFileGenerator();

        // Act
        await generator.GenerateTargetFileAsync(
            outputPath,
            "BuildTailwindCSS",
            "Styles/app.css",
            "wwwroot/css/app.css");

        // Assert
        Assert.True(File.Exists(outputPath));
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.NotEmpty(content);

        // Verify XML is well-formed
        var doc = XDocument.Parse(content);
        Assert.NotNull(doc.Root);
        Assert.Equal("Project", doc.Root.Name.LocalName);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GenerateTargetFile_ContainsCorrectTargetName()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var outputPath = Path.Combine(tempDir, "TailwindBuild.targets");
        var generator = CreateTargetFileGenerator();
        var targetName = "CompileTailwind";

        // Act
        await generator.GenerateTargetFileAsync(
            outputPath,
            targetName,
            "Styles/app.css",
            "wwwroot/css/app.css");

        // Assert
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.Contains(targetName, content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GenerateTargetFile_ContainsInputAndOutputPaths()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var outputPath = Path.Combine(tempDir, "TailwindBuild.targets");
        var generator = CreateTargetFileGenerator();
        var inputCss = "Styles/app.css";
        var outputCss = "wwwroot/css/app.css";

        // Act
        await generator.GenerateTargetFileAsync(
            outputPath,
            "BuildTailwindCSS",
            inputCss,
            outputCss);

        // Assert
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.Contains(inputCss, content);
        Assert.Contains(outputCss, content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task UpdateCsprojImport_AddsImportElement()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "Test.csproj");
        var targetsFileName = "TailwindBuild.targets";

        // Create a minimal .csproj file
        var initialCsproj = """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
            </Project>
            """;
        await File.WriteAllTextAsync(csprojPath, initialCsproj);

        var generator = CreateTargetFileGenerator();

        // Act
        await generator.UpdateCsprojWithImportAsync(csprojPath, targetsFileName);

        // Assert
        var content = await File.ReadAllTextAsync(csprojPath);
        Assert.Contains($"<Import Project=\"{targetsFileName}\"", content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task UpdateCsprojImport_DoesNotDuplicateExistingImport()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var csprojPath = Path.Combine(tempDir, "Test.csproj");
        var targetsFileName = "TailwindBuild.targets";

        // Create .csproj with existing import
        var initialCsproj = $"""
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <Import Project="{targetsFileName}" />
            </Project>
            """;
        await File.WriteAllTextAsync(csprojPath, initialCsproj);

        var generator = CreateTargetFileGenerator();

        // Act
        await generator.UpdateCsprojWithImportAsync(csprojPath, targetsFileName);

        // Assert
        var content = await File.ReadAllTextAsync(csprojPath);
        var importCount = content.Split($"<Import Project=\"{targetsFileName}\"").Length - 1;
        Assert.Equal(1, importCount); // Should still be just one

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public void ValidateTargetsXml_WithValidXml_DoesNotThrow()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var validXml = """
            <Project>
              <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
                <Exec Command="npx tailwindcss -i Styles/app.css -o wwwroot/css/app.css" />
              </Target>
            </Project>
            """;

        // Act & Assert
        var exception = Record.Exception(() => generator.ValidateTargetsXml(validXml));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateTargetsXml_WithInvalidXml_Throws()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var invalidXml = "<Project><Target>"; // Unclosed tags

        // Act & Assert
        Assert.Throws<System.Xml.XmlException>(() => generator.ValidateTargetsXml(invalidXml));
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static ITargetFileGenerator CreateTargetFileGenerator()
    {
        var fileGenerator = new FileGenerator();
        return new TargetFileGenerator(fileGenerator);
    }
}
