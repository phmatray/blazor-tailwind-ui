using BlazorTailwind.Models;

namespace BlazorTailwind.Services;

public class WeatherForecastService
{
    private static readonly string[] Summaries =
        ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
    
    public async IAsyncEnumerable<WeatherForecast> GetForecastsAsync(int count = 5)
    {
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        
        var forecasts = Enumerable
            // we create a range of 5 integers
            .Range(1, count)
            // then we select the date by adding the index to the start date
            .Select(startDate.AddDays)
            // then we generate a random forecast for each date
            .Select(GenerateRandomForecast);

        foreach (WeatherForecast forecast in forecasts)
        {
            yield return forecast;
        }
        
        await Task.CompletedTask;
    }
    
    private static WeatherForecast GenerateRandomForecast(DateOnly date)
        => new()
        {
            Date = date,
            TemperatureC = GetRandomTemperature(),
            Summary = GetRandomSummary()
        };

    private static int GetRandomTemperature()
        => Random.Shared.Next(-20, 55);
    
    private static string GetRandomSummary()
        => Summaries[Random.Shared.Next(Summaries.Length)];
}