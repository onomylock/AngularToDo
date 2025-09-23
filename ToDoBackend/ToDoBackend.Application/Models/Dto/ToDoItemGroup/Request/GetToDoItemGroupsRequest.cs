using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;

public class GetToDoItemGroupsRequest : IRequestDtoBase
{
    public IEnumerable<int> Ids { get; set; }
}