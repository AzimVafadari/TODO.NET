namespace TODO.Dtos;

public class BaseResponseDto<T>
{
    public T? Data { get; set; }
    public string Message { get; set; }
    
    public BaseResponseDto(T data, string message)
    {
        Data = data;
        Message = message;
    }
}