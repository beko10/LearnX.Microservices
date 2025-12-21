namespace CatalogService.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
