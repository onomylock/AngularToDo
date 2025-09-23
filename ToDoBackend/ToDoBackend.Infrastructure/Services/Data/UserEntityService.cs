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

public class UserEntityService(IRepositoryBase<ToDoDbContext, User> entityRepository) : IUserEntityService
{
    public Task<User> SaveAsync(User entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, User>.SaveAsync(entityRepository, entity, cancellationToken);
    }

    public Task DeleteAsync(User entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, User>.DeleteAsync(entityRepository, entity, cancellationToken);
    }

    public Task<User> GetByIdAsync(int id, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, User>.GetByIdAsync(entityRepository, id, asNoTracking,
            cancellationToken);
    }

    public Task<int> BulkUpdate(
        Func<IQueryable<User>, IQueryable<User>> queryTransformationFunction,
        Expression<Func<SetPropertyCalls<User>, SetPropertyCalls<User>>> setPropertyCalls,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceBase<ToDoDbContext, User>.BulkUpdate(entityRepository, queryTransformationFunction,
            setPropertyCalls, cancellationToken);
    }

    public Task<int> BulkDelete(Func<IQueryable<User>, IQueryable<User>> queryTransformationFunction,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, User>.BulkDelete(entityRepository, queryTransformationFunction,
            cancellationToken);
    }

    public Task<IReadOnlyCollection<User>> SaveAsync(IEnumerable<User> entities,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, User>.SaveAsync(entityRepository, entities,
            cancellationToken);
    }

    public Task DeleteAsync(IEnumerable<User> entities, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, User>.DeleteAsync(entityRepository, entities,
            cancellationToken);
    }

    public Task<(int total, IReadOnlyCollection<User> entities)> GetCollection(
        PageModel pageModel,
        Func<IQueryable<User>, IQueryable<User>> queryTransform,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, User>.GetCollection(entityRepository, pageModel,
            queryTransform, asNoTracking, cancellationToken);
    }
}