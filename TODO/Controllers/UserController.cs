using Microsoft.AspNetCore.Mvc;
using TODO.Business.Exceptions;
using TODO.Dtos;
using TODO.Interfaces;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("createUser")]
        public async Task<BaseResponseDto<UserDto?>> CreateUser([FromBody] UserDto user)
        {
            BaseResponseDto<UserDto?> baseResponseDto;
            try
            {
                baseResponseDto = new BaseResponseDto<UserDto?>(await userService.CreateUserAsync(user), 200,
                    "User created successfully");
                return baseResponseDto;
            }
            catch (Exception e)
            {
                if (e is UserAlreadyExistsException)
                {
                    baseResponseDto = new BaseResponseDto<UserDto?>(null, 500, "User already exists");
                    return baseResponseDto;
                }
                else
                {
                    baseResponseDto = new BaseResponseDto<UserDto?>(null, 500, "Internal error");
                    return baseResponseDto;
                }
            }
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDto userDto)
        {
            try
            {
                return Ok(await userService.Login(userDto));
            }
            catch (Exception e)
            {
                if (e is UserNotFoundException)
                {
                    return StatusCode(404, "User not found");
                } else if (e is PasswordIncorrectException)
                {
                    return StatusCode(404, "Password is incorrect");
                }
                else
                {
                    return StatusCode(500, "Internal error");
                }
            }
        }

        [HttpDelete("deleteUser{id}")]
        public async Task<ActionResult<string>> DeleteUserById(int id)
        {
            bool isDeleted = await userService.DeleteUserAsync(id);
            if (isDeleted)
            {
                return Ok("The user deleted successfully");
            }

            return StatusCode(404, "User not found");
        }
    }
}
