using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;
using TailwindToolbox.Models;
using TailwindToolbox.Services;
using TailwindToolbox.Utilities;

namespace TailwindToolbox.Commands;

/// <summary>
/// Command to update Tailwind CSS and related packages with breaking change detection.
/// </summary>
public sealed class UpdateCommand : AsyncCommand<UpdateCommand.Settings>
{
    private readonly IProjectDetector _projectDetector;
    private readonly INpmService _npmService;

    public UpdateCommand(IProjectDetector projectDetector, INpmService npmService)
    {
        _projectDetector = projectDetector;
        _npmService = npmService;
    }

    public sealed class Settings : BaseCommandSettings
    {
        [Description("Path to the Blazor project directory")]
        [CommandOption("-p|--project-dir")]
        public string ProjectDirectory { get; set; } = ".";

        [Description("Specific package to update (updates all if not specified)")]
        [CommandOption("--package")]
        public string? Package { get; set; }

        [Description("Target version to update to (latest if not specified)")]
        [CommandOption("--target-version")]
        public string? TargetVersion { get; set; }

        [Description("Show what would be updated without making changes")]
        [CommandOption("--dry-run")]
        public bool DryRun { get; set; }

        [Description("Skip major version updates (only update minor/patch)")]
        [CommandOption("--skip-breaking")]
        public bool SkipBreaking { get; set; }
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

        AnsiConsole.MarkupLine($"[cyan]Checking for updates in:[/] {project.ProjectName}");
        AnsiConsole.WriteLine();

        // Check for available updates
        List<DependencyVersion> availableUpdates;

        try
        {
            availableUpdates = await _npmService.CheckForUpdatesAsync(project.ProjectDirectory);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error checking for updates:[/] {ex.Message}");
            return 1;
        }

        // Filter by package if specified
        if (!string.IsNullOrEmpty(settings.Package))
        {
            availableUpdates = availableUpdates
                .Where(u => u.PackageName.Equals(settings.Package, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Filter by update availability
        availableUpdates = availableUpdates.Where(u => u.HasUpdate).ToList();

        if (availableUpdates.Count == 0)
        {
            AnsiConsole.MarkupLine("[green]✓[/] All packages are up to date!");
            return 3; // No updates available
        }

        // Detect breaking changes
        var breakingChanges = availableUpdates
            .Where(u => u.InstalledVersion != null && u.LatestVersion != null &&
                       _npmService.DetectBreakingChanges(u.InstalledVersion, u.LatestVersion))
            .ToList();

        // Skip breaking changes if requested
        if (settings.SkipBreaking && breakingChanges.Any())
        {
            AnsiConsole.MarkupLine("[yellow]Skipping major version updates (--skip-breaking flag set):[/]");
            foreach (var update in breakingChanges)
            {
                AnsiConsole.MarkupLine($"  [yellow]•[/] {update.PackageName}: {update.InstalledVersion} → {update.LatestVersion}");
            }
            AnsiConsole.WriteLine();

            availableUpdates = availableUpdates.Except(breakingChanges).ToList();

            if (availableUpdates.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]No non-breaking updates available.[/]");
                return 0;
            }
        }

        // Display update table
        DisplayUpdateTable(availableUpdates, breakingChanges);

        // Warn about breaking changes
        if (breakingChanges.Any() && !settings.SkipBreaking)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[yellow]⚠ Warning: The following updates contain breaking changes:[/]");

            foreach (var update in breakingChanges)
            {
                AnsiConsole.MarkupLine($"  [yellow]•[/] {update.PackageName}: {update.InstalledVersion} → {update.LatestVersion}");
            }

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[yellow]Breaking changes may require code modifications.[/]");
            AnsiConsole.MarkupLine("[dim]Migration guide: https://tailwindcss.com/docs/upgrade-guide[/]");
            AnsiConsole.WriteLine();

            if (!settings.DryRun)
            {
                var proceed = AnsiConsole.Confirm("Do you want to proceed with these updates?", false);
                if (!proceed)
                {
                    AnsiConsole.MarkupLine("[yellow]Update cancelled by user.[/]");
                    return 2; // User cancelled
                }
            }
        }

        // Dry run mode
        if (settings.DryRun)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine("[cyan]Dry run mode - no changes will be made.[/]");
            return 0;
        }

        // Execute updates
        AnsiConsole.WriteLine();
        var successCount = 0;

        await AnsiConsole.Status()
            .StartAsync("Updating packages...", async ctx =>
            {
                foreach (var update in availableUpdates)
                {
                    if (update.LatestVersion == null) continue;

                    ctx.Status($"Updating {update.PackageName} to {update.LatestVersion}...");

                    try
                    {
                        var result = await _npmService.UpdatePackageAsync(
                            update.PackageName,
                            update.LatestVersion,
                            project.ProjectDirectory);

                        if (result.Success)
                        {
                            AnsiConsole.MarkupLine($"[green]✓[/] Updated {update.PackageName} to {update.LatestVersion}");
                            successCount++;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine($"[red]✗[/] Failed to update {update.PackageName}: {result.StandardError}");
                        }
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.MarkupLine($"[red]✗[/] Failed to update {update.PackageName}: {ex.Message}");
                    }
                }
            });

        AnsiConsole.WriteLine();

        if (successCount == availableUpdates.Count)
        {
            AnsiConsole.MarkupLine($"[green]✓ Successfully updated {successCount} package(s)![/]");
            return 0;
        }
        else
        {
            AnsiConsole.MarkupLine($"[yellow]⚠ Updated {successCount}/{availableUpdates.Count} package(s).[/]");
            return 1;
        }
    }

    private void DisplayUpdateTable(List<DependencyVersion> updates, List<DependencyVersion> breakingChanges)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[bold]Package[/]");
        table.AddColumn("[bold]Current[/]");
        table.AddColumn("[bold]Latest[/]");
        table.AddColumn("[bold]Change Type[/]");

        foreach (var update in updates)
        {
            var isBreaking = breakingChanges.Contains(update);
            var changeType = isBreaking ? "[red]Major[/]" : "[green]Minor/Patch[/]";

            table.AddRow(
                update.PackageName,
                update.InstalledVersion ?? "N/A",
                update.LatestVersion ?? "N/A",
                changeType);
        }

        AnsiConsole.Write(table);
    }
}
