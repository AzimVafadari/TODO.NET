using Microsoft.EntityFrameworkCore;
using TODO.Data;
using TODO.Dtos;
using TODO.Interfaces;
using TODO.Models;

namespace TODO.Services;

public class UserService(AppDbContext appDbContext) : IUserService
{
    public async Task<UserDto> CreateUserAsync(UserDto user)
    {
        appDbContext.Users.Add(new User(user.Username, user.Password));
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserById(int id)
    {
        User user = await appDbContext.Users.FindAsync(id) ?? throw new InvalidOperationException();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        User user = await appDbContext.Users.FindAsync(id) ?? throw new InvalidOperationException();
        user.IsDeleted = true;
        appDbContext.Users.Update(user);
        await appDbContext.SaveChangesAsync();
        return true;
    }
    
    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return appDbContext.Users
            .FirstOrDefaultAsync(u => u.Username.Equals(username));
    }
}