using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using NSubstitute;
using RestSharp;

namespace TodoApp.Tests
{
    public class TodoTests
    {
       public class Get
        {
            [Test]
            public async Task GivenNoTodosWithMatchingID_ShouldReturn404NotFound()
            {
                // Arrange
                using WebApplication app = await TestHelpers.StartWebApplication();
                RestClient client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todo/0");

                //Act

                var response = client.Get(request);

                //Assert
                response.AssertIs404NotFound();
            }

            [Test]
            public async Task GivenTodosWithMatchingID_ShouldReturnTodoAsJson()
            {
                // Arrange
                var todos = TestHelpers.CreateTodoList("todo0");
                using var app = await TestHelpers.StartWebApplication(todos);
                var client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todo/0");

                //Act
                var response = client.Get(request);

                //Assert
                response.Assert200OK();
                response.Content.AssertIsJsonForTodo(todos.First());
            }
        }
        public class Post
        {
            [TestCase("")]
            [TestCase("{}")]
            [TestCase($"{{\"description\":\"\"}}")]
            public async Task GivenInvalidJsonBody_ShouldResturn400BadRequest(string jsonBody)
            {
                //Arrange
                using var app = await TestHelpers.StartWebApplication();
                var client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todo");
                request.AddJsonBody(jsonBody);
                //Act
                var response = client.Post(request);
                //Assert
                response.AssertIs400BadRequest();

            }


            [Test]
            public async Task GivenJsonBodyWithDescription_ShouldResturn201Created()
            {
                //Arrange
                using var app = await TestHelpers.StartWebApplication();
                var client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todo");
                request.AddParameter("application/json", $"{{\"description\":\"test\"}}", ParameterType.RequestBody);
                //Act
                var response = client.Post(request);
                //Assert
                response.AssertIs201Created();
                Assert.AreEqual("0", response.Content);
                response.AssertLocationHeaders("todo/0");
            }
        }
    }
}
