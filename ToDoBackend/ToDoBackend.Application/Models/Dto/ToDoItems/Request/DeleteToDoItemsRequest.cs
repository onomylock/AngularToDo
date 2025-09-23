using Common.Common.Models;

namespace ToDoBackend.Application.Models.Dto.ToDoItems.Request;

public class DeleteToDoItemsRequest : IRequestDtoBase
{
    public IEnumerable<int> Ids { get; set; }
}