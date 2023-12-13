namespace BlazorTailwind.Components.Layout;

public class NavigationItem
{
    public string Name { get; set; }
    public string Href { get; set; }
    public Type? Icon { get; set; }
    public string? Initial { get; set; }
    public bool Current { get; set; }
}