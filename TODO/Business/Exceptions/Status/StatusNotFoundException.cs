namespace TODO.Business.Exceptions;

public class StatusNotFoundException : Exception
{
    // Constructor that takes a message parameter
    public StatusNotFoundException(string message) : base(message) 
    {
    }
    
    public StatusNotFoundException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public StatusNotFoundException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}