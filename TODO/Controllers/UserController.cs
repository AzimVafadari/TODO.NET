using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TODO.Dtos;
using TODO.Interfaces;
using TODO.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TODO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService, IConfiguration config) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto user)
        {
            await userService.CreateUserAsync(user);
            return Ok("User created successfully");
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            User? user = await userService.GetUserByUsernameAsync(userDto.Username);
            if (user == null)
            {
                return BadRequest("User Not found");
            }
            if (user.Password.Equals(userDto.Password))
            {
                var tokenString = GenerateJwt();
                return Ok(new { token = tokenString });
            }
            return Unauthorized();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            bool isDeleted = await userService.DeleteUserAsync(id);
            if (isDeleted)
            {
                return Ok("The user deleted successfully");
            }

            return BadRequest("User not found");
        }
        
        private string GenerateJwt()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Azim"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
