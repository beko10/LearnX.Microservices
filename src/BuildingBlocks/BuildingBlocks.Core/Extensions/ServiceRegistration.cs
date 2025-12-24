using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Core.Extensions;

public static class ServiceRegistration
{
    /// <summary>
    /// BuildingBlocks.Core içindeki tüm servisleri DI container'a kayıt eder.
    /// Bu method, GlobalExceptionHandler ve diğer core servisleri kaydeder.
    /// </summary>
    /// <param name="services">IServiceCollection instance</param>
    /// <returns>IServiceCollection instance (fluent API için)</returns>
    public static IServiceCollection AddBuildingBlocksCore(this IServiceCollection services)
    {
        // Global Exception Handler kaydı
        services.AddGlobalExceptionHandler();

        return services;
    }
}

