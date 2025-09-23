using System.Linq.Expressions;
using Common.Common.Models;
using Common.Domain.Repository.Base;
using Common.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Data;

namespace ToDoBackend.Infrastructure.Services.Data;

public class ToDoItemEntityService(IRepositoryBase<ToDoDbContext, ToDoItem> entityRepository) : IToDoItemEntityService
{
    public Task<ToDoItem> SaveAsync(ToDoItem entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItem>.SaveAsync(entityRepository, entity, cancellationToken);
    }

    public Task DeleteAsync(ToDoItem entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItem>.DeleteAsync(entityRepository, entity, cancellationToken);
    }

    public Task<ToDoItem> GetByIdAsync(int id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItem>.GetByIdAsync(entityRepository, id, asNoTracking,
            cancellationToken);
    }

    public Task<int> BulkUpdate(
        Func<IQueryable<ToDoItem>, IQueryable<ToDoItem>> queryTransformationFunction,
        Expression<Func<SetPropertyCalls<ToDoItem>, SetPropertyCalls<ToDoItem>>> setPropertyCalls,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItem>.BulkUpdate(entityRepository, queryTransformationFunction,
            setPropertyCalls, cancellationToken);
    }

    public Task<int> BulkDelete(Func<IQueryable<ToDoItem>, IQueryable<ToDoItem>> queryTransformationFunction,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItem>.BulkDelete(entityRepository, queryTransformationFunction,
            cancellationToken);
    }

    public Task<IReadOnlyCollection<ToDoItem>> SaveAsync(IEnumerable<ToDoItem> entities,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, ToDoItem>.SaveAsync(entityRepository, entities,
            cancellationToken);
    }

    public Task DeleteAsync(IEnumerable<ToDoItem> entities, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, ToDoItem>.DeleteAsync(entityRepository, entities,
            cancellationToken);
    }

    public Task<(int total, IReadOnlyCollection<ToDoItem> entities)> GetCollection(
        PageModel pageModel,
        Func<IQueryable<ToDoItem>, IQueryable<ToDoItem>> queryTransform,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, ToDoItem>.GetCollection(entityRepository, pageModel,
            queryTransform, asNoTracking, cancellationToken);
    }
}