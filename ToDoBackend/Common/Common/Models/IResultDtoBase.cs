namespace Common.Common.Models;

public interface IResultDtoBase : IWarningModelResult
{
    public string TraceId { get; set; }
}