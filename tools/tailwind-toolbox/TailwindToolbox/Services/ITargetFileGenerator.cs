namespace TailwindToolbox.Services;

/// <summary>
/// Generates and manages MSBuild .targets files.
/// </summary>
public interface ITargetFileGenerator
{
    /// <summary>
    /// Generates an MSBuild .targets file.
    /// </summary>
    Task GenerateTargetFileAsync(string outputPath, string targetName, string inputCss, string outputCss);

    /// <summary>
    /// Updates a .csproj file to import a .targets file.
    /// </summary>
    Task UpdateCsprojWithImportAsync(string csprojPath, string targetsFileName);

    /// <summary>
    /// Validates that the targets XML is well-formed.
    /// </summary>
    void ValidateTargetsXml(string xml);
}
