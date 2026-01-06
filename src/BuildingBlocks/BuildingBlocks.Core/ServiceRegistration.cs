using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Core;

public static class ServiceRegistration
{
    /// <summary>
    /// BuildingBlocks.Core içindeki tüm servisleri DI container'a kayıt eder.
    /// MediatR behaviors, FluentValidation ve diğer core servisleri kaydeder.
    /// </summary>
    /// <param name="services">IServiceCollection instance</param>
    /// <returns>IServiceCollection instance (fluent API için)</returns>
    public static IServiceCollection AddBuildingBlocksCore(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddBuildingBlocksCore(assemblies);
        return services;
    }
}

