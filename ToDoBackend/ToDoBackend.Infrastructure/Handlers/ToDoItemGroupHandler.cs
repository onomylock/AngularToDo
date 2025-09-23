using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Response;

namespace ToDoBackend.Infrastructure.Handlers;

public sealed class ToDoItemGroupHandler : IToDoItemGroupHandler
{
    public Task<CreateToDoItemGroupsResponse> CreateToDoItemGroups(CreateToDoItemGroupsRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UpdateToDoItemGroupsResponse> UpdateToDoItemGroups(UpdateToDoItemGroupsRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<DeleteToDoItemGroupsResponse> DeleteToDoItemGroups(DeleteToDoItemGroupsRequest request, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}