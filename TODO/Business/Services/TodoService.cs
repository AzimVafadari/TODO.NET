using Microsoft.EntityFrameworkCore;
using TODO.Business.Exceptions;
using TODO.Business.Interfaces;
using TODO.Data;
using TODO.Dtos;
using TODO.Models;

namespace TODO.Business.Services;
public class TodoService(AppDbContext appDbContext, IHttpContextAccessor httpContextAccessor) : ITodoService
{
    public async Task<CreateTodoDto> CreateTodoAsync(CreateTodoDto todo)
    {
        try
        {
            int userId = GetUserIdFromToken();
            Todo newTodo = new Todo(todo.Status, todo.Title, todo.Description, userId);
            appDbContext.Todos.Add(newTodo);
            await appDbContext.SaveChangesAsync();
            return todo;
        }
        catch (Exception e)
        {
            throw;
        }
    }

    private IHttpContextAccessor HttpContextAccessor { get; } = httpContextAccessor;

    public async Task<TodoDto> UpdateTodoAsync(TodoDto todo)
    {
        try
        {
            int userId = GetUserIdFromToken();
            // find the todo
            Todo? foundTodo = await appDbContext.Todos.FindAsync(todo.TodoId);
            if (foundTodo == null)
            {
                throw new KeyNotFoundException("Todo not found");
            }
            if (!foundTodo.UserId.Equals(userId))
            {
                throw new BadHttpRequestException("No matching");
            }

            foundTodo.Status = todo.Status;
            foundTodo.Description = todo.Description;
            foundTodo.Title = todo.Title;
            appDbContext.Todos.Update(foundTodo);
            await appDbContext.SaveChangesAsync();
            Todo? updatedTodo = await appDbContext.Todos.FindAsync(todo.TodoId);
            return new TodoDto(updatedTodo.TodoId, updatedTodo.Status, updatedTodo.Title, updatedTodo.Description);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<IEnumerable<TodoDto>> GetAllTodosWithUserIdAsync()
    {
        try
        {
            int userId = GetUserIdFromToken();
            var user = await appDbContext.Users
                .Include(u => u.Todos)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

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
        catch (Exception e)
        {
            throw;
        }
    }

    public async Task<TodoDto> DeleteTodoAsync(int id)
    {
        Todo? todo = await appDbContext.Todos.FindAsync(id);
        if (todo == null)
        {
            throw new TodoNotFoundException("Todo not found");
        }

        if (todo.IsDeleted)
        {
            throw new TodoAlreadyDeletedException("Todo already deleted");
        }
        todo.IsDeleted = true;
        appDbContext.Todos.Update(todo);
        await appDbContext.SaveChangesAsync();
        return new TodoDto(todo.TodoId, todo.Status, todo.Title, todo.Description);
    }
    
    private int GetUserIdFromToken()
    {
        if (HttpContextAccessor == null)
        {
            throw new ArgumentNullException(nameof(HttpContextAccessor));
        }

        var userIdClaim = HttpContextAccessor.HttpContext?.User.FindFirst("UserId");

        if (userIdClaim == null)
        {
            throw new InvalidOperationException("User ID claim not found in token.");
        }

        if (!int.TryParse(userIdClaim.Value, out int userId))
        {
            throw new InvalidOperationException("Invalid User ID claim value.");
        }

        return userId;
    }
}