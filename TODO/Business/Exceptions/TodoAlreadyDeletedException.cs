namespace TODO.Business.Exceptions;

public class TodoAlreadyDeletedException : Exception
{
    // Constructor that takes a message parameter
    public TodoAlreadyDeletedException(string message) : base(message) 
    {
    }
    
    public TodoAlreadyDeletedException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public TodoAlreadyDeletedException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}