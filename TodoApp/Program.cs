using TodoApp.Todos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var todos = new List<Todo>()
{
    new Todo(0,"Say Hello"),
    new Todo(1,"Say Goodbye")
};
var repo = new InMemoryTodoRepository(todos);
builder.Services.AddScoped<ITodoRepository>((sp) => repo);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
