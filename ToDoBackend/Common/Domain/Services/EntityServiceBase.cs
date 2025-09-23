using System.Linq.Expressions;
using Common.Domain.Extensions;
using Common.Domain.Models.Base;
using Common.Domain.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Common.Domain.Services;

/// <summary>
///     A base service every entity (Database Entity, Remote Entity, ...) must inherit
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityServiceBase<TEntity> where TEntity : EntityBase
{
    /// <summary>
    ///     Creates or Updates an Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes an Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets and Entity by its key
    /// </summary>
    /// <param name="id"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(int id, bool asNoTracking = false, CancellationToken cancellationToken = default);

    Task<int> BulkUpdate(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryTransformationFunction,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        CancellationToken cancellationToken = default
    );

    Task<int> BulkDelete(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryTransformationFunction,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
///     A common implementation for Database Entity. Call methods of this class inside implementations of
///     IEntityServiceBase
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TBase"></typeparam>
public static class DbEntityServiceBase<TBase, TEntity>
    where TBase : DbContext
    where TEntity : EntityBase
{
    /// <summary>
    ///     Creates or Updates an Entity
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<TEntity> SaveAsync(
        IRepositoryBase<TBase, TEntity> repository,
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        repository.Save(entity);
        await repository.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    ///     Deletes an Entity
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task DeleteAsync(
        IRepositoryBase<TBase, TEntity> repository,
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        repository.Delete(entity);
        return repository.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    ///     Gets and Entity by its key
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="id"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task<TEntity> GetByIdAsync(
        IRepositoryBase<TBase, TEntity> repository,
        int id,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return repository
            .Query(asNoTracking)
            .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
    }

    public static Task<int> BulkUpdate(
        IRepositoryBase<TBase, TEntity> repository,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryTransformationFunction,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls,
        CancellationToken cancellationToken = default
    )
    {
        return repository.Query().Transform(queryTransformationFunction)
            .ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
    }

    public static Task<int> BulkDelete(
        IRepositoryBase<TBase, TEntity> repository,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryTransformationFunction,
        CancellationToken cancellationToken = default
    )
    {
        return repository.Query().Transform(queryTransformationFunction).ExecuteDeleteAsync(cancellationToken);
    }
}