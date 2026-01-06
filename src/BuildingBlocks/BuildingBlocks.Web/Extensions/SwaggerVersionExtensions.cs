using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlocks.Web.Extensions;

public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = new OpenApiPaths();
        var version = swaggerDoc.Info.Version; // "v1"
        var versionNumber = version.StartsWith("v") ? version[1..] : version; // "1"
        
        foreach (var path in swaggerDoc.Paths)
        {
            // v{version:apiVersion} -> v1 (v + 1)
            // {version:apiVersion} -> v1
            var newPath = path.Key
                .Replace("v{version:apiVersion}", version)
                .Replace("{version:apiVersion}", version)
                .Replace("v{version}", version)
                .Replace("{version}", version);
            
            paths.Add(newPath, path.Value);
        }
        
        swaggerDoc.Paths = paths;
    }
}

public static class SwaggerVersionExtensions
{
    public static void AddBuildingBlocksSwagger(this IServiceCollection services, string title, string version = "v1", string? description = null)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(version, new OpenApiInfo
            {
                Title = title,
                Version = version,
                Description = description
            });
            
            // Version placeholder'ı otomatik değiştir
            c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
        });
    }
}
