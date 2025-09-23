using Common.Common.Models;

namespace Common.Common.Dto.Generic;

public sealed class RedirectResultDto : IResultDtoBase
{
    public string Url { get; set; }
    public bool IsPermanent { get; set; }
    public bool PreserveMethod { get; set; }
    public List<WarningModelResultEntry> Warnings { get; set; }

    public string TraceId { get; set; }
}