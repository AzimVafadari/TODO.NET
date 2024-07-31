using TODO.Dtos;

namespace TODO.Interfaces;
using TODO.Models;

public interface IUserService
{
    // create a user
    Task<UserDto> CreateUserAsync(UserDto user);
    
    // delete a user
    Task<bool> DeleteUserAsync(int id);
    
    // find user by username
    Task<User?> GetUserByUsernameAsync(string username);
}