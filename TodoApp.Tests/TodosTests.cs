﻿using TodoApp.Todos;
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
                using WebApplication app = await TestHelpers.StartWebApplication();
                RestClient client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert
                ResponseAssert.Assert200OK(response);
                Assert.AreEqual("[]", response.Content);
            }

            [Test]
            public async Task GivenOneTodos_ShouldReturnTodosInJsonArray()
            {
                // Arrange
                var todos = TestHelpers.CreateTodoList("Say Hello world!") ;
                using WebApplication app = await TestHelpers.StartWebApplication(todos);
                RestClient client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert
                ResponseAssert.Assert200OK(response);
                response.Content.AssertIsJsonForTodos(todos);
                //Assert.AreEqual("[{\"description\":\"Say Hello world!\"}]", response.Content);
            }

            

            [Test]
            public async Task GivenManyTodos_ShouldReturnTodosInJsonArray()
            {
                // Arrange
                var todos = TestHelpers.CreateTodoList("1","2","3");
                using WebApplication app = await TestHelpers.StartWebApplication(todos);
                RestClient client = TestHelpers.CreateRestClient(app);
                var request = new RestRequest("todos");

                //Act

                var response = client.Get(request);

                //Assert
                ResponseAssert.Assert200OK(response);
                response.Content.AssertIsJsonForTodos(todos);
                //Assert.AreEqual("[{\"description\":\"1\"},{\"description\":\"2\"},{\"description\":\"3\"}]", response.Content);
            }
            
        }

    }
}
