namespace TODO.Business.Exceptions;

public class UserAlreadyExistsException : Exception
{
    // Constructor that takes a message parameter
    public UserAlreadyExistsException(string message) : base(message) 
    {
    }
    
    public UserAlreadyExistsException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public UserAlreadyExistsException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}