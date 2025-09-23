using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;

namespace ToDoBackend.Application.Handlers;

public interface IToDoItemGroupHandler
{
    public Task<IResultDtoBase> CreateToDoItemGroups(CreateToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> UpdateToDoItemGroups(UpdateToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> DeleteToDoItemGroups(DeleteToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> GetToDoItemGroups(GetToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default);
}