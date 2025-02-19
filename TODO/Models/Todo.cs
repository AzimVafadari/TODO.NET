﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TODO.Models;

public class Todo
{
    public int TodoId { get; }
    public string Status { set; get; } = string.Empty;
    [MinLength(4), MaxLength(15), Required]
    public string Title { set; get; } = string.Empty;
    [MinLength(4), MaxLength(30), Required]
    public string Description { set; get; } = string.Empty;
    public bool IsDeleted { set; get; }
    
    // Foreign key for User
    public int UserId { set; get; }

    // Navigation property
    [ForeignKey("UserId")]
    public User User { set; get; }

    public Todo(string status, string title, string description, int userId, int todoId)
    {
        Status = status;
        Title = title;
        Description = description;
        UserId = userId;
        TodoId = todoId;
    }
    
    public Todo(string status, string title, string description, int userId)
    {
        Status = status;
        Title = title;
        Description = description;
        UserId = userId;
    }
}