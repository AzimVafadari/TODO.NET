namespace TODO.Business.Exceptions;

public class StatusAlreadyDeletedException : Exception
{
    // Constructor that takes a message parameter
    public StatusAlreadyDeletedException(string message) : base(message) 
    {
    }
    
    public StatusAlreadyDeletedException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public StatusAlreadyDeletedException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}