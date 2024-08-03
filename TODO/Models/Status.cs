namespace TODO.Models;

public class Status
{
    public int StatusId { get; }

    public string StatusName { get; set; } = string.Empty;
    
    public bool IsDeleted { get; set; }
}