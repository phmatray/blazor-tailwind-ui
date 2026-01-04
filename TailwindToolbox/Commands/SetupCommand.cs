using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;
using TailwindToolbox.Models;
using TailwindToolbox.Services;
using TailwindToolbox.Utilities;

namespace TailwindToolbox.Commands;

/// <summary>
/// Command to initialize Tailwind CSS configuration in a Blazor project.
/// </summary>
public sealed class SetupCommand : AsyncCommand<SetupCommand.Settings>
{
    private readonly IProjectDetector _projectDetector;
    private readonly IFileGenerator _fileGenerator;
    private readonly INpmService _npmService;
    private readonly ITargetFileGenerator _targetFileGenerator;

    public SetupCommand(
        IProjectDetector projectDetector,
        IFileGenerator fileGenerator,
        INpmService npmService,
        ITargetFileGenerator targetFileGenerator)
    {
        _projectDetector = projectDetector;
        _fileGenerator = fileGenerator;
        _npmService = npmService;
        _targetFileGenerator = targetFileGenerator;
    }

    public sealed class Settings : BaseCommandSettings
    {
        [Description("Path to Blazor project directory")]
        [CommandOption("-p|--project-dir")]
        public string ProjectDirectory { get; set; } = ".";

        [Description("Tailwind CSS version to install")]
        [CommandOption("-t|--tailwind-version")]
        public string? TailwindVersion { get; set; }

        [Description("Overwrite existing configuration")]
        [CommandOption("-f|--force")]
        public bool Force { get; set; }

        [Description("Show what would be done without executing")]
        [CommandOption("-d|--dry-run")]
        public bool DryRun { get; set; }

        [Description("Skip npm package installation")]
        [CommandOption("--skip-npm-install")]
        public bool SkipNpmInstall { get; set; }

