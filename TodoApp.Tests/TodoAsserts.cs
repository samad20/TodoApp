using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Todos;
using NUnit.Framework;
namespace TodoApp.Tests
{
    public static class TodoAsserts
    {
        public static void AssertIsJsonForTodos(this string body, IEnumerable<Todo> todos)
        {
            StringAssert.StartsWith("[", body);
            foreach (var todo in todos) { 
                StringAssert.Contains($"{{\"id\":{todo.Id},\"description\":\"{todo.description}\"}}",body);
            }
            StringAssert.EndsWith("]", body);
        }
    }
}
