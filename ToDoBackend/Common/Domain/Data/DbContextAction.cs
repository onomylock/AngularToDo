using System.Data;
using Common.Common.Exceptions;
using Common.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Domain.Data;

public interface IDbContextTransactionAction<TDbContext> where TDbContext : DbContext
{
    /// <summary>
    ///     Starts a new transaction
    /// </summary>
    /// <param name="shouldThrow"></param>
    void BeginTransaction(bool shouldThrow = false);

    /// <summary>
    ///     Starts a new transaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="shouldThrow"></param>
    void BeginTransaction(IsolationLevel isolationLevel, bool shouldThrow = false);

    /// <summary>
    ///     Asynchronously starts a new transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default, bool shouldThrow = false);

    /// <summary>
    ///     Asynchronously starts a new transaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    Task BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default,
        bool shouldThrow = false
    );

    /// <summary>
    ///     Applies the outstanding operations in the current transaction to the database
    /// </summary>
    /// <param name="shouldThrow"></param>
    void CommitTransaction(bool shouldThrow = false);

    /// <summary>
    ///     Applies the outstanding operations in the current transaction to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default, bool shouldThrow = false);

    /// <summary>
    ///     Discards the outstanding operations in the current transaction
    /// </summary>
    /// <param name="shouldThrow"></param>
    void RollbackTransaction(bool shouldThrow = false);

    /// <summary>
    ///     Discards the outstanding operations in the current transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default, bool shouldThrow = false);
}

public interface IDbContextEntityAction<TDbContext> where TDbContext : DbContext
{
    /// <summary>
    ///     Saves all changes made in this context to the database
    /// </summary>
    void SaveChanges();

    /// <summary>
    ///     Saves all changes made in this context to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

/// <summary>
///     Use of this service through DI requires you to have ServiceLifetime.Scoped DbContext.
///     This way you are able to achieve single transaction control over multiple repository-pattern DbContexts.
///     All repositories implement interfaces to control their own DbContext transaction.
///     If the passed DbContext in repository is created via ServiceLifetime.Transient or via DbContext Factory
/// </summary>
public sealed class DbContextAction<TDbContext>(TDbContext appDbContext) : IDbContextTransactionAction<TDbContext>,
    IDbContextEntityAction<TDbContext>, IDisposable
    where TDbContext : DbContext
{
    private bool TransactionInProgress { get; set; }

    /// <summary>
    ///     Saves all changes made in this context to the database
    /// </summary>
    public void SaveChanges()
    {
        appDbContext.SaveChanges();
    }

    /// <summary>
    ///     Saves all changes made in this context to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await appDbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    ///     Starts a new transaction
    /// </summary>
    /// <param name="shouldThrow"></param>
    public void BeginTransaction(bool shouldThrow = false)
    {
        if (TransactionInProgress)
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextAnotherTransactionInProgress);
        }
        else
        {
            appDbContext.Database.BeginTransaction();
            TransactionInProgress = true;
        }
    }

    /// <summary>
    ///     Starts a new transaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="shouldThrow"></param>
    public void BeginTransaction(IsolationLevel isolationLevel, bool shouldThrow = false)
    {
        if (TransactionInProgress)
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextAnotherTransactionInProgress);
        }
        else
        {
            appDbContext.Database.BeginTransaction(isolationLevel);
            TransactionInProgress = true;
        }
    }

    /// <summary>
    ///     Asynchronously starts a new transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default, bool shouldThrow = false)
    {
        if (TransactionInProgress)
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextAnotherTransactionInProgress);
        }
        else
        {
            await appDbContext.Database.BeginTransactionAsync(cancellationToken);
            TransactionInProgress = true;
        }
    }

    /// <summary>
    ///     Asynchronously starts a new transaction
    /// </summary>
    /// <param name="isolationLevel"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    public async Task BeginTransactionAsync(
        IsolationLevel isolationLevel,
        CancellationToken cancellationToken = default,
        bool shouldThrow = false
    )
    {
        if (TransactionInProgress)
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextAnotherTransactionInProgress);
        }
        else
        {
            await appDbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            TransactionInProgress = true;
        }
    }

    /// <summary>
    ///     Applies the outstanding operations in the current transaction to the database
    /// </summary>
    /// <param name="shouldThrow"></param>
    public void CommitTransaction(bool shouldThrow = false)
    {
        if (!TransactionInProgress)
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextNoTransactionInProgress);
        }
        else
        {
            appDbContext.Database.CommitTransaction();
            TransactionInProgress = false;
        }
    }

    /// <summary>
    ///     Applies the outstanding operations in the current transaction to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default, bool shouldThrow = false)
    {
        if (!TransactionInProgress)
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextNoTransactionInProgress);
        }
        else
        {
            await appDbContext.Database.CommitTransactionAsync(cancellationToken);
            TransactionInProgress = false;
        }
    }

    /// <summary>
    ///     Discards the outstanding operations in the current transaction
    /// </summary>
    /// <param name="shouldThrow"></param>
    public void RollbackTransaction(bool shouldThrow = false)
    {
        if (TransactionInProgress)
        {
            appDbContext.Database.RollbackTransaction();
        }
        else
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextNoTransactionInProgress);
        }
    }

    /// <summary>
    ///     Discards the outstanding operations in the current transaction
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="shouldThrow"></param>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default, bool shouldThrow = false)
    {
        if (TransactionInProgress)
        {
            await appDbContext.Database.RollbackTransactionAsync(cancellationToken);
        }
        else
        {
            if (shouldThrow)
                throw new OperationFailedException(Localize.Keys.Error.DbContextNoTransactionInProgress);
        }
    }

    /// <summary>
    ///     Releases the allocated resources for this context
    /// </summary>
    public void Dispose()
    {
        appDbContext?.Dispose();
    }
}