using System.Xml.Linq;
using TailwindToolbox.Models;

namespace TailwindToolbox.Services;

/// <summary>
/// Detects and analyzes Blazor projects from a directory.
/// </summary>
public sealed class ProjectDetector : IProjectDetector
{
    public async Task<BlazorProject?> DetectProjectAsync(string projectDirectory)
    {
        // Find .csproj file
        var csprojFiles = Directory.GetFiles(projectDirectory, "*.csproj");
        if (csprojFiles.Length == 0)
        {
            return null;
        }

        var csprojPath = csprojFiles[0];
        var projectName = Path.GetFileNameWithoutExtension(csprojPath);

        // Parse .csproj XML
        var doc = await LoadCsprojAsync(csprojPath);
        if (doc?.Root == null)
        {
            return null;
        }

        var targetFramework = GetTargetFramework(doc);
        var projectType = DetectBlazorProjectType(doc);

        // Check for existing configuration files
        var hasTailwindConfig = File.Exists(Path.Combine(projectDirectory, "tailwind.config.js"));
        var hasPackageJson = File.Exists(Path.Combine(projectDirectory, "package.json"));
        var hasBuildTargets = Directory.GetFiles(projectDirectory, "*.targets").Length > 0;

        // Find wwwroot directory
        var wwwrootPath = Path.Combine(projectDirectory, "wwwroot");
        var wwwroot = Directory.Exists(wwwrootPath) ? wwwrootPath : null;

        return new BlazorProject
        {
            ProjectFilePath = csprojPath,
            ProjectDirectory = projectDirectory,
            ProjectName = projectName,
            DotNetVersion = targetFramework ?? "unknown",
            ProjectType = projectType,
            HasTailwindConfig = hasTailwindConfig,
            HasPackageJson = hasPackageJson,
            HasBuildTargets = hasBuildTargets,
            WwwRootPath = wwwroot
        };
    }

    private static async Task<XDocument?> LoadCsprojAsync(string csprojPath)
    {
        try
        {
            var content = await File.ReadAllTextAsync(csprojPath);
            return XDocument.Parse(content);
        }
        catch
        {
            return null;
        }
    }

    private static string? GetTargetFramework(XDocument doc)
    {
        return doc.Descendants("TargetFramework").FirstOrDefault()?.Value;
    }

    private static BlazorProjectType DetectBlazorProjectType(XDocument doc)
    {
        var root = doc.Root;
        if (root == null)
        {
            return BlazorProjectType.Unknown;
        }

        // Check SDK attribute
        var sdk = root.Attribute("Sdk")?.Value ?? "";

        // Check for package references
        var packageRefs = doc.Descendants("PackageReference")
            .Select(pr => pr.Attribute("Include")?.Value ?? "")
            .ToList();

        // Check for Blazor-specific properties (for .NET 10+ implicit references)
        var hasBlazorProperty = doc.Descendants("BlazorDisableThrowNavigationException").Any() ||
                                doc.Descendants("BlazorWebAssemblyLoadAllGlobalizationData").Any();

        // Blazor WebAssembly
        if (sdk.Contains("BlazorWebAssembly") ||
            packageRefs.Any(p => p.Contains("Components.WebAssembly")))
        {
            return BlazorProjectType.WebAssembly;
        }

        // Blazor Hybrid
        if (packageRefs.Any(p => p.Contains("Components.WebView")))
        {
            return BlazorProjectType.Hybrid;
        }

        // Blazor Server (including .NET 10+ with implicit references)
        if ((sdk.Contains("Microsoft.NET.Sdk.Web") || sdk.Contains("Microsoft.NET.Sdk.Razor")) &&
            (packageRefs.Any(p => p.Contains("AspNetCore.Components")) || hasBlazorProperty))
        {
            return BlazorProjectType.Server;
        }

        return BlazorProjectType.Unknown;
    }
}
