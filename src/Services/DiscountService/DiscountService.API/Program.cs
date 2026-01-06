using BuildingBlocks.Web;
using BuildingBlocks.Web.Extensions;
using DiscountService.API.Common.Extensions;
using DiscountService.API.Features.CreateCoupon;
using DiscountService.API.Features.DeleteCoupon;
using DiscountService.API.Features.GetCoupon;
using DiscountService.API.Features.GetCouponByCode;
using DiscountService.API.Features.UpdateCoupon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddBuildingBlocksSwagger(
    title: "Discount Service API",
    version: "v1",
    description: "Discount Service için API dokümantasyonu - Vertical Slice Architecture + MongoDB"
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

// Discount Services (MongoDB, MediatR, FluentValidation, BusinessRules)
builder.Services.AddDiscountServices();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Register all endpoints - Vertical Slice pattern
app.MapCreateCouponEndpoint();
app.MapGetCouponEndpoint();
app.MapGetCouponByCodeEndpoint();
app.MapUpdateCouponEndpoint();
app.MapDeleteCouponEndpoint();

// Run
var port = Environment.GetEnvironmentVariable("PORT") ?? "8082";
var host = app.Environment.IsDevelopment() ? "localhost" : "0.0.0.0";
app.Run($"http://{host}:{port}");
