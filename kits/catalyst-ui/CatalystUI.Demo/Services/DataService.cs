using CatalystUI.Demo.Models;

namespace CatalystUI.Demo.Services;

public class DataService
{
    private readonly List<Event> _events;
    private readonly List<Order> _orders;
    private readonly List<Country> _countries;

    public DataService()
    {
        _events = InitializeEvents();
        _orders = InitializeOrders();
        _countries = InitializeCountries();
    }

    public Task<List<Event>> GetEventsAsync() => Task.FromResult(_events);
    
    public Task<Event?> GetEventAsync(int id) => 
        Task.FromResult(_events.FirstOrDefault(e => e.Id == id));

    public Task<List<Order>> GetOrdersAsync() => Task.FromResult(_orders);
    
    public Task<Order?> GetOrderAsync(int id) => 
        Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    public Task<List<Order>> GetRecentOrdersAsync(int count = 10) => 
        Task.FromResult(_orders.Take(count).ToList());

    public Task<List<Order>> GetEventOrdersAsync(int eventId) =>
        Task.FromResult(_orders.Where(o => o.Event.Id == eventId).ToList());

    public List<Country> GetCountries() => _countries;

    private List<Event> InitializeEvents()
    {
        return new List<Event>
        {
            new Event
            {
                Id = 1000,
                Name = "Bear Hug: Live in Concert",
                Date = "May 20, 2024",
                Time = "10 PM",
                Location = "Harmony Theater, Winnipeg, MB",
                TotalRevenue = "$102,552",
                TotalRevenueChange = "+3.2%",
                TicketsAvailable = 500,
                TicketsSold = 350,
                TicketsSoldChange = "+8.1%",
                PageViews = "24,300",
                PageViewsChange = "-0.75%",
                Status = "On Sale",
                ImgUrl = "/events/bear-hug.jpg",
                ThumbUrl = "/events/bear-hug-thumb.jpg"
            },
            new Event
            {
                Id = 1001,
                Name = "Six Fingers — DJ Set",
                Date = "Jun 2, 2024",
                Time = "8 PM",
                Location = "Moonbeam Arena, Uxbridge, ON",
                TotalRevenue = "$24,115",
                TotalRevenueChange = "+3.2%",
                TicketsAvailable = 150,
                TicketsSold = 72,
                TicketsSoldChange = "+8.1%",
                PageViews = "57,544",
                PageViewsChange = "-2.5%",
                Status = "On Sale",
                ImgUrl = "/events/six-fingers.jpg",
                ThumbUrl = "/events/six-fingers-thumb.jpg"
            },
            new Event
            {
                Id = 1002,
                Name = "We All Look The Same",
                Date = "Aug 5, 2024",
                Time = "4 PM",
                Location = "Electric Coliseum, New York, NY",
                TotalRevenue = "$40,598",
                TotalRevenueChange = "+3.2%",
                TicketsAvailable = 275,
                TicketsSold = 275,
                TicketsSoldChange = "+8.1%",
                PageViews = "122,122",
                PageViewsChange = "-8.0%",
                Status = "Closed",
                ImgUrl = "/events/we-all-look-the-same.jpg",
                ThumbUrl = "/events/we-all-look-the-same-thumb.jpg"
            },
            new Event
            {
                Id = 1003,
                Name = "Viking People",
                Date = "Dec 31, 2024",
                Time = "8 PM",
                Location = "Tapestry Hall, Cambridge, ON",
                TotalRevenue = "$3,552",
                TotalRevenueChange = "+3.2%",
                TicketsAvailable = 40,
                TicketsSold = 6,
                TicketsSoldChange = "+8.1%",
                PageViews = "9,000",
                PageViewsChange = "-0.15%",
                Status = "On Sale",
                ImgUrl = "/events/viking-people.jpg",
                ThumbUrl = "/events/viking-people-thumb.jpg"
            }
        };
    }

