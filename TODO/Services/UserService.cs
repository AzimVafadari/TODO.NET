using TODO.Data;
using TODO.Interfaces;
using TODO.Models;

namespace TODO.Services;

public class UserService(AppDbContext appDbContext) : IUserService
{
    public async Task<User> CreateUserAsync(User user)
    {
        appDbContext.Users.Add(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        appDbContext.Users.Update(user);
        await appDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> GetUserById(int id)
    {
        User user = await appDbContext.Users.FindAsync(id) ?? throw new InvalidOperationException();
        return user;
    }

    public IEnumerable<User> GetAllUsersAsync()
    {
        return appDbContext.Users.ToList();
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        User user = await appDbContext.Users.FindAsync(id) ?? throw new InvalidOperationException();
        appDbContext.Users.Remove(user);
        return true;
    }
}