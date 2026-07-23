namespace CatalystUI.Demo.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url => $"/events/{Id}";
    public string Date { get; set; } = string.Empty;
    public string Time { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string TotalRevenue { get; set; } = string.Empty;
    public string TotalRevenueChange { get; set; } = string.Empty;
    public int TicketsAvailable { get; set; }
    public int TicketsSold { get; set; }
    public string TicketsSoldChange { get; set; } = string.Empty;
    public string PageViews { get; set; } = string.Empty;
    public string PageViewsChange { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ImgUrl { get; set; } = string.Empty;
    public string ThumbUrl { get; set; } = string.Empty;
}