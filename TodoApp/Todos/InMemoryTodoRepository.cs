namespace TodoApp.Todos
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private List<Todo> _todos= new();
        public InMemoryTodoRepository(IEnumerable<Todo> todos) {
            _todos.AddRange(todos);
        }
        public IEnumerable<Todo> Todos { get { return _todos.AsReadOnly(); } }

        public int Add(string description)
        {
            var newId = 0;
            if(_todos.Any()) { newId = _todos.Max(x => x.Id) + 1; }
            _todos.Add(new Todo(newId, description));
            return newId;
        }

        public Todo Put(int id, string description)
        {
            var existingTodo = _todos.FirstOrDefault(t => t.Id == id);
            if (existingTodo != null)
            {
                _todos.Remove(existingTodo);
                _todos.Add(new Todo(id, description));
            }
            return existingTodo;
        }
    }
}
