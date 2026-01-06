using BuildingBlocks.Web;
using BuildingBlocks.Web.Extensions;
using CatalogService.Application.Extensions;
using CatalogService.Persistance.Extensions;
using CategoryCatalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddBuildingBlocksSwagger(
    title: "Catalog Service API",
    version: "v1",
    description: "Catalog Service için API dokümantasyonu"
);

// CORS yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddBuildingBlocksWeb();

// Infrastructure Layer - Persistence Services
builder.Services.AddPersistenceServices();

// Application Layer - Application Services
builder.Services.AddApplicationServices();

var app = builder.Build();

// Global Exception Handler - En başa eklenmeli (tüm hataları yakalamak için)
app.UseGlobalExceptionHandler();

// CORS middleware'ini en başa ekleyin (routing'den önce)
app.UseCors("AllowAll");

// Swagger middleware'ini ekleyin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.RegisterCatalogServiceAllEndpoints();

// Development ortamında localhost, Production/Docker'da 0.0.0.0 kullan
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var host = app.Environment.IsDevelopment() ? "localhost" : "0.0.0.0";
app.Run($"http://{host}:{port}");