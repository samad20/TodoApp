using TodoApp.Todos;
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
            public async Task GivenNoTodosShouldReturnEmptyJsonArray()
            {
                // Arrange
                using WebApplication app = await StartWebApplication();
                RestClient client = CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert
                Assert200OK(response);
                Assert.AreEqual("[]", response.Content);
            }

            [Test]
            public async Task GivenOneTodos_ShouldReturnTodosInJsonArray()
            {
                // Arrange
                var todos = CreateTodoList("Say Hello world!") ;
                using WebApplication app = await StartWebApplication(todos);
                RestClient client = CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert
                Assert200OK(response);
                Assert.AreEqual("[{\"description\":\"Say Hello world!\"}]", response.Content);
            }

            

            [Test]
            public async Task GivenManyTodos_ShouldReturnTodosInJsonArray()
            {
                // Arrange
                var todos = CreateTodoList("1","2","3");
                using WebApplication app = await StartWebApplication(todos);
                RestClient client = CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert
                Assert200OK(response);
                Assert.AreEqual("[{\"description\":\"1\"},{\"description\":\"2\"},{\"description\":\"3\"}]", response.Content);
            }

            private static void Assert200OK(RestResponse response)
            {
                Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            }
            private static IEnumerable<Todo> CreateTodoList(params string[] todos)
            {
                return todos.Select(t => new Todo(t));
            }

            private static async Task<WebApplication> StartWebApplication()
            {
                var app = CreateWebApplication(new List<Todo>());
                await app.StartAsync();
                return app;
            }
            private static async Task<WebApplication> StartWebApplication(IEnumerable<Todo> todos)
            {
                var app = CreateWebApplication(todos);
                await app.StartAsync();
                return app;
            }

            private static WebApplication CreateWebApplication(IEnumerable<Todo> todos)
            {
                var builder = WebApplication.CreateBuilder();
                builder.Services.AddControllers().AddApplicationPart(typeof(TodosController).Assembly);
                builder.Services.AddScoped<ITodoRepository>((sp) => new InMemoryTodoRepository(todos));
                var app = builder.Build();
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
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
