using System.ComponentModel;
using System.Text.Json;
using Spectre.Console;
using Spectre.Console.Cli;
using TailwindToolbox.Models;
using TailwindToolbox.Services;
using TailwindToolbox.Utilities;
using ValidationResult = TailwindToolbox.Models.ValidationResult;

namespace TailwindToolbox.Commands;

/// <summary>
/// Command to validate Tailwind CSS configuration in a Blazor project.
/// </summary>
public sealed class CheckCommand : AsyncCommand<CheckCommand.Settings>
{
    private readonly IProjectDetector _projectDetector;
    private readonly IValidationService _validationService;

    public CheckCommand(
        IProjectDetector projectDetector,
        IValidationService validationService)
    {
        _projectDetector = projectDetector;
        _validationService = validationService;
    }

    public sealed class Settings : BaseCommandSettings
    {
        [Description("Path to Blazor project directory")]
        [CommandOption("-p|--project-dir")]
        public string ProjectDirectory { get; set; } = ".";

        [Description("Filter by category (environment|files|config|dependencies|integration)")]
        [CommandOption("-c|--category")]
        public string? Category { get; set; }

        [Description("Output format (table|json|text)")]
        [CommandOption("-f|--format")]
        public string Format { get; set; } = "table";

        [Description("Treat warnings as errors")]
        [CommandOption("--fail-on-warning")]
        public bool FailOnWarning { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken = default)
    {
        try
        {
            // Resolve project directory to absolute path
            var projectDir = Path.GetFullPath(settings.ProjectDirectory);

            // Detect Blazor project
            var project = await _projectDetector.DetectProjectAsync(projectDir);
            if (project == null)
            {
                if (settings.Format == "json")
                {
                    Console.WriteLine(JsonSerializer.Serialize(new
                    {
                        error = "No Blazor project found in directory",
                        exitCode = 2
                    }));
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Error:[/] No Blazor project found in directory");
                }
                return 2; // Project not found
            }

            // Display header (except for JSON output)
            if (settings.Format != "json")
            {
                AnsiConsole.Write(new Rule("[bold]Tailwind Configuration Validation[/]").RuleStyle("blue"));
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine($"[dim]Project:[/] {project.ProjectName} ([dim]{project.ProjectType}, {project.DotNetVersion}[/])");
                AnsiConsole.WriteLine();
            }

            // Create validation rules
            var allRules = _validationService.CreateValidationRules();

            // Filter by category if specified
            var rules = FilterRulesByCategory(allRules, settings.Category);

            // Execute validation rules
            var results = await _validationService.ExecuteValidationRulesAsync(rules, project);

            // Categorize results
            var categorized = _validationService.CategorizeResults(results, rules);

            // Display results based on format
            switch (settings.Format.ToLowerInvariant())
            {
                case "json":
                    DisplayJsonOutput(results, rules, project);
                    break;
                case "text":
                    DisplayTextOutput(results, rules);
                    break;
                case "table":
                default:
                    DisplayTableOutput(categorized, rules);
                    break;
            }

            // Determine exit code
            return DetermineExitCode(results, rules, settings.FailOnWarning);
        }
        catch (Exception ex)
        {
            if (settings.Format == "json")
            {
                Console.WriteLine(JsonSerializer.Serialize(new
                {
                    error = ex.Message,
                    exitCode = 1
                }));
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
            return 1;
        }
    }

    private List<ValidationRule> FilterRulesByCategory(List<ValidationRule> rules, string? categoryFilter)
    {
        if (string.IsNullOrWhiteSpace(categoryFilter))
        {
            return rules;
        }

        var category = categoryFilter.ToLowerInvariant() switch
        {
            "environment" => ValidationCategory.Environment,
            "files" => ValidationCategory.Files,
            "config" or "configuration" => ValidationCategory.Configuration,
            "dependencies" => ValidationCategory.Dependencies,
            "integration" => ValidationCategory.Integration,
            _ => throw new ArgumentException($"Invalid category: {categoryFilter}")
        };

        return rules.Where(r => r.Category == category).ToList();
    }

    private void DisplayTableOutput(
        Dictionary<ValidationCategory, List<ValidationResult>> categorized,
        List<ValidationRule> rules)
    {
        foreach (var category in categorized.Keys.OrderBy(c => c))
        {
            var categoryResults = categorized[category];
            var categoryRules = rules.Where(r => r.Category == category).ToList();

            AnsiConsole.MarkupLine($"[bold]{category}[/]");
            AnsiConsole.WriteLine();

            foreach (var result in categoryResults)
            {
                var rule = categoryRules.FirstOrDefault(r => r.RuleId == result.RuleId);
                if (rule == null) continue;

                var icon = result.Passed ? "[green]✓[/]" : GetSeverityIcon(rule.Severity);
                var status = result.Passed ? "[green]PASS[/]" : GetSeverityText(rule.Severity);

                AnsiConsole.MarkupLine($"  {icon} {rule.Name}");
                AnsiConsole.MarkupLine($"    [dim]{result.Message}[/]");

                if (!result.Passed && !string.IsNullOrWhiteSpace(rule.Remediation))
                {
                    AnsiConsole.MarkupLine($"    [yellow]→[/] {rule.Remediation}");
                }

                if (result.FilePath != null)
                {
                    AnsiConsole.MarkupLine($"    [dim]File: {result.FilePath}[/]");
                }

                AnsiConsole.WriteLine();
            }
        }

        // Display summary
        var totalPassed = categorized.Values.SelectMany(v => v).Count(r => r.Passed);
        var totalFailed = categorized.Values.SelectMany(v => v).Count(r => !r.Passed);
        var total = totalPassed + totalFailed;

        AnsiConsole.Write(new Rule().RuleStyle("blue"));
        var summaryColor = totalFailed == 0 ? "green" : "red";
        AnsiConsole.MarkupLine($"[{summaryColor} bold]Summary:[/] {totalPassed}/{total} checks passed");

        if (totalFailed > 0)
        {
            var errors = categorized.Values.SelectMany(v => v)
                .Count(r => !r.Passed && rules.First(ru => ru.RuleId == r.RuleId).Severity == ValidationSeverity.Error);
            var warnings = categorized.Values.SelectMany(v => v)
                .Count(r => !r.Passed && rules.First(ru => ru.RuleId == r.RuleId).Severity == ValidationSeverity.Warning);

            if (errors > 0)
            {
                AnsiConsole.MarkupLine($"[red]  • {errors} error(s)[/]");
            }
            if (warnings > 0)
            {
                AnsiConsole.MarkupLine($"[yellow]  • {warnings} warning(s)[/]");
            }
        }
    }

    private void DisplayTextOutput(List<ValidationResult> results, List<ValidationRule> rules)
    {
        foreach (var result in results)
        {
            var rule = rules.FirstOrDefault(r => r.RuleId == result.RuleId);
            if (rule == null) continue;

            var status = result.Passed ? "PASS" : "FAIL";
            var severity = rule.Severity.ToString().ToUpperInvariant();

            Console.WriteLine($"[{status}] [{severity}] {rule.Name}");
            Console.WriteLine($"  {result.Message}");

            if (!result.Passed && !string.IsNullOrWhiteSpace(rule.Remediation))
            {
                Console.WriteLine($"  → {rule.Remediation}");
            }

            Console.WriteLine();
        }

        // Summary
        var totalPassed = results.Count(r => r.Passed);
        var total = results.Count;
        Console.WriteLine($"Summary: {totalPassed}/{total} checks passed");
    }

    private void DisplayJsonOutput(
        List<ValidationResult> results,
        List<ValidationRule> rules,
        BlazorProject project)
    {
        var errors = results.Count(r => !r.Passed &&
            rules.First(ru => ru.RuleId == r.RuleId).Severity == ValidationSeverity.Error);
        var warnings = results.Count(r => !r.Passed &&
            rules.First(ru => ru.RuleId == r.RuleId).Severity == ValidationSeverity.Warning);

        var output = new
        {
            project = new
            {
                name = project.ProjectName,
                type = project.ProjectType.ToString(),
                dotNetVersion = project.DotNetVersion,
                directory = project.ProjectDirectory
            },
            summary = new
            {
                total = results.Count,
                passed = results.Count(r => r.Passed),
                failed = results.Count(r => !r.Passed),
                errors,
                warnings
            },
            results = results.Select(r =>
            {
                var rule = rules.First(ru => ru.RuleId == r.RuleId);
                return new
                {
                    ruleId = r.RuleId,
                    name = rule.Name,
                    category = rule.Category.ToString(),
                    severity = rule.Severity.ToString(),
                    passed = r.Passed,
                    message = r.Message,
                    filePath = r.FilePath,
                    actualValue = r.ActualValue,
                    expectedValue = r.ExpectedValue,
                    remediation = r.Passed ? null : rule.Remediation,
                    timestamp = r.TimestampUtc
                };
            }).ToList()
        };

        var json = JsonSerializer.Serialize(output, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Console.WriteLine(json);
    }

    private int DetermineExitCode(
        List<ValidationResult> results,
        List<ValidationRule> rules,
        bool failOnWarning)
    {
        var hasErrors = results.Any(r => !r.Passed &&
            rules.First(ru => ru.RuleId == r.RuleId).Severity == ValidationSeverity.Error);

        if (hasErrors)
        {
            return 1; // Errors present
        }

        if (failOnWarning)
        {
            var hasWarnings = results.Any(r => !r.Passed &&
                rules.First(ru => ru.RuleId == r.RuleId).Severity == ValidationSeverity.Warning);

            if (hasWarnings)
            {
                return 1; // Treat warnings as errors
            }
        }

        return 0; // Success
    }

    private static string GetSeverityIcon(ValidationSeverity severity)
    {
        return severity switch
        {
            ValidationSeverity.Error => "[red]✗[/]",
            ValidationSeverity.Warning => "[yellow]⚠[/]",
            ValidationSeverity.Info => "[blue]ℹ[/]",
            _ => "[dim]?[/]"
        };
    }

    private static string GetSeverityText(ValidationSeverity severity)
    {
        return severity switch
        {
            ValidationSeverity.Error => "[red]FAIL[/]",
            ValidationSeverity.Warning => "[yellow]WARN[/]",
            ValidationSeverity.Info => "[blue]INFO[/]",
            _ => "[dim]UNKNOWN[/]"
        };
    }
}
