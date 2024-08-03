namespace TODO.Models;

public class Status
{
    public int StatusId { get; }

    public string StatusName { get; set; } = string.Empty;
    
    public bool IsDeleted { get; set; }

    public Status(string statusName)
    {
        StatusName = statusName;
    }
    
    public Status(int statusId, string statusName)
    {
        StatusId = statusId;
        StatusName = statusName;
    }
}