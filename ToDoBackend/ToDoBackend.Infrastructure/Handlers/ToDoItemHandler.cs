using Common.Common.Halpers;
using Common.Common.Models;
using Common.Domain.Data;
using Microsoft.Extensions.Logging;
using ToDoBackend.Application.Handlers;
using ToDoBackend.Application.Models.Dto.ToDoItems.Request;
using ToDoBackend.Application.Models.Dto.ToDoItems.Response;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Infrastructure.Data;
using ToDoBackend.Infrastructure.Mappers;

namespace ToDoBackend.Infrastructure.Handlers;

public sealed class ToDoItemHandler(
    IToDoItemEntityService entityService,
    IUserEntityService userEntityService,
    IToDoItemGroupEntityService groupEntityService,
    ILogger<ToDoItemHandler> logger,
    IDbContextTransactionAction<ToDoDbContext> dbContextTransactionAction
) : IToDoItemHandler
{
    public async Task<IResultDtoBase> CreateToDoItems(CreateToDoItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CreateToDoItems request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        try
        {
            await dbContextTransactionAction.BeginTransactionAsync(cancellationToken);

            var entitiesToAdd = await ToDoItemMapper.MapCrateToDoItemsRequest(request, userEntityService,
                groupEntityService, cancellationToken);

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

    public async Task<IResultDtoBase> UpdateToDoItems(UpdateToDoItemsRequest request,
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

            var targets = await ToDoItemMapper.MapUpdateToDoItemsRequest(request, entityToUpdate.entities,
                userEntityService, groupEntityService, cancellationToken);

            await entityService.SaveAsync(targets, cancellationToken);

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

    public async Task<IResultDtoBase> DeleteToDoItems(DeleteToDoItemsRequest request,
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

            return new DeleteToDoItemsResponse();
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            await dbContextTransactionAction.RollbackTransactionAsync(CancellationToken.None);
            throw;
        }
    }

    public async Task<IResultDtoBase> GetToDoItems(GetToDoItemsRequest request,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetToDoItems request");

        if (request.Validate() is { } validationResult)
            return validationResult;

        var entities = await entityService.GetCollection(PageModel.All,
            query => query.IntersectBy(request.Ids, toDoItem => toDoItem.Id), true, cancellationToken);

        return new GetToDoItemsResponse { Items = ToDoItemMapper.MapToDoItem(entities.entities) };
    }
}