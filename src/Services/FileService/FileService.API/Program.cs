using BuildingBlocks.Web;
using BuildingBlocks.Web.Extensions;
using FileService.API.Common.Extensions;
using FileService.API.Features.DeleteFile;
using FileService.API.Features.UploadFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddBuildingBlocksSwagger(
    title: "File Service API",
    version: "v1",
    description: "File Service için API dokümantasyonu - Vertical Slice Architecture"
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

// File Services
builder.Services.AddFileServices();

var app = builder.Build();

// Global Exception Handler
app.UseGlobalExceptionHandler();

// CORS
app.UseCors("AllowAll");

// Static Files (wwwroot/files)
app.UseStaticFiles();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "File Service API v1");
        c.RoutePrefix = string.Empty;
    });
}

// Register all endpoints - Vertical Slice pattern
app.MapUploadFileEndpoint();
app.MapDeleteFileEndpoint();

// Run
var port = Environment.GetEnvironmentVariable("PORT") ?? "8083";
var host = app.Environment.IsDevelopment() ? "localhost" : "0.0.0.0";
app.Run($"http://{host}:{port}");
