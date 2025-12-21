using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Domain.Entities;
using CatalogService.Persistance.Context;

namespace CatalogService.Persistance.Repositories.CourseRepository;

public class EfCoreCourseWriteRepository : EfCoreWriteRepository<Course>,
    IWriteCourseRepository
{
    public EfCoreCourseWriteRepository(AppDbContext context) : base(context)
    {
    }
}
