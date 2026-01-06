using BuildingBlocks.Core.Extensions;
using CatalogService.Application.Features.CategoryFeature.Rules;
using CatalogService.Application.Features.CourseFeature.Rules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogService.Application.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddBuildingBlocksMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<ICategoryBusinessRules, CategoryBusinessRules>();
        services.AddScoped<ICourseBusinessRules, CourseBusinessRules>();
        return services;

    }
}
