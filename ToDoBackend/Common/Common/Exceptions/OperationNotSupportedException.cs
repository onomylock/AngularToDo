namespace Common.Common.Exceptions;

public class OperationNotSupportedException : LocalizedException
{
    public OperationNotSupportedException()
    {
    }

    public OperationNotSupportedException(string message) : base(message)
    {
    }
}