namespace SPSVN.Shared.Exceptions;

public class MaximumRetryAttemptedException(string message) : Exception(message)
{
}