        [Description("Output path for compiled CSS")]
        [CommandOption("--css-output")]
        public string? CssOutputPath { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken = default)
    {
        var logger = new ConsoleLogger(settings.Verbose, settings.NoColor);

        try
        {
            logger.Verbose("Verbose logging enabled");
            logger.Verbose($"Command settings: ProjectDirectory={settings.ProjectDirectory}, Force={settings.Force}, DryRun={settings.DryRun}");

            // Display header
            AnsiConsole.Write(new Rule("[bold]Tailwind Blazor Setup[/]").RuleStyle("blue"));
            AnsiConsole.WriteLine();

            // Resolve project directory to absolute path
            var projectDir = Path.GetFullPath(settings.ProjectDirectory);
            logger.Verbose($"Resolved project directory: {projectDir}");

            // Detect Blazor project
            var project = await _projectDetector.DetectProjectAsync(projectDir);
            if (project == null)
            {
                AnsiConsole.MarkupLine("[red]Error:[/] No Blazor project found in directory");
                return 1; // Error exit code
            }

            if (project.ProjectType == BlazorProjectType.Unknown)
            {
                AnsiConsole.MarkupLine("[yellow]Warning:[/] Project type could not be determined. This may not be a Blazor project.");
                return 1; // Error exit code
            }

            // Display project info
            AnsiConsole.MarkupLine($"[green]✓[/] Blazor project detected: [bold]{project.ProjectName}[/] ([dim]{project.ProjectType}, {project.DotNetVersion}[/])");

            // Check Node.js and npm
            var nodeCheck = await _npmService.CheckNodeInstalledAsync();
            if (!nodeCheck.IsInstalled)
            {
                AnsiConsole.MarkupLine("[red]Error: Node.js not found[/]");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("Tailwind CSS requires Node.js to be installed.");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("Install Node.js:");
                AnsiConsole.MarkupLine("  • Download from: https://nodejs.org/");
                AnsiConsole.MarkupLine("  • Or use nvm: curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.0/install.sh | bash");
                AnsiConsole.WriteLine();
                return 3; // Prerequisites missing exit code
            }

            AnsiConsole.MarkupLine($"[green]✓[/] Node.js {nodeCheck.Version} detected");

            var npmCheck = await _npmService.CheckNpmInstalledAsync();
            if (!npmCheck.IsInstalled)
            {
                AnsiConsole.MarkupLine("[red]Error:[/] npm not found");
                return 3; // Prerequisites missing exit code
            }

            AnsiConsole.MarkupLine($"[green]✓[/] npm {npmCheck.Version} detected");
            AnsiConsole.WriteLine();

            // Check for existing configuration
            if (project.HasTailwindConfig && !settings.Force)
            {
                AnsiConsole.MarkupLine("[yellow]⚠ Warning:[/] tailwind.config.js already exists");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("Overwriting will replace any custom modifications.");
                AnsiConsole.WriteLine();

                if (!settings.DryRun && !AnsiConsole.Confirm("Overwrite existing file?", false))
                {
                    AnsiConsole.MarkupLine("[dim]Setup cancelled.[/]");
                    return 2; // User cancelled exit code
                }

                AnsiConsole.WriteLine();
            }

            // Dry run mode
            if (settings.DryRun)
            {
                AnsiConsole.Write(new Rule("[yellow]Dry Run Mode - No changes will be made[/]").RuleStyle("yellow"));
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("The following changes would be made:");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[bold]Files to create:[/]");
                AnsiConsole.MarkupLine("  • tailwind.config.js");
                AnsiConsole.MarkupLine("  • package.json");
                AnsiConsole.MarkupLine("  • Styles/app.css");
                AnsiConsole.MarkupLine("  • TailwindBuild.targets");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[bold]Files to modify:[/]");
                AnsiConsole.MarkupLine($"  • {project.ProjectName}.csproj (add MSBuild import)");
                AnsiConsole.MarkupLine("  • .gitignore (add node_modules)");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[bold]npm packages to install:[/]");
                AnsiConsole.MarkupLine($"  • tailwindcss@{settings.TailwindVersion ?? "^4.0.0"}");
                AnsiConsole.MarkupLine("  • autoprefixer@^10.4.16");
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("Run without --dry-run to apply changes.");
                return 0;
            }

            // Install npm packages
            if (!settings.SkipNpmInstall)
            {
                await AnsiConsole.Status()
                    .StartAsync("Installing Tailwind CSS packages...", async ctx =>
                    {
                        var packages = new Dictionary<string, string>
                        {
                            { "tailwindcss", settings.TailwindVersion ?? "^4.0.0" },
                            { "@tailwindcss/cli", settings.TailwindVersion ?? "^4.0.0" },
                            { "autoprefixer", "^10.4.16" }
                        };

                        await _npmService.InstallPackagesAsync(packages, projectDir);
                    });

                AnsiConsole.MarkupLine("[green]✓[/] tailwindcss installed");
                AnsiConsole.MarkupLine("[green]✓[/] @tailwindcss/cli installed");
                AnsiConsole.MarkupLine("[green]✓[/] autoprefixer installed");
                AnsiConsole.WriteLine();
            }

            // Create configuration files
            AnsiConsole.MarkupLine("[bold]Creating configuration files...[/]");

            var tailwindConfigPath = Path.Combine(projectDir, "tailwind.config.js");
            await _fileGenerator.GenerateTailwindConfigAsync(tailwindConfigPath);
            AnsiConsole.MarkupLine("[green]✓[/] tailwind.config.js created");

            var packageJsonPath = Path.Combine(projectDir, "package.json");
            await _fileGenerator.GeneratePackageJsonAsync(packageJsonPath, project.ProjectName);
            AnsiConsole.MarkupLine("[green]✓[/] package.json created");

            var stylesDir = Path.Combine(projectDir, "Styles");
            var appCssPath = Path.Combine(stylesDir, "app.css");
            await _fileGenerator.GenerateAppCssAsync(appCssPath);
            AnsiConsole.MarkupLine("[green]✓[/] Styles/app.css created");

            // Update .gitignore
            var gitignorePath = Path.Combine(projectDir, ".gitignore");
            await UpdateGitignoreAsync(gitignorePath);
            AnsiConsole.MarkupLine("[green]✓[/] .gitignore updated");

            AnsiConsole.WriteLine();

            // Setup build integration
            AnsiConsole.MarkupLine("[bold]Setting up build integration...[/]");

            var targetsPath = Path.Combine(projectDir, "TailwindBuild.targets");
            var outputCss = settings.CssOutputPath ?? "wwwroot/css/app.css";
            await _targetFileGenerator.GenerateTargetFileAsync(
                targetsPath,
                "BuildTailwindCSS",
                "Styles/app.css",
                outputCss);
            AnsiConsole.MarkupLine("[green]✓[/] TailwindBuild.targets created");

            await _targetFileGenerator.UpdateCsprojWithImportAsync(
                project.ProjectFilePath,
                "TailwindBuild.targets");
            AnsiConsole.MarkupLine($"[green]✓[/] {project.ProjectName}.csproj updated");

            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule().RuleStyle("blue"));
            AnsiConsole.MarkupLine("[green bold]Setup complete![/] Run 'dotnet build' to compile Tailwind CSS.");

            return 0; // Success exit code
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            return 1; // Error exit code
        }
    }

    private static async Task UpdateGitignoreAsync(string gitignorePath)
    {
        var lines = new List<string>();

        if (File.Exists(gitignorePath))
        {
            lines.AddRange(await File.ReadAllLinesAsync(gitignorePath));
        }

        if (!lines.Any(line => line.Trim() == "node_modules/"))
        {
            if (lines.Count > 0 && !string.IsNullOrWhiteSpace(lines[^1]))
            {
                lines.Add("");
            }
            lines.Add("# npm/node");
            lines.Add("node_modules/");

            await File.WriteAllLinesAsync(gitignorePath, lines);
        }
    }
}
