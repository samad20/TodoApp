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
            ResponseAssert.AssertIs404NotFound(response);
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
            ResponseAssert.Assert200OK(response);

            Assert.AreEqual("{\"id\":0,\"description\":\"todo0\"}", response.Content);
        }
    }
}
