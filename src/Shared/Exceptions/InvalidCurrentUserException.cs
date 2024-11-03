namespace SPSVN.Shared.Exceptions;

public class InvalidCurrentUserException : Exception
{
    public InvalidCurrentUserException() : base()
    {
    }

    public InvalidCurrentUserException(string message) : base(message)
    {

    }
}