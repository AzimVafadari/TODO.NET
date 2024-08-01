namespace TODO.Dtos;

public class BaseResponseDto<T>
{
    public T? Data { get; set; }
    public int Status { get; set; }
    public string Message { get; set; }
    
    public BaseResponseDto(T data, int status, string message)
    {
        Data = data;
        Status = status;
        Message = message;
    }
}