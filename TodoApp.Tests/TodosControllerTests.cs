using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Todos;

namespace TodoApp.Tests
{
    [TestFixture]
    public class TodosControllerTests
    {
        public class Get
        {
            [Test]
            public void WhenRepositoryHasNoTodos_ReturnEmptyResultSet()
            {
                //Arrange
                var sut = CreateTodosController();

                //Act
                var result = sut.Get();

                //Assert
                Assert.AreEqual(0, result.Count());
            }

            
            [Test]
            public void WhenRepositoryHasTodos_shouldReturnTodos()
            {
                //Arrange
                var sut = CreateTodosController("Test");

                //Act
                var result = sut.Get();

                //Assert
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual("Test", result.First().description);
            }
            public static TodosController CreateTodosController(params string[] todos)
            {
                var repo = new InMemoryTodoRepository(todos.Select(t => new Todo(t)));
                return new TodosController(repo);
            }

        }
    }
}