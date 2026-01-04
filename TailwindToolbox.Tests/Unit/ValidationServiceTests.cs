using TailwindToolbox.Models;
using TailwindToolbox.Services;

namespace TailwindToolbox.Tests.Unit;

/// <summary>
/// Unit tests for ValidationService.
/// </summary>
public sealed class ValidationServiceTests
{
    [Fact]
    public void CreateValidationRules_ReturnsAllRequiredRules()
    {
        // Arrange
        var service = new ValidationService();

        // Act
        var rules = service.CreateValidationRules();

        // Assert
        Assert.NotNull(rules);
        Assert.NotEmpty(rules);

        // Should have all 17 validation rules from data-model.md
        Assert.Equal(17, rules.Count);

        // Verify rule categories are present
        Assert.Contains(rules, r => r.Category == ValidationCategory.Environment);
        Assert.Contains(rules, r => r.Category == ValidationCategory.Files);
        Assert.Contains(rules, r => r.Category == ValidationCategory.Configuration);
        Assert.Contains(rules, r => r.Category == ValidationCategory.Dependencies);
        Assert.Contains(rules, r => r.Category == ValidationCategory.Integration);
    }

    [Fact]
    public void CreateValidationRules_EachRuleHasUniqueId()
    {
        // Arrange
        var service = new ValidationService();

        // Act
        var rules = service.CreateValidationRules();

        // Assert
        var ruleIds = rules.Select(r => r.RuleId).ToList();
        var distinctIds = ruleIds.Distinct().ToList();

        Assert.Equal(ruleIds.Count, distinctIds.Count);
    }

    [Fact]
    public void CreateValidationRules_EachRuleHasCheckFunction()
    {
        // Arrange
        var service = new ValidationService();

        // Act
        var rules = service.CreateValidationRules();

        // Assert
        foreach (var rule in rules)
        {
            Assert.NotNull(rule.CheckFunction);
        }
    }

    [Fact]
    public void CreateValidationRules_EachRuleHasRemediation()
    {
        // Arrange
        var service = new ValidationService();

        // Act
        var rules = service.CreateValidationRules();

        // Assert
        foreach (var rule in rules)
        {
            Assert.False(string.IsNullOrWhiteSpace(rule.Remediation));
        }
    }

    [Fact]
    public async Task ExecuteValidationRules_WithValidProject_ReturnsResults()
    {
        // Arrange
        var service = new ValidationService();
        var project = CreateTestProject();
        var rules = service.CreateValidationRules();

        // Act
        var results = await service.ExecuteValidationRulesAsync(rules, project);

        // Assert
        Assert.NotNull(results);
        Assert.Equal(rules.Count, results.Count);
    }

    [Fact]
    public async Task ExecuteValidationRules_RunsInParallel()
    {
        // Arrange
        var service = new ValidationService();
        var project = CreateTestProject();
        var rules = service.CreateValidationRules();

        // Act
        var startTime = DateTime.UtcNow;
        var results = await service.ExecuteValidationRulesAsync(rules, project);
        var duration = DateTime.UtcNow - startTime;

        // Assert - parallel execution should be fast
        Assert.NotNull(results);
        Assert.True(duration.TotalSeconds < 10, "Validation should complete in under 10 seconds");
    }

    [Fact]
    public void CategorizeResults_GroupsByCategory()
    {
        // Arrange
        var service = new ValidationService();
        var results = new List<ValidationResult>
        {
            new ValidationResult
            {
                RuleId = "NODE_INSTALLED",
                Passed = true,
                Message = "Node.js found",
                TimestampUtc = DateTime.UtcNow
            },
            new ValidationResult
            {
                RuleId = "NPM_INSTALLED",
                Passed = true,
                Message = "npm found",
                TimestampUtc = DateTime.UtcNow
            },
            new ValidationResult
            {
                RuleId = "TAILWIND_CONFIG_EXISTS",
                Passed = false,
                Message = "tailwind.config.js not found",
                TimestampUtc = DateTime.UtcNow
            }
        };

        var rules = new List<ValidationRule>
        {
            new ValidationRule
            {
                RuleId = "NODE_INSTALLED",
                Name = "Node.js Installation",
                Description = "Verifies Node.js is installed",
                Category = ValidationCategory.Environment,
                Severity = ValidationSeverity.Error,
                CheckFunction = _ => results[0],
                Remediation = "Install Node.js"
            },
            new ValidationRule
            {
                RuleId = "NPM_INSTALLED",
                Name = "npm Installation",
                Description = "Verifies npm is installed",
                Category = ValidationCategory.Environment,
                Severity = ValidationSeverity.Error,
                CheckFunction = _ => results[1],
                Remediation = "Install npm"
            },
            new ValidationRule
            {
                RuleId = "TAILWIND_CONFIG_EXISTS",
                Name = "Tailwind Config File",
                Description = "Checks for tailwind.config.js",
                Category = ValidationCategory.Files,
                Severity = ValidationSeverity.Error,
                CheckFunction = _ => results[2],
                Remediation = "Run setup command"
            }
        };

        // Act
        var categorized = service.CategorizeResults(results, rules);

        // Assert
        Assert.NotNull(categorized);
        Assert.True(categorized.ContainsKey(ValidationCategory.Environment));
        Assert.True(categorized.ContainsKey(ValidationCategory.Files));
        Assert.Equal(2, categorized[ValidationCategory.Environment].Count);
        Assert.Single(categorized[ValidationCategory.Files]);
    }

    private static BlazorProject CreateTestProject()
    {
        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        return new BlazorProject
        {
            ProjectFilePath = Path.Combine(tempDir, "Test.csproj"),
            ProjectDirectory = tempDir,
            ProjectName = "Test",
            DotNetVersion = "net10.0",
            ProjectType = BlazorProjectType.Server,
            HasTailwindConfig = false,
            HasPackageJson = false,
            HasBuildTargets = false,
            WwwRootPath = null
        };
    }
}
