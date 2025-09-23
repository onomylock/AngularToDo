using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.ToDoItems.Request;

namespace ToDoBackend.Application.Handlers;

public interface IToDoItemHandler
{
    public Task<IResultDtoBase> CreateToDoItems(CreateToDoItemsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> UpdateToDoItems(UpdateToDoItemsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> DeleteToDoItems(DeleteToDoItemsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> GetToDoItems(GetToDoItemsRequest request,
        CancellationToken cancellationToken = default);
}