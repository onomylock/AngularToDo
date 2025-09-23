using Common.Common.Models;

namespace Common.Common.Dto.Generic;

public class FileReadResultDto : IResultDtoBase
{
    public Stream Stream { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
    public List<WarningModelResultEntry> Warnings { get; set; }

    public string TraceId { get; set; }
}