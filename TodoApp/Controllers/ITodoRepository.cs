namespace TodoApp.Controllers
{
    public interface ITodoRepository
    {
        IEnumerable<Todo>Todos { get; }
    }
}
