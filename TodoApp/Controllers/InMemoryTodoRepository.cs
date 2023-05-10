﻿namespace TodoApp.Controllers
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        public InMemoryTodoRepository(IEnumerable<Todo> todos) {
            Todos = todos;
        }
        public IEnumerable<Todo> Todos { get; }
    }
}
