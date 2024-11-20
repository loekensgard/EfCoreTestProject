using TestApi.Common;
using TestApi.SQL;

namespace TestApi.Tests.Helpers;

public static class TestDatabaseFixture
{
    private static readonly object _lock = new();
    private static bool _databaseInitialized;

    public static void InitializeDatabase(TestContext context)
    {
        lock (_lock)
        {
            if (!_databaseInitialized)
            {

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();


                context.Forecasts.Add(new WeatherForecast
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    TemperatureC = 25,
                    Summary = "Hot"
                });

                context.Forecasts.Add(new WeatherForecast
                {
                    Id = Guid.NewGuid(),
                    Date = DateTime.Now,
                    TemperatureC = 10,
                    Summary = "Cold"
                });


                context.SaveChanges();

                _databaseInitialized = true;
            }
        }
    }
}