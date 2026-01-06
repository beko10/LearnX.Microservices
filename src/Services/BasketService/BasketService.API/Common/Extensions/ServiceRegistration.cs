using BasketService.API.Common.Redis;
using BasketService.API.Common.Rules;
using BuildingBlocks.Core.Extensions;
using FluentValidation;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Reflection;

namespace BasketService.API.Common.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddBasketServices(this IServiceCollection services)
    {
        // Redis Options
        services.AddOptions<RedisOptions>()
            .BindConfiguration(RedisOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Redis Connection
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
            return ConnectionMultiplexer.Connect(options.ConnectionString);
        });

        // Redis Service
        services.AddScoped<IRedisService, RedisService>();

        // Business Rules
        services.AddScoped<IBasketBusinessRules, BasketBusinessRules>();

        // MediatR
        services.AddBuildingBlocksMediatR(Assembly.GetExecutingAssembly());

        // FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // AutoMapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}


