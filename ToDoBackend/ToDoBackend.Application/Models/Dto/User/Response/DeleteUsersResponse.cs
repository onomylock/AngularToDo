using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.User.Response;

public class DeleteUsersResponse : IResultDtoBase
{
    public List<WarningModelResultEntry> Warnings { get; set; }
    public string TraceId { get; set; }
}