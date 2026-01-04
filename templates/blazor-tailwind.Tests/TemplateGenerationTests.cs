namespace TailwindToolbox.Templates.Tests;

/// <summary>
/// Tests for template generation and structure verification.
/// These tests ensure the template creates the correct file structure and content.
/// </summary>
public class TemplateGenerationTests
{
    private const string TemplateName = "blazor-tailwind";
    private const string TestProjectName = "TestBlazorApp";

    [Fact]
    public void Template_GeneratesExpectedFileStructure()
    {
        // Arrange
        var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);

        try
        {
            // Act
            var result = RunDotNetNew(TemplateName, TestProjectName, outputPath);

            // Assert
            Assert.Equal(0, result.ExitCode);

            // Verify core project files exist
            Assert.True(File.Exists(Path.Combine(outputPath, $"{TestProjectName}.csproj")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Program.cs")));
            Assert.True(File.Exists(Path.Combine(outputPath, "appsettings.json")));
            Assert.True(File.Exists(Path.Combine(outputPath, "appsettings.Development.json")));

            // Verify template configuration files
            Assert.True(File.Exists(Path.Combine(outputPath, "tailwind.config.js")));
            Assert.True(File.Exists(Path.Combine(outputPath, "package.json")));
            Assert.True(File.Exists(Path.Combine(outputPath, "TailwindBuild.targets")));

            // Verify Tailwind CSS files
            Assert.True(File.Exists(Path.Combine(outputPath, "Styles", "app.css")));

            // Verify component structure
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "App.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Routes.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "_Imports.razor")));

            // Verify layout components
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Layout", "MainLayout.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Layout", "NavMenu.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Layout", "ReconnectModal.razor")));

            // Verify page components
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Pages", "Home.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Pages", "Counter.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Pages", "Weather.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Pages", "Error.razor")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Pages", "NotFound.razor")));

            // Verify wwwroot structure
            Assert.True(Directory.Exists(Path.Combine(outputPath, "wwwroot")));
            Assert.True(Directory.Exists(Path.Combine(outputPath, "wwwroot", "css")));
            Assert.True(File.Exists(Path.Combine(outputPath, "wwwroot", "favicon.png")));

            // Verify Properties folder
            Assert.True(File.Exists(Path.Combine(outputPath, "Properties", "launchSettings.json")));

