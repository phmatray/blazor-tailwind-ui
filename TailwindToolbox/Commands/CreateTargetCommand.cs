using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;
using TailwindToolbox.Services;
using TailwindToolbox.Utilities;

namespace TailwindToolbox.Commands;

/// <summary>
/// Command to create or update MSBuild .targets files for Tailwind CSS compilation.
/// </summary>
public sealed class CreateTargetCommand : AsyncCommand<CreateTargetCommand.Settings>
{
    private readonly IProjectDetector _projectDetector;
    private readonly ITargetFileGenerator _targetFileGenerator;

    public CreateTargetCommand(IProjectDetector projectDetector, ITargetFileGenerator targetFileGenerator)
    {
        _projectDetector = projectDetector;
        _targetFileGenerator = targetFileGenerator;
    }

    public sealed class Settings : BaseCommandSettings
    {
        [Description("Path to the Blazor project directory")]
        [CommandOption("-p|--project-dir")]
        public string ProjectDirectory { get; set; } = ".";

        [Description("Name of the MSBuild target (default: BuildTailwindCSS)")]
        [CommandOption("--target-name")]
        public string TargetName { get; set; } = "BuildTailwindCSS";

        [Description("Input CSS file path (default: Styles/app.css)")]
        [CommandOption("--input-css")]
        public string InputCss { get; set; } = "Styles/app.css";

        [Description("Output CSS file path (default: wwwroot/css/app.css)")]
        [CommandOption("--output-css")]
        public string OutputCss { get; set; } = "wwwroot/css/app.css";

        [Description("Minify mode: always, never, or release-only (default: release-only)")]
        [CommandOption("--minify")]
        public string Minify { get; set; } = "release-only";

        [Description("Overwrite existing .targets file")]
        [CommandOption("--force")]
        public bool Force { get; set; }

        [Description("Show what would be created without making changes")]
        [CommandOption("--dry-run")]
        public bool DryRun { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken = default)
    {
        // Detect Blazor project
        var projectPath = Path.GetFullPath(settings.ProjectDirectory);
        var project = await _projectDetector.DetectProjectAsync(projectPath);

        if (project == null)
        {
            AnsiConsole.MarkupLine("[red]Error:[/] No Blazor project found in the specified directory.");
            return 2;
        }

        AnsiConsole.MarkupLine($"[cyan]Creating MSBuild target for:[/] {project.ProjectName}");
        AnsiConsole.WriteLine();

        // Determine .targets file path
        var targetsFileName = "TailwindBuild.targets";
        var targetsPath = Path.Combine(project.ProjectDirectory, targetsFileName);

        // Check for existing .targets file
        if (File.Exists(targetsPath) && !settings.Force)
        {
            AnsiConsole.MarkupLine($"[yellow]Warning:[/] {targetsFileName} already exists.");
            AnsiConsole.MarkupLine("[dim]Use --force to overwrite the existing file.[/]");
            return 1;
        }

        // Display configuration summary
        DisplayConfigurationSummary(settings, targetsFileName);

        if (settings.DryRun)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[cyan]Dry run mode - displaying generated content without writing files:[/]");
            AnsiConsole.WriteLine();

            // Generate content for preview
            var tempPath = Path.Combine(Path.GetTempPath(), $"temp_{Guid.NewGuid()}.targets");
            try
            {
                await _targetFileGenerator.GenerateTargetFileAsync(
                    tempPath,
                    settings.TargetName,
                    settings.InputCss,
                    settings.OutputCss);

                var content = await File.ReadAllTextAsync(tempPath);
                var panel = new Panel(content)
                {
                    Header = new PanelHeader($"[bold]{targetsFileName}[/]"),
                    Border = BoxBorder.Rounded
                };
                AnsiConsole.Write(panel);

                File.Delete(tempPath);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error generating preview:[/] {ex.Message}");
                return 1;
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[cyan]No files were modified (dry-run mode)[/]");
            return 0;
        }

        try
        {
            // Generate .targets file
            await _targetFileGenerator.GenerateTargetFileAsync(
                targetsPath,
                settings.TargetName,
                settings.InputCss,
                settings.OutputCss);

            AnsiConsole.MarkupLine($"[green]✓[/] Created {targetsFileName}");

            // Validate generated XML
            var generatedContent = await File.ReadAllTextAsync(targetsPath);
            _targetFileGenerator.ValidateTargetsXml(generatedContent);
            AnsiConsole.MarkupLine($"[green]✓[/] Validated .targets XML structure");

            // Update .csproj with Import
            await _targetFileGenerator.UpdateCsprojWithImportAsync(project.ProjectFilePath, targetsFileName);
            AnsiConsole.MarkupLine($"[green]✓[/] Updated {Path.GetFileName(project.ProjectFilePath)} with Import reference");

            // Display success summary
            AnsiConsole.WriteLine();
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("[bold]Configuration[/]");
            table.AddColumn("[bold]Value[/]");

            table.AddRow("Target Name", settings.TargetName);
            table.AddRow("Input CSS", settings.InputCss);
            table.AddRow("Output CSS", settings.OutputCss);
            table.AddRow("Minify Mode", settings.Minify);
            table.AddRow("Targets File", targetsFileName);

            AnsiConsole.Write(table);

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[green]✓ MSBuild target created successfully![/]");
            AnsiConsole.MarkupLine("[dim]Tailwind CSS will now compile automatically during builds.[/]");

            return 0;
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error creating .targets file:[/] {ex.Message}");
            return 1;
        }
    }

    private void DisplayConfigurationSummary(Settings settings, string targetsFileName)
    {
        var table = new Table();
        table.Border(TableBorder.None);
        table.HideHeaders();
        table.AddColumn("");
        table.AddColumn("");

        table.AddRow("[dim]Target Name:[/]", settings.TargetName);
        table.AddRow("[dim]Input CSS:[/]", settings.InputCss);
        table.AddRow("[dim]Output CSS:[/]", settings.OutputCss);
        table.AddRow("[dim]Minify:[/]", settings.Minify);
        table.AddRow("[dim]Targets File:[/]", targetsFileName);

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }
}
