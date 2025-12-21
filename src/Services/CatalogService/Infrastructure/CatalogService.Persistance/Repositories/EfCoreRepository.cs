using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities.Common;
using CatalogService.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Persistance.Repositories;

public class EfCoreRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _context;
    protected DbSet<TEntity> _dbSet => _context.Set<TEntity>();

    public EfCoreRepository(AppDbContext context)
    {
        _context = context;
    }
}
