using Microsoft.AspNetCore.Components;

namespace CatalystUI.Components;

public abstract class CatalystComponentBase : ComponentBase
{
    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Gets or sets the CSS class names for the component.
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the component.
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets whether the component is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Combines the provided class names with the component's base classes.
    /// </summary>
    protected string CombineClasses(params string?[] classes)
    {
        var allClasses = new List<string>();
        
        // Add base component class
        allClasses.Add("catalyst-component");
        
        // Add provided classes
        foreach (var cls in classes)
        {
            if (!string.IsNullOrWhiteSpace(cls))
            {
                allClasses.Add(cls);
            }
        }
        
        // Add user-provided class
        if (!string.IsNullOrWhiteSpace(Class))
        {
            allClasses.Add(Class);
        }
        
        // Add disabled class if needed
        if (Disabled)
        {
            allClasses.Add("catalyst-disabled");
        }
        
        return string.Join(" ", allClasses);
    }

    /// <summary>
    /// Creates a dictionary of HTML attributes including the additional attributes.
    /// </summary>
    protected Dictionary<string, object> GetAttributes()
    {
        var attributes = new Dictionary<string, object>();
        
        // Add ID if provided
        if (!string.IsNullOrWhiteSpace(Id))
        {
            attributes["id"] = Id;
        }
        
        // Add disabled attribute if needed
        if (Disabled)
        {
            attributes["disabled"] = true;
        }
        
        // Merge with additional attributes
        if (AdditionalAttributes != null)
        {
            foreach (var kvp in AdditionalAttributes)
            {
                attributes[kvp.Key] = kvp.Value;
            }
        }
        
        return attributes;
    }
    
    /// <summary>
    /// Creates a dictionary of HTML attributes with additional pairs.
    /// </summary>
    protected Dictionary<string, object> GetAttributesWithPairs(params (string key, object? value)[] additionalPairs)
    {
        var attributes = GetAttributes();
        
        // Add additional pairs
        foreach (var (key, value) in additionalPairs)
        {
            if (value != null)
            {
                attributes[key] = value;
            }
        }
        
        return attributes;
    }

    /// <summary>
    /// Determines if a CSS class should be applied based on a condition.
    /// </summary>
    protected static string? If(bool condition, string className)
    {
        return condition ? className : null;
    }
}