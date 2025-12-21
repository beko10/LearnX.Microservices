using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Domain.Entities;
using CatalogService.Persistance.Context;

namespace CatalogService.Persistance.Repositories.CategoryRepository;

public class EfCoreCategoryWriteRepository : EfCoreWriteRepository<Category>,
    IWriteCategoryRepository
{
    public EfCoreCategoryWriteRepository(AppDbContext context) : base(context)
    {
    }
}
