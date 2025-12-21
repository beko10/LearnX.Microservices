using CatalogService.Domain.Entities.Common;
using System.Linq.Expressions;

namespace CatalogService.Application.Interfaces.Repositories;


public interface IReadRepository<TEntity>:IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default
       );
    Task<IEnumerable<TEntity>> GetWhere(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default
       );

    Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default
       );
    Task<TEntity> GetByIdAsync(string id,
        CancellationToken cancellationToken = default
       );

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );
    Task<int> CountAsync(
        CancellationToken cancellationToken = default
       );
    Task<int> CountWhereAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
       );

    // === SAYFALAMA İŞLEMLERİ ===
    //Task<PagedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    //Task<PagedResult<TEntity>> GetWherePagedAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
}
