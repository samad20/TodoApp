using TodoApp.Controllers;
using NSubstitute;
using Microsoft.Extensions.Logging;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace TodoApp.Tests;

public class WeatherForecastControllerTests
{
    [Test]
    public void Get_ReturnsFive()
    {
        //Arrange
        var logger = Substitute.For<ILogger<WeatherForecastController>>();
        var sut = new WeatherForecastController(logger);

        //Act
        var result = sut.Get();

        // Assert
        Assert.AreEqual(5, result.Count());
    }

    [Test]
    public async Task TestWeb()
    {
        // Arrange
        var builder = WebApplication.CreateBuilder();
        // Add services to the container.
        builder.Services.AddControllers().AddApplicationPart(typeof(WeatherForecastController).Assembly);
        var app = builder.Build();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        await app.StartAsync();
        var uri = app.Urls.First();
        var client = new RestClient(uri);
        var request = new RestRequest("WeatherForecast");

        //Act

        var response = client.Get(request);

        //Assert

        Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

    }
}