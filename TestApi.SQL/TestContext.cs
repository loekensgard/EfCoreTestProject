using Microsoft.EntityFrameworkCore;
using TestApi.Common;
using TestApi.SQL.Configurations;

namespace TestApi.SQL;

public class TestContext : DbContext
{
    public TestContext() : base() { }
    public TestContext(DbContextOptions options) : base(options) { }
    public DbSet<WeatherForecast> Forecasts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ForecastConfiguration());
    }
}
