using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.ToDoItems.Response;

public class DeleteToDoItemsResponse : IResultDtoBase
{
    public List<WarningModelResultEntry> Warnings { get; set; }
    public string TraceId { get; set; }
}