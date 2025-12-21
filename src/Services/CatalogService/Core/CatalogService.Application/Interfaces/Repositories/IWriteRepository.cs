using CatalogService.Domain.Entities.Common;

namespace CatalogService.Application.Interfaces.Repositories;

public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
        );
    Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default
        );

    Task RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
       );
    Task<bool> RemoveIdAsync(
        string id,
        CancellationToken cancellationToken = default
        );
    Task RemoveRangeAsync(
        IEnumerable<TEntity> entities, CancellationToken cancellationToken = default
        );

    //Task<bool> SoftRemoveIdAsync(string id, CancellationToken cancellationToken = default);
    //Task SoftRemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
        );

    Task UpdateRangeAsync(
        IEnumerable<TEntity> entities, CancellationToken cancellationToken = default
    );
}
