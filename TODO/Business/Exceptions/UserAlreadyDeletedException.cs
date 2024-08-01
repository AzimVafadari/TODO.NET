namespace TODO.Business.Exceptions;

public class UserAlreadyDeletedException : Exception
{
    // Constructor that takes a message parameter
    public UserAlreadyDeletedException(string message) : base(message) 
    {
    }
    
    public UserAlreadyDeletedException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public UserAlreadyDeletedException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}