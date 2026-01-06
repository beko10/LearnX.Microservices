using System.Reflection;
using BuildingBlocks.Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Core.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddBuildingBlocksMediatR(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies);

        return services;
    }
}