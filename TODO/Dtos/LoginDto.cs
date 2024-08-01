namespace TODO.Dtos;

public class LoginDto(string token)
{
    public string Token { get; set; } = token;
}