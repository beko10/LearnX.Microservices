using DiscountService.API.Common.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DiscountService.API.Common.MongoDB;

public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; }

    public static DiscountDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DiscountDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName);

        return new DiscountDbContext(optionsBuilder.Options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DiscountDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<Coupon>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                if (string.IsNullOrEmpty(entry.Entity.Id))
                {
                    entry.Entity.Id = ObjectId.GenerateNewId().ToString();
                }
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
