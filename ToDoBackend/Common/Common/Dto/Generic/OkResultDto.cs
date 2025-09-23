using Common.Common.Models;

namespace Common.Common.Dto.Generic;

public sealed class OkResultDto : IResultDtoBase
{
    public List<WarningModelResultEntry> Warnings { get; set; }

    public string TraceId { get; set; }
}