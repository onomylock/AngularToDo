using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.ToDoItems.Common;

namespace ToDoBackend.Application.Models.Dto.ToDoItems.Response;

public class GetToDoItemsResponse : PageModelResult<ToDoItemDto>;