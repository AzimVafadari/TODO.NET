namespace TODO.Business.Exceptions;

public class UserNotFoundException : Exception
{
    // Constructor that takes a message parameter
    public UserNotFoundException(string message) : base(message) 
    {
    }
    
    public UserNotFoundException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public UserNotFoundException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}