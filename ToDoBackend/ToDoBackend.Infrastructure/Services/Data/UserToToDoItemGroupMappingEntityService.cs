using System.Linq.Expressions;
using Common.Common.Models;
using Common.Domain.Repository.Base;
using Common.Domain.Services;
using Microsoft.EntityFrameworkCore.Query;
using ToDoBackend.Application.Services.Data;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.Data;

namespace ToDoBackend.Infrastructure.Services.Data;

public sealed class UserToToDoItemGroupMappingEntityService(
    IRepositoryBase<ToDoDbContext, UserToToDoItemGroupMapping> entityRepository)
    : IUserToToDoItemGroupMappingEntityService
{
    public Task<UserToToDoItemGroupMapping> SaveAsync(UserToToDoItemGroupMapping entity,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, UserToToDoItemGroupMapping>.SaveAsync(entityRepository, entity,
            cancellationToken);
    }

    public Task DeleteAsync(UserToToDoItemGroupMapping entity, CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, UserToToDoItemGroupMapping>.DeleteAsync(entityRepository, entity,
            cancellationToken);
    }

    public Task<UserToToDoItemGroupMapping> GetByIdAsync(int id, bool asNoTracking = false,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, UserToToDoItemGroupMapping>.GetByIdAsync(entityRepository, id,
            asNoTracking,
            cancellationToken);
    }

    public Task<int> BulkUpdate(
        Func<IQueryable<UserToToDoItemGroupMapping>, IQueryable<UserToToDoItemGroupMapping>>
            queryTransformationFunction,
        Expression<Func<SetPropertyCalls<UserToToDoItemGroupMapping>, SetPropertyCalls<UserToToDoItemGroupMapping>>>
            setPropertyCalls,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceBase<ToDoDbContext, UserToToDoItemGroupMapping>.BulkUpdate(entityRepository,
            queryTransformationFunction,
            setPropertyCalls, cancellationToken);
    }

    public Task<int> BulkDelete(
        Func<IQueryable<UserToToDoItemGroupMapping>, IQueryable<UserToToDoItemGroupMapping>>
            queryTransformationFunction,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceBase<ToDoDbContext, UserToToDoItemGroupMapping>.BulkDelete(entityRepository,
            queryTransformationFunction,
            cancellationToken);
    }

    public Task<IReadOnlyCollection<UserToToDoItemGroupMapping>> SaveAsync(
        IEnumerable<UserToToDoItemGroupMapping> entities,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, UserToToDoItemGroupMapping>.SaveAsync(entityRepository,
            entities,
            cancellationToken);
    }

    public Task DeleteAsync(IEnumerable<UserToToDoItemGroupMapping> entities,
        CancellationToken cancellationToken = default)
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, UserToToDoItemGroupMapping>.DeleteAsync(entityRepository,
            entities,
            cancellationToken);
    }

    public Task<(int total, IReadOnlyCollection<UserToToDoItemGroupMapping> entities)> GetCollection(
        PageModel pageModel,
        Func<IQueryable<UserToToDoItemGroupMapping>, IQueryable<UserToToDoItemGroupMapping>> queryTransform,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityServiceCollectionBase<ToDoDbContext, UserToToDoItemGroupMapping>.GetCollection(entityRepository,
            pageModel,
            queryTransform, asNoTracking, cancellationToken);
    }

    public Task<UserToToDoItemGroupMapping> GetByEntityLeftIdEntityRightIdAsync(
        int entityLeftId,
        int entityRightId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityToEntityMappingServiceBase<ToDoDbContext, UserToToDoItemGroupMapping, User, ToDoItemGroup>
            .GetByEntityLeftIdEntityRightIdAsync(entityRepository,
                entityLeftId, entityRightId, asNoTracking, cancellationToken);
    }

    public Task<(int total, IReadOnlyCollection<UserToToDoItemGroupMapping> entities)> GetByEntityLeftIdAsync(
        int entityLeftId,
        PageModel pageModel,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityToEntityMappingServiceBase<ToDoDbContext, UserToToDoItemGroupMapping, User, ToDoItemGroup>
            .GetByEntityLeftIdAsync(entityRepository, entityLeftId,
                pageModel, asNoTracking, cancellationToken);
    }

    public Task<(int total, IReadOnlyCollection<UserToToDoItemGroupMapping> entities)> GetByEntityRightIdAsync(
        int entityRightId,
        PageModel pageModel,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return DbEntityToEntityMappingServiceBase<ToDoDbContext, UserToToDoItemGroupMapping, User, ToDoItemGroup>
            .GetByEntityRightIdAsync(entityRepository, entityRightId,
                pageModel, asNoTracking, cancellationToken);
    }
}