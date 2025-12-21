using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Domain.Entities;
using CatalogService.Persistance.Context;

namespace CatalogService.Persistance.Repositories.CategoryRepository;

public class EfCoreCategoryReadRepository :
    EfCoreReadRepository<Category>, 
    IReadCategoryRepository
{
    public EfCoreCategoryReadRepository(AppDbContext context) : base(context)
    {
    }
}
