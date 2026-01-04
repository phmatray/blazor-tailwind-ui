using System.Xml.Linq;
using TailwindToolbox.Services;

namespace TailwindToolbox.Tests.Contract;

/// <summary>
/// Contract tests for MSBuild target file generation.
/// Validates that generated .targets files conform to MSBuild XML schema.
/// </summary>
public class MSBuildTargetContractTests : IDisposable
{
    private readonly string _testDirectory;

    public MSBuildTargetContractTests()
    {
        _testDirectory = CreateTempDirectory();
    }

    [Fact]
    public async Task GeneratedTargetsFile_IsWellFormedXml()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var targetsPath = Path.Combine(_testDirectory, "TailwindBuild.targets");

        // Act
        await generator.GenerateTargetFileAsync(
            targetsPath,
            "BuildTailwindCSS",
            "Styles/app.css",
            "wwwroot/css/app.css");

        // Assert
        Assert.True(File.Exists(targetsPath));

        // Verify it's valid XML
        var xmlDoc = XDocument.Load(targetsPath);
        Assert.NotNull(xmlDoc.Root);
        Assert.Equal("Project", xmlDoc.Root.Name.LocalName);
    }

    [Fact]
    public async Task GeneratedTargetsFile_ContainsRequiredElements()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var targetsPath = Path.Combine(_testDirectory, "TailwindBuild.targets");
        var targetName = "BuildTailwindCSS";

        // Act
        await generator.GenerateTargetFileAsync(
            targetsPath,
            targetName,
            "Styles/app.css",
            "wwwroot/css/app.css");

        // Assert
        var xmlDoc = XDocument.Load(targetsPath);

        // Verify Target element exists
        var targetElement = xmlDoc.Descendants("Target").FirstOrDefault();
        Assert.NotNull(targetElement);
        Assert.Equal(targetName, targetElement.Attribute("Name")?.Value);

        // Verify BeforeTargets attribute
        Assert.NotNull(targetElement.Attribute("BeforeTargets"));
        Assert.Equal("BeforeBuild", targetElement.Attribute("BeforeTargets")?.Value);

        // Verify Exec element exists
        var execElements = targetElement.Descendants("Exec").ToList();
        Assert.NotEmpty(execElements);
        Assert.True(execElements.Count >= 1); // At least one Exec element
    }

    [Fact]
    public async Task GeneratedTargetsFile_ContainsNpxTailwindCssCommand()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var targetsPath = Path.Combine(_testDirectory, "TailwindBuild.targets");
        var inputCss = "Styles/app.css";
        var outputCss = "wwwroot/css/app.css";

        // Act
        await generator.GenerateTargetFileAsync(
            targetsPath,
            "BuildTailwindCSS",
            inputCss,
            outputCss);

        // Assert
        var xmlDoc = XDocument.Load(targetsPath);
        var execElements = xmlDoc.Descendants("Exec").ToList();

        var hasCorrectCommand = execElements.Any(exec =>
        {
            var command = exec.Attribute("Command")?.Value;
            return command != null &&
                   command.Contains("npx tailwindcss") &&
                   command.Contains($"-i {inputCss}") &&
                   command.Contains($"-o {outputCss}");
        });

        Assert.True(hasCorrectCommand, "No Exec element found with correct tailwindcss command");
    }

    [Fact]
    public async Task GeneratedTargetsFile_HasConfigurationBasedMinification()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var targetsPath = Path.Combine(_testDirectory, "TailwindBuild.targets");

        // Act
        await generator.GenerateTargetFileAsync(
            targetsPath,
            "BuildTailwindCSS",
            "Styles/app.css",
            "wwwroot/css/app.css");

        // Assert
        var xmlDoc = XDocument.Load(targetsPath);
        var execElements = xmlDoc.Descendants("Exec").ToList();

        // Should have separate commands for Debug and Release
        var hasReleaseCondition = execElements.Any(exec =>
            exec.Attribute("Condition")?.Value.Contains("Release") == true &&
            exec.Attribute("Command")?.Value.Contains("--minify") == true);

        var hasDebugCondition = execElements.Any(exec =>
            exec.Attribute("Condition")?.Value.Contains("Debug") == true &&
            exec.Attribute("Command")?.Value.Contains("--minify") == false);

        Assert.True(hasReleaseCondition, "No Exec element with Release condition and --minify flag");
        Assert.True(hasDebugCondition, "No Exec element with Debug condition without --minify flag");
    }

    [Fact]
    public async Task GeneratedTargetsFile_HasWorkingDirectoryAttribute()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var targetsPath = Path.Combine(_testDirectory, "TailwindBuild.targets");

        // Act
        await generator.GenerateTargetFileAsync(
            targetsPath,
            "BuildTailwindCSS",
            "Styles/app.css",
            "wwwroot/css/app.css");

        // Assert
        var xmlDoc = XDocument.Load(targetsPath);
        var execElements = xmlDoc.Descendants("Exec").ToList();

        var allHaveWorkingDirectory = execElements.All(exec =>
            exec.Attribute("WorkingDirectory")?.Value == "$(ProjectDir)");

        Assert.True(allHaveWorkingDirectory, "Not all Exec elements have WorkingDirectory=\"$(ProjectDir)\"");
    }

    [Fact]
    public async Task GeneratedTargetsFile_CanBeImportedByCsproj()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var targetsPath = Path.Combine(_testDirectory, "TailwindBuild.targets");
        var csprojPath = Path.Combine(_testDirectory, "TestProject.csproj");

        await generator.GenerateTargetFileAsync(
            targetsPath,
            "BuildTailwindCSS",
            "Styles/app.css",
            "wwwroot/css/app.css");

        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <Import Project="TailwindBuild.targets" />
            </Project>
            """;
        await File.WriteAllTextAsync(csprojPath, csprojContent);

        // Act
        var csprojXml = XDocument.Load(csprojPath);

        // Assert
        // Verify the Import element exists and points to the targets file
        var importElement = csprojXml.Descendants("Import")
            .FirstOrDefault(i => i.Attribute("Project")?.Value == "TailwindBuild.targets");

        Assert.NotNull(importElement);

        // Verify both files are valid XML and can be parsed together
        var targetsXml = XDocument.Load(targetsPath);
        Assert.NotNull(targetsXml.Root);
        Assert.NotNull(csprojXml.Root);
    }

    [Fact]
    public async Task UpdateCsprojWithImport_AddsImportElement()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var csprojPath = Path.Combine(_testDirectory, "TestProject.csproj");
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
            </Project>
            """;
        await File.WriteAllTextAsync(csprojPath, csprojContent);

        // Act
        await generator.UpdateCsprojWithImportAsync(csprojPath, "TailwindBuild.targets");

        // Assert
        var updatedContent = await File.ReadAllTextAsync(csprojPath);
        Assert.Contains("<Import Project=\"TailwindBuild.targets\" />", updatedContent);

        // Verify it's still valid XML
        var xmlDoc = XDocument.Load(csprojPath);
        Assert.NotNull(xmlDoc.Root);
    }

    [Fact]
    public async Task UpdateCsprojWithImport_DoesNotDuplicateImport()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var csprojPath = Path.Combine(_testDirectory, "TestProject.csproj");
        var csprojContent = """
            <Project Sdk="Microsoft.NET.Sdk.Web">
              <PropertyGroup>
                <TargetFramework>net10.0</TargetFramework>
              </PropertyGroup>
              <Import Project="TailwindBuild.targets" />
            </Project>
            """;
        await File.WriteAllTextAsync(csprojPath, csprojContent);

        // Act
        await generator.UpdateCsprojWithImportAsync(csprojPath, "TailwindBuild.targets");

        // Assert
        var updatedContent = await File.ReadAllTextAsync(csprojPath);
        var importCount = updatedContent.Split("TailwindBuild.targets").Length - 1;
        Assert.Equal(1, importCount); // Should only appear once
    }

    [Fact]
    public void ValidateTargetsXml_WithValidStructure_DoesNotThrow()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var validXml = """
            <Project>
              <Target Name="BuildTailwindCSS" BeforeTargets="BeforeBuild">
                <Exec Command="npx tailwindcss -i input.css -o output.css" WorkingDirectory="$(ProjectDir)" />
              </Target>
            </Project>
            """;

        // Act & Assert
        var exception = Record.Exception(() =>
            generator.ValidateTargetsXml(validXml));

        Assert.Null(exception);
    }

    [Fact]
    public void ValidateTargetsXml_WithMalformedXml_Throws()
    {
        // Arrange
        var generator = CreateTargetFileGenerator();
        var malformedXml = "<Project><Target Name=\"Test\">";  // Unclosed tags

        // Act & Assert
        Assert.Throws<System.Xml.XmlException>(() =>
            generator.ValidateTargetsXml(malformedXml));
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxContractTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static ITargetFileGenerator CreateTargetFileGenerator()
    {
        var fileGenerator = new FileGenerator();
        return new TargetFileGenerator(fileGenerator);
    }

    public void Dispose()
    {
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }
}
