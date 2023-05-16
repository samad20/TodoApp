﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using TodoApp.Todos;

namespace TodoApp.Tests
{
    [TestFixture]
    public class TodoControllerTests
    {
        public class Get
        {
            [Test]
            public void WhenNoMatchingTodo_ReturnNotFound()
            {
                //Arrange
                var sut = CreateTodoController();

                //Act
                var result = sut.Get(0);

                //Assert
                Assert.IsInstanceOf<NotFoundResult>(result.Result);
            }


            [Test]
            public void WhenRepositoryHasTodoWithMatchingId_shouldReturnTodo()
            {
                //Arrange
                var sut = CreateTodoController("Test");

                //Act
                var result = sut.Get(0);

                //Assert
                Assert.IsInstanceOf<Todo>(result.Value);
                Assert.AreEqual("Test", result.Value?.description);
            }
            [Test]
            public void WhenRepositoryHasTodoWithMatchingId_shouldReturnTodo2()
            {
                //Arrange
                var sut = CreateTodoController("Test0", "Test1");

                //Act
                var result = sut.Get(1);

                //Assert
                Assert.IsInstanceOf<Todo>(result.Value);
                Assert.AreEqual("Test1", result.Value?.description);
            }
            
        }
        public class Post {
            [Test]
            public void ReturnsId()
            {
                //Arrange
                var todoRequest = CreateTodoPostRequest();
                var sut = CreateTodoController();

                //Act
                var result = sut.Post(todoRequest);

                //Assert
                Assert.Greater(result.Value, -1);
            }
            [Test]
            public void AddsTodo()
            {
                //Arrange
                var todoRequest = CreateTodoPostRequest();
                var sut = CreateTodoController();

                //Act
                var result = sut.Post(todoRequest);

                //Assert
                var id = result.Value;
                var todo = sut.Get(id).Value;
                Assert.AreEqual(todoRequest.description, todo?.description);
            }
            [Test]
            public void AddsTodo_AndReturnNewId()
            {
                //Arrange
                var todoRequest1 = CreateTodoPostRequest("Test TODO 1");
                var todoRequest2 = CreateTodoPostRequest("Test TODO 2");
                var sut = CreateTodoController();

                //Act
                var result1 = sut.Post(todoRequest1);
                var result2 = sut.Post(todoRequest2);
                //Assert
                var id1 = GetResultValueAsInt(result1);
                var todo1 = sut.Get(id1).Value;
                var id2 = GetResultValueAsInt(result2);
                var todo2 = sut.Get(id2).Value;

                Assert.AreEqual(todoRequest1.description, todo1?.description);
                Assert.AreEqual(todoRequest2.description, todo2?.description);
            }

            private static TodoPostRequest CreateTodoPostRequest(string description)
            {
                return new TodoPostRequest(description);
            }
            private static TodoPostRequest CreateTodoPostRequest()
            {
                return new TodoPostRequest("Test TODO");
            }

            public static int GetResultValueAsInt(ActionResult<int> result)
            {
                Assert.IsInstanceOf<CreatedResult>(result.Result);
                var resultValue = ((CreatedResult)result.Result).Value;
                Assert.IsInstanceOf<int>(resultValue);
                Assert.IsNotNull(resultValue);
                return (int)resultValue;
            }
        }
        public static TodoController CreateTodoController(params string[] todos)
        {
            var repo = new InMemoryTodoRepository(todos.Select((descrepition, id) => new Todo(id, descrepition)));
            return new TodoController(repo);
        }
    }
}
