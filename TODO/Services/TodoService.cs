using TODO.Data;
using TODO.Interfaces;
using TODO.Models;

namespace TODO.Services;

public class TodoService(AppDbContext appDbContext) : ITodoService
{
    public async Task<Todo> CreateTodoAsync(Todo todo)
    {
        appDbContext.Todos.Add(todo);
        await appDbContext.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> UpdateTodoAsync(Todo todo)
    {
        appDbContext.Todos.Update(todo);
        await appDbContext.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo> GetTodoById(int id)
    {
        Todo todo = await appDbContext.Todos.FindAsync(id) ?? throw new InvalidOperationException();
        return todo;
    }

    public IEnumerable<Todo> GetAllTodosAsync()
    {
        return appDbContext.Todos.ToList();
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        Todo todo = await appDbContext.Todos.FindAsync(id) ?? throw new InvalidOperationException();
        appDbContext.Todos.Remove(todo);
        return true;
    }
}