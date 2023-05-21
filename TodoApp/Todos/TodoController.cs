using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Todos
{
    [ApiController]
    [Route("todo")]
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
        [HttpPost(Name = "PostTodo")]
        public ActionResult<int> Post(TodoPostRequest todoPostRequest)
        {
            var id = repo.Add(todoPostRequest.description);
            return new CreatedResult($"todo/{id}", id);
        }

        [HttpPut("{id}", Name = "PutTodo")]
        public ActionResult Put([FromRoute] int id, TodoPutRequest putRequest)
        {
            var existingTodo = repo.Put(id, putRequest.description);
            return existingTodo is null ? new NotFoundResult() : new NoContentResult();
        }
    }
}
