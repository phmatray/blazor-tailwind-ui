namespace CatalystUI.Demo.Models;

public class Order
{
    public int Id { get; set; }
    public string Url => $"/orders/{Id}";
    public string Date { get; set; } = string.Empty;
    public OrderAmount Amount { get; set; } = new();
    public Payment Payment { get; set; } = new();
    public Customer Customer { get; set; } = new();
    public Event Event { get; set; } = new();
}

public class OrderAmount
{
    public string Usd { get; set; } = string.Empty;
    public string Cad { get; set; } = string.Empty;
    public string Fee { get; set; } = string.Empty;
    public string Net { get; set; } = string.Empty;
}

public class Payment
{
    public string TransactionId { get; set; } = string.Empty;
    public Card Card { get; set; } = new();
}

public class Card
{
    public string Number { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Expiry { get; set; } = string.Empty;
}

public class Customer
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string CountryFlagUrl { get; set; } = string.Empty;
}