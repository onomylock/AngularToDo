using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;

public class DeleteToDoItemGroupsRequest : IRequestDtoBase
{
    public IEnumerable<int> Ids { get; set; }
}