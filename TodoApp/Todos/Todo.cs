namespace TodoApp.Todos
{
    public class Todo
    {
        public int Id { get; private set; }
        public string description { get; private set; }

        public Todo(int id, string description)
        {
            this.Id = id;
            this.description = description;
        }
    }
}