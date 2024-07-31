using System.ComponentModel.DataAnnotations;

namespace TODO.Models;

public class User
{
    public int UserId { get; }
    [MinLength(4), MaxLength(10), Required]
    public string Username { set; get; } = string.Empty;
    [MinLength(8), MaxLength(16), Required]
    public string Password { set; get; } = string.Empty;
    public bool IsDeleted { set; get; }
    // Navigation property
    public ICollection<Todo> Todos { get; } = new List<Todo>();

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}