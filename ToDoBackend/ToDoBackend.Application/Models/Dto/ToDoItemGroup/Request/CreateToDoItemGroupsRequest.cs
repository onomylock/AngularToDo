using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Common;

namespace ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;

public class CreateToDoItemGroupsRequest : IRequestDtoBase
{
    public IEnumerable<ToDoItemGroupDto> Items { get; set; }
}