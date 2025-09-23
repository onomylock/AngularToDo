using Common.Common.Models;

namespace Common.Common.Dto.Base;

public abstract record EntityBaseResultDto : IEntityBaseResultDto, IResultDtoBase
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public List<WarningModelResultEntry> Warnings { get; set; }
    public string TraceId { get; set; }
}