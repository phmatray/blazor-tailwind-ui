using TailwindToolbox.Services;

namespace TailwindToolbox.Tests.Unit;

/// <summary>
/// Unit tests for FileGenerator service.
/// Tests template substitution and file generation logic.
/// </summary>
public class FileGeneratorTests
{
    [Fact]
    public async Task GenerateTailwindConfigAsync_SubstitutesVariablesCorrectly()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var generator = CreateFileGenerator();
        var outputPath = Path.Combine(tempDir, "tailwind.config.js");

        // Act
        await generator.GenerateTailwindConfigAsync(outputPath);

        // Assert
        Assert.True(File.Exists(outputPath));
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.Contains("'./**/*.razor'", content);
        Assert.Contains("'./**/*.html'", content);
        Assert.Contains("'./**/*.cshtml'", content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GeneratePackageJsonAsync_SubstitutesProjectName()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var generator = CreateFileGenerator();
        var outputPath = Path.Combine(tempDir, "package.json");
        var projectName = "my-blazor-app";

        // Act
        await generator.GeneratePackageJsonAsync(outputPath, projectName);

        // Assert
        Assert.True(File.Exists(outputPath));
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.Contains($"\"name\": \"{projectName}\"", content);
        Assert.Contains("\"tailwindcss\"", content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GenerateAppCssAsync_CreatesFileWithTailwindDirectives()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var generator = CreateFileGenerator();
        var outputPath = Path.Combine(tempDir, "app.css");

        // Act
        await generator.GenerateAppCssAsync(outputPath);

        // Assert
        Assert.True(File.Exists(outputPath));
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.Contains("@tailwind base;", content);
        Assert.Contains("@tailwind components;", content);
        Assert.Contains("@tailwind utilities;", content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GenerateBuildTargetsAsync_SubstitutesPathsCorrectly()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var generator = CreateFileGenerator();
        var outputPath = Path.Combine(tempDir, "TailwindBuild.targets");
        var targetName = "BuildTailwindCSS";
        var inputCss = "Styles/app.css";
        var outputCss = "wwwroot/css/app.css";

        // Act
        await generator.GenerateBuildTargetsAsync(outputPath, targetName, inputCss, outputCss);

        // Assert
        Assert.True(File.Exists(outputPath));
        var content = await File.ReadAllTextAsync(outputPath);
        Assert.Contains($"Name=\"{targetName}\"", content);
        Assert.Contains(inputCss, content);
        Assert.Contains(outputCss, content);
        Assert.Contains("--minify", content);

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GenerateFileAsync_CreatesDirectoryIfNotExists()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var generator = CreateFileGenerator();
        var nestedPath = Path.Combine(tempDir, "nested", "folder", "file.txt");

        // Act
        await generator.GenerateTailwindConfigAsync(nestedPath);

        // Assert
        Assert.True(File.Exists(nestedPath));
        Assert.True(Directory.Exists(Path.GetDirectoryName(nestedPath)));

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Fact]
    public async Task GenerateFileAsync_ThrowsWhenTemplateNotFound()
    {
        // Arrange
        var tempDir = CreateTempDirectory();
        var generator = CreateFileGenerator();
        var outputPath = Path.Combine(tempDir, "output.txt");

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            await generator.GenerateFromTemplateAsync("nonexistent.template", outputPath, new Dictionary<string, string>());
        });

        // Cleanup
        Directory.Delete(tempDir, true);
    }

    [Theory]
    [InlineData("{{PROJECT_NAME}}", "my-project", "my-project")]
    [InlineData("{{TARGET_NAME}}", "BuildCSS", "BuildCSS")]
    [InlineData("{{INPUT_CSS}}", "input.css", "input.css")]
    [InlineData("{{OUTPUT_CSS}}", "output.css", "output.css")]
    public void SubstituteVariables_ReplacesVariablesCorrectly(
        string template,
        string value,
        string expected)
    {
        // Arrange
        var generator = CreateFileGenerator();
        var variables = new Dictionary<string, string> { { template, value } };

        // Act
        var result = generator.SubstituteVariables(template, variables);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task LoadEmbeddedTemplate_LoadsTemplateFromAssembly()
    {
        // Arrange
        var generator = CreateFileGenerator();

        // Act
        var template = await generator.LoadEmbeddedTemplateAsync("tailwind.config.template.js");

        // Assert
        Assert.NotNull(template);
        Assert.NotEmpty(template);
        Assert.Contains("module.exports", template);
    }

    private static string CreateTempDirectory()
    {
        var tempPath = Path.Combine(Path.GetTempPath(), $"TailwindToolboxTest_{Guid.NewGuid()}");
        Directory.CreateDirectory(tempPath);
        return tempPath;
    }

    private static IFileGenerator CreateFileGenerator()
    {
        return new FileGenerator();
    }
}
