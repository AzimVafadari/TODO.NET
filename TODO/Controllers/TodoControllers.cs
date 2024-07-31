using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODO.Dtos;
using TODO.Interfaces;
using TODO.Models;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoControllers(ITodoService todoService) : ControllerBase
    {
        [HttpPost("createTodo"), Authorize]
        public async Task<Todo> CreateTodo([FromBody] CreateTodoDto todo)
        {
            int userId = GetUserIdFromToken();
            Todo newTodo = new Todo(todo.Status, todo.Title, todo.Description, userId);
            return await todoService.CreateTodoAsync(newTodo);
        }
        
        [HttpPut("updateTodo"), Authorize]
        public async Task<Todo> UpdateTodo([FromBody] TodoDto todo)
        {
            int userId = GetUserIdFromToken();
            Todo newTodo = new Todo(todo.Status, todo.Title, todo.Description, userId, todo.TodoId);
            return await todoService.UpdateTodoAsync(newTodo);
        }

        [HttpGet("getAllTodos"), Authorize]
        public async Task<IEnumerable<TodoDto>> GetAllTodos()
        {
            int userId = GetUserIdFromToken();
            return await todoService.GetAllTodosWithUserIdAsync(userId);
        }

        [HttpDelete("deleteTodo"), Authorize]
        public async Task<bool> DeleteTodo(int todoId)
        {
           return await todoService.DeleteTodoAsync(todoId);
        } 

        private int GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst("UserId");

            if (userIdClaim == null)
            {
                throw new InvalidOperationException("User ID claim not found in token.");
            }

            return int.Parse(userIdClaim.Value);
        }

    }
    
}
