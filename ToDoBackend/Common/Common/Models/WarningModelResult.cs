using Common.Common.Enums;

namespace Common.Common.Models;

public sealed class WarningModelResultEntry(
    WarningType warningType,
    string message,
    WarningEntryType warningEntryType = WarningEntryType.None
)
{
    public WarningType WarningType { get; } = warningType;
    public string Message { get; } = message;
    public WarningEntryType WarningEntryType { get; } = warningEntryType;
}

public interface IWarningModelResult
{
    public List<WarningModelResultEntry> Warnings { get; set; }
}