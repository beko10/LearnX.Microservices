using BuildingBlocks.Core.Extensions;
using FileService.API.Common.Options;
using FileService.API.Common.Rules;
using FileService.API.Common.Services;
using FluentValidation;
using System.Reflection;

namespace FileService.API.Common.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddFileServices(this IServiceCollection services)
    {
        // File Options
        services.AddOptions<FileUploadOptions>()
            .BindConfiguration(FileUploadOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // File Storage Service
        services.AddScoped<IFileStorageService, FileStorageService>();

        // Business Rules
        services.AddScoped<IFileBusinessRules, FileBusinessRules>();

        // MediatR
        services.AddBuildingBlocksMediatR(Assembly.GetExecutingAssembly());

        // FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
