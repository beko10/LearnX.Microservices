using CatalogService.Domain.Entities.Common;

namespace CatalogService.Application.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
}
