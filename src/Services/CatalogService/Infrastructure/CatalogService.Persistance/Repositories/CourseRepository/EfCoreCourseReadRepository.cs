using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Domain.Entities;
using CatalogService.Persistance.Context;

namespace CatalogService.Persistance.Repositories.CourseRepository;

public class EfCoreCourseReadRepository : EfCoreReadRepository<Course>,
    IReadCourseRepository
{
    public EfCoreCourseReadRepository(AppDbContext context) : base(context)
    {
    }
}
