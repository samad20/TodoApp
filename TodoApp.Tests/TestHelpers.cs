using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Todos;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace TodoApp.Tests
{
    public  class TestHelpers
    {
        public static async Task<WebApplication> StartWebApplication()
        {
            var app = CreateWebApplication(new List<Todo>());
            await app.StartAsync();
            return app;
        }
        public static async Task<WebApplication> StartWebApplication(IEnumerable<Todo> todos)
        {
            var app = CreateWebApplication(todos);
            await app.StartAsync();
            return app;
        }

        public static WebApplication CreateWebApplication(IEnumerable<Todo> todos)
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
        public static RestClient CreateRestClient(WebApplication app)
        {
            var uri = app.Urls.First();
            var client = new RestClient(uri);
            return client;
        }

        public static IEnumerable<Todo> CreateTodoList(params string[] todos)
        {
            return todos.Select((descrepition,i) => new Todo(i, descrepition));
        }
    }
}
