namespace Shared.Exceptions;

public class SpServerException : Exception
{
    public SpServerException()
    {
    }

    public SpServerException(string message) : base(message)
    {
    }

    public SpServerException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public SpServerException(int serverErrorCode, string message, Exception? innerException) : base(message,
        innerException)
    {
        ServerErrorCode = serverErrorCode;
    }

    public int ServerErrorCode { get; set; }
}