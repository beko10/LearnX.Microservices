using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities.Common;
using CatalogService.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CatalogService.Persistance.Repositories;

public class EfCoreWriteRepository<TEntity> :
    EfCoreRepository<TEntity>,
    IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    public EfCoreWriteRepository(AppDbContext context) : base(context)
    {

    }

    public async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        // AddAsync'ten ÖNCE Id set et
        if (string.IsNullOrEmpty(entity.Id))
        {
            entity.Id = ObjectId.GenerateNewId().ToString();
        }

        entity.CreatedDate = DateTime.UtcNow;
        entity.UpdatedDate = DateTime.UtcNow;

        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            entity.CreatedDate = DateTime.UtcNow;
            entity.UpdatedDate = DateTime.UtcNow;
        }

        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public Task RemoveAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> RemoveIdAsync(
        string id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        return true;
    }

    public Task RemoveRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task UpdateRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedDate = DateTime.UtcNow;
        }
        _dbSet.UpdateRange(entities);
        return Task.CompletedTask;
    }
}