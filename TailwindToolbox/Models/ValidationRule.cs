namespace TailwindToolbox.Models;

/// <summary>
/// Represents a single validation check with pass/fail logic and remediation guidance.
/// </summary>
public sealed record ValidationRule
{
    /// <summary>
    /// Unique identifier (e.g., "NODE_INSTALLED").
    /// </summary>
    public required string RuleId { get; init; }

    /// <summary>
    /// Human-readable name (short description, max 50 chars).
    /// </summary>
    public required string Name { get; init; }

    /// <summary>
    /// Detailed explanation of what is checked.
    /// </summary>
    public required string Description { get; init; }

    /// <summary>
    /// Grouping category (Environment, Files, Config, Dependencies, Integration).
    /// </summary>
    public required ValidationCategory Category { get; init; }

    /// <summary>
    /// Severity level (Error, Warning, or Info).
    /// </summary>
    public required ValidationSeverity Severity { get; init; }

    /// <summary>
    /// Logic to perform the check (must not throw exceptions).
    /// </summary>
    public required Func<BlazorProject, ValidationResult> CheckFunction { get; init; }

    /// <summary>
    /// Actionable steps to fix if the check failed.
    /// </summary>
    public required string Remediation { get; init; }
}
