namespace TailwindToolbox.Models;

/// <summary>
/// Represents the type of Blazor project detected.
/// </summary>
public enum BlazorProjectType
{
    /// <summary>
    /// Blazor Server application (renders on server, uses SignalR).
    /// </summary>
    Server,

    /// <summary>
    /// Blazor WebAssembly application (runs in browser via WASM).
    /// </summary>
    WebAssembly,

    /// <summary>
    /// Blazor Hybrid application (.NET MAUI or WPF with Blazor components).
    /// </summary>
    Hybrid,

    /// <summary>
    /// Could not determine project type (error condition).
    /// </summary>
    Unknown
}
