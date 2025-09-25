using Common.Common.Converters;
using Common.Domain.Models.Base;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ToDoBackend.Domain.Entities;
using ToDoBackend.Infrastructure.SeedData;

namespace ToDoBackend.Infrastructure.Data;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
        // Do not migrate database on DbContext creation
        // Migrations must be done manually via dotnet-ef tool before launching any applications that might consume the context
        // Database.Migrate();

        ((SqliteConnection)Database.GetDbConnection()).DefaultTimeout = 300;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<ToDoItem> ToDoItems { get; set; }
    public DbSet<ToDoItemGroup> ToDoItemGroups { get; set; }
    public DbSet<UserToToDoItemGroupMapping> UserToToDoItemGroupMappings { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        /*
         * SQLite doesn't natively support the following data types: DateTimeOffset, Decimal, TimeSpan, UInt64
         */
        configurationBuilder
            .Properties<DateTime>()
            .HaveConversion<DateTimeToLongConverter>();

        configurationBuilder
            .Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetToLongConverter>();

        configurationBuilder
            .Properties<DateTime?>()
            .HaveConversion<NullableDateTimeToLongConverter>();

        configurationBuilder
            .Properties<DateTimeOffset?>()
            .HaveConversion<NullableDateTimeOffsetToLongConverter>();

        configurationBuilder.Properties<DateOnly>().HaveConversion<DateOnlyToLongConverter>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(x => x.HasData(DataSeeder.SeedUsers()));

        builder.Entity<ToDoItemGroup>(x =>
        {
            x.HasData(DataSeeder.SeedToDoItemGroups());
            x.HasMany(x => x.ToDoItems).WithOne(x => x.Group).HasForeignKey(x => x.GroupId);
        });

        builder.Entity<ToDoItem>(x => x.HasData(DataSeeder.SeedToDoItems()));

        builder.Entity<UserToToDoItemGroupMapping>(_ =>
        {
            _.HasData(DataSeeder.SeedUserToToDoItemGroupMappings());
            _.HasIndex(__ => new { __.EntityLeftId, __.EntityRightId }).IsUnique();
        });
    }

    #region Helpers

    /// <summary>
    ///     Used to automatically update CreatedAt, UpdatedAt fields
    /// </summary>
    private void UpdateTimestamps()
    {
        try
        {
            var entityEntries = ChangeTracker.Entries();

            foreach (var entityEntry in entityEntries.Where(_ => _.State is EntityState.Modified or EntityState.Added))
            {
                var dateTimeOffsetUtcNow = DateTimeOffset.UtcNow;

                if (entityEntry.State == EntityState.Added)
                    ((EntityBase)entityEntry.Entity).CreatedAt = dateTimeOffsetUtcNow;
                ((EntityBase)entityEntry.Entity).UpdatedAt = dateTimeOffsetUtcNow;
            }
        }
        catch (Exception)
        {
            // ignored
        }
    }

    #endregion

    /// <summary>
    ///     Saves all changes made in this context to the database
    /// </summary>
    /// <returns></returns>
    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    /// <summary>
    ///     Saves all changes made in this context to the database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }
}