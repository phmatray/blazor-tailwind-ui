namespace ShadBz.BlazorApp.Components.UI;

/// <summary>
/// Helper methods for CSS-related operations
/// </summary>
public static class CssHelper
{
    /// <summary>
    /// Determines if a value is a space scale value (0-9 or -1 to -9)
    /// </summary>
    public static bool IsSpaceScaleValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        
        // Check if it's a number 0-9 or negative number -1 to -9
        return value == "0" || 
               (value.Length == 1 && char.IsDigit(value[0])) ||
               (value.Length == 2 && value[0] == '-' && char.IsDigit(value[1]));
    }
    
    /// <summary>
    /// Determines if a value is a grid count value (1-9)
    /// </summary>
    public static bool IsGridCountValue(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;
        
        // Check if it's a single digit 1-9
        return value.Length == 1 && char.IsDigit(value[0]) && value[0] != '0';
    }
    
    /// <summary>
    /// Parses a grid value, converting numeric strings to repeat syntax
    /// </summary>
    public static string ParseGridValue(string value)
    {
        // If it's just a number, convert to repeat syntax
        if (System.Text.RegularExpressions.Regex.IsMatch(value, @"^\d+$"))
        {
            return $"repeat({value}, minmax(0, 1fr))";
        }
        
        // Otherwise return as-is
        return value;
    }
}