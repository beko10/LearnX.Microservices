using BuildingBlocks.Web.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Web;

public static class ServiceRegistration
{
    public static IServiceCollection AddBuildingBlocksWeb(this IServiceCollection services)
    {
        services.AddGlobalExceptionHandler();
        services.AddBuildingBlocksApiVersioning();
        return services;
    }
}
