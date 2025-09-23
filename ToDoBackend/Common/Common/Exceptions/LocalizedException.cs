namespace Common.Common.Exceptions;

public abstract class LocalizedException : Exception
{
    protected LocalizedException()
    {
    }

    protected LocalizedException(string message) : base(message)
    {
    }

    protected LocalizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public string AsUi => $"#UI_{GetType().Name}";
}