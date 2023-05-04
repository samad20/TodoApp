using Microsoft.AspNetCore.Mvc;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController
    {
        [HttpGet(Name = "GetTodos")]
        public string[] Get() {
            return Array.Empty<string>();
        }
    }
}
