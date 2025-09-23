using System.Linq.Expressions;
using Common.Common.Models;
using Common.Domain.Repository.Base;
using Common.Domain.Services;
using Microsoft.EntityFrameworkCore.Query;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Data;

namespace ToDoBackend.Infrastructure.Services.Data;

public class ToDoItemGroupEntityService(IRepositoryBase<ToDoDbContext, ToDoItemGroup> entityRepository) : IToDoItemGroupEntityService
{
    public Task<ToDoItemGroup> SaveAsync(ToDoItemGroup entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItemGroup>.SaveAsync(entityRepository, entity, cancellationToken);
    }

    public Task DeleteAsync(ToDoItemGroup entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItemGroup>.DeleteAsync(entityRepository, entity, cancellationToken);
    }

    public Task<ToDoItemGroup> GetByIdAsync(int id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItemGroup>.GetByIdAsync(entityRepository, id, asNoTracking,
            cancellationToken);
    }

    public Task<int> BulkUpdate(
        Func<IQueryable<ToDoItemGroup>, IQueryable<ToDoItemGroup>> queryTransformationFunction,
        Expression<Func<SetPropertyCalls<ToDoItemGroup>, SetPropertyCalls<ToDoItemGroup>>> setPropertyCalls,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItemGroup>.BulkUpdate(entityRepository, queryTransformationFunction,
            setPropertyCalls, cancellationToken);
    }

    public Task<int> BulkDelete(Func<IQueryable<ToDoItemGroup>, IQueryable<ToDoItemGroup>> queryTransformationFunction,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, ToDoItemGroup>.BulkDelete(entityRepository, queryTransformationFunction,
            cancellationToken);
    }

    public Task<IReadOnlyCollection<ToDoItemGroup>> SaveAsync(IEnumerable<ToDoItemGroup> entities,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, ToDoItemGroup>.SaveAsync(entityRepository, entities,
            cancellationToken);
    }

    public Task DeleteAsync(IEnumerable<ToDoItemGroup> entities, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, ToDoItemGroup>.DeleteAsync(entityRepository, entities,
            cancellationToken);
    }

    public Task<(int total, IReadOnlyCollection<ToDoItemGroup> entities)> GetCollection(
        PageModel pageModel,
        Func<IQueryable<ToDoItemGroup>, IQueryable<ToDoItemGroup>> queryTransform,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, ToDoItemGroup>.GetCollection(entityRepository, pageModel,
            queryTransform, asNoTracking, cancellationToken);
    }
}