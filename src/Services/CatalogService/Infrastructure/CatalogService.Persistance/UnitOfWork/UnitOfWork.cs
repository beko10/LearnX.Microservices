using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Persistance.Context;

namespace CatalogService.Persistance.UnitOfWork;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }


    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}
