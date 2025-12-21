using CatalogService.Persistance.Context;
using CatalogService.Persistance.Options; // Namespace'leri kontrol edin
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogService.Persistance.Extensions;

public static class ServiceRegistration
{
    // Extension method "this" ile başlamalı ve class içinde doğrudan tanımlanmalı
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        // 1. Options Pattern Ayarları
        services.AddOptions<MongoOption>()
            .BindConfiguration(nameof(MongoOption))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // 2. MongoOption'ı Singleton olarak DI'ya ekleyelim (Kullanımı kolaylaştırmak için)
        services.AddSingleton(sp =>
            sp.GetRequiredService<IOptions<MongoOption>>().Value);

        // 3. MongoClient Kaydı
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<MongoOption>();
            return new MongoClient(options.ConnectionString);
        });

        
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();

            var mongoOption = sp.GetRequiredService<MongoOption>();

            var database = mongoClient.GetDatabase(mongoOption.DatabaseName);

  
            return AppDbContext.Create(database);
        });

        return services;
    }
}