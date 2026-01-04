using System.Xml.Linq;

namespace TailwindToolbox.Services;

/// <summary>
/// Generates MSBuild .targets files for Tailwind CSS compilation.
/// </summary>
public sealed class TargetFileGenerator : ITargetFileGenerator
{
    private readonly IFileGenerator _fileGenerator;

    public TargetFileGenerator(IFileGenerator fileGenerator)
    {
        _fileGenerator = fileGenerator;
    }

    public async Task GenerateTargetFileAsync(string outputPath, string targetName, string inputCss, string outputCss)
    {
        await _fileGenerator.GenerateBuildTargetsAsync(outputPath, targetName, inputCss, outputCss);
    }

    public async Task UpdateCsprojWithImportAsync(string csprojPath, string targetsFileName)
    {
        var doc = XDocument.Load(csprojPath);
        var root = doc.Root;

        if (root == null)
        {
            throw new InvalidOperationException("Invalid .csproj file");
        }

        // Check if Import already exists
        var existingImport = root.Descendants("Import")
            .FirstOrDefault(i => i.Attribute("Project")?.Value == targetsFileName);

        if (existingImport != null)
        {
            // Import already exists, don't duplicate
            return;
        }

        // Add Import element
        var importElement = new XElement("Import",
            new XAttribute("Project", targetsFileName));

        root.Add(importElement);

        await File.WriteAllTextAsync(csprojPath, doc.ToString());
    }

    public void ValidateTargetsXml(string xml)
    {
        // This will throw XmlException if invalid
        XDocument.Parse(xml);
    }
}
