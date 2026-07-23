using System.Reflection;

namespace TailwindToolbox.Services;

/// <summary>
/// Generates configuration files from embedded templates.
/// </summary>
public sealed class FileGenerator : IFileGenerator
{
    public async Task GenerateTailwindConfigAsync(string outputPath)
    {
        var template = await LoadEmbeddedTemplateAsync("tailwind.config.template.js");
        await WriteFileAsync(outputPath, template);
    }

    public async Task GeneratePackageJsonAsync(string outputPath, string projectName)
    {
        var template = await LoadEmbeddedTemplateAsync("package.template.json");
        var variables = new Dictionary<string, string>
        {
            { "{{PROJECT_NAME}}", projectName.ToLowerInvariant().Replace(" ", "-") }
        };
        var content = SubstituteVariables(template, variables);
        await WriteFileAsync(outputPath, content);
    }

    public async Task GenerateAppCssAsync(string outputPath)
    {
        var template = await LoadEmbeddedTemplateAsync("app.template.css");
        await WriteFileAsync(outputPath, template);
    }

    public async Task GenerateBuildTargetsAsync(string outputPath, string targetName, string inputCss, string outputCss)
    {
        var template = await LoadEmbeddedTemplateAsync("TailwindBuild.template.targets");
        var variables = new Dictionary<string, string>
        {
            { "{{TARGET_NAME}}", targetName },
            { "{{INPUT_CSS}}", inputCss },
            { "{{OUTPUT_CSS}}", outputCss }
        };
        var content = SubstituteVariables(template, variables);
        await WriteFileAsync(outputPath, content);
    }

    public async Task GenerateFromTemplateAsync(string templateName, string outputPath, Dictionary<string, string> variables)
    {
        var template = await LoadEmbeddedTemplateAsync(templateName);
        var content = SubstituteVariables(template, variables);
        await WriteFileAsync(outputPath, content);
    }

    public string SubstituteVariables(string template, Dictionary<string, string> variables)
    {
        var result = template;
        foreach (var (key, value) in variables)
        {
            result = result.Replace(key, value);
        }
        return result;
    }

    public async Task<string> LoadEmbeddedTemplateAsync(string templateName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"TailwindToolbox.Templates.{templateName}";

        await using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new InvalidOperationException($"Template not found: {templateName}");
        }

        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

    private static async Task WriteFileAsync(string filePath, string content)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await File.WriteAllTextAsync(filePath, content);
    }
}
