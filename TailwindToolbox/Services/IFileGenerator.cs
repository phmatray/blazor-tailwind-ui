namespace TailwindToolbox.Services;

/// <summary>
/// Generates configuration files from embedded templates.
/// </summary>
public interface IFileGenerator
{
    /// <summary>
    /// Generates tailwind.config.js file.
    /// </summary>
    Task GenerateTailwindConfigAsync(string outputPath);

    /// <summary>
    /// Generates package.json file with project name substitution.
    /// </summary>
    Task GeneratePackageJsonAsync(string outputPath, string projectName);

    /// <summary>
    /// Generates app.css file with Tailwind directives.
    /// </summary>
    Task GenerateAppCssAsync(string outputPath);

    /// <summary>
    /// Generates MSBuild .targets file with variable substitution.
    /// </summary>
    Task GenerateBuildTargetsAsync(string outputPath, string targetName, string inputCss, string outputCss);

    /// <summary>
    /// Generates a file from a template with variable substitution.
    /// </summary>
    Task GenerateFromTemplateAsync(string templateName, string outputPath, Dictionary<string, string> variables);

    /// <summary>
    /// Substitutes variables in template content.
    /// </summary>
    string SubstituteVariables(string template, Dictionary<string, string> variables);

    /// <summary>
    /// Loads an embedded template from the assembly.
    /// </summary>
    Task<string> LoadEmbeddedTemplateAsync(string templateName);
}
