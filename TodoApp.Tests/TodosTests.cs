using TodoApp.Controllers;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace TodoApp.Tests
{
    [TestFixture]
    public class TodosTests
    {
        public class Get
        {
            

        [Test]
            public async Task ShouldReturn200OK()
            {
                // Arrange
                using WebApplication app = await StartWebApplication();
                RestClient client = CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert

                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public async Task ShouldReturnEmptyJsonArray()
            {
                // Arrange
                using WebApplication app = await StartWebApplication();
                RestClient client = CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert

                Assert.AreEqual("[]", response.Content);
            }

            private static async Task<WebApplication> StartWebApplication()
            {
                var builder = WebApplication.CreateBuilder();
                builder.Services.AddControllers().AddApplicationPart(typeof(TodosController).Assembly);
                var app = builder.Build();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                await app.StartAsync();
                return app;
            }

            private static RestClient CreateRestClient(WebApplication app)
            {
                var uri = app.Urls.First();
                var client = new RestClient(uri);
                return client;
            }

        }
    
    }
}
