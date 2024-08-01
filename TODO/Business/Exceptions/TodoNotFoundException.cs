namespace TODO.Business.Exceptions;

public class TodoNotFoundException : Exception
{
    // Constructor that takes a message parameter
    public TodoNotFoundException(string message) : base(message) 
    {
    }
    
    public TodoNotFoundException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public TodoNotFoundException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}