using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.User.Response;

public class UpdateUsersResponse : IResultDtoBase
{
    public List<WarningModelResultEntry> Warnings { get; set; }
    public string TraceId { get; set; }
}