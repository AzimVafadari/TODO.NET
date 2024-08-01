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
                if (e is UserAlreadyExistsException)
                {
                    return Conflict(new BaseResponseDto<UserDto?>(null, "User already exists"));
                }
                else
                {
                    return StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"));
                }
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
                if (e is UserNotFoundException)
                {
                    return NotFound(new BaseResponseDto<LoginDto?>(null, "User not found"));
                } else if (e is PasswordIncorrectException)
                {
                    return BadRequest(new BaseResponseDto<LoginDto?>(null, "Password is incorrect"));
                }
                else
                {
                    return StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"));
                }
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
                if (e is UserNotFoundException)
                {
                    return NotFound(new BaseResponseDto<LoginDto?>(null, "User not found"));
                }
                else
                {
                    return StatusCode(500, new BaseResponseDto<UserDto?>(null, "Internal error"));
                }
            }
        }
    }
}
