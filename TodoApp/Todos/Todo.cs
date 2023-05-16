using System.ComponentModel.DataAnnotations;

namespace TodoApp.Todos
{
    public class Todo
    {
        public int Id { get; private set; }
        [MinLength(3)]
        public string description { get; private set; }

        public Todo(int id, string description)
        {
            this.Id = id;
            this.description = description;
        }
    }
}