    private List<Order> InitializeOrders()
    {
        var orders = new List<Order>();
        var events = _events;

        orders.Add(new Order
        {
            Id = 3000,
            Date = "May 9, 2024",
            Amount = new OrderAmount
            {
                Usd = "$80.00",
                Cad = "$109.47",
                Fee = "$3.28",
                Net = "$106.19"
            },
            Payment = new Payment
            {
                TransactionId = "ch_2HLf8DfYJ0Db7asfCC5T546TY",
                Card = new Card
                {
                    Number = "1254",
                    Type = "American Express",
                    Expiry = "01 / 2025"
                }
            },
            Customer = new Customer
            {
                Name = "Leslie Alexander",
                Email = "leslie.alexander@example.com",
                Address = "123 Main St. Toronto, ON",
                Country = "Canada",
                CountryFlagUrl = "/flags/ca.svg"
            },
            Event = events.First(e => e.Id == 1000)
        });

        orders.Add(new Order
        {
            Id = 3001,
            Date = "May 5, 2024",
            Amount = new OrderAmount
            {
                Usd = "$299.00",
                Cad = "$409.13",
                Fee = "$12.27",
                Net = "$396.86"
            },
            Payment = new Payment
            {
                TransactionId = "ch_1KLf7AsYJ0Dda7fs3CC5d46TY",
                Card = new Card
                {
                    Number = "3897",
                    Type = "Visa",
                    Expiry = "06 / 2024"
                }
            },
            Customer = new Customer
            {
                Name = "Michael Foster",
                Email = "michael.foster@example.com",
                Address = "357 Bridge St. New York, NY",
                Country = "USA",
                CountryFlagUrl = "/flags/us.svg"
            },
            Event = events.First(e => e.Id == 1001)
        });

        orders.Add(new Order
        {
            Id = 3002,
            Date = "Apr 28, 2024",
            Amount = new OrderAmount
            {
                Usd = "$150.00",
                Cad = "$205.25",
                Fee = "$6.15",
                Net = "$199.10"
            },
            Payment = new Payment
            {
                TransactionId = "ch_2DLf5AsYJ0Ddb7fs3CC5d46TY",
                Card = new Card
                {
                    Number = "7421",
                    Type = "Mastercard",
                    Expiry = "12 / 2026"
                }
            },
            Customer = new Customer
            {
                Name = "Dries Vincent",
                Email = "dries.vincent@example.com",
                Address = "456 Elm St. Vancouver, BC",
                Country = "Canada",
                CountryFlagUrl = "/flags/ca.svg"
            },
            Event = events.First(e => e.Id == 1002)
        });

        orders.Add(new Order
        {
            Id = 3003,
            Date = "Apr 23, 2024",
            Amount = new OrderAmount
            {
                Usd = "$80.00",
                Cad = "$109.47",
                Fee = "$3.28",
                Net = "$106.19"
            },
            Payment = new Payment
            {
                TransactionId = "ch_3KLf6DfYJ0Db7fassCC546TY",
                Card = new Card
                {
                    Number = "5683",
                    Type = "Visa",
                    Expiry = "06 / 2024"
                }
            },
            Customer = new Customer
            {
                Name = "Lindsay Walton",
                Email = "lindsay.walton@example.com",
                Address = "789 Oak St. Montreal, QC",
                Country = "Canada",
                CountryFlagUrl = "/flags/ca.svg"
            },
            Event = events.First(e => e.Id == 1000)
        });

        orders.Add(new Order
        {
            Id = 3004,
            Date = "Apr 18, 2024",
            Amount = new OrderAmount
            {
                Usd = "$114.99",
                Cad = "$157.34",
                Fee = "$4.72",
                Net = "$152.62"
            },
            Payment = new Payment
            {
                TransactionId = "ch_4HLf7DfYJ0Db78fas3C5d6TY",
                Card = new Card
                {
                    Number = "9576",
                    Type = "Visa",
                    Expiry = "01 / 2025"
                }
            },
            Customer = new Customer
            {
                Name = "Courtney Henry",
                Email = "courtney.henry@example.com",
                Address = "321 Pine St. Calgary, AB",
                Country = "Canada",
                CountryFlagUrl = "/flags/ca.svg"
            },
            Event = events.First(e => e.Id == 1003)
        });

        return orders;
    }

    private List<Country> InitializeCountries()
    {
        return new List<Country>
        {
            new Country
            {
                Name = "Canada",
                Code = "CA",
                FlagUrl = "/flags/ca.svg",
                Regions = new List<string>
                {
                    "Alberta", "British Columbia", "Manitoba", "New Brunswick",
                    "Newfoundland and Labrador", "Northwest Territories", "Nova Scotia",
                    "Nunavut", "Ontario", "Prince Edward Island", "Quebec",
                    "Saskatchewan", "Yukon"
                }
            },
            new Country
            {
                Name = "Mexico",
                Code = "MX",
                FlagUrl = "/flags/mx.svg",
                Regions = new List<string>
                {
                    "Aguascalientes", "Baja California", "Baja California Sur", "Campeche",
                    "Chiapas", "Chihuahua", "Ciudad de Mexico", "Coahuila", "Colima",
                    "Durango", "Guanajuato", "Guerrero", "Hidalgo", "Jalisco",
                    "Mexico State", "Michoacán", "Morelos", "Nayarit", "Nuevo León",
                    "Oaxaca", "Puebla", "Querétaro", "Quintana Roo", "San Luis Potosí",
                    "Sinaloa", "Sonora", "Tabasco", "Tamaulipas", "Tlaxcala",
                    "Veracruz", "Yucatán", "Zacatecas"
                }
            },
            new Country
            {
                Name = "United States",
                Code = "US",
                FlagUrl = "/flags/us.svg",
                Regions = new List<string>
                {
                    "Alabama", "Alaska", "American Samoa", "Arizona", "Arkansas",
                    "California", "Colorado", "Connecticut", "Delaware", "Washington DC",
                    "Florida", "Georgia", "Guam", "Hawaii", "Idaho", "Illinois",
                    "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine",
                    "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi",
                    "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire",
                    "New Jersey", "New Mexico", "New York", "North Carolina",
                    "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania",
                    "Rhode Island", "South Carolina", "South Dakota", "Tennessee",
                    "Texas", "Utah", "Vermont", "Virginia", "Washington",
                    "West Virginia", "Wisconsin", "Wyoming"
                }
            }
        };
    }
}