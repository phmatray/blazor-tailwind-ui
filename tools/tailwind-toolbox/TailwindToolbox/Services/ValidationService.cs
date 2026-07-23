using System.Text.Json;
using System.Xml.Linq;
using TailwindToolbox.Models;

namespace TailwindToolbox.Services;

/// <summary>
/// Service for validating Tailwind CSS configuration in Blazor projects.
/// </summary>
public sealed class ValidationService : IValidationService
{
    private readonly INpmService _npmService;

    public ValidationService()
    {
        _npmService = new NpmService();
    }

    public ValidationService(INpmService npmService)
    {
        _npmService = npmService;
    }

    public List<ValidationRule> CreateValidationRules()
    {
        return new List<ValidationRule>
        {
            // Environment validation rules (3)
            CreateNodeInstalledRule(),
            CreateNpmInstalledRule(),
            CreateDotNetVersionRule(),

            // Files validation rules (5)
            CreateTailwindConfigExistsRule(),
            CreatePackageJsonExistsRule(),
            CreateCssInputExistsRule(),
            CreateBuildTargetsExistRule(),
            CreateGitignoreConfiguredRule(),

            // Configuration validation rules (4)
            CreateTailwindConfigValidRule(),
            CreatePackageJsonValidRule(),
            CreateContentPathsCorrectRule(),
            CreateBuildScriptsPresentRule(),

            // Dependencies validation rules (3)
            CreateTailwindCssVersionRule(),
            CreateAutoprefixerVersionRule(),
            CreateNoDeprecatedPackagesRule(),

            // Integration validation rules (2)
            CreateMsBuildImportExistsRule(),
            CreateTargetXmlValidRule()
        };
    }

    public async Task<List<ValidationResult>> ExecuteValidationRulesAsync(
        List<ValidationRule> rules,
        BlazorProject project)
    {
        // Execute rules in parallel using PLINQ
        var results = await Task.Run(() =>
            rules.AsParallel()
                .Select(rule => rule.CheckFunction(project))
                .ToList()
        );

        return results;
    }

    public Dictionary<ValidationCategory, List<ValidationResult>> CategorizeResults(
        List<ValidationResult> results,
        List<ValidationRule> rules)
    {
        var categorized = new Dictionary<ValidationCategory, List<ValidationResult>>();

        foreach (var result in results)
        {
            var rule = rules.FirstOrDefault(r => r.RuleId == result.RuleId);
            if (rule == null) continue;

            if (!categorized.ContainsKey(rule.Category))
            {
                categorized[rule.Category] = new List<ValidationResult>();
            }

            categorized[rule.Category].Add(result);
        }

        return categorized;
    }

    #region Environment Validation Rules

