using Common.Common.Halpers;
using Common.Common.Models;
using Common.Domain.Data;
using Microsoft.Extensions.Logging;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.User.Request;
using ToDoBackend.Application.Models.Dto.User.Response;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Infrastructure.Data;
using ToDoBackend.Infrastructure.Mappers;

namespace ToDoBackend.Infrastructure.Handlers;

public sealed class UserHandler(
    IUserEntityService entityService,
    IToDoItemGroupEntityService groupEntityService,
    IUserToToDoItemGroupMappingEntityService userToToDoItemGroupMappingEntityService,
    ILogger<ToDoItemHandler> logger,
    IDbContextTransactionAction<ToDoDbContext> dbContextTransactionAction
) : IUserHandler
{
    public async Task<IResultDtoBase> CreateUsers(CreateUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CreateUsersRequest request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            var entitiesToAdd = UserMapper.MapCrateUsersRequest(request);

            await entityService.SaveAsync(entitiesToAdd, cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new CreateUsersResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> UpdateUsers(UpdateUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateUsersRequest request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            var toUpdateIds = request.Items.Select(x => x.Id).Distinct().ToList();
            var entityToUpdate = await entityService.GetCollection(PageModel.All,
                query => query.Where(x => toUpdateIds.Contains(x.Id)), true, cancellationToken);

            var targets = UserMapper.MapUpdateUsersRequest(request, entityToUpdate.entities, groupEntityService,
                cancellationToken);

            await entityService.SaveAsync(targets, cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new UpdateUsersResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> DeleteUsers(DeleteUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("DeleteUsersRequest request");
        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            await entityService.BulkDelete(query => query.Where(x => request.Ids.Contains(x.Id)),
                cancellationToken);

            await userToToDoItemGroupMappingEntityService.BulkDelete(
                query => query.Where(x => request.Ids.Contains(x.EntityLeftId)), cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new DeleteUsersResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> GetUsers(GetUsersRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetToDoItems request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        var entities = await entityService.GetCollection(PageModel.All,
            query => query.Where(x => request.Ids.Contains(x.Id)), true, cancellationToken);

        var userDict = entities.entities
            .Select(async x => (x.Id,
                await userToToDoItemGroupMappingEntityService.GetByEntityLeftIdAsync(x.Id, PageModel.All, true,
                    cancellationToken))).ToDictionary(x => x.Result.Id,
                x => x.Result.Item2.entities.Select(x => x.EntityRightId));

        return new GetUsersResponse { Items = UserMapper.MapGetUsers(entities.entities, userDict) };
    }

    public async Task<IResultDtoBase> LinkUserToDoItemGroup(LinkUserToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("LinkUserToDoItemGroupsRequest request");
        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            var targetUser = await entityService.GetByIdAsync(request.TargetUserId, true, cancellationToken);
            var targetToDoItemGroups = await groupEntityService.GetCollection(PageModel.All,
                query => query.Where(x => request.ToDoItemGroupIds.Contains(x.Id)), true, cancellationToken);

            await userToToDoItemGroupMappingEntityService.SaveAsync(
                UserMapper.MapUserToToDoItemGroupMapping(targetUser, targetToDoItemGroups.entities), cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new LinkUserToDoItemGroupsResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public Task<IResultDtoBase> UnlinkUserToDoItemGroup(UnlinkUserToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}