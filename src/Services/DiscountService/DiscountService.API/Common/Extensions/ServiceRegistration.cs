using BuildingBlocks.Core.Extensions;
using DiscountService.API.Common.MongoDB;
using DiscountService.API.Common.Rules;
using FluentValidation;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

namespace DiscountService.API.Common.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddDiscountServices(this IServiceCollection services)
    {
        // MongoDB Options
        services.AddOptions<MongoOptions>()
            .BindConfiguration(MongoOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // MongoDB Client
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
        });

        // DiscountDbContext
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var mongoOptions = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            var database = mongoClient.GetDatabase(mongoOptions.DatabaseName);
            return DiscountDbContext.Create(database);
        });

        // Business Rules
        services.AddScoped<IDiscountBusinessRules, DiscountBusinessRules>();

        // MediatR
        services.AddBuildingBlocksMediatR(Assembly.GetExecutingAssembly());

        // FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}

