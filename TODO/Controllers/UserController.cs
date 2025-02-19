using Microsoft.AspNetCore.Mvc;
using TODO.Business.Exceptions;
using TODO.Business.Interfaces;
using TODO.Dtos;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("createUser")]
        public async Task<ActionResult<UserDto?>> CreateUser([FromBody] UserDto user)
        {
            try
            {
                return Ok(new BaseResponseDto<UserDto>(await userService.CreateUserAsync(user), "User created successfully"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    UserAlreadyExistsException => Conflict(new BaseResponseDto<UserDto?>(null, "User already exists")),
                    _ => StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"))
                };
            }
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<LoginDto?>> Login([FromBody] UserDto userDto)
        {
            try
            {
                return Ok(new BaseResponseDto<LoginDto?>(new LoginDto(await userService.Login(userDto)),
                    "Login successfully"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    UserNotFoundException => NotFound(new BaseResponseDto<LoginDto?>(null, "User not found")),
                    PasswordIncorrectException => BadRequest(new BaseResponseDto<LoginDto?>(null, "Password is incorrect")),
                    UserAlreadyDeletedException => Conflict(new BaseResponseDto<UserDto?>(null, "User already deleted")),
                    _ => StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"))
                };
            }
        }

        [HttpDelete("deleteUser{id}")]
        public async Task<ActionResult<BaseResponseDto<UserDto?>>> DeleteUserById(int id)
        {
            try
            {
                return Ok(new BaseResponseDto<UserDto?>(await userService.DeleteUserAsync(id), "The user deleted successfully"));
            }
            catch (Exception e)
            {
                return e switch
                {
                    UserNotFoundException => NotFound(new BaseResponseDto<LoginDto?>(null, "User not found")),
                    UserAlreadyDeletedException => Conflict(new BaseResponseDto<UserDto?>(null, "User already deleted")),
                    _ => StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"))
                };
            }
        }
    }
}
