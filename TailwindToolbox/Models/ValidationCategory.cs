namespace TailwindToolbox.Models;

/// <summary>
/// Represents the category of a validation rule for organizing check command output.
/// </summary>
public enum ValidationCategory
{
    /// <summary>
    /// System requirements (Node.js, npm, dotnet).
    /// </summary>
    Environment,

    /// <summary>
    /// File existence and structure checks.
    /// </summary>
    Files,

    /// <summary>
    /// Configuration file content validation.
    /// </summary>
    Configuration,

    /// <summary>
    /// npm package version checks.
    /// </summary>
    Dependencies,

    /// <summary>
    /// MSBuild target and .csproj integration.
    /// </summary>
    Integration
}
