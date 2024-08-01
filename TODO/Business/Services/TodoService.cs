using Microsoft.EntityFrameworkCore;
using TODO.Data;
using TODO.Dtos;
using TODO.Interfaces;
using TODO.Models;

namespace TODO.Business.Services;

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

    public async Task<IEnumerable<TodoDto>> GetAllTodosWithUserIdAsync(int userId)
    {
        var user = await appDbContext.Users
            .Include(u => u.Todos)
            .FirstOrDefaultAsync(u => u.UserId == userId);

        var todos = user?.Todos
            .Where(t => !t.IsDeleted)
            .Select(t => new TodoDto
            {
                TodoId = t.TodoId,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
            }) ?? Enumerable.Empty<TodoDto>();

        return todos;
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        Todo todo = await appDbContext.Todos.FindAsync(id) ?? throw new InvalidOperationException();
        if (todo == null)
        {
            return false;
        }
        todo.IsDeleted = true;
        appDbContext.Todos.Update(todo);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}