namespace Common.Common.Exceptions;

public class ValidationFailedException : LocalizedException
{
    public ValidationFailedException()
    {
    }

    public ValidationFailedException(string message) : base(message)
    {
    }
}