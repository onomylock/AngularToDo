using Common.Common.Enums;

namespace Common.Common.Models;

public sealed class ErrorModelResultEntry(
    ErrorType errorType,
    string message,
    ErrorEntryType errorEntryType = ErrorEntryType.None
)
{
    public ErrorType ErrorType { get; } = errorType;
    public string Message { get; } = message;
    public ErrorEntryType ErrorEntryType { get; } = errorEntryType;
}

public interface IErrorModelResult
{
    public List<ErrorModelResultEntry> Errors { get; set; }
}

public sealed class ErrorModelResult : IResultDtoBase, IErrorModelResult
{
    public List<ErrorModelResultEntry> Errors { get; set; } = [];
    public List<WarningModelResultEntry> Warnings { get; set; } = [];
    public string TraceId { get; set; }

    public override string ToString()
    {
        return $"""
                Errors:
                {Errors.Aggregate(string.Empty, (current, validationResult) => current + $"{validationResult.Message}{Environment.NewLine}")}

                Warnings: 
                {Warnings.Aggregate(string.Empty, (current, validationResult) => current + $"{validationResult.Message}{Environment.NewLine}")}
                """;
    }
}