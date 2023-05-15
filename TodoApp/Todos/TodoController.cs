﻿using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Todos
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController
    {
        private ITodoRepository repo;

        public TodoController(ITodoRepository repo)
        {
            this.repo = repo;
        }
        
        [HttpGet("{id}",Name = "GetTodo")]
        public ActionResult<Todo> Get(int id)
        {
            var todo = repo.Todos.FirstOrDefault(todo => todo.Id==id);
            if (todo == null) return new NotFoundResult(); 
            return todo;
        }
    }
}
