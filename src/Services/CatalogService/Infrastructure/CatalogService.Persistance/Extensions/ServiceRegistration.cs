using CatalogService.Application.Interfaces.Repositories.CategoryRepository;
using CatalogService.Application.Interfaces.Repositories.CourseRepository;
using CatalogService.Application.Interfaces.UnitOfWork;
using CatalogService.Persistance.Context;
using CatalogService.Persistance.Options;
using CatalogService.Persistance.Repositories.CategoryRepository;
using CatalogService.Persistance.Repositories.CourseRepository;
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

        // 4. AppDbContext Kaydı
        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var mongoOption = sp.GetRequiredService<MongoOption>();
            var database = mongoClient.GetDatabase(mongoOption.DatabaseName);
            return AppDbContext.Create(database);
        });

        // 5. UnitOfWork Kaydı
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        // 6. Category Repository Kayıtları
        services.AddScoped<IReadCategoryRepository, EfCoreCategoryReadRepository>();
        services.AddScoped<IWriteCategoryRepository, EfCoreCategoryWriteRepository>();


        // 7. Course Repository Kayıtları
        services.AddScoped<IReadCourseRepository, EfCoreCourseReadRepository>();
        services.AddScoped<IWriteCourseRepository, EfCoreCourseWriteRepository>();

        return services;
    }
}