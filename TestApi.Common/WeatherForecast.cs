namespace TestApi.Common;

public class WeatherForecast
{
    public Guid Id { get; set; }
    public DateTimeOffset Date { get; set; }
    public string? Summary { get; set; }
    public int TemperatureC { get; set; }
}
