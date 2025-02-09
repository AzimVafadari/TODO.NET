﻿using System.ComponentModel.DataAnnotations;

namespace TODO.Dtos;

public class TodoDto
{
    public int TodoId { set; get; }
    public string Status { set; get; } = string.Empty;

    [MinLength(4), MaxLength(15), Required]
    public string Title { set; get; } = string.Empty;

    [MinLength(4), MaxLength(30), Required]
    public string Description { set; get; } = string.Empty;

    public TodoDto(int todoId, string status, string title, string description)
    {
        TodoId = todoId;
        Status = status;
        Title = title;
        Description = description;
    }

    public TodoDto()
    {
    }
}