namespace Common.Common.Exceptions;

public class OperationFailedException : LocalizedException
{
    public OperationFailedException()
    {
    }

    public OperationFailedException(string message) : base(message)
    {
    }
}