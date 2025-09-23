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

            var entitiesToAdd = await UserMapper.MapCrateUsersRequest(request,
                groupEntityService, cancellationToken);

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
                query => query.IntersectBy(toUpdateIds, toDoItem => toDoItem.Id), true, cancellationToken);

            var targets = await UserMapper.MapUpdateUsersRequest(request, entityToUpdate.entities, groupEntityService,
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

            await entityService.BulkDelete(query => query.IntersectBy(request.Ids, toDoItem => toDoItem.Id),
                cancellationToken);

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
            query => query.IntersectBy(request.Ids, user => user.Id), true, cancellationToken);

        return new GetUsersResponse { Items = UserMapper.MapGetUsers(entities.entities) };
    }
}