using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODO.Business.Exceptions;
using TODO.Business.Interfaces;
using TODO.Dtos;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TodoController(ITodoService todoService) : ControllerBase
    {
        [HttpPost("createTodo")]
        public async Task<ActionResult<BaseResponseDto<TodoDto?>>> CreateTodo([FromBody] CreateTodoDto todo)
        {
            try
            {
               return Ok(new BaseResponseDto<TodoDto>(await todoService.CreateTodoAsync(todo),
                    "Todo created successfully"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    UnauthorizedAccessException => Unauthorized(new BaseResponseDto<TodoDto?>(null, "User is not authorized")),
                    InvalidOperationException => BadRequest(new BaseResponseDto<TodoDto?>(null, e.Message)),
                    _ => StatusCode(500, new BaseResponseDto<TodoDto?>(null, "Internal error"))
                };
            }
        }
        
        [HttpPut("updateTodo")]
        public async Task<ActionResult<BaseResponseDto<TodoDto?>>> UpdateTodo([FromBody] TodoDto todo)
        {
            try
            {
                return Ok(new BaseResponseDto<TodoDto>(await todoService.UpdateTodoAsync(todo),
                    "Todo updated successfully"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    UnauthorizedAccessException => Unauthorized(new BaseResponseDto<CreateTodoDto?>(null, "User is not authorized")),
                    BadHttpRequestException => BadRequest(new BaseResponseDto<TodoDto?>(null, e.Message)),
                    KeyNotFoundException => NotFound(new BaseResponseDto<TodoDto?>(null, e.Message)),
                    _ => StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"))
                };
            }
        }

        [HttpGet("getAllTodos")]
        public async Task<ActionResult<BaseResponseDto<IEnumerable<TodoDto>?>>> GetAllTodos()
        {
            try
            {
                return Ok(new BaseResponseDto<IEnumerable<TodoDto>>(await todoService.GetAllTodosWithUserIdAsync(),
                    "All todos for the user successfully returned"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    UnauthorizedAccessException => Unauthorized(new BaseResponseDto<CreateTodoDto?>(null, "User is not authorized")),
                    UserNotFoundException => NotFound(new BaseResponseDto<IEnumerable<TodoDto>?>(null, e.Message)),
                    InvalidOperationException => BadRequest(new BaseResponseDto<CreateTodoDto?>(null, e.Message)),
                    _ => StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"))
                };
            }
        }

        [HttpDelete("deleteTodo{todoId}")]
        public async Task<ActionResult<BaseResponseDto<TodoDto?>>> DeleteTodo(int todoId)
        {
            try
            {
                return Ok(new BaseResponseDto<TodoDto>(await todoService.DeleteTodoAsync(todoId),
                    "Todo successfully deleted"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    TodoNotFoundException => NotFound(new BaseResponseDto<TodoDto?>(null, e.Message)),
                    TodoAlreadyDeletedException => Conflict(new BaseResponseDto<TodoDto?>(null, e.Message)),
                    _ => StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"))
                };
            }
        }

    }
    
}
