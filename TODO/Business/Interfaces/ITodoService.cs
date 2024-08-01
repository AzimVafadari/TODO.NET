using TODO.Dtos;

namespace TODO.Interfaces;
using TODO.Models;

public interface ITodoService
{
    // create a todo
    Task<Todo> CreateTodoAsync(Todo todo);
    
    // edit a todo
    Task<Todo> UpdateTodoAsync(Todo todo);
    
    
    // get all todos
    Task<IEnumerable<TodoDto>> GetAllTodosWithUserIdAsync(int userId);
    
    // delete a todo
    Task<bool> DeleteTodoAsync(int id);
}