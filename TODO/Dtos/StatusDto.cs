namespace TODO.Dtos;

public class StatusDto
{
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;

    public StatusDto(int statusId, string statusName)
    {
        StatusId = statusId;
        StatusName = statusName;
    }

    public StatusDto()
    {
    }
}