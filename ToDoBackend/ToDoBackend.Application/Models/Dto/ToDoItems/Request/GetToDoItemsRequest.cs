using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.ToDoItems.Request;

public class GetToDoItemsRequest : IRequestDtoBase
{
    public IEnumerable<int> Ids { get; set; }
}