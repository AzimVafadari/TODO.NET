namespace TODO.Interfaces;
using TODO.Models;

public interface ITodoService
{
    // create a todo
    Task<Todo> CreateTodoAsync(Todo todo);
    
    // edit a todo
    Task<Todo> UpdateTodoAsync(Todo todo);
    
    // get a todo by id
    Task<Todo> GetTodoById(int id);
    
    // get all todos
    IEnumerable<Todo> GetAllTodosAsync();
    
    // delete a todo
    Task<bool> DeleteTodoAsync(int id);
}