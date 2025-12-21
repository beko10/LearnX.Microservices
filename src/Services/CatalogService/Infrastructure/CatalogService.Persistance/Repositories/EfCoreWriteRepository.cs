using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities.Common;
using CatalogService.Persistance.Context;

namespace CatalogService.Persistance.Repositories;

public class EfCoreWriteRepository<TEntity> :
    EfCoreRepository<TEntity>, 
    IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    public EfCoreWriteRepository(AppDbContext context) : base(context)
    {
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default
     )
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default
     )
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default
    )
    {
        await Task.Run(() => { _dbSet.Remove(entity); }, cancellationToken);
    }

    public async Task<bool> RemoveIdAsync(string id, CancellationToken cancellationToken = default
    )
    {
        return await Task.Run(() =>
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                return false;
            _dbSet.Remove(entity);
            return true;
        }, cancellationToken);
    }

    public async Task RemoveRangeAsync(
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default
    )
    {
        await Task.Run(() => { _dbSet.RemoveRange(entities); }, cancellationToken);
    }

    public async Task UpdateAsync(
        TEntity entity, 
        CancellationToken cancellationToken = default
    )
    {
        await Task.Run(() => { _dbSet.Update(entity); }, cancellationToken);
    }

    public async Task UpdateRangeAsync(
        IEnumerable<TEntity> entities, 
        CancellationToken cancellationToken = default
    )
    {
        await Task.Run(() => { _dbSet.UpdateRange(entities); }, cancellationToken);
    }
}
