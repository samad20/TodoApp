﻿namespace TodoApp.Todos
{
    public interface ITodoRepository
    {
        IEnumerable<Todo>Todos { get; }
        int Add(string description);    
    }
}
