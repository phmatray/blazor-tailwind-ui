namespace CatalystUI.Demo.Models;

public class Country
{
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string FlagUrl { get; set; } = string.Empty;
    public List<string> Regions { get; set; } = new();
}