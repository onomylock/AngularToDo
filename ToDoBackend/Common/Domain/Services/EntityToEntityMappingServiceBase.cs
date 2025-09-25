using Common.Common.Models;
using Common.Domain.Extensions;
using Common.Domain.Models.Base;
using Common.Domain.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace Common.Domain.Services;

/// <summary>
///     A base service every mapping entity (Database Entity, Remote Entity, ...) must inherit. This is commonly used in
///     Many-to-Many Database schemes
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IEntityToEntityMappingServiceBase<TEntity> : IEntityServiceBase<TEntity> where TEntity : EntityBase
{
    /// <summary>
    ///     Gets an Entity by composite key
    /// </summary>
    /// <param name="entityLeftId"></param>
    /// <param name="entityRightId"></param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> GetByEntityLeftIdEntityRightIdAsync(
        int entityLeftId,
        int entityRightId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Gets an Entity by a part of composite key
    /// </summary>
    /// <param name="entityLeftId"></param>
    /// <param name="pageModel">Pagination model</param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<(int total, IReadOnlyCollection<TEntity> entities)> GetByEntityLeftIdAsync(
        int entityLeftId,
        PageModel pageModel,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    ///     Gets an Entity by a part of composite key
    /// </summary>
    /// <param name="entityRightId"></param>
    /// <param name="pageModel">Pagination model</param>
    /// <param name="asNoTracking"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<(int total, IReadOnlyCollection<TEntity> entities)> GetByEntityRightIdAsync(
        int entityRightId,
        PageModel pageModel,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
///     A common implementation for Database Entity. Call methods of this class inside implementations of
///     IEntityToEntityMappingServiceBase
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TEntityLeft"></typeparam>
/// <typeparam name="TEntityRight"></typeparam>
/// <typeparam name="TBase"></typeparam>
public static class DbEntityToEntityMappingServiceBase<TBase, TEntity, TEntityLeft, TEntityRight>
    where TBase : DbContext
    where TEntity : EntityToEntityMappingBase<TEntityLeft, TEntityRight>
    where TEntityLeft : EntityBase
    where TEntityRight : EntityBase
{
    public static Task<TEntity> GetByEntityLeftIdEntityRightIdAsync(
        IRepositoryBase<TBase, TEntity> repository,
        int entityLeftId,
        int entityRightId,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        return repository
            .Query(asNoTracking)
            .SingleOrDefaultAsync(_ => _.EntityLeftId == entityLeftId && _.EntityRightId == entityRightId,
                cancellationToken);
    }

    public static async Task<(int total, IReadOnlyCollection<TEntity> entities)> GetByEntityLeftIdAsync(
        IRepositoryBase<TBase, TEntity> repository,
        int entityLeftId,
        PageModel pageModel,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = repository
            .Query(asNoTracking)
            .Where(_ => _.EntityLeftId == entityLeftId)
            .OrderBy(_ => _.CreatedAt);

        var total = await query.CountAsync(cancellationToken);

        var result = query.GetPage(pageModel);

        return (total, await result.ToArrayAsync(cancellationToken)!);
    }

    public static async Task<(int total, IReadOnlyCollection<TEntity> entities)> GetByEntityRightIdAsync(
        IRepositoryBase<TBase, TEntity> repository,
        int entityRightId,
        PageModel pageModel,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    )
    {
        var query = repository
            .Query(asNoTracking)
            .Where(_ => _.EntityRightId == entityRightId)
            .OrderBy(_ => _.CreatedAt);

        var total = await query.CountAsync(cancellationToken);

        var result = query.GetPage(pageModel);

        return (total, await result.ToArrayAsync(cancellationToken)!);
    }
}