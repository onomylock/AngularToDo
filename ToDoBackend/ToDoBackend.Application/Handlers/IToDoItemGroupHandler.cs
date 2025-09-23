using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Response;

namespace ToDoBackend.Application.Handlers;

public interface IToDoItemGroupHandler
{
    public Task<CreateToDoItemGroupsResponse> CreateToDoItemGroups(CreateToDoItemGroupsRequest request, CancellationToken cancellationToken = default);
    public Task<UpdateToDoItemGroupsResponse> UpdateToDoItemGroups(UpdateToDoItemGroupsRequest request, CancellationToken cancellationToken = default);
    public Task<DeleteToDoItemGroupsResponse> DeleteToDoItemGroups(DeleteToDoItemGroupsRequest request, CancellationToken cancellationToken = default);
}