    private ValidationRule CreateNodeInstalledRule()
    {
        return new ValidationRule
        {
            RuleId = "NODE_INSTALLED",
            Name = "Node.js Installation",
            Description = "Verifies Node.js is installed and accessible",
            Category = ValidationCategory.Environment,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var result = _npmService.CheckNodeInstalledAsync().GetAwaiter().GetResult();
                return new ValidationResult
                {
                    RuleId = "NODE_INSTALLED",
                    Passed = result.IsInstalled,
                    Message = result.IsInstalled
                        ? $"Node.js {result.Version} detected"
                        : "Node.js not found",
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Install Node.js from https://nodejs.org/ or use nvm"
        };
    }

    private ValidationRule CreateNpmInstalledRule()
    {
        return new ValidationRule
        {
            RuleId = "NPM_INSTALLED",
            Name = "npm Installation",
            Description = "Verifies npm is installed and accessible",
            Category = ValidationCategory.Environment,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var result = _npmService.CheckNpmInstalledAsync().GetAwaiter().GetResult();
                return new ValidationResult
                {
                    RuleId = "NPM_INSTALLED",
                    Passed = result.IsInstalled,
                    Message = result.IsInstalled
                        ? $"npm {result.Version} detected"
                        : "npm not found",
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Install npm (included with Node.js)"
        };
    }

    private ValidationRule CreateDotNetVersionRule()
    {
        return new ValidationRule
        {
            RuleId = "DOTNET_VERSION",
            Name = ".NET Version",
            Description = "Verifies .NET version is 6.0 or higher",
            Category = ValidationCategory.Environment,
            Severity = ValidationSeverity.Warning,
            CheckFunction = project =>
            {
                var version = project.DotNetVersion;
                var isValid = version.StartsWith("net") &&
                              int.TryParse(version.Replace("net", "").Split('.')[0], out var major) &&
                              major >= 6;

                return new ValidationResult
                {
                    RuleId = "DOTNET_VERSION",
                    Passed = isValid,
                    Message = isValid
                        ? $".NET {version} is supported"
                        : $".NET {version} may not be fully supported",
                    ActualValue = version,
                    ExpectedValue = "net6.0 or higher",
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Upgrade to .NET 6.0 or higher"
        };
    }

    #endregion

    #region Files Validation Rules

    private ValidationRule CreateTailwindConfigExistsRule()
    {
        return new ValidationRule
        {
            RuleId = "TAILWIND_CONFIG_EXISTS",
            Name = "Tailwind Configuration File",
            Description = "Checks for tailwind.config.js in project root",
            Category = ValidationCategory.Files,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var configPath = Path.Combine(project.ProjectDirectory, "tailwind.config.js");
                var exists = File.Exists(configPath);

                return new ValidationResult
                {
                    RuleId = "TAILWIND_CONFIG_EXISTS",
                    Passed = exists,
                    Message = exists
                        ? "tailwind.config.js found"
                        : "tailwind.config.js not found",
                    FilePath = configPath,
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Run 'tailwind-blazor setup' to create configuration"
        };
    }

    private ValidationRule CreatePackageJsonExistsRule()
    {
        return new ValidationRule
        {
            RuleId = "PACKAGE_JSON_EXISTS",
            Name = "Package.json File",
            Description = "Checks for package.json in project root",
            Category = ValidationCategory.Files,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var packageJsonPath = Path.Combine(project.ProjectDirectory, "package.json");
                var exists = File.Exists(packageJsonPath);

                return new ValidationResult
                {
                    RuleId = "PACKAGE_JSON_EXISTS",
                    Passed = exists,
                    Message = exists
                        ? "package.json found"
                        : "package.json not found",
                    FilePath = packageJsonPath,
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Run 'tailwind-blazor setup' to create package.json"
        };
    }

    private ValidationRule CreateCssInputExistsRule()
    {
        return new ValidationRule
        {
            RuleId = "CSS_INPUT_EXISTS",
            Name = "CSS Input File",
            Description = "Checks for Tailwind CSS input file (Styles/app.css)",
            Category = ValidationCategory.Files,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var cssPath = Path.Combine(project.ProjectDirectory, "Styles", "app.css");
                var exists = File.Exists(cssPath);

                return new ValidationResult
                {
                    RuleId = "CSS_INPUT_EXISTS",
                    Passed = exists,
                    Message = exists
                        ? "Styles/app.css found"
                        : "Styles/app.css not found",
                    FilePath = cssPath,
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Run 'tailwind-blazor setup' to create CSS input file"
        };
    }

    private ValidationRule CreateBuildTargetsExistRule()
    {
        return new ValidationRule
        {
            RuleId = "BUILD_TARGETS_EXIST",
            Name = "Build Targets File",
            Description = "Checks for MSBuild .targets file",
            Category = ValidationCategory.Files,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var targetFiles = Directory.GetFiles(project.ProjectDirectory, "*.targets");
                var exists = targetFiles.Length > 0;

                return new ValidationResult
                {
                    RuleId = "BUILD_TARGETS_EXIST",
                    Passed = exists,
                    Message = exists
                        ? $"Build targets file found: {Path.GetFileName(targetFiles[0])}"
                        : "No .targets file found",
                    FilePath = exists ? targetFiles[0] : null,
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Run 'tailwind-blazor setup' to create build targets"
        };
    }

    private ValidationRule CreateGitignoreConfiguredRule()
    {
        return new ValidationRule
        {
            RuleId = "GITIGNORE_CONFIGURED",
            Name = "Gitignore Configuration",
            Description = "Checks that node_modules is in .gitignore",
            Category = ValidationCategory.Files,
            Severity = ValidationSeverity.Warning,
            CheckFunction = project =>
            {
                var gitignorePath = Path.Combine(project.ProjectDirectory, ".gitignore");
                if (!File.Exists(gitignorePath))
                {
                    return new ValidationResult
                    {
                        RuleId = "GITIGNORE_CONFIGURED",
                        Passed = false,
                        Message = ".gitignore not found",
                        FilePath = gitignorePath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                var content = File.ReadAllText(gitignorePath);
                var hasNodeModules = content.Contains("node_modules");

                return new ValidationResult
                {
                    RuleId = "GITIGNORE_CONFIGURED",
                    Passed = hasNodeModules,
                    Message = hasNodeModules
                        ? "node_modules in .gitignore"
                        : "node_modules not in .gitignore",
                    FilePath = gitignorePath,
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Add 'node_modules/' to .gitignore"
        };
    }

    #endregion

    #region Configuration Validation Rules

    private ValidationRule CreateTailwindConfigValidRule()
    {
        return new ValidationRule
        {
            RuleId = "TAILWIND_CONFIG_VALID",
            Name = "Tailwind Config Valid",
            Description = "Validates tailwind.config.js structure",
            Category = ValidationCategory.Configuration,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var configPath = Path.Combine(project.ProjectDirectory, "tailwind.config.js");
                if (!File.Exists(configPath))
                {
                    return new ValidationResult
                    {
                        RuleId = "TAILWIND_CONFIG_VALID",
                        Passed = false,
                        Message = "tailwind.config.js not found",
                        FilePath = configPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                try
                {
                    var content = File.ReadAllText(configPath);
                    var hasContent = content.Contains("content");
                    var hasModuleExports = content.Contains("module.exports") || content.Contains("export default");

                    var isValid = hasContent && hasModuleExports;

                    return new ValidationResult
                    {
                        RuleId = "TAILWIND_CONFIG_VALID",
                        Passed = isValid,
                        Message = isValid
                            ? "tailwind.config.js structure is valid"
                            : "tailwind.config.js is missing required properties",
                        FilePath = configPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch (Exception ex)
                {
                    return new ValidationResult
                    {
                        RuleId = "TAILWIND_CONFIG_VALID",
                        Passed = false,
                        Message = $"Error reading config: {ex.Message}",
                        FilePath = configPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Fix tailwind.config.js or run 'tailwind-blazor setup --force'"
        };
    }

    private ValidationRule CreatePackageJsonValidRule()
    {
        return new ValidationRule
        {
            RuleId = "PACKAGE_JSON_VALID",
            Name = "Package.json Valid",
            Description = "Validates package.json structure",
            Category = ValidationCategory.Configuration,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var packageJsonPath = Path.Combine(project.ProjectDirectory, "package.json");
                if (!File.Exists(packageJsonPath))
                {
                    return new ValidationResult
                    {
                        RuleId = "PACKAGE_JSON_VALID",
                        Passed = false,
                        Message = "package.json not found",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                try
                {
                    var content = File.ReadAllText(packageJsonPath);
                    var json = JsonDocument.Parse(content);

                    var hasName = json.RootElement.TryGetProperty("name", out _);
                    var hasDependencies = json.RootElement.TryGetProperty("dependencies", out _) ||
                                        json.RootElement.TryGetProperty("devDependencies", out _);

                    var isValid = hasName && hasDependencies;

                    return new ValidationResult
                    {
                        RuleId = "PACKAGE_JSON_VALID",
                        Passed = isValid,
                        Message = isValid
                            ? "package.json structure is valid"
                            : "package.json is missing required properties",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch (Exception ex)
                {
                    return new ValidationResult
                    {
                        RuleId = "PACKAGE_JSON_VALID",
                        Passed = false,
                        Message = $"Error parsing JSON: {ex.Message}",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Fix package.json or run 'tailwind-blazor setup --force'"
        };
    }

    private ValidationRule CreateContentPathsCorrectRule()
    {
        return new ValidationRule
        {
            RuleId = "CONTENT_PATHS_CORRECT",
            Name = "Content Paths Correct",
            Description = "Validates Tailwind content paths include Blazor files",
            Category = ValidationCategory.Configuration,
            Severity = ValidationSeverity.Warning,
            CheckFunction = project =>
            {
                var configPath = Path.Combine(project.ProjectDirectory, "tailwind.config.js");
                if (!File.Exists(configPath))
                {
                    return new ValidationResult
                    {
                        RuleId = "CONTENT_PATHS_CORRECT",
                        Passed = false,
                        Message = "tailwind.config.js not found",
                        FilePath = configPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                var content = File.ReadAllText(configPath);
                var hasRazor = content.Contains(".razor");
                var hasHtml = content.Contains(".html") || content.Contains(".cshtml");

                var isCorrect = hasRazor && hasHtml;

                return new ValidationResult
                {
                    RuleId = "CONTENT_PATHS_CORRECT",
                    Passed = isCorrect,
                    Message = isCorrect
                        ? "Content paths include Blazor file types"
                        : "Content paths may be missing Blazor file types (.razor, .html, .cshtml)",
                    FilePath = configPath,
                    TimestampUtc = DateTime.UtcNow
                };
            },
            Remediation = "Update content paths in tailwind.config.js to include './**/*.razor', './**/*.html', './**/*.cshtml'"
        };
    }

    private ValidationRule CreateBuildScriptsPresentRule()
    {
        return new ValidationRule
        {
            RuleId = "BUILD_SCRIPTS_PRESENT",
            Name = "Build Scripts Present",
            Description = "Validates package.json contains Tailwind build scripts",
            Category = ValidationCategory.Configuration,
            Severity = ValidationSeverity.Warning,
            CheckFunction = project =>
            {
                var packageJsonPath = Path.Combine(project.ProjectDirectory, "package.json");
                if (!File.Exists(packageJsonPath))
                {
                    return new ValidationResult
                    {
                        RuleId = "BUILD_SCRIPTS_PRESENT",
                        Passed = false,
                        Message = "package.json not found",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                try
                {
                    var content = File.ReadAllText(packageJsonPath);
                    var json = JsonDocument.Parse(content);

                    if (!json.RootElement.TryGetProperty("scripts", out var scripts))
                    {
                        return new ValidationResult
                        {
                            RuleId = "BUILD_SCRIPTS_PRESENT",
                            Passed = false,
                            Message = "No scripts section in package.json",
                            FilePath = packageJsonPath,
                            TimestampUtc = DateTime.UtcNow
                        };
                    }

                    var scriptsJson = scripts.ToString();
                    var hasTailwindScript = scriptsJson.Contains("tailwindcss");

                    return new ValidationResult
                    {
                        RuleId = "BUILD_SCRIPTS_PRESENT",
                        Passed = hasTailwindScript,
                        Message = hasTailwindScript
                            ? "Tailwind build scripts present"
                            : "Tailwind build scripts not found",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch (Exception ex)
                {
                    return new ValidationResult
                    {
                        RuleId = "BUILD_SCRIPTS_PRESENT",
                        Passed = false,
                        Message = $"Error parsing package.json: {ex.Message}",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Add build scripts to package.json or run 'tailwind-blazor setup --force'"
        };
    }

    #endregion

    #region Dependencies Validation Rules

    private ValidationRule CreateTailwindCssVersionRule()
    {
        return new ValidationRule
        {
            RuleId = "TAILWINDCSS_VERSION",
            Name = "TailwindCSS Version",
            Description = "Validates TailwindCSS is installed with compatible version",
            Category = ValidationCategory.Dependencies,
            Severity = ValidationSeverity.Warning,
            CheckFunction = project =>
            {
                try
                {
                    var version = _npmService.GetInstalledVersionAsync("tailwindcss", project.ProjectDirectory)
                        .GetAwaiter().GetResult();

                    if (version == null)
                    {
                        return new ValidationResult
                        {
                            RuleId = "TAILWINDCSS_VERSION",
                            Passed = false,
                            Message = "tailwindcss not installed",
                            TimestampUtc = DateTime.UtcNow
                        };
                    }

                    return new ValidationResult
                    {
                        RuleId = "TAILWINDCSS_VERSION",
                        Passed = true,
                        Message = $"tailwindcss {version} installed",
                        ActualValue = version,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch
                {
                    return new ValidationResult
                    {
                        RuleId = "TAILWINDCSS_VERSION",
                        Passed = false,
                        Message = "Unable to check tailwindcss version",
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Run 'npm install tailwindcss' or 'tailwind-blazor setup'"
        };
    }

    private ValidationRule CreateAutoprefixerVersionRule()
    {
        return new ValidationRule
        {
            RuleId = "AUTOPREFIXER_VERSION",
            Name = "Autoprefixer Version",
            Description = "Validates autoprefixer is installed",
            Category = ValidationCategory.Dependencies,
            Severity = ValidationSeverity.Info,
            CheckFunction = project =>
            {
                try
                {
                    var version = _npmService.GetInstalledVersionAsync("autoprefixer", project.ProjectDirectory)
                        .GetAwaiter().GetResult();

                    if (version == null)
                    {
                        return new ValidationResult
                        {
                            RuleId = "AUTOPREFIXER_VERSION",
                            Passed = false,
                            Message = "autoprefixer not installed (optional)",
                            TimestampUtc = DateTime.UtcNow
                        };
                    }

                    return new ValidationResult
                    {
                        RuleId = "AUTOPREFIXER_VERSION",
                        Passed = true,
                        Message = $"autoprefixer {version} installed",
                        ActualValue = version,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch
                {
                    return new ValidationResult
                    {
                        RuleId = "AUTOPREFIXER_VERSION",
                        Passed = false,
                        Message = "Unable to check autoprefixer version",
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Run 'npm install autoprefixer' (optional enhancement)"
        };
    }

    private ValidationRule CreateNoDeprecatedPackagesRule()
    {
        return new ValidationRule
        {
            RuleId = "NO_DEPRECATED_PACKAGES",
            Name = "No Deprecated Packages",
            Description = "Checks for deprecated Tailwind-related packages",
            Category = ValidationCategory.Dependencies,
            Severity = ValidationSeverity.Info,
            CheckFunction = project =>
            {
                var packageJsonPath = Path.Combine(project.ProjectDirectory, "package.json");
                if (!File.Exists(packageJsonPath))
                {
                    return new ValidationResult
                    {
                        RuleId = "NO_DEPRECATED_PACKAGES",
                        Passed = true,
                        Message = "No package.json to check",
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                try
                {
                    var content = File.ReadAllText(packageJsonPath);
                    var deprecatedPackages = new[] { "@tailwindcss/jit", "tailwindcss-jit" };
                    var foundDeprecated = new List<string>();

                    foreach (var pkg in deprecatedPackages)
                    {
                        if (content.Contains(pkg))
                        {
                            foundDeprecated.Add(pkg);
                        }
                    }

                    var passed = foundDeprecated.Count == 0;

                    return new ValidationResult
                    {
                        RuleId = "NO_DEPRECATED_PACKAGES",
                        Passed = passed,
                        Message = passed
                            ? "No deprecated packages found"
                            : $"Deprecated packages found: {string.Join(", ", foundDeprecated)}",
                        FilePath = packageJsonPath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch
                {
                    return new ValidationResult
                    {
                        RuleId = "NO_DEPRECATED_PACKAGES",
                        Passed = true,
                        Message = "Unable to check for deprecated packages",
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Remove deprecated packages and use latest TailwindCSS"
        };
    }

    #endregion

    #region Integration Validation Rules

    private ValidationRule CreateMsBuildImportExistsRule()
    {
        return new ValidationRule
        {
            RuleId = "MSBUILD_IMPORT_EXISTS",
            Name = "MSBuild Import Exists",
            Description = "Validates .csproj imports the .targets file",
            Category = ValidationCategory.Integration,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                try
                {
                    var csprojContent = File.ReadAllText(project.ProjectFilePath);
                    var doc = XDocument.Parse(csprojContent);

                    var imports = doc.Descendants("Import")
                        .Select(i => i.Attribute("Project")?.Value)
                        .Where(v => v != null)
                        .ToList();

                    var hasTargetsImport = imports.Any(i => i!.EndsWith(".targets"));

                    return new ValidationResult
                    {
                        RuleId = "MSBUILD_IMPORT_EXISTS",
                        Passed = hasTargetsImport,
                        Message = hasTargetsImport
                            ? "MSBuild targets imported in .csproj"
                            : "No .targets file import found in .csproj",
                        FilePath = project.ProjectFilePath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch (Exception ex)
                {
                    return new ValidationResult
                    {
                        RuleId = "MSBUILD_IMPORT_EXISTS",
                        Passed = false,
                        Message = $"Error checking .csproj: {ex.Message}",
                        FilePath = project.ProjectFilePath,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Run 'tailwind-blazor setup' to add MSBuild import"
        };
    }

    private ValidationRule CreateTargetXmlValidRule()
    {
        return new ValidationRule
        {
            RuleId = "TARGET_XML_VALID",
            Name = "Target XML Valid",
            Description = "Validates .targets file is well-formed XML",
            Category = ValidationCategory.Integration,
            Severity = ValidationSeverity.Error,
            CheckFunction = project =>
            {
                var targetFiles = Directory.GetFiles(project.ProjectDirectory, "*.targets");
                if (targetFiles.Length == 0)
                {
                    return new ValidationResult
                    {
                        RuleId = "TARGET_XML_VALID",
                        Passed = false,
                        Message = "No .targets file found",
                        TimestampUtc = DateTime.UtcNow
                    };
                }

                try
                {
                    var targetFile = targetFiles[0];
                    var content = File.ReadAllText(targetFile);
                    XDocument.Parse(content); // Will throw if invalid

                    return new ValidationResult
                    {
                        RuleId = "TARGET_XML_VALID",
                        Passed = true,
                        Message = $"{Path.GetFileName(targetFile)} is valid XML",
                        FilePath = targetFile,
                        TimestampUtc = DateTime.UtcNow
                    };
                }
                catch (Exception ex)
                {
                    return new ValidationResult
                    {
                        RuleId = "TARGET_XML_VALID",
                        Passed = false,
                        Message = $"Invalid XML in .targets file: {ex.Message}",
                        FilePath = targetFiles[0],
                        TimestampUtc = DateTime.UtcNow
                    };
                }
            },
            Remediation = "Fix .targets file or run 'tailwind-blazor create-target --force'"
        };
    }

    #endregion
}
