namespace TailwindToolbox.Models;

/// <summary>
/// Represents the outcome of a single validation rule execution.
/// </summary>
public sealed record ValidationResult
{
    /// <summary>
    /// Associated validation rule ID.
    /// </summary>
    public required string RuleId { get; init; }

    /// <summary>
    /// Whether the check succeeded.
    /// </summary>
    public required bool Passed { get; init; }

    /// <summary>
    /// Additional context shown to user (optional).
    /// </summary>
    public string? Message { get; init; }

    /// <summary>
    /// Relevant file path if applicable (optional, absolute path).
    /// </summary>
    public string? FilePath { get; init; }

    /// <summary>
    /// What was found (optional, for version mismatches).
    /// </summary>
    public string? ActualValue { get; init; }

    /// <summary>
    /// What was expected (optional, for version mismatches).
    /// </summary>
    public string? ExpectedValue { get; init; }

    /// <summary>
    /// When the check was performed (UTC timestamp).
    /// </summary>
    public DateTime TimestampUtc { get; init; } = DateTime.UtcNow;
}
