namespace Common.Common.Exceptions;

public class OperationNotPermittedException : LocalizedException
{
    public OperationNotPermittedException()
    {
    }

    public OperationNotPermittedException(string message) : base(message)
    {
    }
}