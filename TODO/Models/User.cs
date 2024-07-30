using System.ComponentModel.DataAnnotations;

namespace TODO.Models;

public class User
{
    [Key]
    public int UserId { get; }
    [MinLength(4), MaxLength(10), Required]
    public string Username { set; get; } = string.Empty;
    [MinLength(8), MaxLength(16), Required]
    public string Password { set; get; } = string.Empty;
    // Navigation property
    public virtual ICollection<Todo> Todos { get; } = new List<Todo>();
}