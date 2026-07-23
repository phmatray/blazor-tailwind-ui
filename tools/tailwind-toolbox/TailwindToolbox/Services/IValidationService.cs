using TailwindToolbox.Models;

namespace TailwindToolbox.Services;

/// <summary>
/// Service for validating Tailwind CSS configuration in Blazor projects.
/// </summary>
public interface IValidationService
{
    /// <summary>
    /// Creates all validation rules defined in the data model.
    /// </summary>
    /// <returns>List of all validation rules.</returns>
    List<ValidationRule> CreateValidationRules();

    /// <summary>
    /// Executes validation rules in parallel against a Blazor project.
    /// </summary>
    /// <param name="rules">Validation rules to execute.</param>
    /// <param name="project">Blazor project to validate.</param>
    /// <returns>Validation results for each rule.</returns>
    Task<List<ValidationResult>> ExecuteValidationRulesAsync(
        List<ValidationRule> rules,
        BlazorProject project);

    /// <summary>
    /// Categorizes validation results by their category.
    /// </summary>
    /// <param name="results">Validation results to categorize.</param>
    /// <param name="rules">Validation rules (for category lookup).</param>
    /// <returns>Dictionary of results grouped by category.</returns>
    Dictionary<ValidationCategory, List<ValidationResult>> CategorizeResults(
        List<ValidationResult> results,
        List<ValidationRule> rules);
}
