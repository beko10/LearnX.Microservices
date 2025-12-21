using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities.Common;
using CatalogService.Persistance.Context;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace CatalogService.Persistance.Repositories;

public class EfCoreReadRepository<TEntity> :    
    EfCoreRepository<TEntity>, IReadRepository<TEntity> 
    where TEntity : BaseEntity
{
    public EfCoreReadRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.CountAsync(cancellationToken);
    }


    public async Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate, 
    CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }


    public async Task<TEntity> GetByIdAsync(
        string id, 
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }


    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression,  CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetWhere(
        Expression<Func<TEntity, bool>> expression, 
        CancellationToken cancellationToken = default
    )
    {
        return await _dbSet.Where(expression).ToListAsync(cancellationToken);
    }
}
