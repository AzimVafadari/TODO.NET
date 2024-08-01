namespace TODO.Business.Exceptions;

public class PasswordIncorrectException : Exception
{
    // Constructor that takes a message parameter
    public PasswordIncorrectException(string message) : base(message) 
    {
    }
    
    public PasswordIncorrectException() 
    {
    }

    // Constructor that takes a message and an inner exception
    public PasswordIncorrectException(string message, Exception innerException) : base(message, innerException) 
    {
    }
}