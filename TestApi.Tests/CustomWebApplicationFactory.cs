using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestApi.SQL;
using TestApi.Tests.Helpers;

namespace TestApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment("Test");
        builder.ConfigureServices(services =>
        {

            var dbContext = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<TestContext>));

            if (dbContext != null)
                services.Remove(dbContext);

            // Add database context to use in-memory
            string databaseName = Guid.NewGuid().ToString();
            services.AddDbContext<TestContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName);
            });


            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<TestContext>();

            try
            {
                TestDatabaseFixture.InitializeDatabase(db);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Could not initialize database", e);
            }
        });
    }
}