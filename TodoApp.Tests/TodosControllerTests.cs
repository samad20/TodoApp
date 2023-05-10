using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Controllers;

namespace TodoApp.Tests
{
    [TestFixture]
    public class TodosControllerTests
    {
        [Test]
        public void Get_ReturnEmptyResultSet()
        {
            //Arrange
            var repo = Substitute.For<ITodoRepository>();
            repo.Todos.Returns(new Todo[] { });
            var sut = new TodosController(repo);

            //Act
            var result = sut.Get();

            //Assert
            Assert.AreEqual(0, result.Count());
        }


        [Test]
        public void Get_WhenRepoHasTodos_shouldReturnTodos()
        {
            //Arrange
            var repo = Substitute.For<ITodoRepository>();
            repo.Todos.Returns(new Todo[] { new Todo("Test") });
            var sut = new TodosController(repo);

            //Act
            var result = sut.Get();

            //Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test", result.First().description);
        }

    }
}