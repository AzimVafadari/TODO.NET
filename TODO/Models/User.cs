using System.ComponentModel.DataAnnotations;

namespace TODO.Models;

public class User
{
    [Key]
    public int UserId { set; get; }
    public string Username { set; get; } = string.Empty;
    public string Password { set; get; } = string.Empty;
    // List of Todos
    public ICollection<Todo> Todos = new List<Todo>();
}