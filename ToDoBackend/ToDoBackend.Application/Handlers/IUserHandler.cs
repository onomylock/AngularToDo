using Common.Common.Models;
using ToDoBackend.Application.Models.Dto.User.Request;

namespace ToDoBackend.Application.Handlers;

public interface IUserHandler
{
    public Task<IResultDtoBase> CreateUsers(CreateUsersRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> UpdateUsers(UpdateUsersRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> DeleteUsers(DeleteUsersRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> GetUsers(GetUsersRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> LinkUserToDoItemGroup(LinkUserToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default);

    public Task<IResultDtoBase> UnlinkUserToDoItemGroup(UnlinkUserToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default);
}