namespace Shared.Exceptions;

public class InvalidCurrentUserException : Exception
{
    public InvalidCurrentUserException()
    {
    }

    public InvalidCurrentUserException(string message) : base(message)
    {
    }
}