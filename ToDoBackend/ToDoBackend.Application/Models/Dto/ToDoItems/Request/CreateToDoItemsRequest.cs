using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.ToDoItems.Common;

namespace ToDoBackend.Application.Models.Dto.ToDoItems.Request;

public class CreateToDoItemsRequest : IRequestDtoBase
{
    public IEnumerable<ToDoItemDto> Items { get; set; }
}