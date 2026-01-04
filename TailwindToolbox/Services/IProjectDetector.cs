using TailwindToolbox.Models;

namespace TailwindToolbox.Services;

/// <summary>
/// Detects and analyzes Blazor projects from a directory or .csproj file.
/// </summary>
public interface IProjectDetector
{
    /// <summary>
    /// Detects a Blazor project in the specified directory.
    /// </summary>
    /// <param name="projectDirectory">The directory to search for a Blazor project.</param>
    /// <returns>A BlazorProject instance if found; otherwise, null.</returns>
    Task<BlazorProject?> DetectProjectAsync(string projectDirectory);
}