            // Verify .gitignore
            Assert.True(File.Exists(Path.Combine(outputPath, ".gitignore")));
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
        }
    }

    [Fact]
    public void Template_VerifiesComponentStructure_MatchesMicrosoftTemplate()
    {
        // Arrange
        var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);

        try
        {
            // Act
            var result = RunDotNetNew(TemplateName, TestProjectName, outputPath);

            // Assert
            Assert.Equal(0, result.ExitCode);

            // Verify .NET 10 specific components exist
            var reconnectModalPath = Path.Combine(outputPath, "Components", "Layout", "ReconnectModal.razor");
            var notFoundPath = Path.Combine(outputPath, "Components", "Pages", "NotFound.razor");

            Assert.True(File.Exists(reconnectModalPath), "ReconnectModal.razor should exist (.NET 10 feature)");
            Assert.True(File.Exists(notFoundPath), "NotFound.razor should exist (.NET 10 feature)");

            // Verify ReconnectModal supporting files
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Layout", "ReconnectModal.razor.css")));
            Assert.True(File.Exists(Path.Combine(outputPath, "Components", "Layout", "ReconnectModal.razor.js")));

            // Verify component structure matches Microsoft's convention
            var appRazorContent = File.ReadAllText(Path.Combine(outputPath, "Components", "App.razor"));
            Assert.Contains("<ReconnectModal />", appRazorContent);
            Assert.Contains("<Routes />", appRazorContent);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
        }
    }

    [Fact]
    public void Template_VerifiesBootstrapIsCompletelyRemoved()
    {
        // Arrange
        var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);

        try
        {
            // Act
            var result = RunDotNetNew(TemplateName, TestProjectName, outputPath);

            // Assert
            Assert.Equal(0, result.ExitCode);

            // Verify Bootstrap files do NOT exist
            var bootstrapLibPath = Path.Combine(outputPath, "wwwroot", "lib", "bootstrap");
            Assert.False(Directory.Exists(bootstrapLibPath), "Bootstrap library directory should not exist");

            // Verify no Bootstrap references in App.razor
            var appRazorContent = File.ReadAllText(Path.Combine(outputPath, "Components", "App.razor"));
            Assert.DoesNotContain("bootstrap", appRazorContent, StringComparison.OrdinalIgnoreCase);

            // Verify Tailwind CSS is referenced instead
            Assert.Contains("css/app.css", appRazorContent);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
        }
    }

    private static ProcessResult RunDotNetNew(string templateName, string projectName, string outputPath)
    {
        return RunCommand("dotnet", $"new {templateName} -n {projectName} -o \"{outputPath}\"");
    }

    private static ProcessResult RunCommand(string command, string arguments)
    {
        var processInfo = new System.Diagnostics.ProcessStartInfo
        {
            FileName = command,
            Arguments = arguments,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = System.Diagnostics.Process.Start(processInfo);
        if (process == null)
        {
            throw new InvalidOperationException($"Failed to start {command} process");
        }

        process.WaitForExit();

        return new ProcessResult
        {
            ExitCode = process.ExitCode,
            StandardOutput = process.StandardOutput.ReadToEnd(),
            StandardError = process.StandardError.ReadToEnd()
        };
    }

    [Fact]
    public void Template_SubstitutesProjectName_InCsprojFile()
    {
        // Arrange
        var customProjectName = "MyCustomApp";
        var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);

        try
        {
            // Act
            var result = RunDotNetNew(TemplateName, customProjectName, outputPath);

            // Assert
            Assert.Equal(0, result.ExitCode);

            // Verify .csproj file is renamed to custom project name
            var csprojPath = Path.Combine(outputPath, $"{customProjectName}.csproj");
            Assert.True(File.Exists(csprojPath), $"{customProjectName}.csproj should exist");

            // Verify BlazorTailwind.csproj does NOT exist
            Assert.False(File.Exists(Path.Combine(outputPath, "BlazorTailwind.csproj")), "BlazorTailwind.csproj should be renamed");

            // Verify project file content has correct namespace and assembly name
            var csprojContent = File.ReadAllText(csprojPath);
            Assert.Contains($"<RootNamespace>{customProjectName}</RootNamespace>", csprojContent);
            Assert.Contains($"<AssemblyName>{customProjectName}</AssemblyName>", csprojContent);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
        }
    }

    [Fact]
    public void Template_SubstitutesNamespace_InRazorFiles()
    {
        // Arrange
        var customProjectName = "MyCompany.MyApp";
        var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);

        try
        {
            // Act
            var result = RunDotNetNew(TemplateName, customProjectName, outputPath);

            // Assert
            Assert.Equal(0, result.ExitCode);

            // Verify _Imports.razor has correct namespace
            var importsPath = Path.Combine(outputPath, "Components", "_Imports.razor");
            var importsContent = File.ReadAllText(importsPath);
            Assert.Contains($"@using {customProjectName}", importsContent);
            Assert.Contains($"@using {customProjectName}.Components", importsContent);
            Assert.DoesNotContain("@using BlazorTailwind", importsContent);

            // Verify Program.cs has correct using directive
            var programPath = Path.Combine(outputPath, "Program.cs");
            var programContent = File.ReadAllText(programPath);
            Assert.Contains($"using {customProjectName}.Components;", programContent);
            Assert.DoesNotContain("using BlazorTailwind.Components;", programContent);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
        }
    }

    [Fact]
    public void Template_SubstitutesLowercaseName_InPackageJson()
    {
        // Arrange
        var customProjectName = "MyAwesomeApp";
        var expectedLowercase = "myawesomeapp";
        var outputPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(outputPath);

        try
        {
            // Act
            var result = RunDotNetNew(TemplateName, customProjectName, outputPath);

            // Assert
            Assert.Equal(0, result.ExitCode);

            // Verify package.json has lowercase project name
            var packageJsonPath = Path.Combine(outputPath, "package.json");
            var packageJsonContent = File.ReadAllText(packageJsonPath);
            Assert.Contains($"\"name\": \"{expectedLowercase}\"", packageJsonContent);
            Assert.DoesNotContain("\"name\": \"blazor-tailwind\"", packageJsonContent);
            Assert.DoesNotContain("\"name\": \"BlazorTailwind\"", packageJsonContent);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
        }
    }

    [Fact(Skip = "Requires template to be installed first")]
    public void Template_CanBeInstalled_FromNuGetPackage()
    {
        // This test verifies that the template can be installed via dotnet new install
        // Note: This is a manual verification test that requires the package to be built first

        // Arrange
        var packagePath = Path.Combine(Path.GetDirectoryName(typeof(TemplateGenerationTests).Assembly.Location)!, "..", "..", "..", "..", "TailwindToolbox.Blazor.Template", "bin", "Release");

        // Act - Install template
        var installResult = RunCommand("dotnet", $"new install {packagePath}");

        try
        {
            // Assert - Template should be installed
            var listResult = RunCommand("dotnet", "new list blazor-tailwind");
            Assert.Equal(0, listResult.ExitCode);
            Assert.Contains("blazor-tailwind", listResult.StandardOutput);
        }
        finally
        {
            // Cleanup - Uninstall template
            RunCommand("dotnet", "new uninstall TailwindToolbox.Blazor.Template");
        }
    }

    [Fact(Skip = "Requires template to be installed first")]
    public void Template_AppearsInList_AfterInstallation()
    {
        // This test verifies the template shows up in dotnet new list after installation

        // Act
        var result = RunCommand("dotnet", "new list");

        // Assert
        Assert.Equal(0, result.ExitCode);
        // Note: This will only pass if template is already installed
        Assert.Contains("blazor-tailwind", result.StandardOutput);
    }

    [Fact(Skip = "Requires template to be installed first")]
    public void Template_CanBeUninstalled_Successfully()
    {
        // This test verifies the template can be uninstalled

        // Act - Uninstall
        var uninstallResult = RunCommand("dotnet", "new uninstall TailwindToolbox.Blazor.Template");

        // Assert
        Assert.Equal(0, uninstallResult.ExitCode);

        // Verify template is no longer in list
        var listResult = RunCommand("dotnet", "new list blazor-tailwind");
        Assert.DoesNotContain("Blazor Web App with Tailwind CSS", listResult.StandardOutput);
    }

    private record ProcessResult
    {
        public required int ExitCode { get; init; }
        public required string StandardOutput { get; init; }
        public required string StandardError { get; init; }
    }
}
