using AngleSharp.Dom;
using System.Linq;

namespace Radixor.Tests.TestHelpers;

public static class TestExtensions
{
    public static string[] GetClasses(this IElement element)
    {
        var classAttribute = element.GetAttribute("class");
        if (string.IsNullOrWhiteSpace(classAttribute))
            return Array.Empty<string>();
        
        return classAttribute.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
    
    public static bool HasClass(this IElement element, string className)
    {
        return element.GetClasses().Contains(className);
    }
    
    public static string? GetAttribute(this IElement element, string name)
    {
        return element.GetAttribute(name);
    }
    
    public static bool HasAttribute(this IElement element, string name)
    {
        return element.HasAttribute(name);
    }
}