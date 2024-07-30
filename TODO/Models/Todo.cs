using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO.Models;

public class Todo
{
    [Key]
    public int TodoId { set; get; }
    public string Status { set; get; } = string.Empty;
    public string Description { set; get; } = string.Empty;
    public bool IsDeleted { set; get; }
    
    // Foreign key for User
    public int UserId { set; get; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { set; get; }
}