namespace TodoApp.Controllers
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        public InMemoryTodoRepository() { 
            Todos = new List<Todo>();
        }
        public IEnumerable<Todo> Todos { get; }
    }
}
