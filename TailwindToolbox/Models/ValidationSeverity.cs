namespace TailwindToolbox.Models;

/// <summary>
/// Represents the severity level of a validation failure.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// Must be fixed for Tailwind to work (blocks setup).
    /// </summary>
    Error,

    /// <summary>
    /// Should be fixed but not blocking (outdated versions).
    /// </summary>
    Warning,

    /// <summary>
    /// Informational only (optimization suggestions).
    /// </summary>
    Info
}
