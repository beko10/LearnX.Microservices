using CatalogService.Application.Features.CategoryFeature.Commands.CreateCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.Commands.UpdateCategoryCommand;
using CatalogService.Application.Features.CategoryFeature.DTOs;
using CatalogService.Application.Features.CategoryFeature.Queries.GetAllCategoryQuery;
using CatalogService.Application.Features.CategoryFeature.Queries.GetByIdCategoryQuery;
using MediatR;

namespace CategoryCatalog.API.Endpoints;

public static class CaregoryEndpoints
{
    public static IEndpointRouteBuilder RegisterCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        // Category Endpoints
        var group = app.MapGroup("/api/categories")
            .WithTags("Categories");


        // Get All Categories Endpoint
        group.MapGet("/", async (IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var queryResponse = await mediator.Send(new GetAllCategoryQueryRequest(),
              cancellationToken  
            );

            return queryResponse.Result.IsSuccess
                ? Results.Ok(queryResponse.Result.Data)
                : Results.BadRequest(queryResponse.Result);
        })
        .WithName("GetAllCategories")
        .WithDescription("Retrieves all categories.");


        // Get Category By Id Endpoint
        app.MapGet("/{id}", async (
            string id,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
           var queryResponse = await mediator.Send(new GetByIdCategoryQueryRequest
                {
                    Id = id
                },
                cancellationToken
            );

            return queryResponse.Result.IsSuccess
                ? Results.Ok(queryResponse.Result.Data)
                : Results.BadRequest(queryResponse.Result);
        })
            .WithName("GetByIdCategory")
            .WithDescription("Get By Id Category")
            .Produces<GetByIdCategoryQueryResponse>(200)
            .Produces(404);


        app.MapPost("/", async (
            CreateCategoryCommandDto createCategoryCommandDto,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new CreateCategoryCommandRequest
            {
                CreateCategoryCommandRequestDto = createCategoryCommandDto
            },
                cancellationToken
            );
            return commandResponse.Result.IsSuccess
                ? Results.Ok(commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        })
            .WithName("CreateCategory")
            .WithDescription("Create Category")
            .Produces<CreateCategoryCommandResponse>(201)
            .Produces(400);


        app.MapPut("/", async (
            UpdateCategoryCommandDto updateCategoryCommandDto,
            IMediator mediator,
            CancellationToken cancellationToken
            ) =>
        {
            var commandResponse = await mediator.Send(new UpdateCategoryCommandRequest
            {
                UpdateCategoryCommandRequestDto = updateCategoryCommandDto
            },
            cancellationToken
            );

            return commandResponse.Result.IsSuccess
                ? Results.Ok(commandResponse.Result.SuccessMessage)
                : Results.BadRequest(commandResponse.Result);
        });




        return app;
    }
}
