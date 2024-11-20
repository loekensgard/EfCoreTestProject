using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using TestApi.Common;

namespace TestApi.Tests.IntegrationTests;

public class ForecastControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{

    [Fact]
    public async Task Get_PrivilegedAccessRequests_Without_CompanyId_Returns_Success()
    {
        //Arrange
        var _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });


        //Act
        var queryParam = $"?Status=TimedOut";
        var response = await _client.GetAsync($"/api/WeatherForecast");
        var responseBody = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


}

