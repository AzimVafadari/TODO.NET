using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TODO.Business.Exceptions;
using TODO.Business.Interfaces;
using TODO.Data;
using TODO.Dtos;
using TODO.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TODO.Business.Services;

public class UserService(AppDbContext appDbContext, IConfiguration config) : IUserService
{
    public async Task<UserDto> CreateUserAsync(UserDto user)
    {
        if (await GetUserByUsernameAsync(user.Username) == null)
        {
            appDbContext.Users.Add(new User(user.Username, user.Password));
            await appDbContext.SaveChangesAsync();
            return user;
        }
        // if user exists
        throw new UserAlreadyExistsException();
    }

    public async Task<User> GetUserById(int id)
    {
        User user = await appDbContext.Users.FindAsync(id) ?? throw new InvalidOperationException();
        return user;
    }

    public async Task<UserDto> DeleteUserAsync(int id)
    {
        User user = await appDbContext.Users.FindAsync(id) ?? throw new InvalidOperationException();
        if (user != null)
        {
            user.IsDeleted = true;
            appDbContext.Users.Update(user);
            await appDbContext.SaveChangesAsync();
        }

        throw new UserNotFoundException();
    }
    
    public Task<User?> GetUserByUsernameAsync(string username)
    {
        return appDbContext.Users
            .FirstOrDefaultAsync(u => u.Username.Equals(username) && !u.IsDeleted);
    }

    public async Task<string> Login(UserDto userDto)
    {
        User? foundUser = await GetUserByUsernameAsync(userDto.Username);
        if (foundUser == null)
            throw new UserNotFoundException();
        if (!foundUser.Password.Equals(userDto.Password))
        {
            throw new PasswordIncorrectException();
        }

        return GenerateJwt(foundUser);
    }
    
    private string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("UserId", user.UserId.ToString())
        };
        var token = new JwtSecurityToken(config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}