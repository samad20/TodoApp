using System;
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
        public class GET
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
            public static TodoController CreateTodoController(params string[] todos)
            {
                var repo = new InMemoryTodoRepository(todos.Select((descrepition, id) => new Todo(id, descrepition)));
                return new TodoController(repo);
            }
        }
    }
}
