using Common.Common.Halpers;
using Common.Common.Models;
using Common.Domain.Data;
using Microsoft.Extensions.Logging;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Request;
using ToDoBackend.Application.Models.Dto.ToDoItemGroup.Response;
using ToDoBackend.Application.Models.Dto.ToDoItems.Response;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Infrastructure.Data;
using ToDoBackend.Infrastructure.Mappers;

namespace ToDoBackend.Infrastructure.Handlers;

public sealed class ToDoItemGroupHandler(
    IToDoItemEntityService toDoItemEntityService,
    IUserEntityService userEntityService,
    IToDoItemGroupEntityService entityService,
    ILogger<ToDoItemHandler> logger,
    IDbContextTransactionAction<ToDoDbContext> dbContextTransactionAction
) : IToDoItemGroupHandler
{
    public async Task<IResultDtoBase> CreateToDoItemGroups(CreateToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CreateToDoItemGroups request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            var entitiesToAdd = await ToDoItemGroupMapper.MapCrateToDoItemGroupsRequest(request, userEntityService,
                toDoItemEntityService, cancellationToken);

            await entityService.SaveAsync(entitiesToAdd, cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new CreateToDoItemsResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> UpdateToDoItemGroups(UpdateToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateToDoItems request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            var toUpdateIds = request.Items.Select(x => x.Id).Distinct().ToList();
            var entityToUpdate = await entityService.GetCollection(PageModel.All,
                query => query.IntersectBy(toUpdateIds, toDoItem => toDoItem.Id), true, cancellationToken);

            var targets = await ToDoItemGroupMapper.MapUpdateToDoItemGroupsRequest(request, entityToUpdate.entities,
                userEntityService, toDoItemEntityService, cancellationToken);

            await entityService.SaveAsync(targets, cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new UpdateToDoItemGroupsResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> DeleteToDoItemGroups(DeleteToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("DeleteToDoItems request");
        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            await entityService.BulkDelete(query => query.IntersectBy(request.Ids, toDoItem => toDoItem.Id),
                cancellationToken);

            await dbContextTransactionAction.CommitTransactionAsync(cancellationToken);

            return new DeleteToDoItemGroupsResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> GetToDoItemGroups(GetToDoItemGroupsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetToDoItems request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        var entities = await entityService.GetCollection(PageModel.All,
            query => query.IntersectBy(request.Ids, toDoItem => toDoItem.Id), true, cancellationToken);

        return new GetToDoItemGroupsResponse { Items = ToDoItemGroupMapper.MapGetToDoItemGroups(entities.entities) };
    }
}