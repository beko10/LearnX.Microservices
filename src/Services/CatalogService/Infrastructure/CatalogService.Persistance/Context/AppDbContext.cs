using CatalogService.Domain.Entities;
using CatalogService.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CatalogService.Persistance.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }

    public static AppDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,database.DatabaseNamespace.DatabaseName);

         return new AppDbContext(optionsBuilder.Options);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
{
    var entries = ChangeTracker.Entries<BaseEntity>();
    
    foreach (var entry in entries)
    {
        if (entry.State == EntityState.Added)
        {
            if (string.IsNullOrEmpty(entry.Entity.Id))
            {
                entry.Entity.Id = ObjectId.GenerateNewId().ToString();
            }
            entry.Entity.CreatedDate = DateTime.UtcNow;
            entry.Entity.UpdatedDate = DateTime.UtcNow;
        }
        else if (entry.State == EntityState.Modified)
        {
            entry.Entity.UpdatedDate = DateTime.UtcNow;
        }
    }
    
    return base.SaveChangesAsync(cancellationToken);
}
}
