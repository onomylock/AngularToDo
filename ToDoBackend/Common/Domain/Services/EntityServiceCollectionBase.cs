using Common.Common.Models;
using Common.Domain.Extensions;
using Common.Domain.Models.Base;
using Common.Domain.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Common.Domain.Services;

/// <summary>
///     A base service some entities (Database Entity, Remote Entity, ...) might implement
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityServiceCollectionBase<TEntity> where TEntity : EntityBase
{
    /// <summary>
    ///     Creates or Saves Entities
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyCollection<TEntity>> SaveAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Deletes Entities
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets a collection of Entities
    /// </summary>
    /// <param name="pageModel">Pagination model</param>
    /// <param name="queryTransform">Transforms query with custom logic</param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<(int total, IReadOnlyCollection<TEntity> entities)> GetCollection(
        PageModel pageModel,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryTransform,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
///     A common implementation for Database Entity. Call methods of this class inside implementations of
///     IEntityServiceCollectionBase
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TDbContext"></typeparam>
public static class DbEntityServiceCollectionBase<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : EntityBase
{
    /// <summary>
    ///     Creates or Saves Entities
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<IReadOnlyCollection<TEntity>> SaveAsync(
        IRepositoryBase<TDbContext, TEntity> repository,
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        var entitiesEnumerated = entities as TEntity[] ?? entities.ToArray();

        repository.Save(entitiesEnumerated);
        await repository.SaveChangesAsync(cancellationToken);

        return entitiesEnumerated;
    }

    /// <summary>
    ///     Deletes Entities
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task DeleteAsync(
        IRepositoryBase<TDbContext, TEntity> repository,
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
    )
    {
        var entitiesEnumerated = entities as TEntity[] ?? entities.ToArray();

        repository.Delete(entitiesEnumerated);
        return repository.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    ///     Gets a collection of Entities
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="pageModel">Pagination model</param>
    /// <param name="queryTransform">Transforms query with custom logic</param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task<(int total, IReadOnlyCollection<TEntity> entities)> GetCollection(
        IRepositoryBase<TDbContext, TEntity> repository,
        PageModel pageModel,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> queryTransform,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        var queryTransformed = repository
            .Query(asNoTracking)
            .Transform(queryTransform);

        var (total, result) = await queryTransformed.GetPage(pageModel, cancellationToken);

        return (total, await result!.ToArrayAsync(cancellationToken));
    }
}