namespace TodoApp.Todos
{
    public interface ITodoRepository
    {
        IEnumerable<Todo>Todos { get; }
        int Add(string description);
        Todo Put(int id, string description);
    }
}
