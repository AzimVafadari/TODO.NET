namespace TODO.Interfaces;
using TODO.Models;

public interface IUserService
{
    // create a user
    Task<User> CreateUserAsync(User user);
    
    // edit a user
    Task<User> UpdateUserAsync(User user);
    
    // get a user by id
    Task<User> GetUserById(int id);
    
    // get all users
    IEnumerable<User> GetAllUsersAsync();
    
    // delete a user
    Task<bool> DeleteUserAsync(int id);
}