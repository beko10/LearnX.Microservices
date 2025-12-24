using CatalogService.Application.Extensions;
using CatalogService.Persistance.Extensions;
using CategoryCatalog.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Infrastructure Layer - Persistence Services
builder.Services.AddPersistenceServices();

// Application Layer - Application Services
builder.Services.AddApplicationServices();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

app.MapGroup(prefix: "/api")
    .RegisterCatalogServiceAllEndpoints();

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
