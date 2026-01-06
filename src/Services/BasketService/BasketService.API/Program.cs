using BasketService.API.Common.Extensions;
using BasketService.API.Features.AddItemToBasket;
using BasketService.API.Features.ClearBasket;
using BasketService.API.Features.GetBasket;
using BasketService.API.Features.RemoveItemFromBasket;
using BasketService.API.Features.UpdateQuantity;
using BuildingBlocks.Web;
using BuildingBlocks.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddBuildingBlocksSwagger(
    title: "Basket Service API",
    version: "v1",
    description: "Basket Service için API dokümantasyonu - Vertical Slice Architecture"
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

// BuildingBlocks.Web
builder.Services.AddBuildingBlocksWeb();

// Basket Services (Redis, MediatR, FluentValidation)
builder.Services.AddBasketServices();

var app = builder.Build();

// Global Exception Handler
app.UseGlobalExceptionHandler();

// CORS
app.UseCors("AllowAll");

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Register all endpoints - Vertical Slice pattern
app.MapAddItemToBasketEndpoint();
app.MapGetBasketEndpoint();
app.MapRemoveItemFromBasketEndpoint();
app.MapClearBasketEndpoint();
app.MapUpdateQuantityEndpoint();

// Run
var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
var host = app.Environment.IsDevelopment() ? "localhost" : "0.0.0.0";
app.Run($"http://{host}:{port}");
