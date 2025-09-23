using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.ToDoItemGroup.Response;

public class UpdateToDoItemGroupsResponse : IResultDtoBase
{
    public List<WarningModelResultEntry> Warnings { get; set; }
    public string TraceId { get; set; }
}