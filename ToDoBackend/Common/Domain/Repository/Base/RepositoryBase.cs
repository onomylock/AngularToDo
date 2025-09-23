using System.Data.Common;
using Common.Domain.Data;
using Common.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Common.Domain.Repository.Base;

public interface IRepositoryBase<TDbContext, TEntity> : IDbContextEntityAction<TDbContext>
    where TDbContext : DbContext
    where TEntity : EntityBase
{
    DbConnection DbConnection { get; }

    /// <summary>
    ///     Gets type of a generic used in implementation
    /// </summary>
    /// <returns></returns>
    Type GetEntityType();

    /// <summary>
    ///     Gets a table name from Database. Can be used in raw sql code
    /// </summary>
    /// <returns></returns>
    string GetTableName();

    /// <summary>
    ///     Creates or Updates an Entity
    /// </summary>
    /// <param name="entity"></param>
    void Save(TEntity entity);

    /// <summary>
    ///     Creates or Updates an Entity, afterwards commits changes
    /// </summary>
    /// <param name="entity"></param>
    void SaveAndCommit(TEntity entity);

    /// <summary>
    ///     Creates or Updates an Entity, afterwards commits changes
    /// </summary>
    /// <param name="entity"></param>
    Task SaveAndCommitAsync(TEntity entity);

    /// <summary>
    ///     Creates or Updates Entities
    /// </summary>
    /// <param name="entities"></param>
    void Save(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Creates or Updates Entities, afterwards commits changes
    /// </summary>
    /// <param name="entities"></param>
    void SaveAndCommit(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Creates or Updates Entities, afterwards commits changes
    /// </summary>
    /// <param name="entities"></param>
    Task SaveAndCommitAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Deletes an Entity
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);

    /// <summary>
    ///     Deletes an Entity, afterwards commits changes
    /// </summary>
    /// <param name="entity"></param>
    void DeleteAndCommit(TEntity entity);

    /// <summary>
    ///     Deletes an Entity, afterwards commits changes
    /// </summary>
    /// <param name="entity"></param>
    Task DeleteAndCommitAsync(TEntity entity);

    /// <summary>
    ///     Deletes Entities
    /// </summary>
    /// <param name="entities"></param>
    void Delete(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Deletes Entities, afterwards commits changes
    /// </summary>
    /// <param name="entities"></param>
    void DeleteAndCommit(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Deletes Entities, afterwards commits changes
    /// </summary>
    /// <param name="entities"></param>
    Task DeleteAndCommitAsync(IEnumerable<TEntity> entities);

    /// <summary>
    ///     Returns Entity queryable
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> Query(bool asNoTracking = false);

    /// <summary>
    ///     Returns Entity DbSet
    /// </summary>
    /// <returns></returns>
    DbSet<TEntity> DbSet();
}

public class RepositoryBase<TDbContext, TEntity> : IRepositoryBase<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : EntityBase
{
    private readonly DbContext _context;
    private readonly DbContextAction<TDbContext> _dbContextAction;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(TDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
        _dbContextAction = new DbContextAction<TDbContext>(context);
    }

    public void Save(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void SaveAndCommit(TEntity entity)
    {
        Save(entity);
        _dbContextAction.SaveChanges();
    }

    public async Task SaveAndCommitAsync(TEntity entity)
    {
        Save(entity);
        await _dbContextAction.SaveChangesAsync();
    }

    public void Save(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public void SaveAndCommit(IEnumerable<TEntity> entities)
    {
        Save(entities);
        _dbContextAction.SaveChanges();
    }

    public async Task SaveAndCommitAsync(IEnumerable<TEntity> entities)
    {
        Save(entities);
        await _dbContextAction.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteAndCommit(TEntity entity)
    {
        Delete(entity);
        _dbContextAction.SaveChanges();
    }

    public async Task DeleteAndCommitAsync(TEntity entity)
    {
        Delete(entity);
        await _dbContextAction.SaveChangesAsync();
    }

    public void Delete(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public void DeleteAndCommit(IEnumerable<TEntity> entities)
    {
        Delete(entities);
        _dbContextAction.SaveChanges();
    }

    public async Task DeleteAndCommitAsync(IEnumerable<TEntity> entities)
    {
        Delete(entities);
        await _dbContextAction.SaveChangesAsync();
    }

    public IQueryable<TEntity> Query(bool asNoTracking = false)
    {
        return asNoTracking ? _dbSet.AsNoTracking().OrderBy(_ => _.CreatedAt) : _dbSet.OrderBy(_ => _.CreatedAt);
    }

    public DbConnection DbConnection => _context.Database.GetDbConnection();

    public Type GetEntityType()
    {
        return typeof(TEntity);
    }

    public string GetTableName()
    {
        var model = _context.Model;
        var entityTypes = model.GetEntityTypes();
        var entityType = entityTypes.First(t => t.ClrType == typeof(TEntity));
        var tableNameAnnotation = entityType.GetAnnotation("Relational:TableName");
        return tableNameAnnotation.Value?.ToString();
    }

    public void SaveChanges()
    {
        _dbContextAction.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContextAction.SaveChangesAsync(cancellationToken);
    }

    public DbSet<TEntity> DbSet()
    {
        return _dbSet;
    }
}