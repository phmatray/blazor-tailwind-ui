using System.Text;

namespace ShadBz.BlazorApp.Components.UI;

public class CssBuilder
{
    private readonly List<string> _classes = new();

    public CssBuilder(string? initialClass = null)
    {
        if (!string.IsNullOrWhiteSpace(initialClass))
        {
            _classes.Add(initialClass);
        }
    }

    public CssBuilder AddClass(string? className)
    {
        if (!string.IsNullOrWhiteSpace(className))
        {
            _classes.Add(className);
        }
        return this;
    }

    public CssBuilder AddClass(string className, bool condition)
    {
        if (condition && !string.IsNullOrWhiteSpace(className))
        {
            _classes.Add(className);
        }
        return this;
    }

    public string Build()
    {
        return string.Join(" ", _classes);
    }
}