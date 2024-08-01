using Microsoft.AspNetCore.Mvc;
using TODO.Dtos;

namespace TODO.Business.Interfaces;
using TODO.Models;

public interface IUserService
{
    // create a user
    Task<UserDto> CreateUserAsync(UserDto user);
    
    // delete a user
    Task<UserDto> DeleteUserAsync(int id);
    
    // find user by username
    Task<User?> GetUserByUsernameAsync(string username);
    
    // get token in login
    Task<string> Login([FromBody] UserDto userDto);
}