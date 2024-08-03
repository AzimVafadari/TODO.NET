using TODO.Dtos;

namespace TODO.Business.Interfaces;
public interface ITodoService
{
    // create a todo
    Task<TodoDto> CreateTodoAsync(CreateTodoDto todo);
    
    // edit a todo
    Task<TodoDto> UpdateTodoAsync(TodoDto todo);
    
    
    // get all todos
    Task<IEnumerable<TodoDto>> GetAllTodosWithUserIdAsync();
    
    // delete a todo
    Task<TodoDto> DeleteTodoAsync(int id);
}