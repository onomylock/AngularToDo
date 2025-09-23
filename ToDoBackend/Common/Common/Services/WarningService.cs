using Common.Common.Models;

namespace Common.Common.Services;

public interface IWarningService
{
    void Add(WarningModelResultEntry warningModelResultEntry);
    List<WarningModelResultEntry> GetAll();
}

public sealed class WarningService : IWarningService
{
    private readonly List<WarningModelResultEntry> _warningModelResultEntries = [];

    public void Add(WarningModelResultEntry warningModelResultEntry)
    {
        _warningModelResultEntries.Add(warningModelResultEntry);
    }

    public List<WarningModelResultEntry> GetAll()
    {
        return _warningModelResultEntries;
    }
}