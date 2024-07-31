namespace TODO.Dtos;

public class UserDto
{
    public string Username { set; get; }
    public string Password { set; get; }

    public UserDto(string username, string password)
    {
        Username = username;
        Password = password;
    }
}