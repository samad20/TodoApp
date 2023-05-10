namespace TodoApp.Todos
{
    public class Todo
    {
        public string description { get; private set; }

        public Todo(string description)
        {
            this.description = description;
        }
    }
}