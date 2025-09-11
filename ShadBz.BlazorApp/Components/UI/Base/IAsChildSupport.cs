namespace ShadBz.BlazorApp.Components.UI.Base;

/// <summary>
/// Interface for components that support the AsChild pattern
/// </summary>
public interface IAsChildSupport
{
    /// <summary>
    /// When true, the component will not render its own element but will pass its props to the child
    /// </summary>
    bool AsChild { get; set; }
}