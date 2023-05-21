using System.ComponentModel.DataAnnotations;

namespace TodoApp.Todos
{
    public record TodoPostRequest([MinLength(3)] string description);
    public record TodoPutRequest([MinLength(3)] string description);
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